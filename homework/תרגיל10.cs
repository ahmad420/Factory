using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] arrOfPersons = new Person[6];
            arrOfPersons[0] = new Student("aaa", 22, 80);
            arrOfPersons[1] = new Student("bbb", 22, 80);
            arrOfPersons[2] = new Worker("ccc", 22, 10000);
            arrOfPersons[3] = new Worker("ddd", 22, 10000);
            arrOfPersons[4] = new Manager("eee", 22, 10000, 5000);
            arrOfPersons[5] = new Manager("fff", 22, 10000, 5000);

            foreach (Person p in arrOfPersons)
            {
                Console.WriteLine(p);
                p.Print();
                Console.WriteLine();

                if (p is Worker)
                {
                    Worker w = (Worker)p;
                    w.SetSalary(w.GetSalary() * 1.1);
                }
            }

            Console.WriteLine();
            foreach (Person p in arrOfPersons)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine();
            int maxIndex = -1;
            double maxSalary = 0;
            for (int i=0; i < arrOfPersons.Length; i++)
            {
                if (arrOfPersons[i] is Manager)
                {
                    Manager m = (Manager)arrOfPersons[i];
                    if (m.GetSalary() > maxSalary)
                    {
                        maxSalary = m.GetSalary();
                        maxIndex = i;
                    }
                }
            }

            if (maxIndex > -1)
            {
                Console.WriteLine("Managers with max salary: ");
                for (int i = maxIndex; i < arrOfPersons.Length; i++)
                {
                    if (arrOfPersons[i] is Manager)
                    {
                        Manager m = (Manager)arrOfPersons[i];
                        if (m.GetSalary() == maxSalary)
                        {
                            Console.WriteLine(m.GetName());
                        }
                    }
                }
            }
        }
    }
}
