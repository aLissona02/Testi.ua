using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace i.uaEmailTestTask
{
    [TestFixture]
    class Program
    {
        IWebDriver driver = new ChromeDriver();
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
        [SetUp]     
        public void Customize()
        {
            Driver webDr = new Driver();
            webDr.driverLaunch("https://www.i.ua/", driver);
            //driver.Url = "https://www.i.ua/";
            //driver.Manage().Window.Maximize();
        }
        
        [Test]
        public void TestExecution()
        {
            //log in
            string login = System.Configuration.ConfigurationManager.AppSettings["log"];
            string password = System.Configuration.ConfigurationManager.AppSettings["pass"];
            driver.FindElement(By.Name("login")).SendKeys(login);
            driver.FindElement(By.Name("pass")).SendKeys(password + Keys.Enter);
            driver.FindElement(By.ClassName("Left")).Click();
            //create message
            string sendToData = "linatest@ukr.net";
            string dataInTextField = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ";

            string elTo = "//textarea[@id='to']";
            string elText = "//textarea[@id='text']";
            string elSubject = "//div[@class='field_value']//span[@class= 'field']//input[@name='subject']";
            string ChernButt = "//a[text()=' Чернетки']";
            string lastCreatedMess = "//div/a/span[@class='frm']";
            driver.FindElement(By.XPath(elTo)).SendKeys(sendToData);
            driver.FindElement(By.XPath(elText)).SendKeys(dataInTextField);
            IWebElement SaveDrafts = driver.FindElement(By.Name("save_in_drafts"));
            SaveDrafts.Click();
            //navigate to created message
            driver.FindElement(By.XPath(ChernButt)).Click();
            driver.FindElement(By.XPath(lastCreatedMess)).Click();

            //editing field TO
            string toEdited = "nanerltest@ukr.net";
            IWebElement elementTo = driver.FindElement(By.XPath(elTo));
            elementTo.Clear();
            elementTo.SendKeys(toEdited);

            //editing field SUBJECT
            string EditedSub = "Test Subject";
            IWebElement elementSub = driver.FindElement(By.XPath(elSubject));
            elementSub.Clear();
            elementSub.SendKeys(EditedSub);

            //editing field TEXT    
            string TextEdited = "Just simple first automated test";
            IWebElement elementText = driver.FindElement(By.XPath(elText));
            elementText.Clear();
            elementText.SendKeys(TextEdited);

            //saving changes

            driver.FindElement(By.XPath("//div[@class='Left']//p/input[@value='Зберегти чернетку']")).Click();
            driver.FindElement(By.XPath(ChernButt)).Click();
            driver.FindElement(By.XPath(lastCreatedMess)).Click();

            //verifying saved message
            string actualEditedSub = driver.FindElement(By.Name("subject")).GetAttribute("value");
            Assert.AreEqual( EditedSub,  actualEditedSub);
            //Console.WriteLine($"Expected result:{EditedSub};/n Actual result:{actualEditedSub}");

            string actualtoEdited = driver.FindElement(By.Id("to")).GetAttribute("innerHTML");
            Assert.AreEqual(toEdited, actualtoEdited);

            string actualTextEdited = driver.FindElement(By.Id("text")).GetAttribute("innerHTML");
            Assert.AreEqual(TextEdited, actualTextEdited);
        }
        [TearDown]
        public void cleanUp()
        {
            driver.Close();    
        }

    }
    
}

