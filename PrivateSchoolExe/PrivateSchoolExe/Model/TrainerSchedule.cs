using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Model
{
    public class TrainerSchedule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TrainerId { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public bool IsSubmitted { get; set; }
        public decimal OralMark { get; set; }
        public decimal TotalMark { get; set; }
    }
}
