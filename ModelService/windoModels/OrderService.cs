using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class OrderService
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Status")]
        public int StatusOrderId { get; set; }
        public int Makat { get; set; }
        public DateTime? CreationDate { get; set; }
        public float? Price { get; set; }

        public virtual Client Client { get; set; }
        public virtual OrderStatuses Status { get; set; }
        public virtual CatalogService CatalogService { get; set; }

        public virtual AdvertismentServiceOrder AdvertismentServiceOrder { get; set; }
    }
}
