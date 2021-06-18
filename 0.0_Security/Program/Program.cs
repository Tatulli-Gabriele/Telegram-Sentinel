using System;
using Telegram.Bot;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;

namespace Security
{
    class Program
    {
        public static TelegramBotClient Bot;

        internal static async Task Main()
        {
            await Configuration.InitializeFileVariables(".vars");

            Bot = new TelegramBotClient(
                Configuration.Variables["Token"]
            );

            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            var cts = new CancellationTokenSource();

            // Non-blocking receive
            Bot.StartReceiving(
                new DefaultUpdateHandler(
                    Handle.UpdateAsync,
                    Handle.ErrorAsync
                ),
                cts.Token
            );

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }
    }
}
