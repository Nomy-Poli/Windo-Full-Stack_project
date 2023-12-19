//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    public class BuisnessServices
//    {
//        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int buisnessServicesId { get; set; }

//        [ForeignKey("Services")]
//        public int servicesId { get; set; }

//        [ForeignKey("Buisness")]
//        public int buisnessId { get; set; }

//        [ForeignKey("ServiceType")]
//        public int serviceTypeId { get; set; }//give or get

//        public virtual ServiceType ServiceType { get; set; }
//        public virtual Services Services { get; set; }
//        public virtual Buisness Buisness { get; set; }
//    }
//}
