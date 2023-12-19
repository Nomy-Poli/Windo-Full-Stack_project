using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelService.windoModels;

namespace CMS_CORE_NG.Repository
{
    public interface INotesRepo
    {
        Task <Note> CreateNote(Note model, int? boardId);
        Task <bool> UpdateNote(Note model);
        Task <bool> UpdateReplayNote(NoteReplay model);
        Task <bool> DeleteNote(int noteId, int reason);
        Task <bool> DeleteReplayNote(int replayId, int reason);
        Task<int> CreateReplayToNote(NoteReplay model);
        Task <int> CreateReplayToNoteMessage(ReplayNoteMessage model);
        Note GetNoteById(int noteId);
        public List<NoteReplay> getNoteReplays(int noteId);
        List<Note> getBoardsWithNotes(NoteSearchParameters searchParameters);
        //List<Board> GetLatestNotes();
        List<ReplayNoteMessage> getReplayNoteMessages(int noteId);
        List<Board> getBoards();



        Task<List<Note>> EmailsAboatNews();
        Task<List<string>> getEmailsByCategotyIdToSendNotes(int categoryId, bool? help);
        Task<List<Buisness>> getEmailsToSendDaily();
    }
}
