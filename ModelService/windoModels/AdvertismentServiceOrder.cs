using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class AdvertismentServiceOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("orderService")]
        public int OrderServiceId { get; set; }
        public int Makat { get; set; }
        public DateTime? adFromDate { get; set; }
        public DateTime? adTillDate { get; set; }
        public Guid? PicGuid { get; set; }
        public string LinkToSite { get; set; }
        public virtual OrderService OrderService { get; set; }
    }
}
