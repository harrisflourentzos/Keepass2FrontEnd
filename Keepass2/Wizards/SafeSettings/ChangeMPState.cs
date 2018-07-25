using System;

namespace Keepass2.Wizards.SafeSettings
{
    public class ChangeMPState
    {
        public string OldMp { get; set; }
        public string NewMp { get; set; }

        public Action OnConfirm { get; set; }
    }
}
