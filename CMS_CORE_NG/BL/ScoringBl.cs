
using AutoMapper;
using AutoMapper.Configuration;
using CMS_CORE_NG.Repository;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.BL;
using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG.BL
{
    public class ScoringBl : IScoringBl
    {
        private readonly IScoringRepo _repository;
        //private readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        private readonly IScoring _scoring;
        public ScoringBl(IMapper mapper, IScoringRepo repository, IScoring scoring)/*, IConfiguration configuration*/
        {
            _mapper = mapper;
            _repository = repository;
            _scoring = scoring;
            //_configuration = configuration;
        }
        public List<ScroingOperationVM> getScroingOperation(int typeId)
        {

            var SP = _repository.getScroingOperation(typeId).Select(x => _mapper.Map<ScroingOperationVM>(x)).ToList();
            return SP;
        }
        public List<ScoringAction> GetScoringActions()
        {

            return _repository.GetScoringActions();

        }
        public async Task<bool> DeleteScroingOperation(int operitionId)
        {
            return await _repository.DeleteScroingOperation(operitionId);
        }
        public async Task<bool> ResetCount(int buisnessId)
        {
            return await _repository.ResetCount(buisnessId);
        }

        public async Task<bool> UpdateScroingOperation(ScroingOperationVM model)
        {
            return await _repository.UpdateScroingOperation(_mapper.Map<ScroingOperation>(model));
        }

        public async Task<int> CreateScroingOperation(ScroingOperationVM model)
        {
            var so = await _repository.CreateScroingOperation(_mapper.Map<ScroingOperation>(model));
            return so;
        }

        public List<BusinessForScoringVM> GetListOfBuisnessesWithScoring()
        {
            try
            {
                List<Buisness> ModelbuisnessesList = _repository.GetListOfBuisnessesWithScoring();
                List<BusinessForScoringVM> buisnessScoringVm = ModelbuisnessesList.Select(_mapper.Map<Buisness, BusinessForScoringVM>).ToList();
                buisnessScoringVm = buisnessScoringVm
                    .OrderByDescending(x => x.Score)
                    .ToList();



                #region get workPicList


                #endregion

                return buisnessScoringVm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BusinessScoringsDetailVM> GetBusinessScoringsDetailById(int buisnessId)
        {
            try
            {
                var order = _repository.GetBusinessScoringsDetailById(buisnessId).Select(x => _mapper.Map<BusinessScoringsDetailVM>(x))
                    .OrderByDescending(x => x.Date)
                    .ToList();
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetScoreToBusiness(int scoringActionId, int buisnessId)
        {
            return _scoring.ScoreBusiness(scoringActionId, buisnessId);
        }

        public bool AddMultipleActions(MultipleActions model)
        {
            var ifSucsess = false;

            for (var a = 0; a < model.scoringActionId.Length; a++)
            {
                for (var b = 0; b < model.buisnessId.Length; b++)
                {
                    ifSucsess = _scoring.ScoreBuisnessOP(model.scoringActionId[a], model.buisnessId[b]);
                }

            }

            return ifSucsess;
        }


    }

}



