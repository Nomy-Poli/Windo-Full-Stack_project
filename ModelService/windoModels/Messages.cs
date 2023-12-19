using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Guid? ParentMessagesId  { get; set; } // אם ההודעה מקושרת להודעה אחרת יהיה כאן את המזהה של ההודעה.
        [ForeignKey("BusinessFrom")] 
        public int BusinessIdFrom { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Subject { get; set; }
        public bool IsGroup { get; set; }
        public string MessageText { get; set; }
        public int? CollaborationType { get; set; }
        public virtual ICollection<Message> ChildrenMessages { get; set; }
        public virtual ICollection<MessagesTo> ListMessagesTo { get; set; }
        public virtual Buisness BusinessFrom { get; set;}
  

    }
}
