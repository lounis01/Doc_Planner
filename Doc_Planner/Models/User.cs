using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Doc_Planner.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
