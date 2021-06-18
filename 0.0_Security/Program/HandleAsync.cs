using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#pragma warning disable CS8846

namespace Security
{
    class Handle
    {
        public static async Task UpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = (update.Type) switch
            {
                UpdateType.Message => MessageReceived(update.Message),
                _                  => UnknownReceived(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await ErrorAsync(botClient, exception, cancellationToken);
            }
        }

        public static Task ErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private static async Task MessageReceived(Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            using (var module = new Module())
            {
                var prompt = message.Text.Split(' ').First();
                var prefix = Configuration.Variables["prefix"];
                await await ((prompt) switch {
                    _ when prompt.Contains($"{prefix}ping") => module.Ping(message),
                    _ when prompt.Contains($"/help") => module.Usage(message),
                });
            }
        }

        private static Task UnknownReceived(Update update)
        {
            Console.WriteLine($"Unknown Update: {update.Type}");
            return UnknownReceived(update);
        }
    }
}
