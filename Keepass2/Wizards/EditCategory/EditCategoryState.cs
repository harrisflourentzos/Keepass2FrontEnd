using System;

namespace Keepass2.Wizards.EditCategory
{
    public class EditCategoryState
    {
        public string OldCategory { get; set; }
        public string NewCategory { get; set; }
        public Action OnConfirm { get; set; }
    }
}
