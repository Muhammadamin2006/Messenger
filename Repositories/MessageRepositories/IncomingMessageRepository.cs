using Messenger.Infrastractures.Database;
using Messenger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.MessageRepositories;

public class IncomingMessageRepository  : IIncomingMessageRepository
{
    private readonly MessengerContext _context;

    public IncomingMessageRepository(MessengerContext context)
    {
        _context = context;
    }
    
    public async Task<IncomingMessage> GetByIdAsync(Guid id)
    {
            return await _context.IncomingMessages
            .Include(i => i.OutcomingMessage)
                .ThenInclude(m => m.Sender)
            
            .Include(i => i.OutcomingMessage)
                .ThenInclude(m => m.ReceiverUser)
            
            .Include(i => i.OutcomingMessage)
                .ThenInclude(m => m.ReceiverGroup)
            
            .Include(i => i.Receiver)
                .FirstOrDefaultAsync(i => i.Id == id);
    }
}