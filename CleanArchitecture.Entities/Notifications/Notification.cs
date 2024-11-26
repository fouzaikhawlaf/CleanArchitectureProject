using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Notifications
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;// ID of the user receiving the notification
        public string Message { get; set; } = string.Empty; // Notification message
        public DateTime CreatedAt { get; set; } // When the notification was created
        public bool IsRead { get; set; } = false; // Whether the notification has been read or not

        // Reference to the user (optional for relations)
        public Employee? User { get; set; }
    }
}
