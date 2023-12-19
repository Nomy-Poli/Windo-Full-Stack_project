using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class PaidTransaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("SupplierBusiness")]
       // [ForeignKey(nameof(SupplierBusiness)), Column(Order = 0)]
        public int SupplierBusinessId { get; set; }
        //[ForeignKey("ConsumerBusiness")]
        //[ForeignKey(nameof(ConsumerBusiness)), Column(Order = 1)] 
        public int ConsumerBusinessId { get; set; }
        public int CategorySubCategoryId { get; set; }
        public string Description { get; set; }
        public string Review { get; set; }
        public int? ScopTransactionNIS { get; set; }//הקף העיסקה בש"ח
        public Guid? PictureID { get; set; }
        public bool? Availability { get; set; }
        public bool? Service { get; set; }
        public bool? Professionalism { get; set; }
        public bool? Price { get; set; }
        public bool? Flexable { get; set; }
        public bool? IfDisplayedInCS { get; set; }
        //[ForeignKey("SupplierBusinessId")]
        //[InverseProperty("GetPaidTransactionsSupplier")]
        [NotMapped]
        public virtual Buisness SupplierBusiness { get; set; }
        //[ForeignKey("ConsumerBusinessId")]
        //[InverseProperty("GetPaidTransactionsConsumer")]
        [NotMapped]
        public virtual Buisness ConsumerBusiness { get; set; }

    }
}
