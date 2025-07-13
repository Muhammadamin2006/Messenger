using Messenger.Domain.Models;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastracture.Repositories;

public class OutgoingRepository : GenericRepository<OutgoingMessage>, IOutgoingRepository
{
    private readonly DbSet<OutgoingMessage> _messages;

    public OutgoingRepository(MessengerContext context) : base(context)
    {
        _messages = context.Set<OutgoingMessage>();
    }

    public async Task<List<OutgoingMessage>> GetMessagesBySenderIdAsync(Guid senderId)
    {
        return await _messages
            .Where(m => m.SenderId == senderId)
            .Include(m => m.ReceiverUser)
            .Include(m => m.ReceiverGroup)
            .ToListAsync();
    }

    public void Delete(OutgoingMessage message)
    {
        _messages.Remove(message);
    }
}
