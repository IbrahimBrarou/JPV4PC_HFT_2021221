using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JPV4PC_HFT_2021221.Models
{
    [Table("artist")]
    public class Artists
    {
        public Artists()
        {
            this.Reservations = new HashSet<Reservations>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(50)]
        [Required]
        public string Name { get; set; }


        [MaxLength(100)]
        [Required]
        public string Category { get; set; }


        [Required]
        public int Duration { get; set; }



        [Required]
        public int Price { get; set; }
        public override string ToString()
        {
            return $"\n{this.Id,3} |  {this.Duration} hours {this.Price,10} MAD {this.Category,10}\t {this.Name,-1}";
        }
        [NotMapped]
        public virtual ICollection<Reservations> Reservations { get; }
    }
}
