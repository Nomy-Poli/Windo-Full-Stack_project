using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_CORE_NG.Repository;
using AutoMapper;
using ModelService.windoModels;
using Microsoft.Extensions.Caching.Memory;
using Scriban;
using ModelService.windoModels.templates;

using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG.BL
{
    public class NotesBl : INotesBl
    {
        private readonly IMapper _mapper;
        private readonly INotesRepo _repo;
        private readonly IScoring _scoring;
        private readonly IMemoryCache _memoryCache;

        public NotesBl(IMapper mapper, INotesRepo repo, IScoring scoring, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _repo = repo;
            _scoring = scoring;
            _memoryCache = memoryCache;
        }

        public async Task <NoteVM> CreateNote(NoteVM model,int? boardId)
        {
            try
            {
                _scoring.ScoreBusiness(4, model.BusinessId);
                model.CreatetionDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                var newNote =await _repo.CreateNote(_mapper.Map<Note>(model), boardId);
                return _mapper.Map<NoteVM>(newNote);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> CreateReplayToNote(NoteReplayVM model)
        {
            try
            {
                _scoring.ScoreBusiness(5, model.BusinessId);
                model.CreationDate = DateTime.Now;
                var id = await _repo.CreateReplayToNote(_mapper.Map<NoteReplay>(model));
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task <int> CreateReplayToNoteMessage(ReplayNoteMessageVM model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                var id =await _repo.CreateReplayToNoteMessage(_mapper.Map<ReplayNoteMessage>(model));
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task <bool> UpdateReplayNote(NoteReplayVM model)
        
            {
                try
                {
                    return await _repo.UpdateReplayNote(_mapper.Map<NoteReplay>(model));
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
                return await _repo.DeleteNote(noteId, reason);
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
                return await _repo.DeleteReplayNote(replayId, reason);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NoteWithReplayVM GetNoteById(int noteId)
        {
            try
            {
                var note = _mapper.Map<NoteWithReplayVM>(_repo.GetNoteById(noteId));
                //note.ReplayNoteMessages = getReplayNoteMessages(noteId);
                return note;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NoteReplayVM> getNoteReplays(int noteId)//,int? currentBuisnessId
        {
            //if(currentBuisnessId!=null)
            //{
            //    _scoring.ScoreBusiness(10,(int) currentBuisnessId);
            //}
            
            var list = _repo.getNoteReplays(noteId).Select(r => _mapper.Map<NoteReplayVM>(r)).ToList();
            return list;
        }
        public List<NoteVM>  getBoardsWithNotes(NoteSearchParameters searchParameters)
        {
            try
            {
                var list = _repo.getBoardsWithNotes(searchParameters).Select(b => _mapper.Map<Note, NoteVM>(b)).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BoardForCardVM> GetLatestNotes()
        {
            try
            {
                var noteList = _repo.getBoardsWithNotes(null);
                var boards = _repo.getBoards().Select(n => _mapper.Map<Board, BoardForCardVM>(n)).ToList();
                foreach (var board in boards)
                {
                    board.Notes = noteList.Where(n => n.NotesBoards.FirstOrDefault(nb => nb.BoardId == board.Id) != null).Take(6).Select(n => _mapper.Map<Note, NoteForCardVM>(n)).ToList();
                }
                return boards;
            }
            catch (Exception) { throw; }
        }
        public async Task <bool> UpdateNote(NoteVM model)
        {
            try
            {
                return await _repo.UpdateNote(_mapper.Map<Note>(model));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReplayNoteMessageVM> getReplayNoteMessages(int noteId)
        {
            try
            {
                return _repo.getReplayNoteMessages(noteId).Select(r => _mapper.Map<ReplayNoteMessage, ReplayNoteMessageVM>(r)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BoardVM> getBoards()
        {
            try
            {
                return _repo.getBoards().Select(b => _mapper.Map<Board, BoardVM>(b)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> EmailsAboatNews(string callBackURL)
        {
            var list = await _repo.EmailsAboatNews();
            var listForCards = list.Select(n => _mapper.Map<NoteForCardVM>(n));
            var template = Template.Parse(EmailTemplates.dailyContentEmail);
            var result = template.Render(new { Notes = listForCards, Url = callBackURL });
            return result;
        }

        public async Task<List<string>> getEmailsByCategotyIdToSendNotes(int categoryId, bool? help)
        {
            var emailsList = await _repo.getEmailsByCategotyIdToSendNotes(categoryId, help);
            return emailsList;
        }
        public string getRenderToSendNotes(int noteId, string noteHeader, string callback)
        {
            var template = Template.Parse(EmailTemplates.newNoteContentEmail);
            var result = template.Render(new { Id = noteId, Url = callback, Header = noteHeader });
            return result;
        }
    }
}
