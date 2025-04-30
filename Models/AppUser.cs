using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VialambreAppTest1.Models
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public string? UserId { get; set; }

        public string? Document { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set; }

        public string? RolName { get; set; }

        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
    }
}
