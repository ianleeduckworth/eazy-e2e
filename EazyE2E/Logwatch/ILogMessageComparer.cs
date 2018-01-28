//Copyright 2018 Ian Duckworth

namespace EazyE2E.Logwatch
{
    public interface ILogMessageComparer
    {
        bool Compare(string watchText, string logMessage);
    }
}
