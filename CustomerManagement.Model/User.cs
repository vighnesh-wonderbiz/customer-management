using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CustomerManagement.Model
{

    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
        
        [MinLength(10,ErrorMessage="Invalid Phone")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Password must be 8 characters long")]
        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        public int? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; } = 1;
        public virtual Gender Gender {get; set;}

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role {get; set;}

        public virtual ICollection<Order> Orders {get;set;} 
    }
}
