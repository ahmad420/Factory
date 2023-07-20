using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Factory_Project
{

    class Factory
    {
        private List<Product> products;
        private List<Employee> employees;
        public Factory()
        {
            products = new List<Product>();
            employees = new List<Employee>();
            GenerateData();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null.");

            products.Add(product);
        }


        public bool RemoveProductByBarcode(string productBarcode)
        {
            if (string.IsNullOrEmpty(productBarcode))
                throw new ArgumentException("Product barcode cannot be empty.");

            Product productToRemove = products.Find(p => p.Barcode == productBarcode);
            if (productToRemove != null)
            {
                return products.Remove(productToRemove);
            }
            return false;
        }


        public void AddWorker(string id, string name, string phoneNumber, decimal salary, DateTime joiningDate, string department)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Worker ID cannot be empty.");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Worker name cannot be empty.");
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Worker phone number cannot be empty.");
            if (salary < 0)
                throw new ArgumentException("Worker salary cannot be negative.");
            if (joiningDate > DateTime.Now)
                throw new ArgumentException("Joining date cannot be in the future.");

            Worker worker = new Worker(id, name, phoneNumber, salary, joiningDate, department);
            employees.Add(worker);
            Console.WriteLine("Worker added successfully.");
        }

        public bool RemoveEmployee(string employeeName)
        {
            if (string.IsNullOrEmpty(employeeName))
                throw new ArgumentException("Employee name cannot be empty.");

            Employee employeeToRemove = employees.Find(e => e.Name == employeeName);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                return true;
            }
            return false;
        }
        public bool PromoteWorker(string workerId)
        {
            if (string.IsNullOrEmpty(workerId))
                throw new ArgumentException("Worker ID cannot be empty.");

            Worker workerToPromote = employees.Find(e => e.Id == workerId) as Worker;
            if (workerToPromote != null)
            {
                Supervisor supervisor = new Supervisor(workerToPromote.Id, workerToPromote.Name, workerToPromote.PhoneNumber, workerToPromote.Salary, workerToPromote.JoiningDate, workerToPromote.Department);
                employees.Remove(workerToPromote);
                employees.Add(supervisor);
                Console.WriteLine("Worker promoted to Supervisor successfully.");
                return true;
            }
            else
            {
                DisplayErrorMSg("Worker not found.");
                return false;
            }
        }
        public void ShowProducts()
        {
            Console.WriteLine("Product List:");
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }
        public void ShowEmployees()
        {
            Console.WriteLine("Employee List:");
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }
        public List<Product> GetProducts()
        {
            return products;
        }
        public List<Employee> GetEmployees()
        {
            return employees;
        }
   

        public void GenerateData()
        {
            // Check if the data file exists and is not empty
            if (File.Exists("C:\\Users\\ahmad\\Desktop\\opp-factory-project\\Factory-Project\\Factory-Project\\bin\\Debug\\factory_data.txt"))
            {
                string fileContent = File.ReadAllText("C:\\Users\\ahmad\\Desktop\\opp-factory-project\\Factory-Project\\Factory-Project\\bin\\Debug\\factory_data.txt");
                if (!string.IsNullOrEmpty(fileContent))
                {
                    return;
                }
            }


            // Create 5 Workers manually with real names
            Worker worker1 = new Worker("EMP001", "John Smith", "1234567891", 1500, DateTime.Now.AddDays(-30), "Production");
            Worker worker2 = new Worker("EMP002", "Emily Johnson", "1234567892", 1600, DateTime.Now.AddDays(-45), "Production");
            Worker worker3 = new Worker("EMP003", "Michael Williams", "1234567893", 1400, DateTime.Now.AddDays(-60), "Maintenance");
            Worker worker4 = new Worker("EMP004", "Olivia Brown", "1234567894", 1700, DateTime.Now.AddDays(-20), "Maintenance");
            Worker worker5 = new Worker("EMP005", "James Davis", "1234567895", 1550, DateTime.Now.AddDays(-15), "Production");

            // Create 5 more Workers manually with real names
            Worker worker6 = new Worker("EMP006", "Sophia Wilson", "1234567896", 1450, DateTime.Now.AddDays(-10), "Maintenance");
            Worker worker7 = new Worker("EMP007", "Liam Anderson", "1234567897", 1550, DateTime.Now.AddDays(-25), "Production");
            Worker worker8 = new Worker("EMP008", "Isabella Martinez", "1234567898", 1650, DateTime.Now.AddDays(-35), "Maintenance");
            Worker worker9 = new Worker("EMP009", "Mason Taylor", "1234567899", 1600, DateTime.Now.AddDays(-50), "Production");
            Worker worker10 = new Worker("EMP010", "Ava Clark", "1234567890", 1500, DateTime.Now.AddDays(-40), "Maintenance");

            // Create 2 Supervisors manually with real names
            Supervisor supervisor1 = new Supervisor("EMP011", "Oliver Rodriguez", "1234567811", 1800, DateTime.Now.AddDays(-15), "Production");
            Supervisor supervisor2 = new Supervisor("EMP012", "Charlotte Lee", "1234567812", 1900, DateTime.Now.AddDays(-20), "Maintenance");

            // Create 10 real products manually
            Product product1 = new Product("iPhone 12", "1234567891", 5, DateTime.Now.AddDays(-10));
            Product product2 = new Product("Samsung Galaxy S21", "1234567892", 8, DateTime.Now.AddDays(-155));
            Product product3 = new Product("Sony PlayStation 5", "1234567893", 3, DateTime.Now.AddDays(-250));
            Product product4 = new Product("Nike Air Max 270", "1234567894", 10, DateTime.Now.AddDays(-55));
            Product product5 = new Product("Canon EOS R5", "1234567895", 2, DateTime.Now.AddDays(-7));
            Product product6 = new Product("Apple AirPods Pro", "1234567896", 6, DateTime.Now.AddDays(-152));
            Product product7 = new Product("Dell XPS 15", "1234567897", 4, DateTime.Now.AddDays(-58));
            Product product8 = new Product("Nintendo Switch", "1234567898", 7, DateTime.Now.AddDays(-43));
            Product product9 = new Product("Adidas Ultraboost", "1234567899", 9, DateTime.Now.AddDays(-676));
            Product product10 = new Product("LG OLED CX Series", "1234567810", 1, DateTime.Now.AddDays(-9));

            // Add the created workers and supervisors to the employees list
            employees.AddRange(new Employee[]
            {
        worker1, worker2, worker3, worker4, worker5,
        worker6, worker7, worker8, worker9, worker10,
        supervisor1, supervisor2
            });

            // Add the created products to the products list
            products.AddRange(new Product[]
            {
        product1, product2, product3, product4, product5,
        product6, product7, product8, product9, product10
            });
          
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader reader = new StreamReader("C:\\Users\\ahmad\\Desktop\\opp-factory-project\\Factory-Project\\Factory-Project\\bin\\Debug\\factory_data.txt"))
                {
                    string line;
                    bool isReadingProducts = false;
                    bool isReadingEmployees = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "Products:")
                        {
                            isReadingProducts = true;
                            isReadingEmployees = false;
                            continue;
                        }

                        if (line == "Employees:")
                        {
                            isReadingProducts = false;
                            isReadingEmployees = true;
                            continue;
                        }

                        if (isReadingProducts)
                        {
                            // Assuming each line contains a product in the format: Name, Barcode, Count, DateAdded
                            string[] productData = line.Split(',');
                            if (productData.Length == 4)
                            {
                                string productName = productData[0].Trim();
                                string barcode = productData[1].Trim();
                                int count = int.Parse(productData[2].Trim());
                                DateTime dateAdded = DateTime.Parse(productData[3].Trim());

                                // Create a new Product object and add it to the products list
                                Product product = new Product(productName, barcode, count, dateAdded);
                                products.Add(product);
                            }
                           /* else
                            {
                                Console.WriteLine("Invalid product data format.");
                            }*/
                        }

                        if (isReadingEmployees)
                        {
                            // Assuming each line contains an employee in the format: ID: <id>, Name: <name>, Phone Number: <phoneNumber>, Salary: <salary>, Joining Date: <joiningDate>, Type: <type>, Department: <department>
                            string[] employeeData = line.Split(',');
                            if (employeeData.Length >= 6)
                            {
                                string id = employeeData[0].Split(':')[1].Trim();
                                string name = employeeData[1].Split(':')[1].Trim();
                                string phoneNumber = employeeData[2].Split(':')[1].Trim();
                                decimal salary = decimal.Parse(employeeData[3].Split(':')[1].Trim());
                                DateTime joiningDate = DateTime.Parse(employeeData[4].Split(':')[1].Trim());
                                string type = employeeData[5].Split(':')[1].Trim();

                                if (type == "Worker" && employeeData.Length >= 7)
                                {
                                    string department = employeeData[6].Split(':')[1].Trim();
                                    employees.Add(new Worker(id, name, phoneNumber, salary, joiningDate, department));
                                }
                                else if (type == "Supervisor" && employeeData.Length >= 7)
                                {
                                    string department = employeeData[6].Split(':')[1].Trim();
                                    employees.Add(new Supervisor(id, name, phoneNumber, salary, joiningDate, department));
                                }
                                else
                                {
                                    DisplayErrorMSg("Invalid employee data format.");
                                }
                            }
                            else
                            {
                                DisplayErrorMSg("Invalid employee data format.");
                            }
                        }
                    }
                }

                Console.WriteLine("\nData loaded successfully.");
            }
            catch (Exception ex)
            {
                DisplayErrorMSg($"An error occurred while loading the data: {ex.Message}");
            }
        }

        public void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\ahmad\\Desktop\\opp-factory-project\\Factory-Project\\Factory-Project\\bin\\Debug\\factory_data.txt"))
                {
                    writer.WriteLine("Products:");
                    foreach (Product product in products)
                    {
                        // Assuming each product data line follows the format: Name, Barcode, Count, DateAdded
                        writer.WriteLine($"{product.Name}, {product.Barcode}, {product.Count}, {product.DateAdded.ToString("yyyy-MM-dd")}");
                    }

                    writer.WriteLine();
                    writer.WriteLine("Employees:");
                    foreach (Employee employee in employees)
                    {
                        string type = employee.GetType().Name;
                        writer.Write($"ID: {employee.Id}, Name: {employee.Name}, Phone Number: {employee.PhoneNumber}, Salary: {employee.Salary}, Joining Date: {employee.JoiningDate.ToString("yyyy-MM-dd")}, Type: {type}");

                        if (type == "Worker")
                        {
                            Worker worker = employee as Worker;
                            writer.WriteLine($", Department: {worker.Department}");
                        }
                        else if (type == "Supervisor")
                        {
                            Supervisor supervisor = employee as Supervisor;
                            writer.WriteLine($", Department: {supervisor.Department}");
                        }
                    }
                }

                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                DisplayErrorMSg($"An error occurred while saving the data: {ex.Message}");
            }
        }

        public static void DisplayErrorMSg(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
        }


    }
    }

