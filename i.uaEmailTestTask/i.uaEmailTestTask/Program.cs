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
            driver.Url = "https://www.i.ua/";
            driver.Manage().Window.Maximize();
            Console.WriteLine("Customaze is done");

        }
        [Test]
        public void TestExecution()
        {
            //log in
            driver.FindElement(By.Name("login")).SendKeys("alisatest");
            driver.FindElement(By.Name("pass")).SendKeys("zxcasdf12" + Keys.Enter);
            driver.FindElement(By.ClassName("Left")).Click();
            //create message
            driver.FindElement(By.XPath("//*[@id=\"to\"]")).SendKeys("linatest@ukr.net");
            driver.FindElement(By.XPath("//*[@id=\"text\"]")).SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ");
            IWebElement SaveDrafts = driver.FindElement(By.Name("save_in_drafts"));
            SaveDrafts.Click();
            //navigate to created message
            driver.FindElement(By.XPath("/html/body/div[1]/div[6]/div[2]/div/div/div[2]/div[2]/div[3]/ul/li[3]/a")).Click();
            driver.FindElement(By.XPath("//*[@id=\"mesgList\"]/form/div/a")).Click();

            //editing field TO
            string toEdited = "nanerltest@ukr.net";
            IWebElement elementTo = driver.FindElement(By.XPath("//*[@id=\"to\"]"));
            elementTo.Clear();
            elementTo.SendKeys(toEdited);

            //editing field TEXT     

            string TextEdited = "Just simple first automated test";
            IWebElement elementText = driver.FindElement(By.XPath("//*[@id=\"text\"]"));
            elementText.Clear();
            elementText.SendKeys(TextEdited);

            //editing field SUBJECT
            string EditedSub = "Test Subject";
            IWebElement elementSub = driver.FindElement(By.XPath("/html/body/div[4]/div[6]/div[1]/div[1]/div[1]/div/form/div[5]/div[2]/span/input[1]"));
            elementSub.Clear();
            elementSub.SendKeys(EditedSub);

            //saving changes
            driver.FindElement(By.XPath("/html/body/div[4]/div[6]/div[1]/div[1]/p[1]/input[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[6]/div[2]/div/div/div[2]/div[2]/div[3]/ul/li[3]/a")).Click();
            driver.FindElement(By.XPath("//*[@id=\"mesgList\"]/form/div/a")).Click();


            //verifying saved message
            string actualEditedSub = driver.FindElement(By.Name("subject")).GetAttribute("value");
            Assert.AreEqual( EditedSub,  actualEditedSub);
            Console.WriteLine($"Expected result:{EditedSub};/n Actual result:{actualEditedSub}");

            string actualtoEdited = driver.FindElement(By.Id("to")).GetAttribute("innerHTML");
            Assert.AreEqual(toEdited, actualtoEdited);

            string actualTextEdited = driver.FindElement(By.XPath("//*[@id=\"text\"]")).GetAttribute("innerHTML");
            Assert.AreEqual(TextEdited, actualTextEdited);

        }
        [TearDown]
        public void cleanUp()
        {
            driver.Close();
            
        }

    }
    
}

