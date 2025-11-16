using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace ShopTARge24TestingNUnit
{
    public class RealEstateTest
    {

        IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282";
        }

        //----------------POSITIVE-----------------

        //Add
        [Test, Order(1)]
        public void TestRealEstateAddPositive()
        {
            IWebElement idOfLinkElement = driver.FindElement(By.Id("realestate"));
            idOfLinkElement.Click();


            IWebElement idOfCreateButton = driver.FindElement(By.Id("testIdAdd"));
            idOfCreateButton.Click();
            Thread.Sleep(500);

            InsertRealEstateDataPositive(driver);


            IWebElement createSubmitButton = driver.FindElement(By.Id("testICreateSubmit"));
            createSubmitButton.Click();
            Thread.Sleep(1000);

            //ICollection<IWebElement> elementsToCheck = driver.FindElements(By.Id("testIdTableLocation"));
            IWebElement locationElement = driver.FindElement(By.Id("testIdTableLocation"));
            IWebElement roomNumberElement = driver.FindElement(By.Id("testIdTableRoomNumber"));
            IWebElement areaElement = driver.FindElement(By.Id("testIdTableArea"));
            IWebElement buildingTypeElement = driver.FindElement(By.Id("testIdBuildingType"));

            Assert.That(locationElement.Text, Is.EqualTo("TestLocation_01"));
            Assert.That(roomNumberElement.Text, Is.EqualTo("7"));
            Assert.That(areaElement.Text, Is.EqualTo("321"));
            Assert.That(buildingTypeElement.Text, Is.EqualTo("apartment"));

            Console.WriteLine("Test passed");
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


        //get id from URL
        private string GetIdFromUrl(IWebDriver driver)
        {
            string url = driver.Url;
            return url.Substring(url.LastIndexOf('/') + 1);
        }

        //Update
        [Test, Order(2)]
        public void TestRealEstateUpdatePositive()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            Thread.Sleep(500);

            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(1000);

            IWebElement updateElementButton = driver.FindElement(By.Id("testIdUpdate"));
            updateElementButton.Click();
            Thread.Sleep(1000);

            string id = GetIdFromUrl(driver);
            Console.WriteLine("ID from URL: " + id);

            UpdateRealEstateDataPositive(driver);
            Thread.Sleep(1000);

            IWebElement updateSubmitButton = driver.FindElement(By.Id("testIdUpdateSubmit"));
            updateSubmitButton.Click();
            Thread.Sleep(1000); 

            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(1000);

            IWebElement dataInIndexElement = driver.FindElement(By.Id("testIdTableLocation"));
            IWebElement dataInIndex2Element = driver.FindElement(By.Id("testIdTableRoomNumber"));
            IWebElement dataInIndex3Element = driver.FindElement(By.Id("testIdTableArea"));
            IWebElement dataInIndex4Element = driver.FindElement(By.Id("testIdBuildingType"));

            Assert.That(dataInIndexElement.Text, Is.EqualTo("TestLocation_02"));
            Assert.That(dataInIndex2Element.Text, Is.EqualTo("10"));
            Assert.That(dataInIndex3Element.Text, Is.EqualTo("123"));
            Assert.That(dataInIndex4Element.Text, Is.EqualTo("hotel"));

            Console.WriteLine("Update test passed");
        }
        
        private void UpdateRealEstateDataPositive(IWebDriver driver)
        {           
            IWebElement idOfLocation = driver.FindElement(By.Id("testIdLocation"));
            idOfLocation.Clear();
            idOfLocation.SendKeys("TestLocation_02");

            IWebElement idOfRoomNr = driver.FindElement(By.Id("testIdRoomNumber"));
            idOfRoomNr.Clear();
            idOfRoomNr.SendKeys("10");

            IWebElement idOfArea = driver.FindElement(By.Id("testIdArea"));
            idOfArea.Clear(); //kui vaja väli enne puhastada, default value 
            idOfArea.SendKeys("123");

            IWebElement idOfBuildingType = driver.FindElement(By.Id("testIdBuildingType"));
            idOfBuildingType.Clear();
            idOfBuildingType.SendKeys("hotel");
        }

        //Delete after all pos and negative have tested
        [Test, Order(5)]
        public void TestRealEstateDeletePositive()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(500);

            IWebElement firstLocation = driver.FindElement(By.Id("testIdTableLocation"));
            string locationToDelete = firstLocation.Text;

            driver.FindElement(By.Id("testIdDelete")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("testSubmitDelete")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(500);

            var locations = driver.FindElements(By.Id("testIdTableLocation"));
            bool stillExists = locations.Any(e => e.Text == locationToDelete);
            Assert.That(stillExists, Is.False);

            Console.WriteLine("Delete test passed");
        }



        //----------------NEGATIVE-------------------

        //Create
        [Test, Order(3)]
        public void TestRealEstateAddNegative()
        {
            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("testIdAdd")).Click();
            Thread.Sleep(500);

            InsertRealEstateDataNegative(driver);

            var rowsBefore = driver.FindElements(By.CssSelector("#RealestateTableTest tbody tr")).Count;

            driver.FindElement(By.Id("testICreateSubmit")).Click();
            Thread.Sleep(1000);

            var rowsAfter = driver.FindElements(By.CssSelector("#RealestateTableTest tbody tr")).Count;

            Assert.That(rowsAfter, Is.EqualTo(rowsBefore), "No new real estate should be added with invalid input");

            Console.WriteLine("Negative add test passed");
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

        [Test, Order(4)]
        public void TestRealEstateUpdateNegative()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            Thread.Sleep(500);

            driver.FindElement(By.Id("realestate")).Click();
            Thread.Sleep(500);

            var rows = driver.FindElements(By.CssSelector("#RealestateTableTest tbody tr"));
            if (rows.Count == 0)
            {
                Console.WriteLine("No rows exist in the table. Skip negative update test.");
                return;
            }

            var firstRow = rows[0];
            string rowBefore = firstRow.Text;

            firstRow.FindElement(By.CssSelector("a[id='testIdUpdate']")).Click();
            Thread.Sleep(500);

            InsertRealEstateDataNegativeForUpdate(driver);

            driver.FindElement(By.Id("testIdUpdateSubmit")).Click();
            Thread.Sleep(1000);

            rows = driver.FindElements(By.CssSelector("#RealestateTableTest tbody tr"));
            if (rows.Count == 0)
            {
                Console.WriteLine("No rows exist after update.");
                return;
            }

            firstRow = rows[0];
            string rowAfter = firstRow.Text;

            Assert.That(rowAfter, Is.EqualTo(rowBefore), "Update should not accept invalid input");

            Console.WriteLine("Negative update test passed!");
        }

        private void InsertRealEstateDataNegativeForUpdate(IWebDriver driver)
        {
            IWebElement locationField = driver.FindElement(By.Id("testIdLocation"));
            locationField.Clear();
            locationField.SendKeys("1234");

            IWebElement roomNumberField = driver.FindElement(By.Id("testIdRoomNumber"));
            roomNumberField.Clear();
            roomNumberField.SendKeys("Twelve");

            IWebElement areaField = driver.FindElement(By.Id("testIdArea"));
            areaField.Clear();
            areaField.SendKeys("Something");

            IWebElement buildingTypeField = driver.FindElement(By.Id("testIdBuildingType"));
            buildingTypeField.Clear();
            buildingTypeField.SendKeys("9876");
        }

        [TearDown]
        public void EndTest()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}