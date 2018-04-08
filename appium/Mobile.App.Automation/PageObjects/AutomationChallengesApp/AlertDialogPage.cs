using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
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

        public MobileElementWrapper Actual => new MobileElementWrapper(_driver, By.Id("txtActual"));
        public MobileElementWrapper MathQuestion => new MobileElementWrapper(_driver, By.Id("message"));
        public MobileElementWrapper NoButton => new MobileElementWrapper(_driver, By.Id("button2"));
        public MobileElementWrapper YesButton => new MobileElementWrapper(_driver, By.Id("button1"));

        public AlertDialogPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public EquationComponents GetEquationComponents()
        {
            var array = MathQuestion.Text().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
                YesButton.Click();
            }
            else
            {
                NoButton.Click();
            }
        }

        public override bool IsLoaded()
        {
            return YesButton.Displayed() && NoButton.Displayed() && MathQuestion.Displayed();
        }

    }
}
