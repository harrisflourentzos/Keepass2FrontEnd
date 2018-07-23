using System;
using Keepass2.Model;

namespace Keepass2.Wizards.OpenSafe
{
    public class OpenSafeState
    {
        public Safe Safe { get; set; }
        public Action<Safe> OnCompletion { get; set; }
    }
}
