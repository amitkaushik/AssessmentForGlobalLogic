using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowService_POC.Contract;
using System.Linq;

namespace WindowService_POC.Implementation
{
    public class TaskLogTime : ITaskLogTime
    {
        public async Task DoWork(CancellationToken cancellationToken)
        {
            await Execute();
        }

        public async Task Execute()
        {
            try
            {
                string path = $"Logs\\log{DateTime.Now.ToString("d/M/yy")}.txt";
                await using (StreamWriter writer = new StreamWriter(path, true))
                {
                    Console.WriteLine("Log Time: " + DateTime.Now);
                    writer.WriteLine("Log Time: " + DateTime.Now);

                    DataService ds = new DataService();

                    WindowService_POC.Model.Application app = await ds.GetData();

                    if(app.ScheduleResult != null)
                    {
                       ds.InsertData(app);                                               
                    }
                    writer.WriteLine("Log Time End: " + DateTime.Now);
                    Console.WriteLine("Log Time End: " + DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
