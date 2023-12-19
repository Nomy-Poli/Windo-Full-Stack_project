using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelService.windoModels;
using CMS_CORE_NG.BL;
using Serilog;
using ModelService.windoModels.templates;
using Hangfire;

namespace CMS_CORE_NG.Controllers
{

    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailSvc _emailSvc;
        private readonly INotesBl _bl;
        private readonly IBackGroungTask _bgTask;

        public NoteController( IMemoryCache memoryCache, IEmailSvc emailSvc, INotesBl bl, IBackGroungTask backgroundTask )
        {
            _memoryCache = memoryCache;
            _emailSvc = emailSvc;
            _bl = bl;
            _bgTask = backgroundTask;
        }
        [HttpPost]
        [Route("CreateNote")]
        public async Task <NoteVM> CreateNote([FromBody]NoteVM model, int? boardId)
        {
            try
            {
                var newNote =await _bl.CreateNote(model, boardId);
                _memoryCache.Remove(CasheKeyes.ListOfLatestNotesFromCache);
                _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache);
                if (boardId != null)
                {
                    _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache + "_" + boardId);
                }
                //BackgroundJob.Enqueue(() => _bgTask.sendEmailsAboatNewNote(newNote));
                return newNote;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("CreateReplayToNote")]
        public async Task<int> CreateReplayToNote([FromBody] NoteReplayVM model,int BusinessId, string authorEmail,string businessName)
        {
            //להוסיף בפונקציה בקליינט את ה BusinessId
            try
            {
                var id = await _bl.CreateReplayToNote(model);

                _memoryCache.Remove(CasheKeyes.ListOfLatestNotesFromCache);
                _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache);
                var subject = $" {businessName} הגיבה לך למודעה באתר WINDO";
               
                //בדיקה שהמייל לא נשלח למגיב בעצמו
                if (model.BusinessId != BusinessId)
                {
                    //link to the message
                    var callBackURL = Url.ActionLink("", "notes", new { note = model.NoteId }, protocol: HttpContext.Request.Scheme);
                    BackgroundJob.Enqueue(() =>
                        _emailSvc.SendEmailAsync(authorEmail, subject, callBackURL, "NewReplayToNote.html", businessName)
                    );
                    //  Log.Information($"Email About New Replay To Note Was Sent => { model }");

                }

                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("CreateReplayToNoteMessage")]
        public async Task <int> CreateReplayToNoteMessage([FromBody] ReplayNoteMessageVM model)
        {
            try
            {
                var id =await _bl.CreateReplayToNoteMessage(model);
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateReplayNote")]
        public async Task <bool> UpdateReplayNote([FromBody] NoteReplayVM model)
        {
            try
            {
                var success =await _bl.UpdateReplayNote(model);
                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("DeleteNote")]
        public async Task <bool> DeleteNote(int noteId, int reason)
        {
            try
            {
                _memoryCache.Remove(CasheKeyes.ListOfLatestNotesFromCache);
                _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache);
                return await _bl.DeleteNote(noteId, reason);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("DeleteReplayNote")]
        public async Task <bool> DeleteReplayNote(int replayId, int reason)
        {
            try
            {
                _memoryCache.Remove(CasheKeyes.ListOfLatestNotesFromCache);
                _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache);
                return await _bl.DeleteReplayNote(replayId, reason);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetNoteById")]
        public NoteWithReplayVM GetNoteById(int noteId)
        {
            try
            {
                var note = _bl.GetNoteById(noteId);
                return note;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getNoteReplays")]
        public List<NoteReplayVM> getNoteReplays(int noteId)
        {
            try
            {
                return _bl.getNoteReplays(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("getBoardsWithNotes")]
        public List<NoteVM> getBoardsWithNotes([FromBody]NoteSearchParameters searchParameters)
        {
            try
            {
                List<NoteVM> MLCacheValue;
                List<NoteVM> list;
                string casheField = "";
                if (searchParameters == null)
                {
                    casheField = CasheKeyes.ListOfNotesFromCache;
                }
                else if (searchParameters.BoardId != null&& searchParameters.Latest ==null)
                {
                   casheField =  CasheKeyes.ListOfNotesFromCache + "_" + searchParameters.BoardId;
                }
                if (casheField == "" || !_memoryCache.TryGetValue(casheField, out MLCacheValue))
                {
                    list = _bl.getBoardsWithNotes(searchParameters);
                    if (casheField != "")
                    {
                        MLCacheValue = list;
                        _memoryCache.Set(casheField, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));

                    }
                }
                else
                {
                    list = MLCacheValue;
                }
                
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        } 

        [HttpGet("GetLatestNotes")]
        public List<BoardForCardVM> GetLatestNotes()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfLatestNotesFromCache, out List<BoardForCardVM> MLCacheValue))
                {
                    MLCacheValue = _bl.GetLatestNotes();
                    _memoryCache.Set(CasheKeyes.ListOfLatestNotesFromCache, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                }
                return MLCacheValue;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        [HttpPost]
        [Route("UpdateNote")]
        public async Task <bool> UpdateNote([FromBody]NoteVM model)
        {
            try
            {
                _memoryCache.Remove(CasheKeyes.ListOfLatestNotesFromCache);
                _memoryCache.Remove(CasheKeyes.ListOfNotesFromCache);
                return await _bl.UpdateNote(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getReplayNoteMessages")]
        public List<ReplayNoteMessageVM> getReplayNoteMessages(int noteId)
        {
            try
            {
                return _bl.getReplayNoteMessages(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getBoards")]
        public List<BoardVM> getBoards()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfBoardsFromCache, out List<BoardVM> MLCacheValue))
                {
                    MLCacheValue = _bl.getBoards();
                    _memoryCache.Set(CasheKeyes.ListOfBoardsFromCache, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(6)));
                }
                return MLCacheValue;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region send email aboat news in site
        [HttpGet("EmailsAboatNews")]
        public async Task<bool> EmailsAboatNews(string emailTo)
        {
            //ביטלנו לבינתיים את הדיוורים על מודעות חדשות
            //await _bgTask.getEmailsToSenDaily();
            return true;
        }
        [HttpGet("EmailsAboatNewNote")]
        public async Task<bool> EmailsAboatNewNote(int categoryId, int noteId)
        {
            var EmailsList = await _bl.getEmailsByCategotyIdToSendNotes(categoryId, true);
            var callBackURL = Url.ActionLink("", "notes", protocol: HttpContext.Request.Scheme);
            var stringHtml = _bl.getRenderToSendNotes(noteId, "", callBackURL);
            var subject = EmailTemplates.dailySubject;
            //send emails with hangfire
            return true;
        }

        #endregion


    }
}
