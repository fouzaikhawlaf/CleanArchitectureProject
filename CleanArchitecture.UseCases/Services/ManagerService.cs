using CleanArchitecture.Entities.Notifications;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class ManagerService : IManagerService
    {
        private readonly UserManager<Employee> _userManager;

        public ManagerService(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        // Get users that belong to the manager's department
        public async Task<List<Employee>>GetUserInDepartmentAsync(string managerEmail)
        {
            var manager = await _userManager.FindByEmailAsync(managerEmail);
            if (manager == null)
            {
                throw new System.Exception("Manager not found.");
            }

            // Find users in the same department as the manager
            return _userManager.Users
                .Where(u => u.Department == manager.Department && u.Id != manager.Id) // Exclude the manager themselves
                .ToList();
        }

        // Assign task to employee and send notification
        public async Task AssignTaskToEmployeeAsync(string managerEmail, string employeeEmail, TaskProject task)
        {
            var manager = await _userManager.FindByEmailAsync(managerEmail);
            var employee = await _userManager.FindByEmailAsync(employeeEmail);

            if (employee == null || employee.Department != manager.Department)
            {
                throw new System.Exception("Employee not found or not in the same department.");
            }

            // Assign task
            employee.AssignedTasks.Add(task);
            await _userManager.UpdateAsync(employee);

            // Send in-app notification
            var notification = new Notification
            {
                UserId = employee.Id,
                Message = $"A new task has been assigned to you: {task.Title}",
                CreatedAt = DateTime.UtcNow
            };
            employee.Notifications.Add(notification);
            await _userManager.UpdateAsync(employee);

            // Optionally: Send email notification
            await SendEmailNotificationAsync(employee.Email, "New Task Assigned", notification.Message);
        }

        // Helper to send email notification
        private async Task SendEmailNotificationAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeKit.MimeMessage();
            emailMessage.From.Add(new MimeKit.MailboxAddress("Admin", "admin@yourapp.com"));
            emailMessage.To.Add(new MimeKit.MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new MimeKit.TextPart("plain") { Text = message };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.your-email.com", 587, false);
                await client.AuthenticateAsync("your-email@domain.com", "your-email-password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        // Update task and notify employee
        public async Task UpdateTaskForEmployeeAsync(string managerEmail, string employeeEmail, TaskProject updatedTask)
        {
            var employee = await _userManager.FindByEmailAsync(employeeEmail);

            if (employee == null || employee.Department != managerEmail)
            {
                throw new System.Exception("Employee not found or not in the same department.");
            }

            // Find the task
            var task = employee.AssignedTasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;
                await _userManager.UpdateAsync(employee);

                // Send in-app notification
                var notification = new Notification
                {
                    UserId = employee.Id,
                    Message = $"A task has been updated: {updatedTask.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                employee.Notifications.Add(notification);
                await _userManager.UpdateAsync(employee);

                // Optionally: Send email notification
                await SendEmailNotificationAsync(employee.Email, "Task Updated", notification.Message);
            }
        }
        // Get tasks that belong to users in the manager's department
        public async Task<List<TaskProject>> GetTasksForDepartmentAsync(string managerEmail)
        {
            var manager = await _userManager.FindByEmailAsync(managerEmail);
            if (manager == null)
            {
                throw new System.Exception("Manager not found.");
            }

            // Get tasks assigned to all users in the manager's department
            var usersInDepartment = _userManager.Users
                .Where(u => u.Department == manager.Department)
                .SelectMany(u => u.AssignedTasks)
                .ToList();

            return usersInDepartment;
        }

        
    }
}
