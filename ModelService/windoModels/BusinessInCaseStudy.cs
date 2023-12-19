using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class BusinessInCaseStudy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CaseStudy")] 
        public int CaseStudyId { get; set; }
        [ForeignKey("Buisness")]
        public int BusinessId { get; set; }
        public string BuinessOwnerNameForCS { get; set; } // - שם בעלת עסק ל CS
        public string LineOfBusiness { get; set; }//- תחום עיסוק(STRING - לא קשור לקטגוריות)
        public string WordOfPartner { get; set; }//- מילה על הפרטנרית

        public virtual CaseStudy CaseStudy { get; set; }
        public virtual Buisness Buisness { get; set; }

    }
}
