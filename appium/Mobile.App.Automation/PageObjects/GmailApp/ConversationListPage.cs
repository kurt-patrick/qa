using KPE.Mobile.App.Automation.Common;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class ConversationListPage : ListViewWrapper
    {
        public ConversationListPage(TestCaseSettings settings) : base(settings, "//android.widget.ListView")
        {
        }

    }
}
