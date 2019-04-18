using PrivateSchoolExe.Helpers;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Menu
{
    public class TrainerMenu
    {
        enum Menu
        {
            ViewCourses =1,
            ViewAllStudentsPerCourse,
            ViewAllAssignPerStudPerCourse,
            MarkAllTheAssignPerStudPerCourse
        }
        TrainerManager manag = new TrainerManager();
        LogNav log = new LogNav();
        bool done = true;
        public void trainerMenu(User user)
        {
            while (done)
            {
                int input = manag.GetMenu(user);
                Console.Clear();
                Menu menu = (Menu)input;
                switch (menu)
                {
                    case Menu.ViewCourses:
                        manag.ViewCourses(user);
                        done = log.Navigate();
                        break;
                    case Menu.ViewAllStudentsPerCourse:
                        manag.StudentsPerCourse(user);
                        done = log.Navigate();
                        break;
                    case Menu.ViewAllAssignPerStudPerCourse:
                        manag.ViewAssignmentsPerStudentPerCourse(user);
                        done = log.Navigate();
                        break;
                    case Menu.MarkAllTheAssignPerStudPerCourse:
                        manag.MarkAllAssignments(user);
                        done = log.Navigate();
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
