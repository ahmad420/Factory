using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factory_Project
{
    internal class Admin
    {
        private static Admin instance = null;
        private static readonly object lockObject = new object();

        private string username;
        private string password;
        private string recoveryCode;

        public static Admin Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Admin();
                    }
                    return instance;
                }
            }
        }

        private Admin()
        {
            username = "admin";
            password = "password";
            recoveryCode = "12345678";
        }

        public bool Login()
        {
            int tryCount = 0;
            const int maxAttempts = 3;

            while (tryCount < maxAttempts)
            {
                Console.WriteLine("\n▀▄▀▄▀▄admin Login▄▀▄▀▄▀");
                Console.Write("Username: ");
                string enteredUsername = Console.ReadLine();
                Console.Write("Password: ");
                string enteredPassword = Console.ReadLine();

                if (ValidateCredentials(enteredUsername, enteredPassword))
                {
                    Console.WriteLine("Admin login successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Admin login failed.");
                    tryCount++;
                }
            }

            if (ChangePassword())
            {
                Console.WriteLine("Password changed successfully. Please log in with the new credentials.");
                Thread.Sleep(1000);
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("Password change failed. Please contact support for assistance.");
         
            }

            return false;
        }

        private bool ValidateCredentials(string enteredUsername, string enteredPassword)
        {
            // Perform additional validation if required (e.g., check against a database of admins).
            return !string.IsNullOrEmpty(enteredUsername) &&
                   !string.IsNullOrEmpty(enteredPassword) &&
                   enteredUsername == username &&
                   enteredPassword == password;
        }

        private bool ChangePassword()
        {
            Console.Write("Enter recovery code: ");
            string enteredRecoveryCode = Console.ReadLine();

            if (enteredRecoveryCode == recoveryCode)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                Console.Write("Confirm new password: ");
                string confirmPassword = Console.ReadLine();

                if (newPassword == confirmPassword)
                {
                    password = newPassword;
                    Console.WriteLine("Password changed successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Password and confirm password do not match.");
                }
            }
            else
            {
                Console.WriteLine("Invalid recovery code.");
            }
            return false;
        }
        public void ManageProducts(Factory factory)
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Get Product with  min price");
            Console.WriteLine("4. Get Product with  MAX price ");
            Console.WriteLine("5. back to main menu");
            Console.WriteLine("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter product name: ");
                    string productName = Console.ReadLine();
                    Console.Write("Enter product barcode: ");
                    string productBarcode = Console.ReadLine();
                    Console.Write("Enter product count: ");
                    int productCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter product date added (yyyy-MM-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime productDateAdded))
                    {
                        Console.Write("Enter product price: ");
                        decimal productPrice = decimal.Parse(Console.ReadLine());

                        Product product = new Product(productName, productBarcode, productCount, productDateAdded, productPrice);
                        factory.AddProduct(product);
                        Console.WriteLine("Product added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    break;
                case "2":
                    Console.Write("Enter product barcode: ");
                    string barcodeToRemove = Console.ReadLine();
                    if (factory.RemoveProductByBarcode(barcodeToRemove))
                        Console.WriteLine("Product removed successfully.");
                    else
                        Console.WriteLine("Product not found.");
                    break;
                    case "3":
                    Console.WriteLine($"minmum price product is : {factory.GetMinPriceProduct()}");
                    break;
                    case "4":
                    Console.WriteLine($"maximum price product is :{factory.GetMaxPriceProduct()}");
                    break;
                    case "5":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }


        public void ManageEmployees(Factory factory)
        {
            Console.WriteLine("1. Add Worker");
            Console.WriteLine("2. Promote Worker");
            Console.WriteLine("3. Remove Employee");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter worker ID: ");
                    string workerId = Console.ReadLine();
                    Console.Write("Enter worker name: ");
                    string workerName = Console.ReadLine();
                    Console.Write("Enter worker phone number: ");
                    string workerPhoneNumber = Console.ReadLine();
                    Console.Write("Enter worker salary: ");
                    decimal workerSalary = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter joining date (yyyy-MM-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime workerJoiningDate))
                    {
                        Console.Write("Enter worker department: ");
                        string workerDepartment = Console.ReadLine();
                        factory.AddWorker(workerId, workerName, workerPhoneNumber, workerSalary, workerJoiningDate, workerDepartment);
                        Console.WriteLine("Worker added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    break;
                case "2":
                    Console.Write("Enter worker ID: ");
                    string workerToPromoteId = Console.ReadLine();
                    if (factory.PromoteWorker(workerToPromoteId))
                        Console.WriteLine("Worker promoted successfully.");
                    else
                        Console.WriteLine("Worker not found.");
                    break;
                case "3":
                    Console.Write("Enter employee name: ");
                    string employeeToRemove = Console.ReadLine();
                    if (factory.RemoveEmployee(employeeToRemove))
                        Console.WriteLine("Employee removed successfully.");
                    else
                        Console.WriteLine("Employee not found.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public void SaveData(Factory factory)
        {
            factory.SaveData();
        }
    }
}
