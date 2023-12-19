using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class NotesBoards
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }
        [ForeignKey("Board")]
        public int BoardId { get; set; }

        
        public virtual Note Note { get; set; }
        public virtual Board Board { get; set; }
    }
}
