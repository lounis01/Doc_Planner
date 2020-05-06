using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Doc_Planner.Models

{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayName("Heure du RDV")]
        public DateTime HDebutRdv { get; set; }


        [Column(TypeName = "nvarchar(250)")]
        public string Nom { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Prénom")]
        public string Prenom { get; set; }
        [Column(TypeName = "bit")]
        public bool Sexe { get; set; }
        [Column(TypeName = "datetime2")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date de naissance")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Diabète")]
        public string Diabete { get; set; } //==> ne laisser que les choix existants aux utilisateurs

        [Column(TypeName = "float")]
        public float Poids { get; set; }
        [Column(TypeName = "float")]

        public float Taille { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Examen")]
        public string ExamenType { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string NISS { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Téléphone")]
        public string Telephone { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "float")]
        public float BMI { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayName("Heure fin du RDV")]
        public DateTime HFinRdv { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Hôpital")]
        public string HopitalDeRef  { get; set; }

        [DisplayName("Urgent")]
        [Column(TypeName = "bit")]
        public bool Emergency { get; set; }

    }
}
