using CleanArchitecture.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class Manager :Employee
    {
        public List<Employee> ManagedEmployees { get; set; } = new List<Employee>();
        public ICollection<Project> ManagedProjects { get; set; } = new List<Project>();
        public List<TaskProject> DepartmentTasks { get; set; } = new List<TaskProject>();

        public int ManagedTeamSize { get; set; }

        public void AssignTaskToEmployee(Employee employee, TaskProject task)
        {
            if (employee.Department == this.Department)
            {
                employee.AssignedTasks.Add(task);
            }
        }

        public List<TaskProject> GetDepartmentTasks()
        {
            return DepartmentTasks;
        }
    }
}
