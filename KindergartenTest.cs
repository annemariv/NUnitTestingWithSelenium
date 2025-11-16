using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace ShopTARge24TestingNUnit
{
    public class KindergartenTest
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
        public void KindergartenTestAddPositive()
        {
            driver.FindElement(By.Id("Kindergarten")).Click();
            driver.FindElement(By.Id("testIdAddK")).Click();
            Thread.Sleep(500);

            InsertKindergartenTestDataPositive(driver);

            driver.FindElement(By.Id("testICreateSubmitK")).Click();
            Thread.Sleep(1000);

           // string id = GetIdFromUrl(driver);

            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(500);

            IWebElement groupElement = driver.FindElement(By.Id("testTableGroupNameK_"));
            IWebElement childrenCountElement = driver.FindElement(By.Id("testTableChildrenCountK_"));
            IWebElement nameElement = driver.FindElement(By.Id("testTableKindergartenNameK_"));
            IWebElement teacherNameElement = driver.FindElement(By.Id("testTableTeacherNameK_"));

            Assert.That(groupElement.Text, Is.EqualTo("Testers"));
            Assert.That(childrenCountElement.Text, Is.EqualTo("20"));
            Assert.That(nameElement.Text, Is.EqualTo("Kindergarten_1"));
            Assert.That(teacherNameElement.Text, Is.EqualTo("Someone"));

            Console.WriteLine("Add test passed");
        }


        private void InsertKindergartenTestDataPositive(IWebDriver driver)
        {            
            IWebElement idOfgroupName = driver.FindElement(By.Id("testGroupNameK"));
            idOfgroupName.Clear();
            idOfgroupName.SendKeys("Testers");

            IWebElement idOfchildrenCount = driver.FindElement(By.Id("testChildrenCountK"));
            idOfchildrenCount.Clear();
            idOfchildrenCount.SendKeys("20");

            IWebElement idOfKindergartenName = driver.FindElement(By.Id("testKindergartenNameK"));
            idOfKindergartenName.Clear();
            idOfKindergartenName.SendKeys("Kindergarten_1");

            IWebElement idOfTeacherName = driver.FindElement(By.Id("testTeacherNameK"));
            idOfTeacherName.Clear();
            idOfTeacherName.SendKeys("Someone");

        }


        //get id from URL
        private string GetIdFromUrl(IWebDriver driver)
        {
            string url = driver.Url;
            return url.Substring(url.LastIndexOf('/') + 1);
        }

        //Update
        [Test, Order(2)]
        public void UpdateKindergartenTestPositive()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            Thread.Sleep(500);

            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("testIdUpdateK")).Click();
            Thread.Sleep(500);

            string id = GetIdFromUrl(driver);
            Console.WriteLine("ID from URL: " + id);

            UpdateKindergartenTestPositive(driver);
            Thread.Sleep(500);

            // Submit update
            driver.FindElement(By.Id("testIdUpdateSubmitK")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(1000);

            IWebElement groupElement = driver.FindElement(By.Id("testTableGroupNameK_"));
            IWebElement childrenElement = driver.FindElement(By.Id("testTableChildrenCountK_"));
            IWebElement nameElement = driver.FindElement(By.Id("testTableKindergartenNameK_"));
            IWebElement teacherElement = driver.FindElement(By.Id("testTableTeacherNameK_"));

            Assert.That(groupElement.Text, Is.EqualTo("Testers2"));
            Assert.That(childrenElement.Text, Is.EqualTo("10"));
            Assert.That(nameElement.Text, Is.EqualTo("Kindergarten_2"));
            Assert.That(teacherElement.Text, Is.EqualTo("Else"));

            Console.WriteLine("Update test passed!");
        }


        private void UpdateKindergartenTestPositive(IWebDriver driver)
        {
            IWebElement idOfgroupName2 = driver.FindElement(By.Id("testGroupNameK"));
            idOfgroupName2.Clear();
            idOfgroupName2.SendKeys("Testers2");

            IWebElement idOfchildrenCount2 = driver.FindElement(By.Id("testChildrenCountK"));
            idOfchildrenCount2.Clear();
            idOfchildrenCount2.SendKeys("10");

            IWebElement idOfKindergartenName2 = driver.FindElement(By.Id("testKindergartenNameK"));
            idOfKindergartenName2.Clear(); //kui vaja väli enne puhastada, default value 
            idOfKindergartenName2.SendKeys("Kindergarten_2");

            IWebElement idOfTeacherName2 = driver.FindElement(By.Id("testTeacherNameK"));
            idOfTeacherName2.Clear();
            idOfTeacherName2.SendKeys("Else");
        }

        //Delete after all pos and negative have tested
        [Test, Order(5)]
        public void KindergartenTestDeletePositive()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("testIdDeleteK")).Click();
            Thread.Sleep(500);

            string id = driver.Url.Substring(driver.Url.LastIndexOf('/') + 1);

            driver.FindElement(By.Id("testSubmitDeleteK")).Click();
            Thread.Sleep(500);

            var remainingRows = driver.FindElements(By.CssSelector("[id^='testTableGroupNameK_" + id + "']"));
            Assert.That(remainingRows.Count, Is.EqualTo(0));

            Console.WriteLine("Delete test passed");
        }


        //----------------NEGATIVE-------------------

        //Create
        [Test, Order(3)]
        public void KindergartenTestAddNegative()
        {
            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("testIdAddK")).Click();
            Thread.Sleep(500);

            InsertKindergartenTestDataNegative(driver);

            var rowsBefore = driver.FindElements(By.CssSelector("#KindergartenTableTest tbody tr")).Count;

            driver.FindElement(By.Id("testICreateSubmitK")).Click();
            Thread.Sleep(1000);

            var rowsAfter = driver.FindElements(By.CssSelector("#KindergartenTableTest tbody tr")).Count;

            Assert.That(rowsAfter, Is.EqualTo(rowsBefore), "No new kindergarten should be added with invalid input");

            Console.WriteLine("Negative add test passed");
        }


        private void InsertKindergartenTestDataNegative(IWebDriver driver)
        {
            IWebElement idOfgroupName = driver.FindElement(By.Id("testGroupNameK"));
            idOfgroupName.SendKeys("1111");

            IWebElement idOfchildrenCount = driver.FindElement(By.Id("testChildrenCountK"));
            idOfchildrenCount.SendKeys("abc");

            IWebElement idOfKindergartenName = driver.FindElement(By.Id("testKindergartenNameK"));
            idOfKindergartenName.SendKeys("5555");

            IWebElement idOfTeacherName = driver.FindElement(By.Id("testTeacherNameK"));
            idOfTeacherName.SendKeys("4444");

        }

        [Test, Order(4)]
        public void KindergartenTestUpdateNegative()
        {
            driver.Navigate().GoToUrl("https://localhost:7282");
            Thread.Sleep(500);

            driver.FindElement(By.Id("Kindergarten")).Click();
            Thread.Sleep(500);

            var rows = driver.FindElements(By.CssSelector("#KindergartenTableTest tbody tr"));
            if (rows.Count == 0)
            {
                Console.WriteLine("No rows exist in the table. Skipping negative update test.");
                return;
            }

            var firstRow = rows[0];
            string rowBefore = firstRow.Text;

            firstRow.FindElement(By.CssSelector("a[id='testIdUpdateK']")).Click();
            Thread.Sleep(500);

            InsertKindergartenTestDataNegativeForUpdate(driver);

            driver.FindElement(By.Id("testIdUpdateSubmitK")).Click();
            Thread.Sleep(1000);

            rows = driver.FindElements(By.CssSelector("#KindergartenTableTest tbody tr"));
            if (rows.Count == 0)
            {
                Console.WriteLine("No rows exist after update. Skipping check.");
                return;
            }

            firstRow = rows[0];
            string rowAfter = firstRow.Text;

            Assert.That(rowAfter, Is.EqualTo(rowBefore), "Update should not accept invalid input");

            Console.WriteLine("Negative update test passed");
        }


        private void InsertKindergartenTestDataNegativeForUpdate(IWebDriver driver)
        {

            IWebElement idOfgroupName2 = driver.FindElement(By.Id("testGroupNameK"));
            idOfgroupName2.Clear();
            idOfgroupName2.SendKeys("updated555");

            IWebElement idOfchildrenCount2 = driver.FindElement(By.Id("testChildrenCountK"));
            idOfchildrenCount2.Clear();
            idOfchildrenCount2.SendKeys("bbb");

            IWebElement idOfKindergartenName2 = driver.FindElement(By.Id("testKindergartenNameK"));
            idOfKindergartenName2.Clear();
            idOfKindergartenName2.SendKeys("9999a");

            IWebElement idOfTeacherName2 = driver.FindElement(By.Id("testTeacherNameK"));
            idOfTeacherName2.Clear();
            idOfTeacherName2.SendKeys("66556a");
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