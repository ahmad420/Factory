using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Console.SetWindowSize(60, 25);

            // Hide the blinking cursor for a more authentic appearance
            Console.CursorVisible = false;

            // Change the console text color to neon green on black background
            SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 2);


            // Display the 3-second loading ASCII art
            DisplayLoadingScreen(3000);

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
                    Console.Clear();

                    DisplayLoadingScreen(1000);

                }

                Console.WriteLine("█▓▒▒░░░main menu░░░▒▒▓█");
                Console.WriteLine("1. Manage Products\n");
                Console.WriteLine("2. Manage Employees\n");
                Console.WriteLine("3. Show Products List\n");
                Console.WriteLine("4. Show Employees List\n");
                Console.WriteLine("5. Sell Products\n");
                Console.WriteLine("6. Show Supervisers List\n");
                Console.WriteLine("7. Show Total Revenue\n");
                Console.WriteLine("8. Save Data\n");
                Console.WriteLine("9. Exit Program\n");
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
                        Console.Write("Enter the barcode of the product to sell: ");
                        string productBarcode = Console.ReadLine();
                        Console.Write("Enter the amount to sell: ");
                        if (int.TryParse(Console.ReadLine(), out int amountToSell))
                        {
                           SellProduct(factory, productBarcode, amountToSell);
                        }
                        else
                        {
                            DisplayErrorMSg("Invalid amount format. Please enter a valid integer.");
                        }
                        break;
                    case "6":
                        DisplaySupervisorsList(factory);
                        break;
                    case "7":
                        DisplayTotalRevenue(factory);
                        break;
                    case "8":
                        admin.SaveData(factory);
                        break;
                    case "9":
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
            int index = 1;

            Console.WriteLine("Products List:");
            Console.WriteLine(@"           Name              Barcode                 Count          Date              Price");
            foreach (Product product in factory.GetProducts())
            {
                Console.Write($"#{index})");
                Console.WriteLine(product);
                Console.WriteLine("\n");
                Thread.Sleep(1000);
                index++;


            }
        }
        public  static void DisplaySupervisorsList( Factory factory)
        {

            Console.WriteLine("Supervisor List:");
            Console.WriteLine(@"#)       ID              Name             Phone Number           Salary              Joining Date                  Department  ");
                int index = 1;

            foreach (Employee employee in factory.GetEmployees())
            {

                if (employee is Supervisor supervisor)
                {
                    Console.Write($"#{index})");
                    Console.WriteLine(supervisor.ToString());
                    Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    index++;
                }
            }
        }

        private static void ShowEmployeesList(Factory factory)
        {
            int index = 1;
            Console.WriteLine("Employees List:");
            Console.WriteLine(@"#)       ID              Name             Phone Number           Salary              Joining Date    ");
            foreach (Employee employee in factory.GetEmployees())
            {
                Console.Write($"#{index})");
                Console.WriteLine(employee.ToString());
                Console.WriteLine("\n");
                Thread.Sleep(1000);
                index++;


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
            Console.Write("100%");
            Thread.Sleep(500);
            Console.Clear();
            DisplayLoadingScreen(1000);

        }


        private static void DisplayLoadingScreen(int milisec)
        {
            string loadingArt = @"
 ▌║█║▌│║▌│║▌║▌█║factory System ▌│║▌║▌│║║▌█║▌║█                      
";

            Console.WriteLine(loadingArt);
            Thread.Sleep(milisec); 
        }

       public static void DisplayErrorMSg(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 2);
            Console.WriteLine("\n");
        }


        public static void SellProduct(Factory factory, string productBarcode, int amount)
        {
            Product productToSell = factory.GetProducts().Find(p => p.Barcode == productBarcode);

            if (productToSell == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount. Amount must be greater than 0.");
                return;
            }

            if (amount > productToSell.Count)
            {
                Console.WriteLine("Insufficient stock. Cannot sell more than available.");
                return;
            }

            decimal totalPrice = productToSell.Price * amount;
            productToSell.Count -= amount;
           factory.UpdateTotalRevenue (totalPrice);

            Console.WriteLine($"Successfully sold {amount} units of {productToSell.Name}. Total Revenue: {factory.TotalRevenue:C}");
        }

        static void DisplayTotalRevenue(Factory factory)
        {
            Console.WriteLine($"Total Revenue: ${factory.TotalRevenue}");
        }
    }
}
