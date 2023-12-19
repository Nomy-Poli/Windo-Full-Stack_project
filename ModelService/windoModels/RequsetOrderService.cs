using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class RequestOrderService
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Makat { get; set; }

        [StringLength(50)]
        public string BusinessName { get; set; }
        [StringLength(50)]
        public string ContactName { get; set; }
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [StringLength(10)]
        public string Phone2 { get; set; }

        [StringLength(500)]
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ServiceDate { get; set; }
        public bool? ReturningCustomer { get; set; }
        public string LinkForSite { get; set; }
        public int RequestStatus { get; set; }//Enum
    }
}
