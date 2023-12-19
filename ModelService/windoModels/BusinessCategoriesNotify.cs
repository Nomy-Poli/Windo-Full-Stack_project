using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Windo.Models;

namespace ModelService.windoModels
{
    public class BusinessCategoryNotify
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Buisness")]
        public int BusinessId { get; set; }

        [ForeignKey("Category")]
        public int categoryId { get; set; }

        public bool? IfNotify { get; set; }

        public virtual Buisness Buisness { get; set; }
        public virtual Category Category { get; set; }
    }
}
