using ModelService.windoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using ModelService;
using LanguageExt;

namespace CMS_CORE_NG.Repository
{
    public class NotesRepository : INotesRepo
    {
        private readonly ApplicationDbContext _db;

        public NotesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task <Note> CreateNote(Note model,int? boardId)
        {
            try
            {
                 await _db.Notes.AddAsync(model);
                _db.SaveChanges();
                var id = model.Id;
                if (boardId != null && boardId > 0)
                {
                    _db.NotesBoards.Add(new NotesBoards() { Id = 0, BoardId = (int)boardId, NoteId = id });
                    _db.SaveChanges();
                }
                var newNote = _db.Notes.Include(n => n.NotesBoards).ThenInclude(nb => nb.Board)
                    .Include(n => n.CategorySubCategory).ThenInclude(cat => cat.Category)
                    .Include(n => n.CategorySubCategory).ThenInclude(cat => cat.SubCategory)
                    .Include(n => n.Business).FirstOrDefault(n=>n.Id == model.Id);
                    
                return newNote;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> CreateReplayToNote(NoteReplay model)
        {
            try
            {
                await _db.NoteReplays.AddAsync(model);
                var note = _db.Notes.Find(model.NoteId);
                note.LastUpdateDate = DateTime.Now;
                _db.Notes.Update(note);
                await _db.SaveChangesAsync();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task <int> CreateReplayToNoteMessage(ReplayNoteMessage model)
        {
            try
            {
               await _db.ReplayNoteMessages.AddAsync(model);
                _db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task <bool> DeleteNote(int noteId, int reason)
        {
            try
            {
                var note = await _db.Notes.FindAsync(noteId);
                if (note != null)
                {
                    note.IsActive = false;
                    note.ChangedStatus = reason;
                    note.DeletionDate = DateTime.Now;
                    _db.Notes.Update(note);
                    _db.SaveChanges();
                    return true;
                }
                return false;  
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task <bool> UpdateReplayNote(NoteReplay model)
        {
            try
            {
                var replay = await _db.NoteReplays.FindAsync(model.Id);
                if (replay != null)
                {
                    replay.Text = model.Text;
                    _db.NoteReplays.Update(replay);
                  
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task <bool> DeleteReplayNote(int replayId, int reason)
        {
            try
            {
                var replay = await _db.NoteReplays.FindAsync(replayId);
                if (replay != null)
                {

                    _db.NoteReplays.Remove(replay);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Note GetNoteById(int noteId)
        {
            var note = _db.Notes
                .Include(n => n.NotesBoards).ThenInclude(nb => nb.Board)
                .Include(n => n.CategorySubCategory).ThenInclude(cat => cat.Category)
                .Include(n => n.CategorySubCategory).ThenInclude(cat => cat.SubCategory)
                .Include(n => n.Business)
                .Include(n=> n.ReplayToNotes).ThenInclude(r=>r.Business)
                .FirstOrDefault(n=>n.Id == noteId);
            return note;
        }
        public async Task <bool> UpdateNote(Note model)
        {
            try
            {

                var note = await _db.Notes.FindAsync(model.Id);
                if (note != null)
                {
                    note.CategorySubCategoryId = model.CategorySubCategoryId;
                    note.Header = model.Header;
                    note.Text = model.Text;
                    note.Labels = model.Labels;
                    note.IsBold = model.IsBold;
                    note.IsPayNote = model.IsPayNote;
                    note.IsActive = model.IsActive;
                    note.GroupId = model.GroupId;
                    note.ChangedStatus = model.ChangedStatus;
                    note.LastUpdateDate = DateTime.Now;
                    note.NotesBoards= model.NotesBoards;   
                    note.ExpirationDate = model.ExpirationDate;
                    note.NumberOfViews = model.NumberOfViews;
                    _db.Notes.Update(note);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NoteReplay> getNoteReplays(int noteId)
        {
            var list = _db.NoteReplays
                .Include(r => r.Business)
                .Where(r => r.NoteId == noteId)
                .OrderByDescending(r => r.CreationDate)
                .ToList();
            return list;
        }
        public List<ReplayNoteMessage> getReplayNoteMessages(int noteId)
        {
            var list = _db.ReplayNoteMessages
                .Include(r => r.Business)
                .Where(r => r.NoteId == noteId)
                .OrderByDescending(r=>r.CreationDate)
                .ToList();
            return list;
        }
    
        public List<Note> getBoardsWithNotes(NoteSearchParameters searchParameters)
        {
            var list = _db.Notes
                .Include(n=>n.Business)
                .Include(n=>n.NotesBoards).ThenInclude(nb=>nb.Board)
                .Include(n=>n.CategorySubCategory).ThenInclude(cat=>cat.Category)
                .Include(n=>n.CategorySubCategory).ThenInclude(cat=>cat.SubCategory)
                .Include(n=>n.NetworkingGroup)
                .ThenInclude(n=> n.NetworkingGroupBusinesses)
                .Include(n=>n.ReplayToNotes)   
                .OrderByDescending(n => n.ReplayToNotes.Max(msg => msg.CreationDate)!=null && n.ReplayToNotes.Max(msg => msg.CreationDate)> n.LastUpdateDate ? n.ReplayToNotes.Max(msg => msg.CreationDate) : n.LastUpdateDate)
                .Where(n => n.IsActive == true && (n.ExpirationDate == null || DateTime.Now < n.ExpirationDate) )
                .ToList();

                if (searchParameters.IsManager == true && searchParameters.Latest == null)//מנהל שלא בחר לוח ולא דף הבית
                {
                        return list;
                }
               //if (searchParameters.IsTemech == true)//מודעות מטעם תמך 
               //{
               //      list = list
               //     .Where(n=> n.Business.userId == "office@windo.org.il" || n.Business.userId == "rut@busoft.co.il")
               //     .ToList();    
               //}
                if (searchParameters.BusinessId != null)//אני רשומה  
                {
                    //רשומה בהכול
                    list = list
                   .Where(n => n.GroupId == null || (n.NetworkingGroup.NetworkingGroupBusinesses.FirstOrDefault(ngb => ngb.BusinessId == searchParameters.BusinessId) != null))
                   .ToList();
                    if (searchParameters.getMyNote == true)//אזור אישי 
                    {
                        list = list.Where(n => n.BusinessId == searchParameters.BusinessId).ToList();
                    }
                    if (searchParameters.ForGroups == true)// קבוצות
                    {
                        list = list
                            .Where(n => n.GroupId != null)
                            .Where(n => n.NetworkingGroup.NetworkingGroupBusinesses.FirstOrDefault(ngb => ngb.BusinessId == searchParameters.BusinessId) != null)
                            .ToList();
                    }
                }
                 
                if(searchParameters.BusinessId == null&& searchParameters.IsManager==null)
                {
                     list = list
                    .Where(n => n.GroupId == null)
                    .ToList();
                }
                if (searchParameters.BoardId != null )//לוחות
                {
                        list = list.Where(n => n.NotesBoards.FirstOrDefault(nb => nb.BoardId == searchParameters.BoardId) != null).ToList();
                }

                if (searchParameters.Latest > 0)
                {
                  if (searchParameters.BoardId == null)
                  {
                    var filteredList = list.Where(n => !n.NotesBoards.Any(b => b.BoardId == 3)).ToList();
                    list = filteredList;
                  }
                     list = list.Take((int)searchParameters.Latest).ToList();             
                }  

            return list;
        }
         

//                           if (searchParameters.ForGroups == true && searchParameters.BusinessId != null)
//                   {
//                     list = list
//                            .Where(n=> n.GroupId!=null)
//                            .Where(n=> n.NetworkingGroup.NetworkingGroupBusinesses.FirstOrDefault(ngb=>ngb.BusinessId == searchParameters.BusinessId) != null)
//                            .ToList();
//    }
//                   if(searchParameters.ForGroups==false && searchParameters.BusinessId != null)//עבור כל המודעות - מחובר 
//                   {
//                    list = list
//                        .Where(n => n.GroupId == null || (n.NetworkingGroup.NetworkingGroupBusinesses.FirstOrDefault(ngb => ngb.BusinessId == searchParameters.BusinessId) != null))
//                        .ToList();
//}

//public List<Board> GetLatestNotes()
//{
//    var noteList = getBoardsWithNotes(new NoteSearchParameters() { Latest = 6 });
//    var boards = _db.Boards.ToList();
//    foreach (var board in boards)
//    {
//        board.NotesBoards = 
//    }
//}
        public List<Board> getBoards()
        {
            return _db.Boards.ToList();
        }

        public async Task<List<Note>> EmailsAboatNews()
        {
            var now = DateTime.Now;
            var hours = -24.0;
            if (now.DayOfWeek == DayOfWeek.Sunday)
            {
                hours = -72;
            }
            
            var list = await _db.Notes
                .Include(n=>n.Business)
                .Include(n=>n.CategorySubCategory)
                .Where(n=>n.CreatetionDate> now.AddHours(hours))
                .ToListAsync();
            return list;
        }

        public async Task<List<string>> getEmailsByCategotyIdToSendNotes(int categoryId, bool? help)
        {
            var businessess = await _db.Buisness.Include(b => b.BusinessCategoriesNotify)
                            .Where(b => b.BusinessCategoriesNotify.FirstOrDefault(n => n.categoryId == categoryId) != null
                            || (help == true && b.WantedGetHelpNotification==true))
                            .ToListAsync();
            return businessess.Map(b => b.userId).ToList();
        }
        public async Task<List<string>> getEmailsByWantedGetHelp()
        {
            var businessess = await _db.Buisness
                            .Where(b => b.WantedGetHelpNotification == true)
                            .ToListAsync();
            return businessess.Map(b => b.userId).ToList();
        }

        public async Task<List<Buisness>> getEmailsToSendDaily()
        {
            var businesses = await _db.Buisness
                .Include(b=>b.BusinessCategoriesNotify)
                .Include(u=>u.User)
                          .Where(b => b.WantedGetDailyNotification == true)
                          
                          .ToListAsync();
            return businesses;
        }


    }
}
