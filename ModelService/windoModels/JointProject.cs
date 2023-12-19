using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class JointProject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CollaborationType")]
        public int CollaborationTypeId { get; set; }
        public DateTime ReportDate { get; set; }
        public string HeaderCollaboration { get; set; }
        public string JointExplanation { get; set; }
        public Guid? PictureId { get; set; }
        //icons
        public bool? MoreLeisure { get; set; } ///יותר פנאי
        public bool? MoreShopping { get; set; } //יותר קניות
        public bool? IncreasingRevenue { get; set; } //הגדלת הכנסות
        public bool? ReducingExpenses { get; set; } //הקטנת הוצאות
        public bool? ReducingEffort { get; set; } // צמצום מאמץ 
        public bool? ConfirmedByPartners { get; set; }
        public bool? IfDisplayedInCS { get; set; }

        public virtual CollaborationType CollaborationType { get; set; }
        public virtual ICollection<BusinessInCollaboration> BuisnessesInCollaborations { get; set; }
    }
}
