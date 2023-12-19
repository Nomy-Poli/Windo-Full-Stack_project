using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using ModelService.windoModels;

namespace CMS_CORE_NG.BL
{
    public interface IMessageBl
    {
        List<MessageVM> GetMessagesByBusinessId(int businessId);
        List<MessageVM> GetOutgoingMessagesByBusinessId(int businessId);
        MessageVM GetMessageById(int businessId, Guid id);
        int getNewMessagesCount(List<MessageVM> list);
        MessageVM CreateMessage(MessageVM model);
        int CreateMessageTo(MessagesToVM model);
        bool setMessageAsRead(Guid messageId, int businessId, bool isRead);
        bool setMessageAsDeleted(int messageToId, bool isDelete);


    }
}
