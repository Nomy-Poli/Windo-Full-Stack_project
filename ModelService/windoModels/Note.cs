using System;
using System.Collections.Generic;
using System.Text;
using Windo.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class Note
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string  Header { get; set; }
        public string  Text { get; set; }
        [ForeignKey("Business")]
        public int BusinessId { get; set; }

        [ForeignKey("CategorySubCategory")]
        public int? CategorySubCategoryId { get; set; }//מזהה תתקטגוריה לקטגוריה של המודעה - לאיזה קטגוריה המודעה משתייכת

        [ForeignKey("NetworkingGroup")]
        public int? GroupId { get; set; }
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
        public virtual Buisness Business { get; set; }
        public virtual CategorySubCategory CategorySubCategory { get; set; }
        public virtual NetworkingGroup  NetworkingGroup { get; set; }
        public virtual ICollection<NotesBoards> NotesBoards { get; set; }
        public virtual ICollection<NoteReplay> ReplayToNotes { get; set; }

    }
}
