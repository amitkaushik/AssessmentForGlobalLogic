using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using WindowService_POC.Model;
using Quartz;
using WindowService_POC.DataContext;

namespace WindowService_POC
{
    public class DataService
    {
        private SDBContext _context;
        public DataService()
        {
            _context = new SDBContext();
        }
        public async Task<Application> GetData()
        {
            string baseURL = $"https://rndfiles.blob.core.windows.net/pizzacabininc/2015-12-14.json";
            Application application = new Application();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                application = dataObj.ToObject<Application>();
                            }
                            else
                            {
                                Console.WriteLine("Data is empty!");
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return application;

        }


        public void InsertData(Application app)
        {
            IList<string> alreadyExist = new List<string>();
            if (app.ScheduleResult != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in app.ScheduleResult.Schedules)
                        {
                            var dt = _context.Schedules.FirstOrDefault(a => a.PersonId == item.PersonId);
                            if (dt == null)
                            {
                                _context.Schedules.Add(item);
                            }                           
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }            
        }
    }
}
