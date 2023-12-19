

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelService.windoModels
{
    public class NetworkingGroupBusiness
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Business")]
        public int? BusinessId { get; set; }
        public string buisnessName { get; set; } 

        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public string Role { get; set; }

        public virtual Buisness Business { get; set; }
        public virtual NetworkingGroup Group { get; set; }

    }
}