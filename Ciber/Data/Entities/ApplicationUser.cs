using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ciber.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { set; get; }

        [MaxLength(255)]
        public string Address { set; get; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { set; get; }

        public virtual ICollection<Order> Orders { get; set; }
        public void SetInfo(string fullName, string address,DateTime? birthDate)
        {
            this.FullName = fullName;
            this.Address = address;
            this.Birthday = birthDate;
        }
    }
}
