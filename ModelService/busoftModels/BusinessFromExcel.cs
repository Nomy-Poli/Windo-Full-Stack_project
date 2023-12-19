using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelService.busoftModels
{
   public class BusinessFromExcel
    {
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("BusinessName")]
        public string BusinessName { get; set; }                
        [Column("Email")]
        public string Email { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Deletes")]
        public int Deletes { get; set; }// 0 = delete , 1 = dont delete
    }
}
