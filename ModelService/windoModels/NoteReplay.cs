using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class NoteReplay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }
        public virtual Buisness Business { get; set; }
    }
}
