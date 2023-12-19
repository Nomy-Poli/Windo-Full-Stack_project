//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    //Sub categories of buisnesses
//    public class SubTopics
//    {
//        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int subTopicId { get; set; }

//        //Foreign key for Topics
//        [ForeignKey("Topics")]
//        public int topicId { get; set; }
//        public string name { get; set; }

//        public virtual Topics Topics { get; set; }
//        public virtual ICollection<Buisness> Buisness { get; set; }
//        public virtual ICollection<Services> Services { get; set; }
//    }
//}
