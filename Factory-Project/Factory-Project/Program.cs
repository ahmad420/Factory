using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factory_Project
{
    internal class Program
    {
        // Add the necessary imports for 80s-style appearance
        [DllImport("kernel32.dll")]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleFont(IntPtr hOut, uint dwFontNum);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, ushort wAttributes);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        const int STD_OUTPUT_HANDLE = -11;
        const uint FONT_NUM = 6;

        static void Main(string[] args)
        {
            // Set the console font to emulate 80s style (Raster Font)
            SetConsoleFont(GetStdHandle(STD_OUTPUT_HANDLE), FONT_NUM);

            // Set the console window size to a retro-friendly resolution
            Console.SetWindowSize(80, 25);

            // Hide the blinking cursor for a more authentic appearance
            Console.CursorVisible = false;

            // Change the console text color to neon green on black background
            SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 2);


            // Display the 3-second loading ASCII art
            DisplayLoadingScreen();

            Console.Write("Loading System : ");
            DisplayLoadingAnimation();
            
            Factory factory = new Factory();
            Admin admin = Admin.Instance;

            if (!admin.Login())
            {
                Console.WriteLine("authintcation failed, Exiting the program.");
                return;
            }

            bool exitProgram = false;
            bool firstLoad = true;

            while (!exitProgram)
            {
                if (firstLoad)
                {
                    Console.Write("Loading Data :");
                    DisplayLoadingAnimation();
                    firstLoad= false;
                 factory.LoadData();
                    Console.WriteLine("\n");
                }

                Console.WriteLine("█▓▒▒░░░main menu░░░▒▒▓█");
                Console.WriteLine("1. Manage Products\n");
                Console.WriteLine("2. Manage Employees\n");
                Console.WriteLine("3. Show Products List\n");
                Console.WriteLine("4. Show Employees List\n");
                Console.WriteLine("5. Save Data\n");
                Console.WriteLine("6. Show Supervisers List\n");
                Console.WriteLine("7. Exit Program\n");
                Console.Write("Enter your choice: \n");
                Console.WriteLine("\n");
                Console.WriteLine("█▓▓█▓█▓█▓█▓█▓█▓█▓▓█▓█▓█▓█");

                string choice = Console.ReadLine();
                Console.WriteLine("\n");

                switch (choice)
                {
                    case "1":
                        admin.ManageProducts(factory);
                        break;
                    case "2":
                        admin.ManageEmployees(factory);
                        break;
                    case "3":
                        ShowProductsList(factory);
                        break;
                    case "4":
                        ShowEmployeesList(factory);
                        break;
                    case "5":
                        admin.SaveData(factory);
                        break;
                    case "6":
                        DisplaySupervisorsList(factory);
                        break;
                    case "7":
                        exitProgram = true;
                        break;
                    default:

                        DisplayErrorMSg("Invalid choice. Please try again.");

                        break;
                }
            }
        }

        private static void ShowProductsList(Factory factory)
        {
            Console.WriteLine("Products List:");
            foreach (Product product in factory.GetProducts())
            {
                Console.WriteLine(product);
                Console.WriteLine("\n");
            }
        }
        public  static void DisplaySupervisorsList( Factory factory)
        {
            Console.WriteLine("Supervisor List:");
            foreach (Employee employee in factory.GetEmployees())
            {
                if (employee is Supervisor supervisor)
                {
                    Console.WriteLine(supervisor.ToString());
                    Console.WriteLine("\n");

                }
            }
        }

        private static void ShowEmployeesList(Factory factory)
        {
            Console.WriteLine("Employees List:");
            foreach (Employee employee in factory.GetEmployees())
            {
                Console.WriteLine(employee.ToString());
                Console.WriteLine("\n");

            }
        }

        static void DisplayLoadingAnimation()
        {
            int numDots = 10;
            for (int i = 0; i < numDots; i++)
            {
                Console.Write("▌║▌");
                Thread.Sleep(500); // 500 milliseconds delay between each dot
            }
        }


        private static void DisplayLoadingScreen()
        {
            string loadingArt = @"
 ▌║█║▌│║▌│║▌║▌█║factory System ▌│║▌║▌│║║▌█║▌║█                      
";

            Console.WriteLine(loadingArt);
            Thread.Sleep(3000); // Wait for 3 seconds (3000 milliseconds) before proceeding
        }

       public static void DisplayErrorMSg(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 2);
            Console.WriteLine("\n");
        }
    }
}
