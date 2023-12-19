using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.windoModels
{
    public class BuisnessArea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Buisness")]
        public int buisnessId { get; set; }

        [ForeignKey("Area")]
        public int areaId { get; set; }


        public virtual Area Area { get; set; }
        public virtual Buisness Buisness { get; set; }

    }
}
 