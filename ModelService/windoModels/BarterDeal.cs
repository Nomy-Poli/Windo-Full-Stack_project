using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModelService.windoModels
{
    public class BarterDeal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("ReportsBusiness")]
        //[ForeignKey(nameof(ReportsBusiness)), Column(Order = 0)]
        public int ReportsBusinessId { get; set; }// מזהה עסק מדווח
        //[ForeignKey("PartnerBusiness")]
        //[ForeignKey(nameof(PartnerBusiness)), Column(Order = 1)]
        public int PartnerBusinessId { get; set; }// מזהה עסק פרטנר
        public DateTime ReportDate { get; set; }//תאריך דווח
        public int ReportCategorySubCategoryId { get; set; }//תת קטגוריה של עסק מדווח
        public string ReportDescriptionDeal { get; set; }//תאור של השירות/ מוצר עסק מדווח
        public int PartnerCategorySubCategoryId { get; set; }//תת קטגוריה של עסק פרטנר
        public string PartnerDescriptionDeal { get; set; }//תאור של השירות/ מוצר פרטנר
        public string BusinessDescription { get; set; }//טקסט תאור עסקה
        public string QuotePartnerBusiness { get; set; }//ציטוט עסק פרטנר
        public string QuoteReportsBusiness { get; set; }//ציטוט עסק מדווח
        public Guid? ReportsBusinessPictureID { get; set; }//תמונה עסק מדווח
        public Guid? PartnerBusinessPictureID { get; set; }//תמונה עסק פרטנר
        public string JointExplanation { get; set; }//הסבר משותף על עסקת הבארטר
        public bool ConfirmedByPartner { get; set; }//אישור הצהרה על שיתוף פרטנריות

        //icons fields
 
        public bool? MoreLeisure { get; set; } ///יותר פנאי
        public bool? MoreShopping { get; set; } //יותר קניות
        public bool? IncreasingRevenue { get; set; } //הגדלת הכנסות
        public bool? ReducingExpenses { get; set; } //הקטנת הוצאות
        public bool? ReducingEffort { get; set; } // צמצום מאמץ 

        public bool? IfDisplayedInCS { get; set; }
        [NotMapped]
        public virtual Buisness ReportsBusiness { get; set; }
        [NotMapped] 
        public virtual Buisness PartnerBusiness { get; set; }

    }
}
