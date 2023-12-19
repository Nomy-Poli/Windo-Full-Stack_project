using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService
{
   public class AuthResponseModel
    {
        public string Token { get; set; }
        public Boolean IsAuthSuccessful { get; set; }
    }
}
