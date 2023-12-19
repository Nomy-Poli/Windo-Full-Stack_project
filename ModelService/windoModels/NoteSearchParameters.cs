using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.windoModels
{
    public class NoteSearchParameters
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public int? BusinessId { get; set; }
        public int? BoardId { get; set; }
        public bool? ForGroups { get; set; }
        public bool? IsManager { get; set; }
        public bool? getMyNote { get; set; }
        public int? CategorySubCategoryId { get; set; }
        public DateTime? CreationDateFrom { get; set; }//תאריך יצירת המודעה
        public DateTime? CreationDateTill { get; set; }//תאריך יצירת המודעה
        public DateTime? LastUpdateDate { get; set; }//תאריך עדכון המודעה
        public int? Latest { get; set; }

    }
}
