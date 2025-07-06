using Messenger.DTOs.Messages;
namespace Messenger.Services.MessageServices;
using System.Threading.Tasks;


public interface IIncomingMessageService
{
    Task<IncomingMessageDto> GetByIdAsync(Guid id);  
}