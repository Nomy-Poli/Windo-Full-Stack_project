using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.busoftModels
{
   public class FileModel
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }
    }
}
