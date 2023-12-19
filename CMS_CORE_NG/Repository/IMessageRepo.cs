using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using ModelService.windoModels;


namespace CMS_CORE_NG.Repository
{
    public interface IMessageRepo
    {
        List<Message> GetMessagesByBusinessId(int businessId);
        List<Message> GetOutgoingMessagesByBusinessId(int businessId);
        Message GetMessageById(int businessId, Guid id);
        Message CreateMessage(Message model);
        int CreateMessageTo(MessagesTo model);
        bool setMessageAsRead(Guid messageId, int businessId, bool isRead, DateTime? tillDate);
        bool setMessageAsDeleted(int messageToId, bool isDelete);
    }
}
