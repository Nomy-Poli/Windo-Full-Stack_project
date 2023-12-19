using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class CatalogService
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Makat { get; set; }

        [ForeignKey("ServiceType")]
        public int ServiceTypeId { get; set; }
        public string Description { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
