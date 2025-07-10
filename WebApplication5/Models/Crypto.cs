using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Crypto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cid { get; set; }
        public string Name { get; set; }
        
        public DateTime Reviewdate { get; set; }
    }
}
