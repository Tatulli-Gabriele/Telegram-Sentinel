using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using static Security.Program;

#pragma warning disable CS1998

namespace Security
{
    class Module : IModules, IDisposable
    {
        public async Task<Task> Usage(Message message)
        {
            var prefix = Configuration.Variables["Prefix"];
            return Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: new string(
                    "Here's a list of commands!\n" +
                    $"{prefix}usage - show a list of commands" +
                    $"{prefix}ping - ping the bot" +
                    $"{prefix}mute <user> <time> <reason> - mute an user" +
                    $"{prefix}kick <user> <reason> - kick an user" +
                    $"{prefix}ban <user> <time> <reason> - ban an user"
                ) 
            );
        }

        public async Task<Task> Ping(Message message)
        {
            return Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Pong!"
            );
        }

        public async Task Mute()
        {

        }

        public async Task Kick()
        {
            
        }

        public async Task Ban()
        {
            
        }

        private bool _disposed = false;

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _safeHandle?.Dispose();

            _disposed = true;
        }
    }
}