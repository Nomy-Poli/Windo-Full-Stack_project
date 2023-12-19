using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.windoModels
{
    public class BuisnessSubCategoryBarter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Buisness")]
        public int buisnessId { get; set; }

        [ForeignKey("CategorySubCategory")]
        public int categorySubCategoryId { get; set; }

        public virtual CategorySubCategory CategorySubCategory { get; set; }
        public virtual Buisness Buisness { get; set; }

    }
}
