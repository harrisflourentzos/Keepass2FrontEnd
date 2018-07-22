using System;
using Keepass2.Model;

namespace Keepass2.Wizards.EditCredential
{
    public class EditCredentialState
    {
        public Credential OldCredential { get; set; }
        public Credential NewCredential { get; set; }

        public Action OnConfirm { get; set; }
    }
}
