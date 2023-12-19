using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class Banner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Makat { get; set; }//מק"ט - מספר קטלוג
        public int Height { get; set; }
        public int Width { get; set; }
        public string PageName { get; set; }
        public int? PageID { get; set; }
        public string Title { get; set; }
        public int? PriceInPoints { get; set; }
        public float? Price { get; set; }
        public Guid? DefaultPicGuid { get; set; }
        public string DefaultLink { get; set; }
        public Guid? ExamplePicGuid { get; set; }
        public Guid? FormatPicGuid { get; set; }
        public virtual CatalogService CatalogService { get; set; }
        public virtual AdvertismentServiceOrder AdvertismentServiceOrder { get; set; }

    }
}
