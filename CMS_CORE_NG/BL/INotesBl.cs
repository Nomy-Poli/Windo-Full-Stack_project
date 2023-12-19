using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using ModelService.windoModels;


namespace CMS_CORE_NG.BL
{
    public interface INotesBl
    {
        Task <NoteVM> CreateNote(NoteVM model, int? boardId);
        Task <bool> UpdateNote(NoteVM model);
        Task <bool> UpdateReplayNote(NoteReplayVM model);
        Task <bool> DeleteNote(int noteId, int reason);
        Task <bool> DeleteReplayNote(int replayId, int reason);
        Task<int> CreateReplayToNote(NoteReplayVM model);
        Task <int> CreateReplayToNoteMessage(ReplayNoteMessageVM model);
        NoteWithReplayVM GetNoteById(int noteId);
        public List<NoteReplayVM> getNoteReplays(int noteId);
        List<NoteVM> getBoardsWithNotes(NoteSearchParameters searchParameters);
        List<BoardForCardVM> GetLatestNotes();
        //BoardVM GetNotesByBoard(int boardId);
        List<ReplayNoteMessageVM> getReplayNoteMessages(int noteId);
        List<BoardVM> getBoards();




        Task<string> EmailsAboatNews(string callBackURL);
        Task<List<string>> getEmailsByCategotyIdToSendNotes(int categoryId, bool? help);
        string getRenderToSendNotes(int noteId, string noteHeader, string callback);
    }
}
