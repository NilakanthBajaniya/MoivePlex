using System;
using System.Threading.Tasks;

namespace MoviePlex
{
    public static class UtilityFunctions
    {
        static String userRoleSelection = "";

        public static void PrintApplicationTitle()
        {

            Console.WriteLine("**************************************************************");
            Console.WriteLine("**************** Welcome to MoviePlex Theatre ****************");
            Console.WriteLine("**************************************************************");
        }

        public static void PrintApplicationOptions()
        {
            Console.WriteLine("\nPlease Select From The Following Options : ");
            Console.WriteLine("1: Administrtor");
            Console.WriteLine("2: Guests");
            Console.WriteLine("\nSelection:\t");
        }

        public static void PrintErrorMsg(string msg)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);

            Console.ResetColor();
        }

        public static void StartApplication()
        {
            Console.Clear();
            PrintApplicationTitle();
            PrintApplicationOptions();

            while (true)
            {
                userRoleSelection = Console.ReadLine();

                if (!UtilityFunctions.IsNumber(userRoleSelection))
                {
                    Console.Clear();

                    PrintApplicationTitle();
                    PrintErrorMsg("Please enter valid input!");
                    PrintApplicationOptions();

                    continue;
                }
                else
                {
                    break;
                }
            }

            if (Convert.ToInt32(userRoleSelection) == 1)
            {
                Administrator.ValidateAdmin();
            }
            else if (Convert.ToInt32(userRoleSelection) == 2)
            {
                User.InitGuest();
            }
            else
            {
                PrintErrorMsg("Please enter valid input!");
                Task.Delay(2000);

                StartApplication();
            }
        }

        public static void RestartApplication()
        {
            MovieOperations.ClearMovies();
            StartApplication();
        }

        public static bool IsNumber(string value)
        {
            return (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out _)) ? true : false;
        }
    }
}
