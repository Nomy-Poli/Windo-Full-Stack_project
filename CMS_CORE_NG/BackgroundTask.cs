using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using CMS_CORE_NG.BL;
using Hangfire;
using CMS_CORE_NG.Repository;
using ModelService.windoModels;
using System.Collections.Generic;
using System.Linq;
using Scriban;
using ModelService.windoModels.templates;
using EmailService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CMS_CORE_NG
{
    public class BackgroundTask : IBackGroungTask// : IHostedService
    {
       
        //private readonly INotesBl _bl;
        private readonly INotesRepo _repo;
        private readonly IEmailSvc _emailSvc;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;

        // public BackgroundTask() { }
        public BackgroundTask( INotesRepo repo, IEmailSvc emailSvc, IMapper mapper, IConfiguration configuration )
        {
            _repo = repo;
            _emailSvc = emailSvc;
            _mapper = mapper;
            _configuration = configuration;
        }
        public string BaseUrl()
        {
           
            return _configuration["urlPrefix"];
        }
      
        //public void SendDailyEmails()
        //{
            //הקפאנו לבינתיים את הדיוורים האוטומטיים
            //RecurringJob.AddOrUpdate("sendDailyEmails", () => getEmailsToSenDaily(),Cron.Hourly(38));

            //var list = await _bl.EmailsAboatNews("");
        //}

        //public async Task sendEmailsAboatNewNote(NoteVM note)
        //{
            //הקפאת שליחת המיילים עבור מודעות חדשות
            //var help = false;
            //if (note.Boards.FirstOrDefault(x => x.Name == "עזרה") != null)
            //{
            //    help = true;
            //}
            //var subject = note.Business.buisnessName + ' ' + EmailTemplates.newNoteSubject;
            //var template = Template.Parse(EmailTemplates.newNoteContentEmail);
            //var result = template.Render(new { Url = BaseUrl() + "notes", Id= note.Id, Header = note.Header });
            //var categoryId = 0;
            //if (note.CategorySubCategory!=null)
            //{
            //    categoryId = note.CategorySubCategory.categoryId;
            //}
            //var list = await _repo.getEmailsByCategotyIdToSendNotes(categoryId, help);
            //foreach (var email in list)
            //{
            //    BackgroundJob.Enqueue(() => sendEmail(email, subject,"", "LinksListToNewNote.html", result));
            //}
        //}

        //public async Task getEmailsToSenDaily()
        //{
        //    //בדיקה שלא שישי או שבת
        //    if (DateTime.Now.DayOfWeek != DayOfWeek.Friday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
        //    {
        //        var list = await _repo.getEmailsToSendDaily();
        //        var newNotesList = await _repo.EmailsAboatNews();
        //        foreach (var business in list)
        //        {
        //            var b = _mapper.Map<Buisness, BuisnessVm>(business);
        //            var listNotesVm = newNotesList.Select(n => _mapper.Map<Note, NoteForCardVM>(n)).ToList();
        //            BackgroundJob.Enqueue(() => sendEmailToUser(b, listNotesVm));
        //        }
        //    }
        // }

        public async Task sendEmailToUser(BuisnessVm business, List<NoteForCardVM> newNotesList)
        {
            var noteList = newNotesList;
               // .Filter(n =>
                //business.BusinessCategoriesNotify.Find(x => x.categoryId == n.CategorySubCategory.categoryId) != null)
                //.ToList();
            if (noteList.Count() > 0)
            {
                var template = Template.Parse(EmailTemplates.dailyContentEmail);
                var result = template.Render(new { Notes = noteList, Url = BaseUrl()+ "notes" });
                //הפסקת דיוורים לבינתיים
                //await sendEmail(business.userId, EmailTemplates.dailySubject+ ' ' + DateTime.Today.ToString("yyyy-MM-dd"), business.userFullName, "LinksListToNewsInSiteDaily.html", result);
            }
        }

        public async Task<bool> sendEmail(string emailTo,string subject, string message,string fileName,string template)
        {
            if (BaseUrl() == "https://windo.org.il/")
            {
                emailTo = "office@windo.org.il";
                await _emailSvc.SendEmailAsync("rut@busoft.co.il", subject, message, fileName, template);
                await _emailSvc.SendEmailAsync("nikita2630014@gmail.com", subject, message, fileName, template);
                await _emailSvc.SendEmailAsync("s0r0kerj3nny@gmail.com", subject, message, fileName, template);
                await _emailSvc.SendEmailAsync("michaelbugay2@gmail.com", subject, message, fileName, template);
                await _emailSvc.SendEmailAsync("esterkor@busoft.co.il", subject, message, fileName, template);
                await _emailSvc.SendEmailAsync("yaeld@busoft.co.il", subject, message, fileName, template);
            }

            await _emailSvc.SendEmailAsync(emailTo, subject, message, fileName, template);
            return true;
        }
        #region bg task old
        //private Timer timer;
        //private int number;
        //private string path = "C:\\TFS\\busoftBase\\CMS_CORE_NG\\";
        //public void Dispose()
        //{
        //    timer?.Dispose();
        //}

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //timer = new Timer(o => {
        //    //callback
        //    //deleteCashe();

        //},
        //null,
        //TimeSpan.Zero,
        //TimeSpan.FromDays(1));
        //    SendDailyEmails();
        //    return Task.CompletedTask;
        //}
        //public bool deleteCashe()
        //{
        //    if (Directory.Exists(path))
        //    {
        //        try
        //        {
        //            DirectoryInfo di = new DirectoryInfo(path);
        //            var dirlist = di.GetDirectories();

        //            for (var i = dirlist.Length - 1; i >= 0; i--)
        //            {
        //                foreach (DirectoryInfo dir in dirlist[i].GetDirectories())
        //                {
        //                    var files = dir.GetFiles();
        //                    for (int j = 0; j < files.Length; j++)
        //                    {
        //                        files[j].IsReadOnly = true;
        //                        files[j].Delete();
        //                    }
        //                    dir.Delete();
        //                }
        //            }
        //            di.Delete(true);
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }

        //    }
        //    return true;
        //}
        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}
        #endregion
    }

    public interface IBackGroungTask
    {
        //void SendDailyEmails();
        //Task getEmailsToSenDaily();
        //Task sendEmailsAboatNewNote(NoteVM note);
    }
}
