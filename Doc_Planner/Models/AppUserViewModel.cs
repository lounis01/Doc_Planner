using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Doc_Planner.Models
{
    public class AppUserViewModel
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }

        [Required]
        [DisplayName("Nom d'utilisateur")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password  { get; set; }

        [Required]
        [DisplayName("Hopital où vous travaillez")]
        public string HopitRef { get; set; }

    }
}
