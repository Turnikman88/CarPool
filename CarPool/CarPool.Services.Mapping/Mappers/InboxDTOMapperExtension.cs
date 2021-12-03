using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;

namespace CarPool.Services.Mapping.Mappers
{
    public static class InboxDTOMapperExtension
    {
        public static InboxDTO GetDTO(this Inbox model)
        {
            return new InboxDTO
            {
                Author = model.FromUserId.ToString(),
                Recipient = model.ApplicationUserId.ToString(),
                Message = model.Message,
                SendOnDate = model.CreatedOn
            };
        }
    }
}
