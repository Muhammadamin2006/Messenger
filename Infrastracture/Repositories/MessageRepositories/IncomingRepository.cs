using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class IncomingRepository : GenericRepository<IncomingMessage>, IIncomingRepository
{
    private readonly MessengerContext _context;
    private readonly DbSet<IncomingMessage> _incomingMessages;

    public IncomingRepository(MessengerContext context) : base(context)
    {
        _context = context;
        _incomingMessages = context.Set<IncomingMessage>();
    }

    public async Task<List<IncomingMessage>> GetByReceiverIdAsync(Guid receiverId)
    {
        return await _context.IncomingMessages
            .Include(i => i.OutgoingMessage)
            .ThenInclude(o => o.Sender)
            .Include(i => i.Receiver) 
            .Where(i => i.ReceiverId == receiverId)
            .ToListAsync();
    }

}