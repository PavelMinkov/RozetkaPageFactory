using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RozetkaPageFactory.Configurations;
using RozetkaPageFactory.PageObjects;
using RozetkaPageFactory.TestDataAccess;
using System;

namespace RozetkaPageFactory
{
    public class Tests
    {
        UserData prodData = ExcelDataAccess.GetTestData("Test1");

        [SetUp]
        public void Initialize()
        {

            PropertiesCollection.driver = new ChromeDriver();
            PropertiesCollection.driver.Navigate().GoToUrl(prodData.URL);
            PropertiesCollection.driver.Manage().Window.Maximize();
            PropertiesCollection.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [Test]
        public void ExecuteTest()
        {
            HomePage pageHome = new HomePage();
            ProductPage pageProduct = pageHome.ChooseCategoryProduct(prodData.categoryProducts, prodData.nameProducts);
            pageProduct.ChooseProduct(prodData.brand, prodData.sort, prodData.numberProduct);

            //check summ products
            int summ = pageProduct.SummProducts();
            Console.WriteLine(summ);
            Assert.That(summ, Is.LessThan(prodData.price));

            //make screenshot
            try
            {
                Screenshot ss = ((ITakesScreenshot)PropertiesCollection.driver).GetScreenshot();
                ss.SaveAsFile(@prodData.screenshot, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TearDown]
        public void CleanUp()
        {
            PropertiesCollection.driver.Close();
        }
    }
}