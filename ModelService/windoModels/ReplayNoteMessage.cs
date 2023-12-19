using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class ReplayNoteMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        [ForeignKey("Message")]
        public Guid MessageId { get; set; }
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public DateTime CreationDate { get; set; }

        public Note Note { get; set; }
        public Message Message { get; set; }
        public Buisness Business { get; set; }
    }
}
