using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTest
    {
        private IWebDriver driver;
        private CalculatorPage calculatorPage;

        [SetUp]
        public void Setup()
        {
            // Set up WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            // Create an instance of the CalculatorPage
            calculatorPage = new CalculatorPage(driver);
        }

        [Test]
        public void TestAddition()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(3);
            calculatorPage.EnterValueB(3);
            calculatorPage.SelectOperation("+");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "3 + 3 = 6";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestSubtraction()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(5);
            calculatorPage.EnterValueB(2);
            calculatorPage.SelectOperation("-");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "5 - 2 = 3";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestMultiplication()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(4);
            calculatorPage.EnterValueB(6);
            calculatorPage.SelectOperation("*");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "4 * 6 = 24";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestDivision()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(10);
            calculatorPage.EnterValueB(2);
            calculatorPage.SelectOperation("/");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "10 / 2 = 5";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        

        [Test]
        public void TestLargeNumbers()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter large values and select operation
            calculatorPage.EnterValueA(999999999);
            calculatorPage.EnterValueB(999999999);
            calculatorPage.SelectOperation("+");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "999999999 + 999999999 = 1999999998";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        

        

        [Test]
        public void TestZeroMultiplication()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(0);
            calculatorPage.EnterValueB(5);
            calculatorPage.SelectOperation("*");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "0 * 5 = 0";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        public void TestDivisionByZeroWithDecimalResult()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operation
            calculatorPage.EnterValueA(3);
            calculatorPage.EnterValueB(0);
            calculatorPage.SelectOperation("/");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "3 / 0 = null";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        public void TestMultipleOperations()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter values and select operations
            calculatorPage.EnterValueA(-5);
            calculatorPage.EnterValueB(3);
            calculatorPage.SelectOperation("+");

            // Get the intermediate result
            string intermediateResult = calculatorPage.GetResult();

            // Verify the intermediate result
            Assert.AreEqual("-5 + 3 = -2", intermediateResult);

            // Select the second operation
            calculatorPage.SelectOperation("-");

            // Enter the second value
            calculatorPage.EnterValueB(2);

            // Get the final result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "-5 - 2 = -7";

            // Verify the final result
            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test]
        public void TestSquareRoot()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter value and select operation
            calculatorPage.EnterValueA(25);
            calculatorPage.EnterValueB(25);
            calculatorPage.SelectOperation("/");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "25 / 25 = 1";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }



        [Test]
        public void TestInvalidInput()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter invalid value and select operation
            calculatorPage.EnterValueA("ааа");
            calculatorPage.EnterValueB(5);
            calculatorPage.SelectOperation("+");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "null + 5 = null";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test]
        public void TestInvalidInputResult()
        {
            // Open the calculator page
            calculatorPage.NavigateTo();

            // Enter invalid value and select operation
            calculatorPage.EnterValueA("aaa");
            calculatorPage.EnterValueB("!");
            calculatorPage.SelectOperation("+");

            // Get the result
            string actualResult = calculatorPage.GetResult();
            string expectedResult = "null + null = null";

            // Verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TearDown]
        public void Cleanup()
        {
            // Close the browser
            driver.Quit();
        }
    }

    public class CalculatorPage
    {
        private readonly IWebDriver driver;
        private readonly string url = "https://www.globalsqa.com/angularJs-protractor/SimpleCalculator/";

        private By inputA = By.CssSelector("input[ng-model='a']");
        private By inputB = By.CssSelector("input[ng-model='b']");
        private By operationSelect = By.CssSelector("select[ng-model='operation']");
        private By result = By.ClassName("result");

        public CalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void EnterValueA(int value)
        {
            driver.FindElement(inputA).SendKeys(value.ToString());
        }

        public void EnterValueA(string value)
        {
            driver.FindElement(inputA).SendKeys(value);
        }

        public void EnterValueB(int value)
        {
            driver.FindElement(inputB).SendKeys(value.ToString());
        }

        public void EnterValueB(string value)
        {
            driver.FindElement(inputB).SendKeys(value);
        }

        public void SelectOperation(string operation)
        {
            var operationDropdown = driver.FindElement(operationSelect);
            var selectElement = new SelectElement(operationDropdown);
            selectElement.SelectByText(operation, true);
        }
        
        public void Clear()
        {
            driver.FindElement(inputA).Clear();
            driver.FindElement(inputB).Clear();

            // Reset the operation to the default value
            var operationDropdown = driver.FindElement(operationSelect);
            var selectElement = new SelectElement(operationDropdown);
            selectElement.SelectByValue(""); // Сброс значения операции

            // Ждем, пока результат полностью очистится
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                driver.FindElement(result).Clear();
                return driver.FindElement(result).GetAttribute("value") == "";
            });
        }

        public string GetResult()
        {
            return driver.FindElement(result).Text;
        }
    }
}