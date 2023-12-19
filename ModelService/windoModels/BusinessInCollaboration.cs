using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class BusinessInCollaboration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Business")] 
        public int BusinessId { get; set; }
        [ForeignKey("JointProject")]
        public int JoinProjectId { get; set; }
        public string PartInCollaboration { get; set; }// תאור חלק בשת"פ
        public bool? IfReport { get; set; }//האם זה העסק המדווח

        public virtual Buisness Business { get; set; }
        public virtual JointProject JointProject { get; set; }

    }
}
