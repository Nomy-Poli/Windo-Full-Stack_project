using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService
{
    public class ExternalAuthModel
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
        public Boolean ComeFromLogin { get; set; }
    }
}
