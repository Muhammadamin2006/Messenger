using AutoMapper;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class IncomingService : IIncomingService
{
    private readonly IIncomingRepository _incomingRepository;
    private readonly IMapper _mapper;

    public IncomingService(IIncomingRepository incomingRepository, IMapper mapper)
    {
        _incomingRepository = incomingRepository;
        _mapper = mapper;
    }

    public async Task<List<IncomingMessageDto>> GetMessagesByReceiverIdAsync(Guid receiverId)
    {
        var messages = await _incomingRepository.GetByReceiverIdAsync(receiverId);
        return _mapper.Map<List<IncomingMessageDto>>(messages);
    }

    
    public async Task MarkAsReadAsync(Guid id)
    {
        var message = await _incomingRepository.GetByIdAsync(id);
        if (message == null)
            throw new Exception("Сообщение не найдено");

        message.IsRead = true;
        await _incomingRepository.SaveChangesAsync();
    }

}