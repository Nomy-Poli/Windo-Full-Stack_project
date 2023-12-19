using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelService.windoModels
{
    public class MessagesTo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }
        [ForeignKey("BuisnessTo")]
        public int BusinessIdTo { get; set; }
        public int BusinessIdFrom { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Buisness BuisnessTo { get; set; }
        public virtual Message Message { get; set; }
    }
}
