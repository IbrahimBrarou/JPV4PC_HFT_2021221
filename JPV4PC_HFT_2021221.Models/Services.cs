using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JPV4PC_HFT_2021221.Models
{
    [Table("services")]
    public class Services
    {
        public Services()
        {
            this.ConnectorReservationsServices = new HashSet<ConnectorReservationsServices>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        public virtual ICollection<ConnectorReservationsServices> ConnectorReservationsServices { get; }

        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.Rating,2}/10 {this.Price,7} MAD \t{this.Name,-1}";
        }
    }
}
