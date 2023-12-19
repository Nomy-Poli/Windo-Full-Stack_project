using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.windoModels
{
   public class Try
    {
        public int Id { get; set; }
        public string userId { get; set; }
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
        //public bool? isburterPossibleInAllCategory { get; set; }//האם ברטר אפשרי בכל הקטגוריות
        //public bool? isopenToSuggestionsForBarter { get; set; }//האם פתוחה להצעות בתמורה לברטר
        //public string product1 { get; set; }
        //public string product2 { get; set; }
        //public string barterProduct1 { get; set; }
        //public string barterProduct2 { get; set; }
        public int? UpdatedBusinessStatus { get; set; }
        public DateTime? lastupdatedStartDate { get; set; }
        //public int? views { get; set; }//מספר צפיות
        //public string tags { get; set; }
        //public string coverImg { get; set; }
        //public string logoPicture { get; set; }
        //public string ownerName { get; set; }
        //public int? index { get; set; }
        //public List<BuisnessPicture> pictursList { get; set; }
        //public List<Area> areasList { get; set; }
        //public List<CategorySubCategoryVm> CategorySubCategoryList { get; set; }
        //public List<CategorySubCategoryVm> CategorySubCategoryBarterList { get; set; }
        //public List<CategorySubCategoryVm> CategorySubCategoryList1 { get; set; }
        public List<CategoryVm> CategorySubCategoryList { get; set; }
        public List<CategorySubCategoryVm> CategorySubCategoryBarterList { get; set; }
        //public CategorySubCategoryVm[] CategorySubCategoryBarterList1 { get; set; }
        //2
        //public CategorySubCategoryVm[] CategorySubCategoryList2 { get; set; }
        //public CategorySubCategoryVm[] CategorySubCategoryBarterList2 { get; set; }
        //3
        //public CategorySubCategoryVm[] CategorySubCategoryList3 { get; set; }
        //public CategorySubCategoryVm[] CategorySubCategoryBarterList3 { get; set; }
        //4
        //public CategorySubCategoryVm[] CategorySubCategoryList4 { get; set; }
        //public CategorySubCategoryVm[] CategorySubCategoryBarterList4 { get; set; }

    }
}
