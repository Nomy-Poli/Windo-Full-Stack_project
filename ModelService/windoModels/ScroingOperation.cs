using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class ScroingOperation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ScoringAction")]
        public int ActionId { get; set; }

        [ForeignKey("EventType ")]
        public int EventTypeId { get; set; }
        public int Count { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }


        public virtual ScoringAction ScoringAction { get; set; }
        public virtual ScoringEventType EventType { get; set; }
        public virtual ICollection<BusinessScoring> BusinessScorings { get; set; }
    }
}
