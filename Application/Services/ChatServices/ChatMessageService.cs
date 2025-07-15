using Messenger.Domain.Models;
using Messenger.Infrastracture.Repositories;

namespace Messenger.Application.Services;

public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatMessageVisibilityRepository _chatMessageVisibilityRepository;

        public ChatMessageService(IChatMessageRepository chatMessageRepository,  IChatMessageVisibilityRepository chatMessageVisibilityRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatMessageVisibilityRepository = chatMessageVisibilityRepository;
        }

        public async Task<ChatMessage> SendMessageAsync(Guid chatId, Guid senderId, string text)
        {
            var message = new ChatMessage
            {
                MessageId = Guid.NewGuid(),
                ReceivedChatId = chatId,
                SenderId = senderId,
                Text = text,
                SentAt = DateTime.UtcNow,
                EditedAt = null,
                IsDeleted = false
            };

            await _chatMessageRepository.AddAsync(message);
            await _chatMessageRepository.SaveChangesAsync(message);

            return message;
        }

        public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId, Guid forUserId)
        {
            var messages = await _chatMessageRepository.GetMessagesByChatIdAsync(chatId);
            var hiddenIds = await _chatMessageVisibilityRepository
                .GetAllAsync(); 

            return messages
                .Where(m => !m.IsDeleted && !hiddenIds.Any(v => v.UserId == forUserId && v.MessageId == m.MessageId))
                .ToList();
        }
        
        public async Task<ChatMessage?> GetMessageByIdAsync(Guid messageId, Guid forUserId)
        {
            var hidden = await _chatMessageVisibilityRepository.IsMessageHiddenForUserAsync(messageId, forUserId);
            if (hidden) return null;

            var message = await _chatMessageRepository.GetMessageByIdAsync(messageId);
            return message?.IsDeleted == true ? null : message;
        }

        public async Task<ChatMessage?> EditMessageAsync(Guid messageId, Guid userId, string newText)
        {
            var message = await _chatMessageRepository.GetByIdAsync(messageId);

            if (message == null || message.IsDeleted || message.SenderId != userId)
                return null;

            message.Text = newText;
            message.EditedAt = DateTime.UtcNow;

            _chatMessageRepository.Update(message);
            await _chatMessageRepository.SaveChangesAsync(message);

            return message;
        }
        
        
    }
