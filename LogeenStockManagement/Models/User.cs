using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogeenStockManagement.Models
{
    public class User: IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
        //public int RoleId { get; set; }


        [ForeignKey("EmployeeId")]
        public Employee employee { get; set; }
    }
}
