using System;

namespace MoviePlex
{
    public static class Administrator
    {
        public static void ValidateAdmin()
        {
            int passwordAttemptCounter = 1;

            Console.WriteLine("Please Enter Admin Password or Press B to go back to main screen!");

            while (passwordAttemptCounter !=6)
            {
                var password = Console.ReadLine();

                if(password == "B")
                {
                    break;
                }
                else if (CheckPassword(password))
                {
                    
                }
                else
                {
                    UtilityFunctions.PrintErrorMsg("Invalid Password!!");
                    UtilityFunctions.PrintErrorMsg($"You have {5 - passwordAttemptCounter} attempts left!");

                    Console.WriteLine("Enter Password again or Press B to go back to main screen!");
                }

                passwordAttemptCounter++;
            }

            UtilityFunctions.StartApplication();
        }

        private static bool CheckPassword(string password)
        {
            return string.Equals(password, "Abcd1234");
        }
    }
}
