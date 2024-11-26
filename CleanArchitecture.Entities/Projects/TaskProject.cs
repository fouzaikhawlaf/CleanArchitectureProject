﻿using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Projects
{
   // Entities/Task.cs
      public class TaskProject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TaskStatus Status { get; set; }

        // Clé étrangère vers Employee
        [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; } = string.Empty; // Assurez-vous que ceci est bien un string
        public Employee? Employee { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public string AssignedToId { get; set; } = string.Empty;
        public Employee? AssignedTo { get; set; }

        public bool IsOnSchedule() => EndDate.HasValue && EndDate >= DateTime.Now;
    }

}
