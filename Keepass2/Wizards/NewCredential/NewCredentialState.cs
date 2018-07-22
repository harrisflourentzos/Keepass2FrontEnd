using System;
using Keepass2.Model;

namespace Keepass2.Wizards.NewCredential
{
    public class NewCredentialState
    {
        public Credential Credential { get; set; }

        public Action OnConfirm { get; set; }
    }
}
