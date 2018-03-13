using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visionet_Backend_NetCore.Handler;

namespace Visionet_Backend_NetCore.Komunikasi
{
    public class ConsoleBayangan
    {

        public ConsoleBayangan() { }

        public void Send(string Message) {
            AdaPaketWriteConsoleArgs m = new AdaPaketWriteConsoleArgs
            {
                Message = Message
            };
            OnAdaPaketWriteConsole(m);
        }

        #region handler event
        public event AdaPaketWriteConsoleEventHandler AdaPaketWriteConsoleListener;
        protected virtual void OnAdaPaketWriteConsole(AdaPaketWriteConsoleArgs e)
        {
            AdaPaketWriteConsoleListener?.Invoke(this, e);
        }
        #endregion
    }
}
