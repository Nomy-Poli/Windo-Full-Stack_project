using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelService.windoModels
{
    public class CaseStudyCustomerResponses
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MinimalDescription { get; set; }
        public string Response { get; set; }

        [ForeignKey("CaseStudy")]
        public int CaseStudyId { get; set; }

        public virtual CaseStudy CaseStudy { get; set; }

    }
}
