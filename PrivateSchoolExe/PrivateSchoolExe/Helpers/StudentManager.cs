using PrivateSchoolExe.Data;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    public class StudentManager
    {
        DataLists data = new DataLists();
        List<Assignment> assignments;
        User chosenTrainer;
        User Student;
        List<TrainerSchedule> Schedule = new List<TrainerSchedule>();
        public string[] studentMenu = new string[] {"Choose a course",
            "Daily Schedule per Course",
            "Sub Dates of Assigmnets Per Course",
            "Submit Assigments" };

        public void setCourse(List<Course> tempCourses, User user)
        {
            using (DataContext db = new DataContext())
            {
                Student = db.Users.Include("TrainerSchedules").FirstOrDefault(x=>x.Id == user.Id);
            }
            
            int input = 0;
            bool check = true;
            while (check)
            {
                Console.WriteLine("Available Courses are: ");              
                foreach (var item in tempCourses)
                {
                    Console.WriteLine(item);
                }
                Console.Write("\nWhich one you want to Enroll?(Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }

                while (!int.TryParse(Choise, out input) || input > tempCourses.Count)
                {
                    Console.Write($"You can only enter Numbers (Choose between 1-{tempCourses.Count}): ");
                    Choise = Console.ReadLine();
                }
                using(DataContext db = new DataContext())
                {
                    Student = db.Users.Find(user.Id);
                    Course courseTemp = db.Courses.Include("Assignments").FirstOrDefault(x => x.Id == input);
                    Schedule = Student.TrainerSchedules.Where(x => x.UserId == Student.Id).ToList();
                    bool setup = true;
                    if (Schedule.Count == 0)
                    {
                        chosenTrainer = SetTrainer();
                        foreach (var item in courseTemp.Assignments)
                        {
                            db.TrainerSchedules.Add(new TrainerSchedule()
                            {
                                CourseId = courseTemp.Id,
                                UserId = Student.Id,
                                TrainerId = chosenTrainer.Id,
                                AssignmentId = item.Id
                            });
                        }
                    }
                    else
                    {
                        foreach (var item in Schedule)
                        {
                            if (item.CourseId == input)
                            {
                                Console.WriteLine("\nYou have already chosen this course!");
                                check = true;
                                setup = false;
                                break;
                            }
                        }
                        if (setup)
                        {               
                            chosenTrainer = SetTrainer();
                            foreach (var item in courseTemp.Assignments)
                            {
                                
                                db.TrainerSchedules.Add(new TrainerSchedule()
                                {
                                    CourseId = courseTemp.Id,
                                    UserId = Student.Id,
                                    TrainerId = chosenTrainer.Id,
                                    AssignmentId = item.Id
                                });
                            }
                            Console.WriteLine("\nSelection was successful!");
                        }
                    }
                    db.SaveChanges();
                }
                Console.Write("\nDo you want to enter another course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }

        public User SetTrainer()
        {
            List<User> trainers= data.GetTrainers();
            User trainer;

            Console.WriteLine("\nAvailable Trainers");
            foreach (User item in trainers)
            {
                Console.WriteLine($"Code: {item.Id} - {item.LastName} {item.FirstName} teaches '{item.Subject}'");
            }

            Console.Write("\nWhich trainer you want to Enroll? (Type his code): ");
            string Choise = Console.ReadLine();
            int input;
            while (!int.TryParse(Choise, out input))
            {
                Console.Write($"You can only enter Numbers or An available Code from above: ");
                Choise = Console.ReadLine();
            }

            using (DataContext db = new DataContext())
            {
                trainer = db.Users.FirstOrDefault(x => x.Id == input);
            }

            if (trainer == null)
            {
                Console.WriteLine("Trainer didn't found");
                SetTrainer();
            }
            return trainer;
        }

        public int GetMenu(User user)
        {
            Console.Clear();
            Console.WriteLine($"\nWelcome {user.FirstName} {user.LastName}!\n");
            Console.WriteLine($"Your Role is Student\n");
            for (int i = 0; i < studentMenu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {studentMenu[i]}");
            }
            Console.Write("\nChoose your option: ");
            string Choise = Console.ReadLine();
            int input;
            while (!int.TryParse(Choise, out input) || input > studentMenu.Length)
            {
                Console.Write($"You can only enter Numbers (Choose between 1-{studentMenu.Length}): ");
                Choise = Console.ReadLine();
            }
            return input;
        }

        public void GetAssignmentsPerCourse(User user)
        {
            using (DataContext db = new DataContext())
            {
                Schedule = db.TrainerSchedules.Include("User").Include("Assignment").Include("Course").Where(x=>x.UserId == user.Id).ToList();
                assignments = data.GetAssignments();             
            }
            if (Schedule.All(x => x.IsSubmitted == true))
            {
                Console.WriteLine("You have no assignments!");              
            }
            else
            {
                Console.WriteLine("You have the below assignments for submit:\n");
                foreach (var item in Schedule)
                {
                    if (item.IsSubmitted == false)
                    {
                        Console.WriteLine($"Code: {item.Assignment.Id} - '{item.Assignment.Title}' in Course '{item.Course.Title}'. Submit before {item.Assignment.Submission}");
                    }
                    
                }
            }          
        }

        public void SubmitAssignment(User user)
        {
            bool check = true;         
            while (check)
            {
                using (DataContext db = new DataContext())
                {
                    Schedule = db.TrainerSchedules.Include("User").Include("Assignment").Include("Course").Where(x => x.UserId == user.Id).ToList();
                    assignments = data.GetAssignments();
                }

                List<Assignment> tempAssignments = new List<Assignment>();
                if (Schedule.All(x=>x.IsSubmitted == true))
                {
                    Console.WriteLine("You have no assignments to submit!\n");
                    break;
                }
                Console.WriteLine("You have the below assignments for submit:\n");

                foreach (var item in Schedule)
                {
                    if (item.IsSubmitted == false)
                    {
                        Console.WriteLine($"Code: {item.Assignment.Id} - '{item.Assignment.Title}' in Course '{item.Course.Title}'. Submit before {item.Assignment.Submission}");
                    }
                }                        
                Console.Write("\nType assignment code for submit (Press E to exit): ");              
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                int input;
                while (!int.TryParse(Choise, out input))
                {
                    Console.Write($"You can only enter Numbers or Available assignment code: ");
                    Choise = Console.ReadLine();
                }

                bool assignmentCheck = false;
                using (DataContext db = new DataContext())
                {
                    List<TrainerSchedule> result = db.TrainerSchedules.Where(x => x.UserId == user.Id).ToList();
                    foreach (var item in result)
                    {
                        if ((item.AssignmentId == input) && item.IsSubmitted == false)
                        {
                            item.IsSubmitted = true;
                            db.SaveChanges();
                            Console.WriteLine("Submit Was Successful!");
                            assignmentCheck = false;
                            break;
                        }
                        else if ((item.AssignmentId == input) && item.IsSubmitted == true)
                        {
                            Console.WriteLine($"You have already submitted Course with Code: {input}");
                            assignmentCheck = false;
                            break;
                        }
                        else
                        {
                            assignmentCheck = true;
                        }
                    }
                    if (assignmentCheck)
                    {
                        Console.WriteLine($"There is not Assignment with the Code: {input}");
                    }
                }              
                Console.Write("\nDo you want to submit another Assignment? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
               
        }
    }

   
}
