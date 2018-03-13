using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visionet_Backend_NetCore.Handler
{
    #region delegate event handler
    public delegate void AdaPaketWriteConsoleEventHandler(object sender, AdaPaketWriteConsoleArgs e);
    public delegate void AdaPaketReadFromClientEventHandler(object sender, AdaPaketReadFromClientArgs e);
    #endregion

    #region argumen
    public class AdaPaketWriteConsoleArgs : EventArgs
    {
        public string Message { get; set; }
        public AdaPaketWriteConsoleArgs() { }
    }

    public class AdaPaketReadFromClientArgs : EventArgs
    {
        public string Paket { get; set; }
        public AdaPaketReadFromClientArgs() { }
    }
    #endregion
}
