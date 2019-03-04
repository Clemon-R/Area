using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Models
{
    public class Trigger
    {
        [Key]
        public int Id { get; set; }
        public DateTime LastVerificationDate { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Account Owner { get; set; }
    }
}
