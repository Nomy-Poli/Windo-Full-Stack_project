using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.windoModels
{
    public class PagingParameters<T>
    {
        public int PageSize { get; set; }//כמה שורות בכל עמוד
        public int PageNumber { get; set; }//מספר עמוד נוכחי
        public int? TotalRows { get; set; }//כמה כל הרשימה
        public T Data { get; set; }//הרשימה שחוזרת
    }
}
