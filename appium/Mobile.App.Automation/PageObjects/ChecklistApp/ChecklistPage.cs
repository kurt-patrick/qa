using KPE.Mobile.App.Automation.Common;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class ChecklistPage : ListViewWrapper
    {
        public ChecklistPage(TestCaseSettings settings) : base(settings, "//android.widget.ListView")
        {
        }

    }
}
