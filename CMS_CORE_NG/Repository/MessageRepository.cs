using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService.windoModels;
using ModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

namespace CMS_CORE_NG.Repository
{
    public class MessageRepository : IMessageRepo
    {
        private readonly ApplicationDbContext _db;
        public MessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Message CreateMessage(Message model)
        {
            try
            {
                _db.Messages.Add(model);
                _db.SaveChanges();
                return _db.Messages
                    .Include(m => m.ListMessagesTo).ThenInclude(mt => mt.BuisnessTo)
                    .Include(m => m.BusinessFrom)
                    .FirstOrDefault(m => m.Id.Equals(model.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CreateMessageTo(MessagesTo model)
        {
            try
            {
                _db.MessagesTo.Add(model);
                _db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Message> GetMessagesByBusinessId(int businessId)
        {
            List<Message> messagesForUser = new List<Message>();
            //All Of Messages
            var list = _db.Messages
                .Include(m => m.ListMessagesTo).ThenInclude(mt => mt.BuisnessTo)
                .Where(m => m.ParentMessagesId == null)
                .Include(m => m.BusinessFrom)
                .OrderBy(m => m.CreatedAt)
                .ToList();
            foreach (var mess in list)
            {
                mess.ChildrenMessages = new List<Message>();
                //add the parent message as a first 
                mess.ChildrenMessages.Add(new Message() 
                { 
                    Id = mess.Id,
                    BusinessIdFrom = mess.BusinessIdFrom,
                    MessageText = mess.MessageText,
                    ListMessagesTo = mess.ListMessagesTo,
                    BusinessFrom = mess.BusinessFrom,
                    CreatedAt = mess.CreatedAt
                });
                List<Message> children = _db.Messages
                .Where(m => m.ParentMessagesId == (Guid?)mess.Id)
                .Include(m => m.ListMessagesTo).ThenInclude(m => m.BuisnessTo).ToList()
                .OrderByDescending(m => m.CreatedAt).ToList();

                bool flagAlreadyConnect = false;
                foreach (var mch in children)
                {
                    var messageToRead = mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId);
                    if ((messageToRead != null && messageToRead.IsDeleted != true)
                        || mch.BusinessIdFrom == businessId
                        || (flagAlreadyConnect == true && (messageToRead == null
                            || (messageToRead != null && messageToRead.IsDeleted != true))))
                    {
                        flagAlreadyConnect = true;

                        mess.ChildrenMessages.Add(mch);
                    }
                }
                if (flagAlreadyConnect || mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && mto.IsDeleted != true) != null)
                {
                    mess.ChildrenMessages = mess.ChildrenMessages.OrderBy(m => m.CreatedAt).ToList();
                    messagesForUser.Add(mess);
                }
            }
            return messagesForUser.OrderByDescending(m => m.CreatedAt).ToList();
        }
        public List<Message> GetOutgoingMessagesByBusinessId(int businessId)
        {
            List<Message> outgoingMessagesForUser = new List<Message>();
            //All Of Messages
            var list = _db.Messages
                .Include(m => m.ListMessagesTo)
                .Include(m => m.BusinessFrom)
                .Where(m => m.ParentMessagesId == null)
                .OrderBy(m => m.CreatedAt)
                .ToList();
            foreach (var mess in list)
            {
                mess.ChildrenMessages = new List<Message>();
                //add the parent message as a first 
                mess.ChildrenMessages.Add(new Message()
                {
                    Id = mess.Id,
                    BusinessIdFrom = mess.BusinessIdFrom,
                    MessageText = mess.MessageText,
                    ListMessagesTo = mess.ListMessagesTo,
                    BusinessFrom = mess.BusinessFrom,
                    CreatedAt = mess.CreatedAt
                });
                List<Message> children = _db.Messages
                .Where(m => m.ParentMessagesId == (Guid?)mess.Id)
                .Include(m => m.ListMessagesTo).ThenInclude(m => m.BuisnessTo).ToList()
                .OrderByDescending(m => m.CreatedAt).ToList();

                bool flagAlreadyConnect = false;
                foreach (var mch in children)
                {
                    var messageToRead = mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId);
                    if ((messageToRead != null && messageToRead.IsDeleted != true)
                        || mch.BusinessIdFrom == businessId
                        || (flagAlreadyConnect == true && (messageToRead == null
                            || (messageToRead != null && messageToRead.IsDeleted != true))))
                    {        
                        flagAlreadyConnect = true;

                        mess.ChildrenMessages.Add(mch);
                    }
                }
                if (flagAlreadyConnect || mess.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdFrom == businessId && mto.IsDeleted != true) != null)
                {
                    mess.ChildrenMessages = mess.ChildrenMessages.OrderBy(m => m.CreatedAt).ToList();
                    outgoingMessagesForUser.Add(mess);
                }
            }
            return outgoingMessagesForUser.OrderByDescending(m => m.CreatedAt).ToList();
        }

        public Message GetMessageById(int businessId, Guid id)
        {
            Message messageWithConcatenate;
            var currentMessage =_db.Messages
                .Include(m => m.BusinessFrom)
                .Include(m => m.ListMessagesTo).ThenInclude(mto => mto.BuisnessTo)
                .FirstOrDefault(m => m.Id == id);
            //mark the message as read
            var messageto = currentMessage.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId && mto.IsDeleted != true);
            if (messageto != null && messageto.IsRead != true)
            {
                setMessageAsRead(id, businessId, true, currentMessage.CreatedAt);
            }
            else
            {
                //throw exception 
            }
            if (currentMessage.ParentMessagesId != null)
            {
                messageWithConcatenate = _db.Messages
                    .Include(m => m.BusinessFrom)
                    .Include(m => m.ListMessagesTo)
                    .ThenInclude(mto => mto.BuisnessTo)
                    .FirstOrDefault(m => m.Id == currentMessage.ParentMessagesId);
            }
            else
            {
                messageWithConcatenate = currentMessage;
            }
            messageWithConcatenate.ChildrenMessages = new List<Message>();
            //add the parent message as a first 
            messageWithConcatenate.ChildrenMessages.Add(new Message()
            {
                Id = messageWithConcatenate.Id,
                BusinessIdFrom = messageWithConcatenate.BusinessIdFrom,
                MessageText = messageWithConcatenate.MessageText,
                ListMessagesTo = messageWithConcatenate.ListMessagesTo,
                BusinessFrom = messageWithConcatenate.BusinessFrom
            });
            List<Message> children = _db.Messages
            .Where(m => m.ParentMessagesId == (Guid?)messageWithConcatenate.Id)
            .Include(m => m.ListMessagesTo).ThenInclude(m => m.BuisnessTo).ToList()
            .OrderByDescending(m => m.CreatedAt).ToList();

            bool flagAlreadyConnect = false;
            foreach (var mch in children)
            {
                var messageToRead = mch.ListMessagesTo.FirstOrDefault(mto => mto.BusinessIdTo == businessId);
                if ((messageToRead != null && messageToRead.IsDeleted != true)
                    || mch.BusinessIdFrom == businessId
                    || (flagAlreadyConnect == true && (messageToRead == null
                        || (messageToRead != null && messageToRead.IsDeleted != true))))
                {
                    flagAlreadyConnect = true;

                    messageWithConcatenate.ChildrenMessages.Add(mch);
                }
            }
            messageWithConcatenate.ChildrenMessages = messageWithConcatenate.ChildrenMessages.OrderBy(m => m.CreatedAt).ToList();
            return messageWithConcatenate;
        }

        public bool setMessageAsDeleted(int messageToId, bool isDelete)
        {
            try
            {
                var messageTo = _db.MessagesTo.Find(messageToId);
                messageTo.IsDeleted = isDelete;
                _db.MessagesTo.Update(messageTo);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool setMessageAsRead(MessagesTo messageTo, bool isRead)
        {
            try
            {
                messageTo.IsRead = isRead;
                messageTo.IsNew = false;
                //_db.MessagesTo.Update(messageTo);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool setMessageAsRead(Guid messageId,int businessId, bool isRead, DateTime? tillDate)
        {
            try
            {
                var concatenateMessages = _db.Messages.Where(m => m.ParentMessagesId == messageId || m.Id == messageId && (tillDate == null || tillDate > m.CreatedAt)).ToList();
                var messagesTo = concatenateMessages.Map(m => _db.MessagesTo.FirstOrDefault(mto => mto.MessageId == m.Id && mto.BusinessIdTo == businessId)).ToList();
                //set the Concatenate as read
                foreach (var mto in messagesTo)
                {
                    if (mto != null)
                    {
                        mto.IsRead = isRead;
                        mto.IsNew = false;
                        _db.MessagesTo.Update(mto);
                    }
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getNewMessagesCount(string email)
        {
            throw new NotImplementedException();
        }
    }
}
