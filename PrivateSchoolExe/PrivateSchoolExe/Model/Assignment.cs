using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Model
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Submission { get; set; }
        public decimal OralMark { get; set; }
        public decimal TotalMark { get; set; } 
        public ICollection<TrainerSchedule> TrainerSchedules { get; set; }
        public Assignment()
        {
            TrainerSchedules = new List<TrainerSchedule>();
        }

        public override string ToString()
        {
            return $"Assignment code: {Id}. {Title}-{Submission}";
        }
    }
}
