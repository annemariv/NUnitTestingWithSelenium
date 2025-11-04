using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ShopTARge24TestingNUnit
{
    public class RealEstateTest
    {

        IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
        }

        [Test]
        public void TestRealEstateAddPositive()
        {
            driver.Url = "https://localhost:7282";

            IWebElement idOfLinkElement = driver.FindElement(By.Id("realestate"));
            idOfLinkElement.Click();


            IWebElement idOfCreateButton = driver.FindElement(By.Id("testIdAdd"));
            idOfCreateButton.Click();
            Thread.Sleep(500);

            InsertRealEstateDataPositive(driver);

            IWebElement createSubmitButton = driver.FindElement(By.Id("testICreateSubmit"));
            createSubmitButton.Click();
            Thread.Sleep(500);

            ICollection<IWebElement> elementsToCheck = driver.FindElements(By.Id("testIdTableLocation"));


            IWebElement idOfTestData = driver.FindElement(By.Id("testIdTableLocation"));
            var dataInIndex = idOfTestData.Text;

            IWebElement idOfTestData2 = driver.FindElement(By.Id("testIdTableRoomNumber"));
            var dataInIndex2 = idOfTestData2.Text;

            IWebElement idOfTestData3 = driver.FindElement(By.Id("testIdArea"));
            var dataInIndex3 = idOfTestData3.Text;

            IWebElement idOfTestData4 = driver.FindElement(By.Id("testIdArea"));
            var dataInIndex4 = idOfTestData4.Text;

            Assert.That(dataInIndex, Is.EqualTo("TestLocation_01"));
            Assert.That(dataInIndex2, Is.EqualTo("7"));
            Assert.That(dataInIndex3, Is.EqualTo("321"));
            Assert.That(dataInIndex4, Is.EqualTo("apartment"));
            Console.WriteLine("Test passed");

        }

        [Test]
        public void TestRealEstateAddNegative()
        {
            driver.Url = "https://localhost:7282";

            IWebElement idOfLinkElement = driver.FindElement(By.Id("realestate"));
            idOfLinkElement.Click();


            IWebElement idOfCreateButton = driver.FindElement(By.Id("testIdAdd"));
            idOfCreateButton.Click();
            Thread.Sleep(500);

            InsertRealEstateDataNegative(driver);

            IWebElement createSubmitButton = driver.FindElement(By.Id("testICreateSubmit"));
            createSubmitButton.Click();
            Thread.Sleep(3000);

            //ICollection<IWebElement> elementsToCheck = driver.FindElements(By.Id("testIdTableLocation"));


            //IWebElement idOfTestData = driver.FindElement(By.Id("testIdTableLocation"));
            //var nameInIndex = idOfTestData.Text;         

            //IWebElement idOfTestData2 = driver.FindElement(By.Id("testIdTableRoomNumber"));
            //var nameInIndex2 = idOfTestData2.Text;

            //Assert.That(nameInIndex, Is.EqualTo("TestLocation_01"));
            //Assert.That(nameInIndex2, Is.EqualTo("7"));
            //Console.WriteLine("Test passed");

        }

        private void InsertRealEstateDataPositive(IWebDriver driver)
        {
            IWebElement idOfLocation = driver.FindElement(By.Id("testIdLocation"));
            idOfLocation.SendKeys("TestLocation_01");

            IWebElement idOfRoomNr = driver.FindElement(By.Id("testIdRoomNumber"));
            idOfRoomNr.SendKeys("7");

            IWebElement idOfArea = driver.FindElement(By.Id("testIdArea"));
            idOfArea.Clear(); //kui vaja väli enne puhastada, default value 
            idOfArea.SendKeys("321");

            IWebElement idOfBuildingType = driver.FindElement(By.Id("testIdBuildingType"));
            idOfBuildingType.SendKeys("apartment");

        }
        private void InsertRealEstateDataNegative(IWebDriver driver)
        {
            IWebElement idOfLocation = driver.FindElement(By.Id("testIdLocation"));
            idOfLocation.SendKeys("1234");
            IWebElement idOfRoomNr = driver.FindElement(By.Id("testIdRoomNumber"));
            idOfRoomNr.SendKeys("Twelve");

            IWebElement idOfArea = driver.FindElement(By.Id("testIdArea"));
            idOfArea.SendKeys("Something");

            IWebElement idOfBuildingType = driver.FindElement(By.Id("testIdBuildingType"));
            idOfBuildingType.SendKeys("9876");

        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}