using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Domain.Models;

namespace Messenger.Infrastracture.Mappers;

public class SearchContactMapper : Profile
{
    public SearchContactMapper()
    {
        CreateMap<User, SearchContactDto>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Username));
    }
}