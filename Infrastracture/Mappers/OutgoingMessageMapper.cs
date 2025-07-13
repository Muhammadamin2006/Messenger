using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class OutgoingMessageMapper : Profile
{
    public OutgoingMessageMapper()
    {
        CreateMap<OutgoingMessage, OutgoingMessageDto>();
        CreateMap<CreateOutgoingMessageDto, OutgoingMessage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SentAt, opt => opt.Ignore())
            .ForMember(dest => dest.Sender, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverUser, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverGroup, opt => opt.Ignore())
            .ForMember(dest => dest.IncomingMessages, opt => opt.Ignore());

    }
   
}