using System;

namespace models.Enums
{
    [Flags]
    public enum LogCat
    {
        None   = 0,   // Used to create an empty line in the log when the server is started.
        Debug  = 1,      // 0b1
        Info   = 1 << 1, // Ob10
        Warn   = 1 << 2, // 0b100
        Error  = 1 << 3, // 0b1000
        Fatal  = 1 << 4, // 0b10000
        RageMp = 1 << 5  // 0b100000
    }
}
