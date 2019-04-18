using PrivateSchoolExe.Data;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    
    public class TrainerManager
    {
        List<TrainerSchedule> schedules = new List<TrainerSchedule>();
        List<TrainerSchedule> temp = new List<TrainerSchedule>();
        List<User> users = new List<User>();
        List<User> usersTemp = new List<User>();
        List<User> trainerStudents = new List<User>();
        List<Course> courses = new List<Course>();
        List<Course> userCourses = new List<Course>();


        DataLists data = new DataLists();

        public string[] trainerMenu = new string[] {"Enrolled Courses",
            "Students per course",
            "Assignments per student per course",
            "Mark Assigments per student per course" };


        public int GetMenu(User user)
        {
            Console.Clear();
            Console.WriteLine($"\nWelcome {user.FirstName} {user.LastName}!\n");
            Console.WriteLine($"Your Role is Trainer\n");
            for (int i = 0; i < trainerMenu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {trainerMenu[i]}");
            }
            Console.Write("\nChoose your option: ");
            string Choise = Console.ReadLine();
            int input;
            while (!int.TryParse(Choise, out input) || input > trainerMenu.Length)
            {
                Console.Write($"You can only enter Numbers (Choose between 1-{trainerMenu.Length}): ");
                Choise = Console.ReadLine();
            }
            return input;
        }

        public void ViewCourses(User user)
        {
            using(DataContext db = new DataContext())
            {
                schedules = db.TrainerSchedules.Include("Course").Include("User").Where(x => x.TrainerId == user.Id).ToList();
                var result = schedules.GroupBy(x => x.Course.Title, (key) => new { Course = key });
                foreach (var item in result)
                {
                    Console.WriteLine($"You have been enroll in {item.Key}");
                }
            }
        }

        public void StudentsPerCourse(User user)
        {
            using (DataContext db = new DataContext())
            {
                schedules = db.TrainerSchedules.Include("Course").Include("User").Where(x => x.TrainerId == user.Id).ToList();
                foreach (var item in schedules)
                {
                    users.Add(db.Users.Include("TrainerSchedules").FirstOrDefault(x=>x.Id == item.UserId));
                }       
            }
            var result = schedules.GroupBy(x => x.Course.Title, (key) => new { Course = key });
            foreach (var item in result)
            {
                Console.WriteLine($"\nIn course '{item.Key}' you have the below students:\n");
                foreach (var sec in item)
                {
                  Console.WriteLine($"Student Code: {sec.Course.User.Id} -{sec.Course.User.FirstName} {sec.Course.User.LastName}");
                }
            }          
        }

        public void ViewAssignmentsPerStudentPerCourse(User user)
        {
            using (DataContext db = new DataContext())
            {
                schedules = db.TrainerSchedules.Include("Course").Include("User").Include("Assignment").Where(x => x.TrainerId == user.Id).ToList();  
            }
            var result = schedules.GroupBy(x => x.Course, (key) => new { Course = key });
            foreach (var item in result)
            {
                Console.WriteLine($"\nIn course: {item.Key}' you have the below students:\n");
                foreach (var res in item)
                {                   
                    Console.WriteLine($"Student Code: {res.Course.User.Id}" +
                        $" -{res.Course.User.FirstName} {res.Course.User.LastName}" +
                        $"with assignments to deliver {res.Course.Assignment}");
                }
            }
        }

        public void MarkAllAssignments(User user)
        {
            using (DataContext db = new DataContext())
            {
                schedules = db.TrainerSchedules.Include("Course").Include("User").Include("Assignment").Where(x => x.TrainerId == user.Id).ToList();
            }

            if (!schedules.Any(x=>x.IsSubmitted == true))
            {
                Console.WriteLine("There are no submitted assignments from your students yet!");
            }
            else
            {              
                int input = 0;
                int assChoise = 0;
                int oralMarkChoise = 0;
                int totalMarkChoise = 0;
                bool check = true;
                while (check)
                {
                    Console.WriteLine($"You have the below assigments to mark: ");
                    foreach (var item in schedules)
                    {
                        if (item.IsSubmitted == true && item.Assignment.TotalMark == 0)
                        {
                            temp.Add(item);
                            Console.WriteLine($"From student with Code: {item.User.Id} - {item.User.FirstName} {item.User.LastName} in course {item.Course.Title}" +
                                $" the assignment with Code: {item.Assignment.Id} - {item.Assignment.Title}");
                        }
                    }
                    Console.Write("\nWhich one you want to mark? (Type Student code or E for exit): ");
                    string Choise = Console.ReadLine().ToLower();
                    if (Choise == "e")
                    {
                        break;
                    }
                    while (!int.TryParse(Choise, out input))
                    {
                        Console.Write($"You can only enter Numbers: ");
                        Choise = Console.ReadLine();
                    }
                    Console.Write("\nType Assignment code or (E for exit): ");
                    string ass = Console.ReadLine().ToLower();
                    if (Choise == "e")
                    {
                        break;
                    }
                    while (!int.TryParse(ass, out assChoise))
                    {
                        Console.Write($"You can only enter Numbers: ");
                        Choise = Console.ReadLine();
                    }
                    foreach (var item in temp)
                    {
                        if (item.UserId == input && item.AssignmentId == assChoise)
                        {
                            Console.Write($"Set oral mark: ");
                            string oralMark = Console.ReadLine();
                            while (!int.TryParse(oralMark, out oralMarkChoise) ||(oralMarkChoise > 100 || oralMarkChoise < 0))
                            {
                                Console.Write($"You can only enter Numbers between 0-100: ");
                                oralMark = Console.ReadLine();
                            }

                            Console.Write($"Set Total mark: ");
                            string totalMark = Console.ReadLine();
                            while (!int.TryParse(totalMark, out totalMarkChoise) || (totalMarkChoise > 100 || totalMarkChoise < 0))
                            {
                                Console.Write($"You can only enter Numbers between 0-100: ");
                                totalMark = Console.ReadLine();
                            }
                            using (DataContext db = new DataContext())
                            {
                                var result = db.TrainerSchedules.Include("Assignment").FirstOrDefault(x=>x.Id == item.Id);
                                result.OralMark = oralMarkChoise;
                                result.TotalMark = totalMarkChoise;
                                db.SaveChanges();
                                Console.WriteLine("Mark was successful!");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("We didn't find any match data with your above selections.");
                            break;
                        }

                       
                    }

                   


                        Console.Write("\nDo you want to mark another Assignment? Y or N: ");
                    if (!(Console.ReadLine().ToLower() == "y"))
                    {
                        check = false;
                    }
                    Console.Clear();
                }
            }
 
        }
    }
}
