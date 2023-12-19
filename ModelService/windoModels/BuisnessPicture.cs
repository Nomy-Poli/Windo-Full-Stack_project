using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.windoModels
{
    public class BuisnessPicture
    {
        [Key]
        public Guid buisnessPictureId { get; set; }
        
        [ForeignKey("Buisness")]
        public int buisnessId { get; set; }
        public int numberOfPicture { get; set; }

        public virtual Buisness Buisness { get; set; }
    }
}
