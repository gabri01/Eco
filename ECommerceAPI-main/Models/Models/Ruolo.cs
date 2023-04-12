﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("Ruolo")]
    public partial class Ruolo
    {
        public Ruolo()
        {
            Utentes = new HashSet<Utente>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }

        [InverseProperty("IDRuoloNavigation")]
        public virtual ICollection<Utente> Utentes { get; set; }
    }
}