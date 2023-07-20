using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Project
{
    internal abstract class Employee
    {
        private static int employeeCount = 0;

        public string Id { get; private set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }

        public Employee(string id, string name, string phoneNumber, decimal salary, DateTime joiningDate)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Employee ID cannot be empty.");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Employee name cannot be empty.");
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Employee phone number cannot be empty.");
            if (salary < 0)
                throw new ArgumentException("Employee salary cannot be negative.");
            if (joiningDate > DateTime.Now)
                throw new ArgumentException("Joining date cannot be in the future.");

            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Salary = salary;
            JoiningDate = joiningDate;

            IncrementEmployeeCount();
        }

        private static void IncrementEmployeeCount()
        {
            employeeCount++;
        }

        public abstract void PerformDuties();

        public static void DecrementEmployeeCount()
        {
            employeeCount--;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Phone Number: {PhoneNumber}, Salary: {Salary}, Joining Date: {JoiningDate}";
        }
    }
}
