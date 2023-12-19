//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    public class Services
//    {
//        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int servicesId { get; set; }
//        public string name { get; set; }
//        public string discription { get; set; }

//        //[ForeignKey("Topics")]
//        //public int topicId { get; set; }

//        [ForeignKey("SubTopics")]
//        public int subTopicId { get; set; }

//        //public virtual Topics Topics { get; set; }
//        public virtual SubTopics SubTopics { get; set; }
//        public virtual ICollection<BuisnessServices> BuisnessServices { get; set; }

//    }
//}
