using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.windoModels
{
    public class BuisnessStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Buisness")]
        public int buisnessId { get; set; }

        [ForeignKey("Status")]
        public int statusId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate  { get; set; }

        public virtual Buisness Buisness { get; set; }
        public virtual Status Status { get; set; }
    }
}
