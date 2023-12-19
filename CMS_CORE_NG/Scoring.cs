using DataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG
{
                                                                     
    public class Scoring : IScoring
    {
        private readonly ApplicationDbContext db;
        private readonly IMemoryCache _memoryCache;

        public Scoring(ApplicationDbContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            _memoryCache = memoryCache;
        }
        public bool ScoreBusiness(int scoringActionId, int businessId)
        {

            var countScore = 0;
            var operitionList = db.ScroingOperations
                .Where(s => s.ActionId == scoringActionId)
                .Where(c => c.EventTypeId == 2);
            foreach (var item in operitionList)
            {
                countScore = countScore + AddScore(item, businessId);
            }
            var buisnes = db.Buisness.Find(businessId);
            buisnes.Score = buisnes.Score + countScore;
            db.Buisness.Update(buisnes);
            db.SaveChanges();
            _memoryCache.Remove(CasheKeyes.GetListOfBuisnessesFromCashe);
            return true;
        }
        public bool ScoreBuisnessOP(int scoringOperitionId, int businessId)
        {
            var countScore = 0;
            var operition = db.ScroingOperations.Find(scoringOperitionId);
            if (operition.FromDate == null || operition.FromDate <= DateTime.Today && operition.TillDate == null || operition.TillDate >= DateTime.Today)
            {
                countScore = AddScore(operition, businessId);
            }
            var buisnes = db.Buisness.Find(businessId);
            buisnes.Score = buisnes.Score + countScore;
            db.Buisness.Update(buisnes);
            db.SaveChanges();
            _memoryCache.Remove(CasheKeyes.GetListOfBuisnessesFromCashe);
            return true;
        }
        public int AddScore(ScroingOperation operitionObject, int buisnessId)
        {
            var countScore = 0;
            if (operitionObject.FromDate == null || operitionObject.FromDate <= DateTime.Today && operitionObject.TillDate == null || operitionObject.TillDate >= DateTime.Today)
            {
                db.BusinessScorings.Add(new BusinessScoring()
                {
                    BusinessId = buisnessId,
                    ScoringOperationId = operitionObject.Id,
                    Date = DateTime.Now,
                    Count = countScore + operitionObject.Count
                });
                countScore += operitionObject.Count;
            }
            return countScore;
        }
        public async Task<bool> ScoreBusinessByUserEmail(string email)
        {
            var business = await db.Buisness.FirstOrDefaultAsync(x => x.userId == email);
            if (business != null)
            {
                return ScoreBusiness(2, business.Id);
            }
            return false;
        }

        public interface IScoring
        {
            bool ScoreBusiness(int scoringActionId, int businessId);
            bool ScoreBuisnessOP(int scoringOperitionId, int businessId);
            Task<bool> ScoreBusinessByUserEmail(string email);


        }
    }
}
