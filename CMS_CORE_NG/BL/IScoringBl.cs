using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG.BL
{
    public interface IScoringBl
    {
        List<ScroingOperationVM> getScroingOperation(int typeId);
        Task<bool> DeleteScroingOperation(int operitionId);
        Task<bool> ResetCount(int buisnessId);
        List<BusinessForScoringVM> GetListOfBuisnessesWithScoring();
        List<BusinessScoringsDetailVM> GetBusinessScoringsDetailById(int buisnessId);
        Task<bool> UpdateScroingOperation(ScroingOperationVM model);
        Task<int> CreateScroingOperation(ScroingOperationVM model);
        bool GetScoreToBusiness(int scoringActionId, int buisnessId);
        bool AddMultipleActions(MultipleActions model);
        List<ScoringAction> GetScoringActions();
    }
}

