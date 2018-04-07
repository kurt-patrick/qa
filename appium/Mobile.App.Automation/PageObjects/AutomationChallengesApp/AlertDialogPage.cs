using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class AlertDialogPage : PageBase
    {
        public struct EquationComponents
        {
            public int Lhs;
            public int Rhs;
            public string Expression;
            public int ActualAnswer;
            public int ExpectedAnswer;
            public bool IsCorrect;
        }

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "message")]
        private IWebElement _message = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "button1")]
        private IWebElement _yes = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "button2")]
        private IWebElement _no = null;

        readonly By _actualLocator = By.Id("txtActual");

        public string Actual => GetText(_actualLocator, true);
        public string MathQuestion => _message.Text.Trim();

        public AlertDialogPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public EquationComponents GetEquationComponents()
        {
            var array = MathQuestion.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if(array.Length != 5)
            {
                throw new Exceptions.InvalidStateException("Math equation should contains 5 components <lhs> <+-*> <rhs> <==> <answer>");
            }
            var retVal = new EquationComponents()
            {
                Lhs = int.Parse(array[0]),
                Expression = array[1],
                Rhs = int.Parse(array[2]),
                ActualAnswer = int.Parse(array[4])
            };

            switch (retVal.Expression)
            {
                case "+":
                    retVal.ExpectedAnswer = retVal.Lhs + retVal.Rhs;
                    break;
                case "-":
                    retVal.ExpectedAnswer = retVal.Lhs - retVal.Rhs;
                    break;
                case "*":
                    retVal.ExpectedAnswer = retVal.Lhs * retVal.Rhs;
                    break;
                default:
                    throw new Exceptions.InvalidStateException($"The math equation must be of type  [ '+', '-', '*' ] '{retVal.Expression}' is not supported");
            }

            retVal.IsCorrect = retVal.ExpectedAnswer == retVal.ActualAnswer;

            return retVal;
        }

        public bool IsAnswerCorrect()
        {
            var components = GetEquationComponents();
            return components.IsCorrect;
        }

        public void AnswerQuestion(bool correct)
        {
            if (correct)
            {
                Yes();
            }
            else
            {
                No();
            }

            // Wait for the "actual" value to be displayed
            IsVisible(_actualLocator);
        }

        public void Yes()
        {
            _yes.Click();
        }

        public void No()
        {
            _no.Click();
        }

        public override bool IsLoaded()
        {
            return _yes.Displayed && _no.Displayed && _message.Displayed;
        }

    }
}
