using PrivateSchoolExe.Helpers;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Menu
{
    public class HeadMasterMenu
    {
        enum Menu
        {
            manageCourse = 1,
            manageStudent,
            manageAssignment,
            manageTrainer,
            manageStudentPerCourse,
            manageTrainerPerCourse,
            manageAssignmentsPerCourse,
            manageSchedulePerCourse
        }
        HeadMasterManager manag = new HeadMasterManager();
        LogNav log = new LogNav();
        bool done = true;

        public void HdMenu(User user)
        {
            while (done)
            {
                int input = manag.GetMenu(user);
                Console.Clear();
                Menu menu = (Menu)input;
                switch (menu)
                {
                    case Menu.manageCourse:
                        manag.ManageCourses(user);
                        done = log.Navigate();
                        break;
                    case Menu.manageStudent:
                        manag.ManageStudents(user);
                        done = log.Navigate();
                        break;
                    case Menu.manageAssignment:
                        manag.ManageAssignments(user);
                        done = log.Navigate();
                        break;
                    case Menu.manageTrainer:
                        manag.ManageTrainers(user);
                        done = log.Navigate();
                        break;
                    case Menu.manageStudentPerCourse:
                        manag.ManageStudentPerCourse();
                        done = log.Navigate();
                        break;
                    case Menu.manageTrainerPerCourse:
                        manag.ManageTrainerPerCourse();
                        done = log.Navigate();
                        break;
                    case Menu.manageAssignmentsPerCourse:
                        break;
                    case Menu.manageSchedulePerCourse:
                        break;
                }

            }
        }
    }
}
