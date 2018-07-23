using System;
using System.Security;
using Keepass2.Model;

namespace Keepass2.Wizards.NewSafe
{
    public class NewSafeState
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public SecureString MasterPassword { get; set; }
        public Action<Safe> OnCompletion { get; set; }
    }
}
