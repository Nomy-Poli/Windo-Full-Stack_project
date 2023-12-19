using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService
{
   public class ImportResponseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImportId { get; set; }
        public string ImporterName { get; set; }
        [Required]
        public string ImportFileName { get; set; }
        public int TotalRecords { get; set; }//how many records where
        public int ColumnsFound { get; set; }
        public int LoadedRecords { get; set; } // new records
        public int DeletedRecords { get; set; } //delete flag
        public int MarkForDelete { get; set; } //delete flag
        public int MarkForNew { get; set; } //delete flag
        public int NewLoaded { get; set; } //delete flag
        public int ErroredRecords { get; set; } //errors
        public int NotUpdated { get; set; } //not updated
        public int NewRecordsNotInDataBase { get; set; } //not updated
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int Added { get; set; }
        public int Deleted { get; set; }
        //public List<BusinessFromExcel> NotUpdated { get; set; }
    }
}
