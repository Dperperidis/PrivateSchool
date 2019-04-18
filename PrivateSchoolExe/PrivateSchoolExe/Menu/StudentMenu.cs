using PrivateSchoolExe.Data;
using PrivateSchoolExe.Helpers;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Menu
{
    public class StudentMenu
    {
        enum Menu
        {
            EnrollToCourse = 1,
            DailySchedule,
            AssigmentsSub,
            SubmitAssigments
        }     
        DataLists data = new DataLists();
        StudentManager manag = new StudentManager();
        LogNav log = new LogNav();
        bool done = true;
        public bool studMenu(User user)
        {       
            while (done)
            {              
                int input = manag.GetMenu(user);
                Console.Clear();       
                Menu menu = (Menu)input;  
                
                switch (menu)
                {
                    case Menu.EnrollToCourse:
                        List<Course> tempCourses = data.GetCourses();
                        manag.setCourse(tempCourses, user);
                        done = log.Navigate();
                        break;
                    case Menu.DailySchedule:
                        if ((DateTime.Now.DayOfWeek == DayOfWeek.Sunday) || (DateTime.Now.DayOfWeek == DayOfWeek.Saturday))
                            Console.WriteLine("It's weekend you don't have any course today!");                     
                        break;
                    case Menu.AssigmentsSub:
                        manag.GetAssignmentsPerCourse(user);
                        done = log.Navigate();
                        break;
                    case Menu.SubmitAssigments:
                        manag.SubmitAssignment(user);
                        done = log.Navigate();
                        break;
                }
            }
            return true;
        }
    }
}