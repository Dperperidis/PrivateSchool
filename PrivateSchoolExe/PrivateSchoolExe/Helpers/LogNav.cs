using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    public class LogNav
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public void Login()
        {
            Console.WriteLine("Type Email and Password to Login!\n");
            Console.Write("Email: ");
            Email = Console.ReadLine().ToLower();
            Console.Write("Password: ");
            Password = Console.ReadLine().ToLower();
        }

        public bool Navigate()
        {
            bool check = true;
            while (check)
            {
                Console.Write($"\nType E to Exit or B to go back to menu: ");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "e":
                        Environment.Exit(0);
                        break;
                    case "b":
                        check = false;
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
    }
}
