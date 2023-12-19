using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class Client
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string BusinessName { get; set; }
        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        [ForeignKey("ClientType")]
        public int ClientTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
        public string UserEmail { get; set; }

        public virtual ClientType ClientType { get; set; }
    }
}
