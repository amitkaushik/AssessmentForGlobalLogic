using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowService_POC.Model
{
    public class Projection
    {
        [Key]
        public int PId { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public int minutes { get; set; }
        public int SId { get; set; }
        public Schedule Schedule { get; set; }

    }
    public class Schedule
    {
        [Key]
        public int SId { get; set; }
        public int ContractTimeMinutes { get; set; }
        public DateTime Date { get; set; }
        public bool IsFullDayAbsence { get; set; }
        public string Name { get; set; }        
        public string PersonId { get; set; }
        public IList<Projection> Projection { get; set; }

    }
    public class ScheduleResult
    {
        public IList<Schedule> Schedules { get; set; }

    }
    public class Application
    {
        public ScheduleResult ScheduleResult { get; set; }

    }
}
