using PrivateSchoolExe.Data;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    public class DataLists
    {
        List<Course> courses = new List<Course>();
        List<User> users = new List<User>();
        List<Assignment> assignments = new List<Assignment>();
        List<TrainerSchedule> trainerSchedules = new List<TrainerSchedule>();
        List<User> trainers = new List<User>();
        Level level = new Level();

        public List<Course> GetCourses()
        {
            using (DataContext db = new DataContext())
            {
                courses = db.Courses.Include("Assignments").ToList();
            }
            return courses;
        }

        public List<User> GetUsers()
        {
            using (DataContext db = new DataContext())
            {
                users = db.Users.ToList();
            }
            return users;
        }

        public List<User> GetStudents()
        {
            using (DataContext db = new DataContext())
            {
                users = db.Users.Where(x=>x.LevelId == 1).ToList();
            }
            return users;

        }

        public List<User> GetTrainers()
        {
            using (DataContext db = new DataContext())
            {
                users = db.Users.Where(x => x.LevelId == 2).ToList();
            }
            return users;

        }

        public List<Assignment> GetAssignments()
        {
            using (DataContext db = new DataContext())
            {
                assignments = db.Assignments.ToList();
            }
            return assignments;
        }

        public Level GetLevel(int access)
        {
            using (DataContext db = new DataContext())
            {
                level = db.Levels.Find(access);
            }
            return level;
        }

        public List<TrainerSchedule> GetSchedule()
        {
            using (DataContext db = new DataContext())
            {
                trainerSchedules = db.TrainerSchedules.Include("Course").Include("Assignment").ToList();
            }
            return trainerSchedules;
        }


    }
}
