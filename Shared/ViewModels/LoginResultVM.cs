﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public  class LoginResultVM
    {
        public static readonly LoginResultVM Unauthorized = new LoginResultVM();

        public string Token { get; set; }
    }
}
