using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.windoModels
{
    public class Buisness
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //userId - the value that we insert == email of user : userId = email
        public string userId { get; set; }
        public string businessEmailAddress { get; set; }
        public string buisnessName { get; set; }
        public string phoneNumber1 { get; set; }
        public string phoneNumber2 { get; set; }
        public string address { get; set; }
        public string actionDiscription { get; set; }//שורת פעולה
        public string discription { get; set; }//תיאור מפורט
        public string buisnessWebSiteLink { get; set; }//לינק לאתר
        public bool? isdisplayBusinessOwnerName { get; set; }//האם להציג את שם בעלת העסק
        public bool? ispayingBuisness { get; set; }//שיטת עסקים ע"י תשלום
        public bool? isburterBuisness { get; set; }//שיטת עסקים ע"י בארטר
        public bool? iscollaborationBuisness { get; set; }//שיטת עסקים ע"י שת"פ
        public bool? isburterPossibleInAllCategory { get; set; }//האם ברטר אפשרי בכל הקטגוריות
        public string OptionalCollaborationDescription { get; set; }
        public bool? isopenToSuggestionsForBarter { get; set; }//האם פתוחה להצעות בתמורה לברטר
        public Guid coverPictureId { get; set; }
        public Guid logoPictureId { get; set; }
        public string product1 { get; set; }
        public string product2 { get; set; }
        public string barterProduct1 { get; set; }
        public string barterProduct2 { get; set; }

        public bool? WantedGetHelpNotification { get; set; }
        public bool? WantedGetDailyNotification { get; set; }

        [ForeignKey("Status")]
        public int? UpdatedBusinessStatus { get; set; }
        public int? views { get; set; }//מספר צפיות
        public string tags { get; set; }

        //Score
        public int? Score { get; set; }
        public int? ScoreStatus { get; set; }//מעמד
        public virtual Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<BuisnessSubCategory> BuisnessSubCategory { get; set; }
        public virtual ICollection<BuisnessSubCategoryBarter> BuisnessSubCategoryBarter { get; set; }
        public virtual ICollection<BuisnessArea> BuisnessArea { get; set; }
        public virtual ICollection<BuisnessStatus> BuisnessStatus { get; set; }
        public virtual ICollection<BuisnessPicture> BuisnessPicture { get; set; }  
        //public virtual ICollection<BarterDeal> BarterDeals { get; set; }
        //public virtual ICollection<PaidTransaction> PaidTransactions { get; set; }
        public virtual ICollection<BusinessInCaseStudy> BusinessInCaseStudies { get; set; }
        public virtual ICollection<BusinessCategoryNotify> BusinessCategoriesNotify { get; set; }

        public virtual ICollection<NetworkingGroupBusiness> NetworkingGroupBusinesses { get; set; }
        public virtual ICollection<BusinessInCollaboration> BusinessInCollaborations { get; set; }
        [NotMapped] 
        public virtual ICollection<PaidTransaction> GetPaidTransactionsSupplier { get; set; }
        [NotMapped] 
        public virtual ICollection<PaidTransaction> GetPaidTransactionsConsumer { get; set; }

        //Score
        //public virtual ICollection<BusinessScoring> BusinessScoring { get; set; }
    }
}
