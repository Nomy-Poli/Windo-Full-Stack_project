using System;
using System.Collections.Generic;
using System.Text;

namespace ModelService.windoModels.templates
{
    public class EmailTemplates
    {
        // בנושאים שעניינו אותך
        public const string dailySubject = "תקציר של המודעות החדשות שעלו לאתר";

        public const string dailyContentEmail = @"
                <ul id='notes'>
                  {{ for note in notes }}
                    <li>
                             
                              <a href='{{ url}}?note={{note.id}} '>  {{note.header}} </a>
                   </li>
                  {{ end }}
                </ul>
            ";

        public const string newNoteSubject = "פירסמה מודעה חדשה בנושא המעניין אותך באתר windo";
        public const string newNoteContentEmail = @"<a href='{{ url}}?note={{id}} '>  {{header}} </a>
            ";
    }
}
