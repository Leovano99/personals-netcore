using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Visionet_Backend_NetCore.Komunikasi;

namespace Visionet_Backend_NetCore.Komunikasi
{
    #region kumpulan enum
    public enum TipeConsole
    {
        Error,
        Warning,
        Info,
        None,
        NewTrans
    }

    public enum TipeServer
    {
        Mvc,
        NonMvc
    }
    #endregion

    public static class Setting_variabel
    {
        public static bool enable_debug = true;
        public static bool enable_tcp_debug = true;

        public static ConsoleBayangan ConsoleBayangan;
        public static Komunikasi_TCPListener Komunikasi_TCPListener;

        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static string ToString<T>(this Nullable<T> nullable, string format) where T : struct
        {
            return String.Format("{0:" + format + "}", nullable.GetValueOrDefault());
        }

        public static string ToString<T>(this Nullable<T> nullable, string format, string defaultValue) where T : struct
        {
            if (nullable.HasValue)
            {
                return String.Format("{0:" + format + "}", nullable.Value);
            }

            return defaultValue;
        }
        
    }
}
