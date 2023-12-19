using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Windo.Models;

namespace ModelService.windoModels
{
    public class CategorySubCategory
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int categoryId { get; set; }

        [ForeignKey("SubCategory")]
        public int subCategoryId { get; set; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<BuisnessSubCategoryBarter> BuisnessSubCategoryBarter { get; set; }
        public virtual ICollection<BuisnessSubCategory> BuisnessSubCategory { get; set; }

    }
}
