﻿using System.Security;

namespace Keepass2
{
    public class NewSafeState
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public SecureString MasterPassword { get; set; }
    }
}
