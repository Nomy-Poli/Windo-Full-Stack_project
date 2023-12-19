//using ModelService;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    public class Buisness
//    {
//        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int buisnessId { get; set; }
//        public string userId { get; set; }
//        public string buisnessName { get; set; }
//        public string discription { get; set; }

//        [ForeignKey("SubTopics")]
//        public int? subTopicId { get; set; }
//        public string password { get; set; }

//        [ForeignKey("Cities")]
//        public int? cityId { get; set; }
//        //public  File profileImg { get; set; }
//        public string profileImg { get; set; }
//        public string address { get; set; }
//        public string phoneNumber { get; set; }
//        public string emailAddress { get; set; }
//        public string buisnessWebSiteLink { get; set; }
//        public DateTime? dateStarted { get; set; }
//        public string professionalExperienceDesc { get; set; }
//        public bool? countryWide { get; set; }//כלל ארצי
//        public bool? payingBuisness { get; set; }//שיטת עסקים ע"י תשלום
//        public bool? burterBuisness { get; set; }//שיטת עסקים ע"י בארטר
//        public bool? collaborationBuisness { get; set; }//שיטת עסקים ע"י שת"פ
//        public string pictursList { get; set; }
//        public int? views { get; set; }//מספר צפיות
//        public string tags { get; set; }
//        public virtual Cities Cities { get; set; }
//        public virtual SubTopics SubTopics { get; set; }
//        public virtual ICollection<BuisnessServices> BuisnessServices { get; set; }
//    }

//}
