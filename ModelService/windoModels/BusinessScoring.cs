using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class BusinessScoring
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Business")]
        public int BusinessId { get; set; }

        [ForeignKey("ScroingOperation")]
        public int ScoringOperationId { get; set; }
        public DateTime? Date { get; set; }
        public int? Count { get; set; }
        public virtual Buisness Business { get; set; }
        public virtual ScroingOperation ScroingOperation { get; set; }
    }
}
