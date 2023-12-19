using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.busoftModels
{
   public class ImportResponse
    {
        public int ImportId { get; set; }
        public string ImporterName { get; set; }
        public string ImportFileName { get; set; }
        public int TotalRecords { get; set; }//how many records where
        public List<string> ColumnsFoundList { get; set; }//how many  titles
        public int LoadedRecords { get; set; } // new records
        public int DeletedRecords { get; set; } //delete flag
        public int MarkForDelete { get; set; } //delete flag
        public int MarkForNew { get; set; } //delete flag
        public int NewLoaded { get; set; } //delete flag
        public int ErroredRecords { get; set; } //errors
        public int NotUpdated { get; set; } //not updated
        public int NewRecordsNotInDataBase { get; set; } //not updated
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<BusinessFromExcel> AddedList { get; set; }
        public List<BusinessFromExcel> DeletedList { get; set; }
        public List<UserEmailModel> SendEmail { get; set; }
        public int AddedLength { get; set; }
        public int DeletedLength { get; set; }
        //public List<BusinessFromExcel> NotUpdated { get; set; }
    }
}
