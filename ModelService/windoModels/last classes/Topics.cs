//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    // topics/categories of buisnesses
//    public class Topics
//    {
//        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int topicId { get; set; }
//        public string name { get; set; }
//        public virtual ICollection<SubTopics> SubTopic { get; set; }

//        //public virtual ICollection<Buisness> Buisness { get; set; }
//        //public virtual ICollection<Services> Services { get; set; }

//    }
//}
