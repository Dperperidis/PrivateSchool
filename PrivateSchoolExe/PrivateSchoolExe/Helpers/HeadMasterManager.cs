using PrivateSchoolExe.Data;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    public class HeadMasterManager
    {

        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        DataLists data = new DataLists();
        List<Course> courses = new List<Course>();
        Authentication auth = new Authentication();

        public string[] headMasterMenu = new string[] {"Manage Courses",
            "Manage Students",
            "Manage Assignments",
            "Manage Trainers",
            "Manage Students per Courses",
            "Manage Trainers per Courses",
            "Manage Assignments per Courses",
            "Manage Schedule per Courses"};

        public int GetMenu(User user)
        {
            Console.Clear();
            Console.WriteLine($"\nWelcome {user.FirstName} {user.LastName}!\n");
            Console.WriteLine($"Your Role is HeadMaster\n");
            for (int i = 0; i < headMasterMenu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {headMasterMenu[i]}");
            }
            Console.Write("\nChoose your option: ");
            string Choise = Console.ReadLine();
            int input;
            while (!int.TryParse(Choise, out input) || input > headMasterMenu.Length)
            {
                Console.Write($"You can only enter Numbers (Choose between 1-{headMasterMenu.Length}): ");
                Choise = Console.ReadLine();
            }
            return input;
        }

        public void ManageCourses(User user)
        {
            bool menu = true;
            while (menu)
            {              
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. Delete Course");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateCourse();
                        break;
                    case "2":
                        UpdateCourse();
                        break;
                    case "3":
                        DeleteCourse();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }

        }
        private void CreateCourse()
        {
            bool check = true;
            while (check)
            {
                Course course = new Course();
                Console.WriteLine("Enter Course Details\n");
                Console.Write("Title: ");
                string title = Console.ReadLine().ToLower();
                Console.Write("Stream: ");
                string stream = Console.ReadLine().ToLower();
                Console.Write("Type: ");
                string type = Console.ReadLine().ToLower();
                Console.Write("Start Date: ");
                string choise = Console.ReadLine();
                DateTime startDate;
                while (!DateTime.TryParse(choise, out startDate))
                {
                    Console.WriteLine("Enter valid Date (ex. 2019/07/24)");
                    choise = Console.ReadLine();
                }
                Console.Write("End Date: ");
                string secChoise = Console.ReadLine();
                DateTime endDate;
                while (!DateTime.TryParse(secChoise, out endDate))
                {
                    Console.WriteLine("Enter valid Date (ex. 2019/07/24)");
                    secChoise = Console.ReadLine();
                }
                using(DataContext db = new DataContext())
                {
                    db.Courses.Add(new Course()
                    {
                        Title = textInfo.ToTitleCase(title),
                        Stream = textInfo.ToTitleCase(stream),
                        Type = textInfo.ToTitleCase(type),
                        EndDate = endDate,
                        StartDate = startDate
                    });
                    db.SaveChanges();
                }
                    Console.WriteLine("Course created successfully!");
                Console.Write("\nDo you want to add another course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();

            }
              
        }
        private void DeleteCourse()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                courses = data.GetCourses();
                Console.WriteLine("All available courses are below:\n");
                foreach (var item in courses)
                {
                    Console.WriteLine(item);
                }

                Console.Write("\nType course Id to delete: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid course Id: ");
                    input = Console.ReadLine();

                }
                if (!courses.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no course with id: {result}");
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var course = db.Courses.Find(result);
                        db.Courses.Remove(course);
                        db.SaveChanges();
                    }
                    Console.WriteLine("Delete was successful!");
                }

                Console.Write("\nDo you want to delete another course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }         

        }
        private void UpdateCourse()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                courses = data.GetCourses();
                Console.WriteLine("All available courses are below:\n");
                foreach (var item in courses)
                {
                    Console.WriteLine(item);
                }

                Console.Write("\nType course Id to update: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid course Id: ");
                    input = Console.ReadLine();

                }
                if (!courses.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no course with id: {result}\n");
                }
                else
                {
                    var course = courses.First(x => x.Id == result);
                    Console.WriteLine($"Current title: {course.Title}");
                    Console.WriteLine($"Current stream: {course.Stream}");
                    Console.WriteLine($"Current type: {course.Type}");
                    Console.WriteLine($"Current start Date: {course.StartDate}");
                    Console.WriteLine($"Current end Date: {course.EndDate}");

                    Console.Write("\nEnter new Title: ");
                    string title = Console.ReadLine().ToLower();
                    Console.Write("Enter new Stream: ");
                    string stream = Console.ReadLine().ToLower();
                    Console.Write("Enter new type: ");
                    string type = Console.ReadLine().ToLower();
                    Console.Write("Enter new Start Date: ");
                    string choise = Console.ReadLine();
                    DateTime startDate;
                    while (!DateTime.TryParse(choise, out startDate))
                    {
                        Console.WriteLine("Enter valid Date (ex. 2019/07/24)");
                        choise = Console.ReadLine();
                    }
                    Console.Write("End new End Date: ");
                    string secChoise = Console.ReadLine();
                    DateTime endDate;
                    while (!DateTime.TryParse(secChoise, out endDate))
                    {
                        Console.WriteLine("Enter valid Date (ex. 2019/07/24)");
                        secChoise = Console.ReadLine();
                    }

                    using(DataContext db = new DataContext())
                    {
                        var temp = db.Courses.FirstOrDefault(x => x.Id == result);
                        temp.Title = textInfo.ToTitleCase(title);
                        temp.Stream = textInfo.ToTitleCase(stream);
                        temp.Type = textInfo.ToTitleCase(type);
                        temp.StartDate = startDate;
                        temp.EndDate = endDate;
                        db.SaveChanges();
                        Console.WriteLine("\nUpdate was successful!");
                    }

                }

                Console.Write("\nDo you want to update another course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }

        public void ManageStudents(User user)
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }
        }
        private void CreateStudent()
        {
            bool check = true;
            while (check)
            {
                User student = new User();
                Console.WriteLine("Enter Student Details\n");
                Console.Write("First Name: ");
                string first = Console.ReadLine().ToLower();
                Console.Write("Last Name: ");
                string last = Console.ReadLine().ToLower();
                Console.Write("Email: ");
                string email = Console.ReadLine().ToLower();
                Console.Write("Birth Date: ");
                string choise = Console.ReadLine();             
                DateTime birth;
                while (!DateTime.TryParse(choise, out birth))
                {
                    Console.WriteLine("Enter valid Date (ex. 1990/07/13)");
                    choise = Console.ReadLine();
                }
                Console.Write("Tuitions fees: ");
                string fees = Console.ReadLine();
                int total;
                while (!int.TryParse(fees, out total))
                {
                    Console.WriteLine("Enter numbers only.");
                    fees = Console.ReadLine();
                }
                Console.Write("Password: ");
                string pass = Console.ReadLine();             
                student.FirstName = textInfo.ToTitleCase(first);
                student.LastName = textInfo.ToTitleCase(last);
                student.Email = email.ToLower();
                student.LevelId = 1;
                student.TuitionsFees = total;
                student.DateOfBirth = birth;
                auth.Register(student, pass);

                Console.WriteLine("Student created successfully!");
                Console.Write("\nDo you want to add another student? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }           
        }
        private void DeleteStudent()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var users = data.GetStudents();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName}");
                }

                Console.Write("\nChoose student code to delete: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid student code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x=>x.Id == result))
                {
                    Console.WriteLine("Student didn't found. Try a valid student code.");
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var stud = db.Users.Find(result);
                        db.Users.Remove(stud);
                        db.SaveChanges();
                        Console.WriteLine("Student deleted successfully!");
                    }
                }                 
                Console.Write("\nDo you want to delete another student? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }
        private void UpdateStudent()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var users = data.GetStudents();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName}");
                }

                Console.Write("\nType Student Code to update: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid student Id: ");
                    input = Console.ReadLine();

                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no student with id: {result}\n");
                }
                else
                {
                    var student = users.First(x => x.Id == result);
                    Console.WriteLine($"Current First name: {student.FirstName}");
                    Console.WriteLine($"Current Last name: {student.LastName}");
                    Console.WriteLine($"Current email: {student.Email}");
                    Console.WriteLine($"Current birth: {student.DateOfBirth}");
                    Console.WriteLine($"Current fees: {student.TuitionsFees}");


                    Console.Write("\nEnter new First name: ");
                    string firstName = Console.ReadLine().ToLower();
                    Console.Write("Enter new Last name: ");
                    string lastName = Console.ReadLine().ToLower();
                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine().ToLower();                  
                    Console.Write("Enter new birth: ");
                    string choise = Console.ReadLine();
                    DateTime birth;
                    while (!DateTime.TryParse(choise, out birth))
                    {
                        Console.WriteLine("Enter valid Date (ex. 1990/05/24)");
                        choise = Console.ReadLine();
                    }
                    Console.Write("Enter new fees: ");
                    string secChoise = Console.ReadLine();
                    int fees;
                    while (!int.TryParse(secChoise, out fees))
                    {
                        Console.Write("Enter only numbers: ");
                        secChoise = Console.ReadLine();
                    }

                    using (DataContext db = new DataContext())
                    {
                        var temp = db.Users.FirstOrDefault(x => x.Id == result);
                        temp.FirstName = textInfo.ToTitleCase(firstName);
                        temp.LastName = textInfo.ToTitleCase(lastName);
                        temp.Email = email.ToLower();
                        temp.DateOfBirth = birth;
                        temp.TuitionsFees = fees;
                        temp.LevelId = 1;
                        db.SaveChanges();
                        Console.WriteLine("\nUpdate was successful!");
                    }
                }
                Console.Write("\nDo you want to update another student? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }

        public void ManageAssignments(User user)
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create Assignment");
                Console.WriteLine("2. Update Assignment");
                Console.WriteLine("3. Delete Assignment");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateAssignment();
                        break;
                    case "2":
                        UpdateAssignment();
                        break;
                    case "3":
                        DeleteAssignment();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }
        }
        private void CreateAssignment()
        {
            bool check = true;
            while (check)
            {
                Assignment assignment = new Assignment();
                Console.WriteLine("Enter Assignment Details\n");
                Console.Write("Ttile: ");
                string title = Console.ReadLine().ToLower();
                Console.Write("Description: ");
                string description = Console.ReadLine().ToLower();
                Console.Write("Oral Mark: ");
                string oral = Console.ReadLine();
                int oralmark;
                while (!int.TryParse(oral, out oralmark))
                {
                    Console.WriteLine("Enter numbers only.");
                    oral = Console.ReadLine();
                }
                Console.Write("Total Mark: ");
                string total = Console.ReadLine();
                int totalMark;
                while (!int.TryParse(total, out totalMark))
                {
                    Console.WriteLine("Enter numbers only.");
                    oral = Console.ReadLine();
                }
                Console.Write("Submission: ");
                string choise = Console.ReadLine();
                DateTime sub;
                while (!DateTime.TryParse(choise, out sub))
                {
                    Console.WriteLine("Enter valid Date (ex. 2019/05/14)");
                    choise = Console.ReadLine();
                }
                
                using (DataContext db = new DataContext())
                {
                    db.Assignments.Add(new Assignment()
                    {
                    Title = textInfo.ToTitleCase(title),
                    Description = description,
                    OralMark = oralmark,
                    TotalMark = totalMark,
                    Submission = sub
                    });
                    db.SaveChanges();
                }

                Console.WriteLine("Assignment created successfully!");
                Console.Write("\nDo you want to add another Assignment? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }
        private void DeleteAssignment()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var assignments = data.GetAssignments();
                Console.WriteLine("All available Assignments are below:\n");
                foreach (var item in assignments)
                {
                    Console.WriteLine(item);
                }

                Console.Write("\nType Assignment Id to delete: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid Assignment Id: ");
                    input = Console.ReadLine();

                }
                if (!assignments.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no Assignment with id: {result}");
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var assignment = db.Assignments.Find(result);
                        db.Assignments.Remove(assignment);
                        db.SaveChanges();
                    }
                    Console.WriteLine("Delete was successful!");
                }

                Console.Write("\nDo you want to delete another Assignment? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }

        }
        private void UpdateAssignment()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var assignments = data.GetAssignments();
                Console.WriteLine("All available Assignments are below:\n");
                foreach (var item in assignments)
                {
                    Console.WriteLine(item);
                }

                Console.Write("\nType Assignemnt Id to update: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid assignment Id: ");
                    input = Console.ReadLine();

                }
                if (!assignments.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no assignment with id: {result}\n");
                }
                else
                {
                    var assignment = assignments.First(x => x.Id == result);
                    Console.WriteLine($"Current title: {assignment.Title}");
                    Console.WriteLine($"Current Description: {assignment.Description}");
                    Console.WriteLine($"Current Submission: {assignment.Submission}");
                    Console.WriteLine($"Current Oral Mark: {assignment.OralMark}");
                    Console.WriteLine($"Current Total Mark: {assignment.TotalMark}");

                    Console.Write("\nEnter new Title: ");
                    string title = Console.ReadLine().ToLower();
                    Console.Write("Enter new Description: ");
                    string descr = Console.ReadLine().ToLower();                  
                    Console.Write("Enter new Submission: ");
                    string choise = Console.ReadLine();
                    DateTime sub;
                    while (!DateTime.TryParse(choise, out sub))
                    {
                        Console.WriteLine("Enter valid Date (ex. 2019/07/24)");
                        choise = Console.ReadLine();
                    }
                    Console.Write("Enter new Oral Mark: ");
                    string oral = Console.ReadLine();
                    int oralMark;
                    while (!int.TryParse(oral, out oralMark))
                    {
                        Console.WriteLine("Enter numbers only.");
                        oral = Console.ReadLine();
                    }
                    Console.Write("Enter new Total Mark: ");
                    string total = Console.ReadLine();
                    int totalMark;
                    while (!int.TryParse(total, out totalMark))
                    {
                        Console.WriteLine("Enter numbers only.");
                        total = Console.ReadLine();
                    }

                    using (DataContext db = new DataContext())
                    {
                        var temp = db.Assignments.FirstOrDefault(x => x.Id == result);
                        temp.Title = textInfo.ToTitleCase(title);
                        temp.Description = descr;
                        temp.Submission = sub;
                        temp.OralMark = oralMark;
                        temp.TotalMark = totalMark;
                        db.SaveChanges();
                        Console.WriteLine("\nUpdate was successful!");
                    }

                }

                Console.Write("\nDo you want to update another assignment? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }

        public void ManageTrainers(User user)
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create Trainer");
                Console.WriteLine("2. Update Trainer");
                Console.WriteLine("3. Delete Trainer");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateTrainer();
                        break;
                    case "2":
                        UpdateTrainer();
                        break;
                    case "3":
                        DeleteTrainer();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }
        }
        private void CreateTrainer()
        {
            bool check = true;
            while (check)
            {
                User trainer = new User();
                Console.WriteLine("Enter Trainer Details\n");
                Console.Write("First Name: ");
                string first = Console.ReadLine().ToLower();
                Console.Write("Last Name: ");
                string last = Console.ReadLine().ToLower();
                Console.Write("Email: ");
                string email = Console.ReadLine().ToLower();
                Console.Write("Subject: ");
                string sub = Console.ReadLine();
                Console.Write("Birth Date: ");
                string choise = Console.ReadLine();
                DateTime birth;
                while (!DateTime.TryParse(choise, out birth))
                {
                    Console.WriteLine("Enter valid Date (ex. 1980/07/13)");
                    choise = Console.ReadLine();
                }
                Console.Write("Password: ");
                string pass = Console.ReadLine();
                trainer.FirstName = textInfo.ToTitleCase(first);
                trainer.LastName = textInfo.ToTitleCase(last);
                trainer.Email = email.ToLower();
                trainer.LevelId = 2;
                trainer.Subject = sub;
                trainer.DateOfBirth = birth;

                auth.Register(trainer, pass);

                Console.WriteLine("Trainer created successfully!");
                Console.Write("\nDo you want to add another trainer? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }
        private void DeleteTrainer()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var trainers = data.GetTrainers();
                Console.WriteLine("All Trainers are below: \n");
                foreach (var item in trainers)
                {
                    Console.WriteLine($"Trainer Code: {item.Id} - {item.FirstName} {item.LastName} teaches {item.Subject}");
                }

                Console.Write("\nChoose trainer code to delete: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid student code: ");
                    input = Console.ReadLine();
                }
                if (!trainers.Any(x => x.Id == result))
                {
                    Console.WriteLine("Trainer didn't found. Try a valid trainer code.");
                }
                else
                {
                    using (DataContext db = new DataContext())
                    {
                        var stud = db.Users.Find(result);
                        db.Users.Remove(stud);
                        db.SaveChanges();
                        Console.WriteLine("Trainer deleted successfully!");
                    }
                }
                Console.Write("\nDo you want to delete another trainer? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }
        private void UpdateTrainer()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var users = data.GetTrainers();
                Console.WriteLine("All Trainers are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Trainer Code: {item.Id} - {item.FirstName} {item.LastName} teaches {item.Subject}");
                }

                Console.Write("\nType Trainer Code to update: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write($"Choose a valid trainer Id: ");
                    input = Console.ReadLine();

                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine($"There is no trainer with id: {result}\n");
                }
                else
                {
                    var trainer = users.First(x => x.Id == result);
                    Console.WriteLine($"Current First name: {trainer.FirstName}");
                    Console.WriteLine($"Current Last name: {trainer.LastName}");
                    Console.WriteLine($"Current email: {trainer.Email}");
                    Console.WriteLine($"Current Subject: {trainer.Subject}");
                    Console.WriteLine($"Current Birth: {trainer.DateOfBirth}");


                    Console.Write("\nEnter new First name: ");
                    string firstName = Console.ReadLine().ToLower();
                    Console.Write("Enter new Last name: ");
                    string lastName = Console.ReadLine().ToLower();
                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine().ToLower();
                    Console.Write("Enter new Subject: ");
                    string sub = Console.ReadLine();
                    Console.Write("Enter new Birth Date: ");
                    string choise = Console.ReadLine();
                    DateTime birth;
                    while (!DateTime.TryParse(choise, out birth))
                    {
                        Console.WriteLine("Enter valid Date (ex. 1990/07/13)");
                        choise = Console.ReadLine();
                    }

                    using (DataContext db = new DataContext())
                    {
                        var temp = db.Users.FirstOrDefault(x => x.Id == result);
                        temp.FirstName = textInfo.ToTitleCase(firstName);
                        temp.LastName = textInfo.ToTitleCase(lastName);
                        temp.Email = email.ToLower();
                        temp.Subject = sub;
                        temp.LevelId = 2;
                        temp.DateOfBirth = birth;
                        db.SaveChanges();
                        Console.WriteLine("\nUpdate was successful!");
                    }
                }
                Console.Write("\nDo you want to update another trainer? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();
            }
        }
        
        public void ManageStudentPerCourse()
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create student per course");
                Console.WriteLine("2. Update student per course");
                Console.WriteLine("3. Delete student per course");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateStudentPerCourse();
                        break;
                    case "2":
                        UpdateStudentPerCourse();
                        break;
                    case "3":
                        DeleteStudentPerCourse();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }
        }
        private void CreateStudentPerCourse()
        {
            bool check = true;
            while (check)
            {
                var students = data.GetStudents();
                Console.Clear();
                var users = data.GetStudents();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName}");
                }

                Console.Write("\nChoose student code to enroll him in a course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid student code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Student didn't found. Try a valid student code.");
                }
                else
                {
                    courses = data.GetCourses();
                    Console.WriteLine("All available courses are below:\n");
                    foreach (var item in courses)
                    {
                        Console.WriteLine(item);
                    }

                    Console.Write("\nType course Id: ");
                    string couseInput = Console.ReadLine();
                    int courseResult;
                    while (!int.TryParse(couseInput, out courseResult))
                    {
                        Console.Write($"Choose a valid course Id: ");
                        input = Console.ReadLine();

                    }
                    if (!courses.Any(x => x.Id == courseResult))
                    {
                        Console.WriteLine($"There is no course with id: {result}");
                    }
                    else
                    {
                        var course = courses.FirstOrDefault(x => x.Id == courseResult);
                        using (DataContext db = new DataContext())
                        {
                            foreach (var item in course.Assignments)
                            {
                                db.TrainerSchedules.Add(new TrainerSchedule()
                                {
                                    UserId = result,
                                    CourseId = courseResult,
                                    AssignmentId = item.Id,
                                    IsSubmitted = false
                                });                                                           
                            }
                            Console.WriteLine("Inserted data successfully!");
                            db.SaveChanges();
                        }
                    }              
                }
                Console.Write("\nDo you want to add another student per course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();

            }
        }
        private void DeleteStudentPerCourse()
        {
            bool check = true;
            while (check)
            {
                var students = data.GetStudents();
                Console.Clear();
                var users = data.GetStudents();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName}");
                }

                Console.Write("\nChoose student code to delete from his course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid student code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Student didn't found. Try a valid student code.");
                }
                else
                {
                    var trainerSchedule = data.GetSchedule();
                    var studentSchedule = trainerSchedule.Where(x=>x.UserId == result).GroupBy(x => x.Course.Title, (key) => new { Course = key });
                    Console.WriteLine("\nStudent is enrolled in below courses:");
                    var courses = data.GetCourses();
                    foreach (var item in studentSchedule)
                    {
                        foreach (var cor in courses)
                        {
                            if (item.Key == cor.Title)
                            {
                                Console.WriteLine($"Id: {cor.Id}. {cor.Title}\n");
                            }
                        }                                                                       
                    }

                    Console.Write("Which course you want to delete from the student? (Type Course Id): ");         
                    string courseId = Console.ReadLine();
                    int selectedCourse;
                    while (!int.TryParse(courseId, out selectedCourse))
                    {
                        Console.WriteLine("Enter numbers only.");
                        courseId = Console.ReadLine();
                    }
                    if (!trainerSchedule.Any(x => x.CourseId == selectedCourse))
                    {
                        Console.WriteLine("Course didn't found. Try a valid course id.");
                    }
                    else
                    {
                        using(DataContext db = new DataContext())
                        {
                         var temp = db.TrainerSchedules.Where(x => x.CourseId == selectedCourse).ToList();
                         var tempFin = temp.Where(x => x.UserId == result).ToList();
                            foreach (var item in tempFin)
                            {
                                db.TrainerSchedules.Remove(item);
                                db.SaveChanges();
                            }
                            
                        }
                        Console.WriteLine("Delete was successful!");
                        Console.Write("\nDo you want to update another trainer? Y or N: ");
                        if (!(Console.ReadLine().ToLower() == "y"))
                        {
                            check = false;
                        }
                        Console.Clear();
                    }
                }
            }
        }
        private void UpdateStudentPerCourse()
        {
            bool check = true;
            while (check)
            {
                var students = data.GetStudents();
                Console.Clear();
                var users = data.GetStudents();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName}");
                }

                Console.Write("\nChoose student code to delete from his course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid student code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Student didn't found. Try a valid student code.");
                }
                else
                {
                    var trainerSchedule = data.GetSchedule();
                    var studentSchedule = trainerSchedule.Where(x => x.UserId == result).GroupBy(x => x.Course.Title, (key) => new { Course = key });
                    Console.WriteLine("\nStudent is enrolled in below courses:");
                    var courses = data.GetCourses();
                    foreach (var item in studentSchedule)
                    {
                        foreach (var cor in courses)
                        {
                            if (item.Key == cor.Title)
                            {
                                Console.WriteLine($"Id: {cor.Id}. {cor.Title}\n");
                            }
                        }
                    }

                    Console.Write("Which course you want to update from the student? (Type Course Id): ");
                    string courseId = Console.ReadLine();
                    int selectedCourse;
                    while (!int.TryParse(courseId, out selectedCourse))
                    {
                        Console.WriteLine("Enter numbers only.");
                        courseId = Console.ReadLine();
                    }
                    if (!trainerSchedule.Any(x => x.CourseId == selectedCourse))
                    {
                        Console.WriteLine("Course didn't found. Try a valid course id.");
                    }
                    else
                    {
                        Console.WriteLine("With which course you want to change?:");
                        var allCourses = data.GetCourses();
                        foreach (var item in allCourses)
                        {
                            Console.WriteLine();
                            Console.WriteLine(item);
                        }

                        Console.Write("Choose course Id: ");
                        string courseIdnew = Console.ReadLine();
                        int selectedCoursenew;
                        while (!int.TryParse(courseIdnew, out selectedCoursenew))
                        {
                            Console.WriteLine("Enter numbers only.");
                            courseId = Console.ReadLine();
                        }
                        if (!allCourses.Any(x => x.Id == selectedCourse))
                        {
                            Console.WriteLine("Course didn't found. Try a valid course id.");
                        }

                        using (DataContext db = new DataContext())
                        {
                            var temp = db.TrainerSchedules.Where(x => x.CourseId == selectedCourse).ToList();
                            var tempFin = temp.Where(x => x.UserId == result).ToList();
                            foreach (var item in tempFin)
                            {
                               var tempResult = db.TrainerSchedules.FirstOrDefault(x=>x.CourseId == item.CourseId);
                                tempResult.CourseId = selectedCoursenew;
                                db.SaveChanges();
                            }
                        }
                        Console.WriteLine("Update was successful!");
                        Console.Write("\nDo you want to update another student per course? Y or N: ");
                        if (!(Console.ReadLine().ToLower() == "y"))
                        {
                            check = false;
                        }
                        Console.Clear();
                    }
                }
            }
        }

        public void ManageTrainerPerCourse()
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose 1 from below Options:\n");
                Console.WriteLine("1. Create trainer per course");
                Console.WriteLine("2. Update trainer per course");
                Console.WriteLine("3. Delete trainer per course");
                Console.Write("\nChoose your option (Press E for Exit): ");
                string Choise = Console.ReadLine().ToLower();
                if (Choise == "e")
                {
                    break;
                }
                switch (Choise)
                {
                    case "1":
                        CreateTrainerPerCourse();
                        break;
                    case "2":
                        UpdateTrainerPerCourse();
                        break;
                    case "3":
                        DeleteTrainerPerCourse();
                        break;
                    default:
                        Console.WriteLine("Choose a number between 1-3!");
                        break;
                }
            }
        }
        private void CreateTrainerPerCourse()
        {
            bool check = true;
            while (check)
            {
 
                Console.Clear();
                var users = data.GetTrainers();
                Console.WriteLine("All trainers are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Trainer Code: {item.Id} - {item.FirstName} {item.LastName} teaches {item.Subject}");
                }

                Console.Write("\nChoose trainer code to enroll him in a course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid trainer code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Trainer didn't found. Try a valid trainer code.");
                }
                else
                {
                    courses = data.GetCourses();
                    Console.WriteLine("All available courses are below:\n");
                    foreach (var item in courses)
                    {
                        Console.WriteLine(item);
                    }

                    Console.Write("\nType course Id: ");
                    string couseInput = Console.ReadLine();
                    int courseResult;
                    while (!int.TryParse(couseInput, out courseResult))
                    {
                        Console.Write($"Choose a valid course Id: ");
                        input = Console.ReadLine();

                    }
                    if (!courses.Any(x => x.Id == courseResult))
                    {
                        Console.WriteLine($"There is no course with id: {courseResult}");
                    }
                    else
                    {
                        var course = courses.FirstOrDefault(x => x.Id == courseResult);
                        using (DataContext db = new DataContext())
                        {                          
                                db.TrainerSchedules.Add(new TrainerSchedule()
                                {
                                    TrainerId = result,
                                    CourseId = courseResult,
                                    IsSubmitted = false
                                });
                                              
                            db.SaveChanges();
                        }
                        Console.WriteLine("Inserted data successfully!");
                    }
                }
                Console.Write("\nDo you want to add another trainer per course? Y or N: ");
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    check = false;
                }
                Console.Clear();

            }
        }
        private void DeleteTrainerPerCourse()
        {
            bool check = true;
            while (check)
            {
                Console.Clear();
                var users = data.GetTrainers();
                Console.WriteLine("All Trainers are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Student Code: {item.Id} - {item.FirstName} {item.LastName} teaches {item.Subject}.");
                }

                Console.Write("\nChoose trainer code to delete from his course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid trainer code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Trainer didn't found. Try a valid trainer code.");
                }
                else
                {
                    var trainerSchedule = data.GetSchedule();
                    var studentSchedule = trainerSchedule.Where(x => x.TrainerId == result).GroupBy(x => x.Course.Title, (key) => new { Course = key });
                    Console.WriteLine("\nTrainer is enrolled in below courses:");
                    var courses = data.GetCourses();
                    foreach (var item in studentSchedule)
                    {
                        foreach (var cor in courses)
                        {
                            if (item.Key == cor.Title)
                            {
                                Console.WriteLine($"Id: {cor.Id}. {cor.Title}\n");
                            }
                        }
                    }

                    Console.Write("Which course you want to delete from the trainer? (Type Course Id): ");
                    string courseId = Console.ReadLine();
                    int selectedCourse;
                    while (!int.TryParse(courseId, out selectedCourse))
                    {
                        Console.WriteLine("Enter numbers only.");
                        courseId = Console.ReadLine();
                    }
                    if (!trainerSchedule.Any(x => x.CourseId == selectedCourse))
                    {
                        Console.WriteLine("Course didn't found. Try a valid course id.");
                    }
                    else
                    {
                        using (DataContext db = new DataContext())
                        {
                            var temp = db.TrainerSchedules.Where(x => x.CourseId == selectedCourse).ToList();
                            var tempFin = temp.Where(x => x.TrainerId == result).ToList();
                            foreach (var item in tempFin)
                            {
                                db.TrainerSchedules.Remove(item);
                                db.SaveChanges();
                            }

                        }
                        Console.WriteLine("Delete was successful!");
                        Console.Write("\nDo you want to delete another trainer? Y or N: ");
                        if (!(Console.ReadLine().ToLower() == "y"))
                        {
                            check = false;
                        }
                        Console.Clear();
                    }
                }
            }
        }
        private void UpdateTrainerPerCourse()
        {
            bool check = true;
            while (check)
 
                Console.Clear();
                var users = data.GetTrainers();
                Console.WriteLine("All students are below: \n");
                foreach (var item in users)
                {
                    Console.WriteLine($"Trainer Code: {item.Id} - {item.FirstName} {item.LastName} teaches {item.Subject}");
                }

                Console.Write("\nChoose trainer code to delete from his course: ");
                string input = Console.ReadLine();
                int result;
                while (!int.TryParse(input, out result))
                {
                    Console.Write("Enter valid trainer code: ");
                    input = Console.ReadLine();
                }
                if (!users.Any(x => x.Id == result))
                {
                    Console.WriteLine("Trainer didn't found. Try a valid trainer code.");
                }
                else
                {
                    var trainerSchedule = data.GetSchedule();
                    var studentSchedule = trainerSchedule.Where(x => x.UserId == result).GroupBy(x => x.Course.Title, (key) => new { Course = key });
                    Console.WriteLine("\nTrainer is enrolled in below courses:");
                    var courses = data.GetCourses();
                    foreach (var item in studentSchedule)
                    {
                        foreach (var cor in courses)
                        {
                            if (item.Key == cor.Title)
                            {
                                Console.WriteLine($"Id: {cor.Id}. {cor.Title}\n");
                            }
                        }
                    }

                    Console.Write("Which course you want to update from the trainer? (Type Course Id): ");
                    string courseId = Console.ReadLine();
                    int selectedCourse;
                    while (!int.TryParse(courseId, out selectedCourse))
                    {
                        Console.WriteLine("Enter numbers only.");
                        courseId = Console.ReadLine();
                    }
                    if (!trainerSchedule.Any(x => x.CourseId == selectedCourse))
                    {
                        Console.WriteLine("Course didn't found. Try a valid course id.");
                    }
                    else
                    {
                        Console.WriteLine("With which course you want to change?:");
                        var allCourses = data.GetCourses();
                        foreach (var item in allCourses)
                        {
                            Console.WriteLine();
                            Console.WriteLine(item);
                        }

                        Console.Write("Choose course Id: ");
                        string courseIdnew = Console.ReadLine();
                        int selectedCoursenew;
                        while (!int.TryParse(courseIdnew, out selectedCoursenew))
                        {
                            Console.WriteLine("Enter numbers only.");
                            courseId = Console.ReadLine();
                        }
                        if (!allCourses.Any(x => x.Id == selectedCourse))
                        {
                            Console.WriteLine("Course didn't found. Try a valid course id.");
                        }

                        using (DataContext db = new DataContext())
                        {
                            var temp = db.TrainerSchedules.Where(x => x.CourseId == selectedCourse).ToList();
                            var tempFin = temp.Where(x => x.TrainerId == result).ToList();
                            foreach (var item in tempFin)
                            {
                                var tempResult = db.TrainerSchedules.FirstOrDefault(x => x.CourseId == item.CourseId);
                                tempResult.CourseId = selectedCoursenew;
                                db.SaveChanges();
                            }
                        }
                        Console.WriteLine("Update was successful!");
                        Console.Write("\nDo you want to update another trainer per course? Y or N: ");
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

