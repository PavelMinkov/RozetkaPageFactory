using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RozetkaPageFactory.Configurations;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Threading;

namespace RozetkaPageFactory.PageObjects
{
    class ProductPage
    {
        public ProductPage()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@data-filter-name='producer']//input[@name='searchline']")]
        IWebElement brands;

        [FindsBy(How = How.XPath, Using = "//div[@data-filter-name='producer']//input[@class='custom-checkbox']/parent::*")]
        IWebElement brand;

        [FindsBy(How = How.CssSelector, Using = "select[class*='select']")]
        IWebElement elementSort;
        SelectElement DropdownElement
        {
            get { return new SelectElement(elementSort); }
        }

        [FindsBy(How = How.CssSelector, Using = "span.goods-tile__title")]
        IList<IWebElement> listProducts;

        [FindsBy(How = How.CssSelector, Using = "p[class*='product-prices']")]
        IWebElement moveToButtonBuy;

        [FindsBy(How = How.XPath, Using = "//ul[@class='product-buttons']//span[contains(@class,'buy-button')]")]
        IWebElement elementButtonBuy;

        [FindsBy(How = How.CssSelector, Using = "div.modal__header button[class*='close']")]
        IWebElement elementCartClose;

        [FindsBy(How = How.CssSelector, Using = "a.header__logo")]
        IWebElement elementLogo;

        [FindsBy(How = How.XPath, Using = "//li[contains(@class,'cart')]")]
        IWebElement cart;

        [FindsBy(How = How.XPath, Using = "//div[@class='cart-receipt__sum-price']/span[1]")]
        IWebElement cartSumm;

        public void ChooseProduct(string searchBrand, int sort, int number)
        {
            Actions actionProvider = new Actions(PropertiesCollection.driver);

            brands.SendKeys(searchBrand);
            Thread.Sleep(2000);
            brand.Click();
            DropdownElement.SelectByIndex(sort);
            PropertiesCollection.driver.Navigate().Refresh();
            listProducts[number].Click();
            actionProvider.MoveToElement(moveToButtonBuy).Build().Perform();
            elementButtonBuy.Click();
            elementCartClose.Click();
            elementLogo.Click();
        }

        public int SummProducts()
        {
            cart.Click();
            return int.Parse(cartSumm.Text);
        }
    }
}
