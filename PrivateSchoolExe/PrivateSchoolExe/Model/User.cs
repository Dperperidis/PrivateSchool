using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public string Subject { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal TuitionsFees { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<TrainerSchedule> TrainerSchedules { get; set; }
        public User()
        {
            TrainerSchedules = new List<TrainerSchedule>();
        }
    }
}
