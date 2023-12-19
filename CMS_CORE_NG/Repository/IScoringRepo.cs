

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelService.windoModels;

namespace CMS_CORE_NG.Repository
{
    public interface IScoringRepo
    {
        List<ScroingOperation> getScroingOperation(int typeId);
        List<Buisness> GetListOfBuisnessesWithScoring();
        List<BusinessScoring> GetBusinessScoringsDetailById(int buisnessId);
        Task<bool> DeleteScroingOperation(int operitionId);
        Task<bool> ResetCount(int buisnessId);
        Task<int> CreateScroingOperation(ScroingOperation scroingOperation);
        Task<bool> UpdateScroingOperation(ScroingOperation scroingOperation);
        List<ScoringAction> GetScoringActions();
    }
}


