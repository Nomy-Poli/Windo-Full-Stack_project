using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class NetworkingGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GroupName { get; set; }

        [ForeignKey("ManagerBusiness")]
        public int? ManagerBusinessId { get; set; }
        public string ManagerBusinessEmail { get; set; }
        public string Description { get; set; }
        public string City { get; set; }

        [ForeignKey("Area")]
        public int? AreaId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsActive { get; set; }


       public virtual Buisness ManagerBusiness { get; set; }
        public virtual Area Area { get; set; }

        public virtual List<NetworkingGroupBusiness> NetworkingGroupBusinesses { get; set; }
        //שם קבוצה, מייל מנהלת קבוצה, מזהה מנהלת קבוצה, עיר בה הקבוצה מתנהלת , תאור הקבוצה, תאריך הקמת הקבוצה
    }
}
