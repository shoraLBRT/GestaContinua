using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GestaContinua.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public BotController(
            ITelegramBotClient botClient,
            IUserRepository userRepository,
            ITaskRepository taskRepository)
        {
            _botClient = botClient;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] Update update)
        {
            try
            {
                if (update.Message != null)
                {
                    await HandleMessageAsync(update.Message);
                }
                else if (update.CallbackQuery != null)
                {
                    await HandleCallbackQueryAsync(update.CallbackQuery);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error processing update: {ex.Message}");
                return Ok();
            }
        }

        private async Task HandleMessageAsync(Message message)
        {
            if (message.Text != null)
            {
                var command = message.Text.Split(' ')[0];

                switch (command.ToLower())
                {
                    case "/start":
                        await HandleStartCommand(message);
                        break;
                    case "/add_task":
                        await HandleAddTaskCommand(message);
                        break;
                    case "/report":
                        await HandleReportCommand(message);
                        break;
                    case "/list_tasks":
                        await HandleListTasksCommand(message);
                        break;
                    default:
                        await SendMessageAsync(message.Chat.Id, "Unknown command. Available commands: /start, /add_task, /report, /list_tasks");
                        break;
                }
            }
        }

        private async Task HandleStartCommand(Message message)
        {
            var telegramId = message.From.Id;

            // Check if user exists
            var existingUser = await _userRepository.GetByTelegramIdAsync(telegramId);
            if (existingUser == null)
            {
                // Create new user
                var newUser = new User
                {
                    TelegramId = telegramId,
                    CreatedAt = DateTime.UtcNow,
                    LastActiveAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _userRepository.CreateAsync(newUser);
            }

            await SendMessageAsync(message.Chat.Id, "Welcome to GestaContinua! Use /add_task to create a new task, /list_tasks to see your tasks, and /report to update your progress.");
        }

        private async Task HandleAddTaskCommand(Message message)
        {
            await SendMessageAsync(message.Chat.Id, "Adding tasks via command is not implemented yet. You can use the web API to create tasks.");
        }

        private async Task HandleReportCommand(Message message)
        {
            await SendMessageAsync(message.Chat.Id, "Reporting progress via command is not implemented yet. You can use the web API to update progress.");
        }

        private async Task HandleListTasksCommand(Message message)
        {
            var telegramId = message.From.Id;
            var user = await _userRepository.GetByTelegramIdAsync(telegramId);

            if (user == null)
            {
                await SendMessageAsync(message.Chat.Id, "Please use /start first to register.");
                return;
            }

            var tasks = await _taskRepository.GetActiveByUserIdAsync(user.Id);
            var tasksList = "Your active tasks:\n";

            foreach (var task in tasks)
            {
                tasksList += $"• {task.Name}: {task.Progress}/{task.Goal} ({task.Status})\n";
            }

            if (string.IsNullOrEmpty(tasksList))
            {
                tasksList = "You don't have any active tasks. Use /add_task to create one.";
            }

            await SendMessageAsync(message.Chat.Id, tasksList);
        }

        private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            // Handle inline keyboard callbacks
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
        }

        private async Task SendMessageAsync(long chatId, string text)
        {
            await _botClient.SendTextMessageAsync(chatId, text);
        }
    }
}