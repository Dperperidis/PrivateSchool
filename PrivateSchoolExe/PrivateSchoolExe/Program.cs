using PrivateSchoolExe.Helpers;
using PrivateSchoolExe.Menu;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome to Private School Management!\n");
            //Login System
            LogNav login = new LogNav();
            login.Login();

            //Authentication
            Authentication auth = new Authentication();
            User userloggedIn = auth.Login(login.Email, login.Password);

            while (userloggedIn == null)
            {
                Console.WriteLine("--Wrong Email/Password or User Hasn't been created yet--");
                login.Login();
                userloggedIn = auth.Login(login.Email, login.Password);  
            }
            
            switch (userloggedIn.Level.Access)
            {
                case 1:
                    {
                        StudentMenu menu = new StudentMenu();
                        menu.studMenu(userloggedIn);
                    }
                    break;
                case 2:
                    {
                        TrainerMenu menu = new TrainerMenu();
                        menu.trainerMenu(userloggedIn);
                    }
                    break;
                case 3:
                    {
                        HeadMasterMenu menu = new HeadMasterMenu();
                        menu.HdMenu(userloggedIn);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
