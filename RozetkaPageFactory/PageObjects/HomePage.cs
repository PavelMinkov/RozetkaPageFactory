using OpenQA.Selenium;
using RozetkaPageFactory.Configurations;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace RozetkaPageFactory.PageObjects
{
    class HomePage
    {
        public HomePage()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//ul[contains(@class,'menu-categories_type_main')]/li[contains(@class,'menu-categories')]")]
        IList<IWebElement> listMenu;

        [FindsBy(How = How.Name, Using = "search")]
        IWebElement choiseTitle;

        public ProductPage ChooseCategoryProduct(int category, string search)
        {
            listMenu[category].Click();
            choiseTitle.SendKeys(search + "\n");
            return new ProductPage();
        }

    }
}
