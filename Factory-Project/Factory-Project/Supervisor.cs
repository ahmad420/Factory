﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Project
{
    internal class Supervisor : Employee
    {
        public string Department { get; set; }

    

        public Supervisor(string id, string name, string phoneNumber, decimal salary, DateTime joiningDate, string department)
            : base(id, name, phoneNumber, salary, joiningDate)
        {
            Department = department;
        }
        public override string ToString()
        {
            return base.ToString()+$", Department : {Department}.";
        }

        public override void PerformDuties()
        {
            Console.WriteLine("Supervisor duties: Manage and supervise the team.");
        }
    }
}
