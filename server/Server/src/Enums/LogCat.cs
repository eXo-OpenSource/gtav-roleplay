using System;

namespace server.Enums
{
    [Flags]
    public enum LogCat : short
    {
        None = 0,   // Used to create an empty line in the log when the server is started.

        Debug = 1,

        Info = 2,

        Warn = 4,

        Error = 8,

        Fatal = 16,

        RageMp = 32
    }
}
