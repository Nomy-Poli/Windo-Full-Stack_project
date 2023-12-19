//using ModelService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Windo.Models
//{
//    public class BuisnessVm
//    {
//        public int? buisnessId { get; set; }
//        public string userId { get; set; }
//        public string buisnessName { get; set; }
//        public string discription { get; set; }
//        public string tags { get; set; }
//        public int? subTopicId { get; set; }
//        public string subTopicName { get; set; }
//        public string topicName { get; set; }
//        public int? topicId { get; set; }
//        public int? cityId { get; set; }
//        public string cityName { get; set; }
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
//        public virtual ApplicationUser AspNetUsers { get; set; }
//    }
//    public class ServicesVm
//    {
//        public int servicesId { get; set; }
//        public string name { get; set; }
//        public string password { get; set; }
//        public string discription { get; set; }
//        public int subTopicId { get; set; }
//        public string subTopicName { get; set; }

//    }
//    public class BuisnessServicesVm
//    {
//        public int buisnessServicesId { get; set; }
//        public int servicesId { get; set; }
//        public string servicesName { get; set; }
//        public int buisnessId { get; set; }
//        public string buisnessName { get; set; }
//        public int serviceTypeId { get; set; }//give or get
//        public string serviceTypeName { get; set; }

//    }
//    public class TopicsVm
//    {
//        public int topicId { get; set; }
//        public string name { get; set; }
//        public List<SubTopicsVm> subTopicsList  { get; set; }
//    }
//    public class SubTopicsVm
//    {
//        public int subTopicId { get; set; }
//        //public int topicId { get; set; }
//        //public string topicName { get; set; }
//        public string name { get; set; }

//    }
//    public class ServiceTypeVm
//    {
//        public int serviceTypeId { get; set; }
//        public string name { get; set; }
//    }

//    public class CitiesVm
//    {
//        public int cityId { get; set; }
//        public string name { get; set; }
//    }
//    //public class SearchFilterVm
//    //{
//    //    public List<int> topicsList  { get; set; }
//    //    public List<int> subTopicsList { get; set; }
//    //    public List<int> servicesList { get; set; }
//    //    public List<string> tagsList { get; set; }
//    //}
//}
