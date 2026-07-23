using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }=string.Empty;
        public required string LastName { get; set; }= string.Empty;
        public required string Email { get; set; }= string.Empty;
        public string Password {  get; set; }= string.Empty;

    }
}
