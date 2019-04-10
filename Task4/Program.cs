using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
 
        static void Main(string[] args)
        {
            Company company = new Company() { CompanyName = "Epam"};

            Console.WriteLine("Employee who has two and more projects");
            company.GetEmpMoreTwoProjects().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("Projects after certain date in chronological sequence");
            company.GetProjectsAfterDate(new DateTime(2018, 01, 01)).ForEach(a => Console.WriteLine(a));

            Console.WriteLine("Projects less than year and employee younger 30");
            company.GetProjectsLessYear().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("The oldest employee who works on one project and the project started in current year");
            company.GetOldestEmp().ForEach(a => Console.WriteLine(a.LastName));
        }

    }

    class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Project> Projects { get; set; }

        public override string ToString() => $"{LastName } | {Age} | Projects number - {Projects.Count}";

        public void BindProject(Project reference)
        {
            Projects.Add(reference);
            reference.BindEmployee(this);
        }

    }

    class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public List<Employee> Employees { get; set; }

        public override string ToString() => $"{ProjectName } | {StartDate.ToString("d/M/yyyy")} | Employees number - {Employees.Count}";

        public void BindEmployee(Employee reference)
        {
            Employees.Add(reference);
        }
    }


    class Company
    {
        public string CompanyName { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Project> Projects { get; set; }


        public Company()
        {

            Employee employee1 = new Employee() { EmployeeID = 1, LastName = "ALastName1", Age = 25, Projects = new List<Project>() };
            Employee employee2 = new Employee() { EmployeeID = 2, LastName = "BLastName2", Age = 35, Projects = new List<Project>() };
            Employee employee3 = new Employee() { EmployeeID = 3, LastName = "CLastName3", Age = 18, Projects = new List<Project>() };
            Employee employee4 = new Employee() { EmployeeID = 4, LastName = "DLastName4", Age = 38, Projects = new List<Project>() };

            Project project1 = new Project() { ProjectID = 1, ProjectName = "Aproject1", StartDate = new DateTime(2019, 01, 21), Employees = new List<Employee>() };
            Project project2 = new Project() { ProjectID = 2, ProjectName = "Bproject2", StartDate = new DateTime(2018, 03, 05), Employees = new List<Employee>() };
            Project project3 = new Project() { ProjectID = 3, ProjectName = "Cproject3", StartDate = new DateTime(2017, 11, 18), Employees = new List<Employee>() };
            Project project4 = new Project() { ProjectID = 4, ProjectName = "Dproject4", StartDate = new DateTime(2019, 04, 08), Employees = new List<Employee>() };
            

            List<Employee> employees = new List<Employee>() { employee1, employee2, employee3, employee4 };
            List<Project> projects = new List<Project>() { project1, project2, project3, project4 };

            Employees = employees;
            Projects = projects;


            employee1.BindProject(project1);

            employee2.BindProject(project2);

            employee3.BindProject(project1);
            employee3.BindProject(project3);

            employee4.BindProject(project4);
        }

        public List<Employee> GetEmpMoreTwoProjects()
        {
            return Employees.OrderBy(a => a.LastName).Where(a => a.Projects.Count >= 2).ToList();
        }

        public List<Project> GetProjectsAfterDate(DateTime date)
        {
            return Projects.OrderBy(a => a.StartDate).Where(a => a.StartDate > date).ToList();
        }

        public List<Project> GetProjectsLessYear()
        {
            return Projects.Where(a => (DateTime.Now - a.StartDate).Days <= 365 ).Where(a => a.Employees.All(b => b.Age < 35)).ToList();
        }

        public List<Employee> GetOldestEmp()
        {
            int maxAge = Employees.Max(a => a.Age);

            return Employees.Where(a => a.Age == maxAge && a.Projects.Count == 1).Where(b => b.Projects.All(c => c.StartDate.Year == DateTime.Now.Year)).ToList();
        }

    }
}

