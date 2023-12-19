using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

using CMS_CORE_NG.Repository;
using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG.BL
{
    public class MessageBl : IMessageBl
    {
        private readonly IMessageRepo _repository;
        private readonly IScoring _scoring;
        public readonly IMapper _mapper;
        private IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public MessageBl(IMapper mapper, IMessageRepo repository, IScoring scoring, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _repository = repository;
            _scoring = scoring;
            _mapper = mapper;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        public MessageVM CreateMessage(MessageVM model)
        {
            try
            {
                _scoring.ScoreBusiness(12,model.BusinessIdFrom );
                model.CreatedAt = DateTime.Now;
                model.Id = Guid.NewGuid();
                Message newMessage = _repository.CreateMessage(_mapper.Map<Message>(model));
                return _mapper.Map<MessageVM>(newMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateMessageTo(MessagesToVM model)
        {
            try
            {
                int id = _repository.CreateMessageTo(_mapper.Map<MessagesTo>(model));
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MessageVM> GetMessagesByBusinessId(int businessId)
        {
            try
            {
                List<Message> list = _repository.GetMessagesByBusinessId(businessId);
                List<MessageVM> listVM = new List<MessageVM>();
                #region set isCurrentUserRead, isCurrentUserNew, LastUpdate
                foreach (Message message in list)
                {
                    MessageVM mess = _mapper.Map<MessageVM>(message);
                    mess.isCurrentUserRead = true;
                    mess.LastUpdate = mess.CreatedAt;
                    if (mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && (mto.IsRead == false || mto.IsRead == null)) != null)
                    {
                        mess.isCurrentUserRead = false;
                    }
                    if (mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && mto.IsNew == true ) != null)
                    {
                        mess.isCurrentUserNew = true;
                    }
                    if (mess.ChildrenMessages.Count()>0)
                    {
                        foreach (var mch in mess.ChildrenMessages)
                        {
                            if (mch.ListMessagesTo.FirstOrDefault(mto=>mto.BusinessIdTo == businessId && mto.IsDeleted!=true && mto.IsRead!=true)!=null)
                            {
                                mch.isCurrentUserRead = false;
                                mess.isCurrentUserRead = false;
                                if(mess.CreatedAt < mch.CreatedAt)
                                {
                                    mess.LastUpdate = mch.CreatedAt;
                                }
                                
                            }
                            else
                            {
                                mch.isCurrentUserRead = true;
                            }
                            if(mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && mto.IsDeleted != true && mto.IsNew == true) != null)
                            {
                                mch.isCurrentUserNew = true;
                                mess.isCurrentUserNew = true;
                            }
                        }
                    }
                    listVM.Add(mess);
                }
                #endregion
                return listVM.OrderByDescending(m=>m.LastUpdate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MessageVM> GetOutgoingMessagesByBusinessId(int businessId)
        {
            try
            {
                List<Message> list = _repository.GetOutgoingMessagesByBusinessId(businessId);
                List<MessageVM> listVM = new List<MessageVM>();
                #region set isCurrentUserRead, isCurrentUserNew, LastUpdate
                foreach (Message message in list)
                {
                    MessageVM mess = _mapper.Map<MessageVM>(message);
                    mess.isCurrentUserRead = true;
                    mess.LastUpdate = mess.CreatedAt;
                    if (mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId && (mto.IsRead == false || mto.IsRead == null)) != null)
                    {
                        mess.isCurrentUserRead = false;
                    }
                    if (mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId && mto.IsNew == true) != null)
                    {
                        mess.isCurrentUserNew = true;
                    }
                    if (mess.ChildrenMessages.Count() > 0)
                    {
                        foreach (var mch in mess.ChildrenMessages)
                        {
                            if (mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId && mto.IsDeleted != true && mto.IsRead != true) != null)
                            {
                                mch.isCurrentUserRead = false;
                                mess.isCurrentUserRead = false;
                                if (mess.CreatedAt < mch.CreatedAt)
                                {
                                    mess.LastUpdate = mch.CreatedAt;
                                }

                            }
                            else
                            {
                                mch.isCurrentUserRead = true;
                            }
                            if (mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId && mto.IsDeleted != true && mto.IsNew == true) != null)
                            {
                                mch.isCurrentUserNew = true;
                                mess.isCurrentUserNew = true;
                            }
                        }
                    }
                    listVM.Add(mess);
                }
                #endregion
                return listVM.OrderByDescending(m => m.LastUpdate).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int getNewMessagesCount(List<MessageVM> messagesList)
        {
            try
            {
                if(messagesList!=null)
                {
                    int newCount = messagesList.Filter(m => m.isCurrentUserNew == true).Count();
                    return newCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MessageVM GetMessageById(int businessId, Guid id)
        {
            try
            {
                MessageVM currentMessage = _mapper.Map<MessageVM>(_repository.GetMessageById(businessId, id));
                //set isCurrentUserRead, LastUpdate
                currentMessage.isCurrentUserRead = true;
                currentMessage.LastUpdate = currentMessage.CreatedAt;
                if (currentMessage.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && (mto.IsRead == false || mto.IsRead == null)) != null)
                {
                    currentMessage.isCurrentUserRead = false;
                }
                if (currentMessage.ChildrenMessages.Count() > 0)
                {
                    foreach (var mch in currentMessage.ChildrenMessages)
                    {
                        if (mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && mto.IsDeleted != true && mto.IsRead != true) != null)
                        {
                            mch.isCurrentUserRead = false;
                            currentMessage.isCurrentUserRead = false;
                        }
                        else
                        {
                            mch.isCurrentUserRead = true;
                        }
                    }
                }
                return currentMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool setMessageAsRead(Guid messageId, int businessId, bool isRead)
        {
            bool success = _repository.setMessageAsRead(messageId, businessId, isRead, null);
            return success;
        }

        public bool setMessageAsDeleted(int messageToId, bool isDelete)
        {
            bool success = _repository.setMessageAsDeleted(messageToId, isDelete);
            return success;
        }
    }
}
