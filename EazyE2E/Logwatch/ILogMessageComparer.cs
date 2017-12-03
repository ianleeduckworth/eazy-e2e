﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyE2E.Logwatch
{
    public interface ILogMessageComparer
    {
        bool Compare(string watchText, string logMessage);
    }
}
