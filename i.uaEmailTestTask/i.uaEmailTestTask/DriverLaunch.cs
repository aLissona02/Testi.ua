using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace i.uaEmailTestTask
{
    class Driver
    {
        private IWebDriver driver;

        public void driverLaunch(string url,IWebDriver driver)
        {
            this.driver = driver;
            driver.Url = url;
            driver.Manage().Window.Maximize();
        }
        public void driverStop(IWebDriver driver)
        {
            this.driver = driver;
            driver.Close();
        }
    }
}
