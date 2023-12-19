using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.busoftModels
{
    public class ImportRequest
    {
        public int ID { get; set; }
        public string ImporterName { get; set; }
        public string ImportDescription { get; set; }
        public string ImportFileName { get; set; }
        public string ImportFileNameExtension { get; set; }
        public string ImportDirectory { get; set; }
        public byte[] ImportFile { get; set; }
        public List<string> Columns { get; set; }
        public int SheetNumber { get; set; }
        public bool SaveFileToDisk { get; set; }
        public bool UpdateExisting { get; set; }
        public bool DeletedRecords { get; set; }
        public bool ReturnResults { get; set; }
        public bool ColumnsNameSameAsDbName { get; set; }

    }
}
