using Microsoft.AspNetCore.Identity;
using ReportSystem.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportSystem.Domain.Entities.UserEntity
{
    public class User: IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Birthdate { get; set; }
        public State State { get; set; }
        [ForeignKey("Creator")]
        public int? CreatedBy { set; get; }
        public User? Creator { get; set; }
        public DateTime CreatedOn { set; get; }
        [ForeignKey("Updater")]
        public int? UpdatedBy { set; get; }
        public User? Updater { get; set; }

        public DateTime? UpdatedOn { set; get; }
    }
}
