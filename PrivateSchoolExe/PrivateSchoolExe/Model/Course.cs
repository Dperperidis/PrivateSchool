using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<TrainerSchedule> TrainerSchedules { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        public Course()
        {
            Assignments = new List<Assignment>();
            TrainerSchedules = new List<TrainerSchedule>();
        }

        public override string ToString()
        {
            return $"\nId: {Id}. {Title} - '{Stream}/{Type}' Starts: {StartDate?.ToShortDateString()}," +
                $" Ends: {EndDate?.ToShortDateString()}";
        }
    }
}
