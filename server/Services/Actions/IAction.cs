﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.Actions
{
    public interface IAction
    {
        bool IsTriggered();
    }
}