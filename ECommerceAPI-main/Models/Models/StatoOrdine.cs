using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("StatoOrdine")]
    public partial class StatoOrdine
    {
        public StatoOrdine()
        {
            Ordines = new HashSet<Ordine>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(10)]
        public string Descrizione { get; set; }

        [InverseProperty("IDStatoNavigation")]
        public virtual ICollection<Ordine> Ordines { get; set; }
    }
}
