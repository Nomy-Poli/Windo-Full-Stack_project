using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ModelService.windoModels
{
    public class BuisnessVmold
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string buisnessName { get; set; }
        public string phoneNumber1 { get; set; }
        public string phoneNumber2 { get; set; }
        public string address { get; set; }
        public string actionDiscription { get; set; }
        public string discription { get; set; }
        public string buisnessWebSiteLink { get; set; }
        public bool?  isdisplayBusinessOwnerName { get; set; }//האם להציג את שם בעלת העסק
        public bool?  ispayingBuisness { get; set; }//שיטת עסקים ע"י תשלום
        public bool?  isburterBuisness { get; set; }//שיטת עסקים ע"י בארטר
        public bool?  iscollaborationBuisness { get; set; }//שיטת עסקים ע"י שת"פ
        public bool?  isburterPossibleInAllCategory { get; set; }//האם ברטר אפשרי בכל הקטגוריות
        public bool?  isopenToSuggestionsForBarter { get; set; }//האם פתוחה להצעות בתמורה לברטר
        //public string coverPicture { get; set; }
        //public string logoPicture { get; set; }
        public string product1 { get; set; }
        public string product2 { get; set; }
        public string barterProduct1 { get; set; }
        public string barterProduct2 { get; set; }
        public int?   UpdatedBusinessStatus { get; set; }
        public DateTime? lastupdatedStartDate { get; set; }
        public int?   views { get; set; }//מספר צפיות
        public string tags { get; set; }
        public string coverPicture { get; set; }
        public string logoPicture { get; set; }
        public string ownerName { get; set; }
        public int? index { get; set; }
        public List<BuisnessPicture> pictursList { get; set; }
        public List<AreaVm> areasList { get; set; }
        //גרסה ראשונה
        //public List<CategorySubCategoryVm> CategorySubCategoryList { get; set; }
        //public List<CategorySubCategoryVm> CategorySubCategoryBarterList { get; set; }
        //אחרי התיקון:
        //1
        public List<CategorySubCategoryVm> CategorySubCategoryList1 { get; set; }
        public List<CategorySubCategoryVm> CategorySubCategoryBarterList1 { get; set; }
        //2
        public List<CategorySubCategoryVm> CategorySubCategoryList2 { get; set; }
        public List<CategorySubCategoryVm> CategorySubCategoryBarterList2 { get; set; }
        //3
        public List<CategorySubCategoryVm> CategorySubCategoryList3 { get; set; }
        public List<CategorySubCategoryVm> CategorySubCategoryBarterList3 { get; set; }
        //4
        public List<CategorySubCategoryVm> CategorySubCategoryList4 { get; set; }
        public List<CategorySubCategoryVm> CategorySubCategoryBarterList4 { get; set; }
    }
    public class CategorySubCategoryVm
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int subCategoryId { get; set; }
        public string subCategoryName { get; set; }
        public bool isPossibleInBarter { get; set; }

    }
    public class CategoryVm
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<SubCategoryVm> subCategoryList { get; set; }

    }
    public class SubCategoryVm
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int CategorySubCategoryId { get; set; }

    }


    //---------------------------------uri && efrat-------------------------------------------//
    public class BuisnessCategoryVm
    {
        public int? businessId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public int combinationtId { get; set; }
        public bool isPossibleInBarter { get; set; }
        public string categoryName { get; set; }
        public string subCategoryName { get; set; }


    }

    public class BuisnessBarterCategoryVm
    {
        public int? businessId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public int combinationtId { get; set; }
        public string categoryName { get; set; }
        public string subCategoryName { get; set; }
    }

    //public class BuisnessWithFilesVm
    //{
    //    public int id { get; set; }
    //    public string userId { get; set; }
    //    public string buisnessName { get; set; }
    //    public string phoneNumber1 { get; set; }
    //    public string phoneNumber2 { get; set; }
    //    public string address { get; set; }
    //    public string actionDiscription { get; set; }//שורת פעולה
    //    public string discription { get; set; }//תיאור מפורט
    //    public string buisnessWebSiteLink { get; set; }//לינק לאתר
    //    public bool? isdisplayBusinessOwnerName { get; set; }//האם להציג את שם בעלת העסק
    //    public bool? ispayingBuisness { get; set; }//שיטת עסקים ע"י תשלום
    //    public bool? isburterBuisness { get; set; }//שיטת עסקים ע"י בארטר
    //    public bool? iscollaborationBuisness { get; set; }//שיטת עסקים ע"י שת"פ
    //    public IFormFile coverPicture { get; set; } //string that will become a guid
    //    public IFormFile logoPicture { get; set; }//string that will become a guid
    //    public virtual List<BuisnessPictureVm> BuisnessPictureVm { get; set; }
    //}

    public class BuisnessVm
    {
        public int id { get; set; } //V
        public string userId { get; set; }//V
        public string buisnessName { get; set; }//V
        public string businessEmailAddress { get; set; }
        public string phoneNumber1 { get; set; }//V
        public string phoneNumber2 { get; set; }//V
        public string address { get; set; }//V
        public string actionDiscription { get; set; }//V//שורת פעולה
        public string discription { get; set; }//V//תיאור מפורט
        public string buisnessWebSiteLink { get; set; }//V//לינק לאתר
        public bool? isdisplayBusinessOwnerName { get; set; }//V//האם להציג את שם בעלת העסק
        public bool? ispayingBuisness { get; set; }//V//שיטת עסקים ע"י תשלום
        public bool? isburterBuisness { get; set; }//V//שיטת עסקים ע"י בארטר
        public bool? iscollaborationBuisness { get; set; }//V//שיטת עסקים ע"י שת"פ
        public bool? isburterPossibleInAllCategory { get; set; }//X//האם ברטר אפשרי בכל הקטגוריות
        public bool? isopenToSuggestionsForBarter { get; set; }//X//האם פתוחה להצעות בתמורה לברטר
        public string OptionalCollaborationDescription { get; set; }
        public int? index { get; set; }//X//בשביל הצגת רשימת העסקים - לא להוריד - חדוה
        public IFormFile coverPicture { get; set; }//V //string that will become a guid
        public IFormFile logoPicture { get; set; }//V//string that will become a guid
        public Guid? coverPictureId { get; set; }
        public Guid? logoPictureId { get; set; }

        public string product1 { get; set; }//V
        public string product2 { get; set; }//V
        public string barterProduct1 { get; set; }//V
        public string barterProduct2 { get; set; }//V
        public int? Score { get; set; }
        public int? ScoreStatus { get; set; }//מעמד
        public string userFullName { get; set; }

        //performents
        public bool? WantedGetHelpNotification { get; set; }
        public bool? WantedGetDailyNotification { get; set; }

        public int? UpdatedBusinessStatus { get; set; }//V//if =1 =came from update if =0 came from create
        public int? views { get; set; }//X//מספר צפיות
        public string tags { get; set; }//X
        //todo - check:
        public DateTime? lastupdatedStartDate { get; set; }//X

        //-----------not for update ------------------//

        public string ownerName { get; set; }//V
        public List<BuisnessCategoryVm> listOfAll4buisnessCategory { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory1 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory2 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory3 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory4 { get; set; }
        public List<BuisnessBarterCategoryVm> buisnessBarterCategory1 { get; set; }//V
        public List<BuisnessBarterCategoryVm> buisnessBarterCategory2 { get; set; }//V
        public virtual ICollection<BusinessCategoryNotifyVM> BusinessCategoriesNotify { get; set; }
        public List<BuisnessAreaVm> buisnessAreaList1 { get; set; }//V//רשימת הערים בהם נשתמש
        public List<GuideModel> workPictureGuide { get; set; }//V//רשימת הערים בהם נשתמש

        //public virtual Status Status { get; set; }
        //public virtual List<BuisnessSubCategory> BuisnessSubCategory { get; set; }
        //public virtual List<BuisnessSubCategoryBarter> BuisnessSubCategoryBarter { get; set; }

        //public virtual List<BuisnessStatus> BuisnessStatus { get; set; }
        public virtual List<BuisnessPictureVm> BuisnessPictureVm { get; set; }//X
    }

    public class BuisnessPictureVm
    {
        public string buisnessPictureId { get; set; } //string that will become a guid
        public int buisnessId { get; set; }
        public int numberOfPicture { get; set; }
        //public string url { get; set; }
    }
    //~~~~~~~~~~~~~~~~~~~efrat~~~~~~~~~~~~~~~~~~~
    public class BuisnessAreaVm
    {
        //צריך רק את קוד האזור
        //לא נשתמש בה
        public int? id { get; set; }
        public int? buisnessId { get; set; }
        public int areaId { get; set; }
    }
    public class AreaVm
    { 
        public int id { get; set; }
        public string name { get; set; }
        //public virtual ICollection<BuisnessAreaVm> BuisnessArea { get; set; }

    }
    public class BusinessCategoryNotifyVM
    {
        public int Id { get; set; }

        public int BusinessId { get; set; }

        public int categoryId { get; set; }

        public bool? IfNotify { get; set; }

        public virtual CategoryVm Category { get; set; }
    }
    public class BusinessNamesPicturesVM
    {
        public int id { get; set; }
        public string buisnessName { get; set; }
        public int? Score { get; set; }
        public Guid logoPictureId { get; set; }
    }
    public class BusinessNamesPicUserIdVM
    {
        public int id { get; set; }
        public string buisnessName { get; set; }
        public Guid logoPictureId { get; set; }
        public string userId { get; set; }//V
        public int? Score { get; set; }
    }

    public class BusinessForCardVM
    {
        public int id { get; set; } 
        public string userId { get; set; }
        public string buisnessName { get; set; }
        public string businessEmailAddress { get; set; }
        public string actionDiscription { get; set; }//V//שורת פעולה
        public bool? isdisplayBusinessOwnerName { get; set; }//V//האם להציג את שם בעלת העסק
        public bool? ispayingBuisness { get; set; }//V//שיטת עסקים ע"י תשלום
        public bool? isburterBuisness { get; set; }//V//שיטת עסקים ע"י בארטר
        public bool? iscollaborationBuisness { get; set; }//V//שיטת עסקים ע"י שת"פ
        public Guid? logoPictureId { get; set; }
        public List<BuisnessCategoryVm> listOfAll4buisnessCategory { get; set; }
        public int? index { get; set; }//X//בשביל הצגת רשימת העסקים - לא להוריד - חדוה

        public string barterProduct1 { get; set; }//V
        public string barterProduct2 { get; set; }//V
        public DateTime? lastupdatedStartDate { get; set; }//X
        public string ownerName { get; set; }//V
        public int? Score { get; set; }

        public List<BuisnessCategoryVm> buisnessCategory1 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory2 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory3 { get; set; }
        public List<BuisnessCategoryVm> buisnessCategory4 { get; set; }
        public List<BuisnessAreaVm> buisnessAreaList1 { get; set; }//V//רשימת הערים בהם נשתמש
    }


    #region collaboration
    public class PaidTransactionVM
    {
        public int Id { get; set; }
        public int SupplierBusinessId { get; set; }

        public int ConsumerBusinessId { get; set; }
        public int CategorySubCategoryId { get; set; }
        public string Description { get; set; }
        public string Review { get; set; }
        public int? ScopTransactionNIS { get; set; }
        public Guid? PictureID { get; set; }
        public bool? Availability { get; set; }
        public bool? Service { get; set; }
        public bool? Professionalism { get; set; }
        public bool? Price { get; set; }
        public bool? Flexable { get; set; }

        public bool? IfDisplayedInCS { get; set; }
        public IFormFile PaidTransactionPicture { get; set; }
        public BusinessNamesPicturesVM SupplierBusiness { get; set; }
        public BusinessNamesPicturesVM ConsumerBusiness { get; set; }
    }

    public class BarterDealVM
    {
        public int Id { get; set; }
        public int ReportsBusinessId { get; set; }// מזהה עסק מדווח
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
        public IFormFile Business1Picture { get; set; } //תמונה עסק מדווח
        public Guid? PartnerBusinessPictureID { get; set; }//תמונה עסק פרטנר
        public IFormFile Business2Picture { get; set; } //תמונה עסק פרטנר

        public string JointExplanation { get; set; }//הסבר משותף על עסקת הבארטר
        public bool ConfirmedByPartner { get; set; }//אישור הצהרה על שיתוף פרטנריות

        //icons fields
        public bool? MoreLeisure { get; set; } ///יותר פנאי
        public bool? MoreShopping { get; set; } //יותר קניות
        public bool? IncreasingRevenue { get; set; } //הגדלת הכנסות
        public bool? ReducingExpenses { get; set; } //הקטנת הוצאות
        public bool? ReducingEffort { get; set; } // צמצום מאמץ 
        public bool? IfDisplayedInCS { get; set; } // האם מוצג בקייס סטדי
        public BusinessNamesPicturesVM ReportsBusiness { get; set; }
        public BusinessNamesPicturesVM PartnerBusiness { get; set; }

    }


    public class JointProjectVM
    {
        public int Id { get; set; }
        public int CollaborationTypeId { get; set; }
        public DateTime ReportDate { get; set; }
        public string HeaderCollaboration { get; set; }
        public string JointExplanation { get; set; }
        public Guid? PictureId { get; set; }
        public IFormFile Picture { get; set; }
        //icons
        public bool? MoreLeisure { get; set; } ///יותר פנאי
        public bool? MoreShopping { get; set; } //יותר קניות
        public bool? IncreasingRevenue { get; set; } //הגדלת הכנסות
        public bool? ReducingExpenses { get; set; } //הקטנת הוצאות
        public bool? ReducingEffort { get; set; } // צמצום מאמץ 
        public bool? ConfirmedByPartners { get; set; }
        public bool? IfDisplayedInCS { get; set; }
        public CollaborationTypeVM CollaborationType { get; set; }
        public ICollection<BusinessInCollaborationVM> BusinessesInCollaboration { get; set; }
    }
    public class BusinessInCollaborationVM: BusinessNamesPicturesVM
    {
        //public int Id { get; set; } //ID עסק בשת"פ
        public string PartInCollaboration { get; set; }// תאור חלק בשת"פ
        public bool? IfReport { get; set; }//האם זה העסק המדווח
    }
    public class CollaborationTypeVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    #endregion

    #region case study
    public class CaseStudyVM
    {
        public int Id { get; set; }
        public FromTable FromTable { get; set; } // - לדעתי יכול להיות 1/ 2/ 3  או סטרינג של שם הטבלה בהתאם לטבלה שממנה נלקח ה CS
        public int? PaidTransactionID { get; set; } // אם ה CS מדווח על עסקה בתשלום(רכישה) זה ה ID של הרשומה מטבלת PaidTransaction
        public int? BarterDealID { get; set; }// אם ה CS מדווח על עסקת ברטר זה ה ID של הרשומה מטבלת BarterDeal
        public int? JointProjectID { get; set; }//אם ה CS מדווח על עסקה מיזם משותף זה ה ID של הרשומה מטבלת JointProject
        public DateTime ReportDate { get; set; }//- תאריך יצירת הCS

        public string MarketingTitle { get; set; }// כותרת שיווקית
        public string BusinessTitle { get; set; }//כותרת עסקית
        public string Description { get; set; }//תיאור המיזם
        public string Challenge { get; set; }//אתגר בדרך לשיתוף הפעולה
        public string PowerMultiplier { get; set; }//מה הרווחתן כתוצאה מהשת"פ
        public string CustomersGain { get; set; }//מה הרוויחו הלקוחות שלכם משיתוף הפעולה
        public Guid? PicGuid { get; set; }
        public List<CaseStudyCustomerResponsesVM> CustomerResponses { get; set; }//תגובות לקוחות

        public PaidTransactionVM PaidTransaction { get; set; }
        public BarterDealVM BarterDeal { get; set; }
        public JointProjectVM JointProject { get; set; }
        public List<BusinessInCaseStudyVM> BusinessesInCaseStudy { get; set; }
        public List<CaseStudyPictureVM> CaseStudyPictures { get; set; }
    }
    public class CaseStudyForCardsVM
    {
        public int Id { get; set; }
        public FromTable FromTable { get; set; } // - לדעתי יכול להיות 1/ 2/ 3  או סטרינג של שם הטבלה בהתאם לטבלה שממנה נלקח ה CS
        public string MarketingTitle { get; set; }// כותרת שיווקית
        public string BusinessTitle { get; set; }//כותרת עסקית
        public Guid? PicGuid { get; set; }
        public List<BusinessInCaseStudyVM> BusinessesInCaseStudy { get; set; }
        public List<CaseStudyPictureVM> CaseStudyPictures { get; set; }
    }

    public class CSNamePictureVM
    {
        public int Id { get; set; }
        public string MarketingTitle { get; set; }// כותרת שיווקית
        public string BusinessTitle { get; set; }//כותרת עסקית
        public Guid? PicGuid { get; set; }
    }
    public enum FromTable
    {
        PaidTransaction = 1,
        BarterDeal = 2,
        JointProject = 3
    }
    public class BusinessInCaseStudyVM
    {
        public int Id { get; set; }
        public int CaseStudyId { get; set; }
        public int BusinessId { get; set; }// אם זה מיזם משותף יש כאן כפילות
        public string BuinessOwnerNameForCS { get; set; } // - שם בעלת עסק ל CS
        public string LineOfBusiness { get; set; }//- תחום עיסוק(STRING - לא קשור לקטגוריות)
        public string WordOfPartner { get; set; }//- מילה על הפרטנרית
        public BusinessNamesPicturesVM Business { get; set; }
    }
    public class CaseStudyPictureVM
    {
        public int Id { get; set; }// id לטבלה
        public int CaseStudyId { get; set; }
        public Guid PicGuid { get; set; }// id לתמונה

    }

    public class CaseStudyCustomerResponsesVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MinimalDescription { get; set; }
        public string Response { get; set; }
        public int CaseStudyId { get; set; }

    }
    public class IdAndTableNameForCS
    {
        public int Id { get; set; }
        public FromTable FromTable { get; set; }
    }
    #endregion


    #region messages
    public class MessageVM
    {
        public Guid Id { get; set; }
        public Guid? ParentMessagesId { get; set; } // אם ההודעה מקושרת להודעה אחרת יהיה כאן את המזהה של ההודעה.
        public int BusinessIdFrom { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public FromTable? CollaborationType { get; set; }

        public List<MessageVM> ChildrenMessages { get; set; }
        public List<MessagesToVM> ListMessagesTo { get; set; }
        public BusinessNamesPicturesVM BusinessFrom { get; set; }

        public bool? isCurrentUserRead { get; set; }
        public bool? isCurrentUserNew { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string EmailFrom { get; set; }//for cashe and send email in controller

    }

    public class MessagesToVM
    {
        public int Id { get; set; }
        public Guid? MessageId { get; set; }
        public int BusinessIdTo { get; set; }
        public int BusinessIdFrom { get; set; }
        public BusinessNamesPicUserIdVM BuisnessTo { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRead { get; set; }
    }
    
    public class SupportMessageVM
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public string EmailFrom { get; set; }//for cashe and send email in controller
        public string UserName { get; set; }
    }
    #endregion


    #region notes
    public class ReplayNoteMessageVM
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int NoteId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime? CreationDate { get; set; }
        public BusinessNamesPicUserIdVM Business { get; set; }
    }
    public class NoteReplayVM
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int BusinessId { get; set; }
        public string Text { get; set; }
        public DateTime? CreationDate { get; set; }
        public virtual BusinessNamesPicUserIdVM Business { get; set; }
    }
    public class BoardVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
    public class BoardForCardVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public List<NoteForCardVM> Notes { get; set; }
    }
    public class NoteVM
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public int BusinessId { get; set; }

        public int? CategorySubCategoryId { get; set; }//מזהה תתקטגוריה לקטגוריה של המודעה - לאיזה קטגוריה המודעה משתייכת
        public int? GroupId { get; set; } // הקבוצה אליה משויכת המודעה - לא חובה 
        public string Labels { get; set; }//תגיות המודעה - מחרוזת מופרדת בפסיק.
        public DateTime? CreatetionDate { get; set; }//תאריך יצירת המודעה
        public DateTime? LastUpdateDate { get; set; }//תאריך עדכון המודעה
        public DateTime? DeletionDate { get; set; }//תאריך מחיקת המודעה
        public DateTime? ExpirationDate { get; set; }//תאריך תפוגה של המודעה
        public bool? IsActive { get; set; }//סטטוס פעיל -(כן/לא) (מחיקה..)

        public int? ChangedStatus { get; set; }//יישות ששינתה סטטוס( 1- משתמש , 2 - מנהל , 3 -סיסטם).
        public int? NumberOfViews { get; set; } = 0;//כמות צפיות

        public bool? IsBold { get; set; }
        public bool? IsPayNote { get; set; }
        public BusinessNamesPicUserIdVM Business  { get; set; }
        public CategorySubCategoryVm CategorySubCategory { get; set; }
        public  NetworkingGroupVM NetworkingGroup { get; set; }
        public List<BoardVM> Boards { get; set; }
        public int? ReplayCount { get; set; } = 0;
    }
    public class NoteForCardVM
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public int BusinessId { get; set; }
        public int? CategorySubCategoryId { get; set; }//מזהה תתקטגוריה לקטגוריה של המודעה - לאיזה קטגוריה המודעה משתייכת
        public DateTime? LastUpdateDate { get; set; }//תאריך עדכון המודעה
        public bool? IsPayNote { get; set; }
        public BusinessNamesPicUserIdVM Business { get; set; }
        public CategorySubCategoryVm CategorySubCategory { get; set; }

    }
    public class NoteWithReplayVM
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public int BusinessId { get; set; }

        public int? CategorySubCategoryId { get; set; }//מזהה תתקטגוריה לקטגוריה של המודעה - לאיזה קטגוריה המודעה משתייכת
        public string Labels { get; set; }//תגיות המודעה - מחרוזת מופרדת בפסיק.
        public DateTime? CreatetionDate { get; set; }//תאריך יצירת המודעה
        public DateTime? LastUpdateDate { get; set; }//תאריך עדכון המודעה
        public DateTime? DeletionDate { get; set; }//תאריך מחיקת המודעה
        public DateTime? ExpirationDate { get; set; }//תאריך תפוגה של המודעה
        public bool? IsActive { get; set; }//סטטוס פעיל -(כן/לא) (מחיקה..)

        public int? ChangedStatus { get; set; }//יישות ששינתה סטטוס( 1- משתמש , 2 - מנהל , 3 -סיסטם).
        public int? NumberOfViews { get; set; } = 0;//כמות צפיות

        public bool? IsBold { get; set; }
        public bool? IsPayNote { get; set; }
        public BusinessNamesPicUserIdVM Business { get; set; }
        public CategorySubCategoryVm CategorySubCategory { get; set; }

        public List<NoteReplayVM> ReplayToNotes { get; set; }
    }
    public class NotesBoardsVM
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int BoardId { get; set; }
        public virtual NoteVM Note { get; set; }
        public virtual BoardVM Board { get; set; }
    }

    #endregion

    #region clients
    public class ClientTypeVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class ClientVM
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public int ClientTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; } 
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? UserId { get; set; }

        public ClientTypeVM ClientType { get; set; }
    }
    #endregion

    #region advertisment
    public class ServiceTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CatalogServiceVM
    {
        public int Id { get; set; }
        public int Makat { get; set; }
        public int ServiceTypeId { get; set; }
        public string Description { get; set; }

        public ServiceTypeVM ServiceType { get; set; }
    }

    public class BannerVM
    {
        public int Id { get; set; }

        public int Makat { get; set; }//מק"ט - מספר קטלוג
        public int Height { get; set; }
        public int Width { get; set; }
        public string PageName { get; set; }
        public int? PageID { get; set; }
        public string Title { get; set; }
        public int? PriceInPoints { get; set; }
        public float? Price { get; set; }
        public Guid? DefaultPicGuid { get; set; }
        public string DefaultLink { get; set; }
        public Guid? ExamplePicGuid { get; set; }
        public Guid? FormatPicGuid { get; set; }

        //public Guid? AdvertismentPicGuid { get; set; }
        //public string AdvertismentLink { get; set; }
        public AdvertismentServiceOrderVM AdvertismentServiceOrder { get; set; }
        public CatalogServiceVM CatalogService { get; set; }
    }
    public class OrderStatusesVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public enum RequestStatus
    {
        waiting = 1,
        completing = 2,
        rejected = 3

    }
    public class OrderServiceVM
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int StatusOrderId { get; set; }
        public int Makat { get; set; }
        public DateTime CreationDate { get; set; }
        public float? Price { get; set; }

        public Client Client { get; set; }
        public OrderStatuses Status { get; set; }
        public CatalogService CatalogService { get; set; }
    }

    public class AdvertismentServiceOrderVM
    {
        public int Id { get; set; }

        public int OrderServiceId { get; set; }
        public int Makat { get; set; }
        public DateTime? adFromDate { get; set; }
        public DateTime? adTillDate { get; set; }
        public Guid? PicGuid { get; set; }
        public string LinkToSite { get; set; }
    }

    public class OrderServiceWithAdDetailsVM
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int StatusOrderId { get; set; }
        public int Makat { get; set; }
        public DateTime? CreationDate { get; set; }
        public float? Price { get; set; }

        public Client Client { get; set; }
        public OrderStatuses Status { get; set; }
        public CatalogService CatalogService { get; set; }
        public virtual AdvertismentServiceOrderVM AdvertismentServiceOrder { get; set; }
        
    }

    public class RequestOrderServiceVM
    {
        public int Id { get; set; }
        public int? Makat { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Text { get; set; }

        public string Email { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ServiceDate { get; set; }
        public bool? ReturningCustomer { get; set; }
        public string LinkForSite { get; set; }
        public RequestStatus RequestStatus { get; set; }//Enum
    }


    public class IfAvalibleDate
    {
        public int Makat { get; set; }
        public DateTime adFromDate { get; set; }
        public DateTime adTillDate { get; set; }
        public int ClientId { get; set; }
        public int OrderId { get; set; }
    }
    // Scoring
    public class ScroingOperationVM
    {
        public int Id { get; set; }
        public int Count   { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
        public int? ActionId { get; set; }
        public int? EventTypeId { get; set; }
        public ScoringAction ScoringAction { get; set; }
        public ScoringEventType EventType { get; set; }

    }
 
    public class BusinessForScoringVM
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string buisnessName { get; set; }
        public string businessEmailAddress { get; set; }
        public int? Score { get; set; }

    }
    public class MultipleActions
    {
        public int[] scoringActionId { get; set; }
        public int[] buisnessId { get; set; }
    }
    public class BusinessScoringsDetailVM
    {
        public int id { get; set; }
        public string BusinessId { get; set; }
        public DateTime? Date { get; set; }
        public ScroingOperationVM ScroingOperation { get; set; }
        public int Count { get; set; }


    }
    //networking
    public class NetworkingGroupVM
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int? ManagerBusinessId { get; set; }
        public string ManagerBusinessEmail { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public int? AreaId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public BusinessForCardVM ManagerBusiness { get; set; }
        public AreaVm Area { get; set; }

        //public List<NetworkingGroupBusinessVM> NetworkingGroupBusinesses { get; set; }
    }
    public class NetworkingGroupBusinessVM
    {
        public int Id { get; set; }
        public int? BusinessId { get; set; }
        public string buisnessName { get; set; }
        public int? GroupId { get; set; } 
        public string Role { get; set; }
   //     public virtual Buisness Business { get; set; }
   //     public  NetworkingGroupVM Group { get; set; }
    }
    #endregion
}
