using Messenger.Application.Dtos;
using Messenger.Application.Services;
using Messenger.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Presentation.Controllers;
[ApiController]
[Route("ChatMessage")]
public class ChatMessageController  : ControllerBase
{
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatMessage>> Send([FromBody] CreateChatMessageDto dto)
        {
            var message = await _chatMessageService.SendMessageAsync(dto.ChatId, dto.SenderId, dto.Text);
            return Ok(message);
        }

        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<List<ChatMessage>>> GetByChat(Guid chatId, [FromQuery] Guid userId)
        {
            var messages = await _chatMessageService.GetMessagesByChatIdAsync(chatId, userId);
            return Ok(messages);
        }

        [HttpGet("{messageId}")]
        public async Task<ActionResult<ChatMessage>> GetById(Guid messageId, [FromQuery] Guid userId)
        {
            var message = await _chatMessageService.GetMessageByIdAsync(messageId, userId);
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ChatMessage>> Edit([FromBody] EditChatMessageDto dto)
        {
            var edited = await _chatMessageService.EditMessageAsync(dto.MessageId, dto.SenderId, dto.NewText);
            if (edited == null) return NotFound();
            return Ok(edited);
        }
        
    }