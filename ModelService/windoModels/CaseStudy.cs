using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelService.windoModels
{
    public class CaseStudy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromTable { get; set; } // ENUM  בהתאם לטבלה שממנה סוגי שת"פ CS
        [ForeignKey("PaidTransaction")]
        public int? PaidTransactionID { get; set; } // אם ה CS מדווח על עסקה בתשלום(רכישה) זה ה ID של הרשומה מטבלת PaidTransaction
        [ForeignKey("BarterDeal")]
        public int? BarterDealID { get; set; }// אם ה CS מדווח על עסקת ברטר זה ה ID של הרשומה מטבלת BarterDeal
        [ForeignKey("JointProject")]
        public int? JointProjectID { get; set; }//אם ה CS מדווח על עסקה מיזם משותף זה ה ID של הרשומה מטבלת JointProject

        public DateTime ReportDate { get; set; }//- תאריך יצירת הCS
        public string MarketingTitle { get; set; }// כותרת שיווקית
        public string BusinessTitle { get; set; }//כותרת עסקית
        public string Description { get; set; }//תיאור המיזם
        public string Challenge { get; set; }//אתגר בדרך לשיתוף הפעולה
        public string PowerMultiplier { get; set; }//מה הרווחתן כתוצאה מהשת"פ
        public string CustomersGain { get; set; }//מה הרוויחו הלקוחות שלכם משיתוף הפעולה

        public Guid? PicGuid { get; set; }
        public virtual PaidTransaction PaidTransaction { get; set; }
        public virtual BarterDeal  BarterDeal { get; set; }
        public virtual JointProject JointProject { get; set; }
        public virtual ICollection<BusinessInCaseStudy> BusinessesInCaseStudy  { get; set; }
        public virtual ICollection<CaseStudyPicture> Pictures  { get; set; }
        public virtual ICollection<CaseStudyCustomerResponses> CustomerResponseses { get; set; }//תגובות לקוחות

    }
}
