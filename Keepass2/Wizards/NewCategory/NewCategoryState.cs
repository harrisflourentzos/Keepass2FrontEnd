using System;

namespace Keepass2.Wizards.NewCategory
{
    public class NewCategoryState
    {
        public string Category { get; set; }
        public Action OnConfirm { get; set; }
    }
}
