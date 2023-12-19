using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class CaseStudyPicture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }// id לטבלה
        [ForeignKey("CaseStudy")]
        public int CaseStudyId { get; set; }
        public Guid PicGuid { get; set; }// id לתמונה
        public virtual CaseStudy CaseStudy { get; set; }
    }
}
