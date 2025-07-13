using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class IncomingMessagesMapper : Profile
{
    public IncomingMessagesMapper()
    {
        
        CreateMap<IncomingMessage, IncomingMessageDto>()
            .ForMember(dest => dest.Text,
                opt => opt.MapFrom(src => src.OutgoingMessage.Text))
            .ForMember(dest => dest.SenderName,
                opt => opt.MapFrom(src => src.OutgoingMessage.Sender.Username))
            .ForMember(dest => dest.ReceiverName,
                opt => opt.MapFrom(src => src.Receiver.Username));

    }
}