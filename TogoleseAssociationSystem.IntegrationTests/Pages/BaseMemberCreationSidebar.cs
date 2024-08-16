 using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogoleseAssociationSystem.IntegrationTests.Constants;
using TogoleseAssociationSystem.IntegrationTests.Helpers;

namespace TogoleseAssociationSystem.IntegrationTests.Pages
{
    public class BaseMemberCreationSidebar
    {
        public IWebElement FirstnameInput =>
            BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement LaststnameInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement TitleInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);
        public IWebElement AddressInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);
        public IWebElement PostcodeInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);
        public IWebElement CityInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement NextOfKinInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement RelationshipInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement TelephoneInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement DateOfBirthInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement IsActiveCheckbox =>
         BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement IsChairCheckbox =>
         BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement PhotoUrlInput =>
         BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement ContributionNameInput =>
               BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement AmountInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement DateOfContributionInput =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);
       
        public IWebElement IsAnnualContributionCheckbox =>
           BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);

        public IWebElement MemberIdInput=>
              BrowserHelper.FindElementWithWaits(By.Id(""), TestConstants.Helpers.MediumWaitSeconds);
    }
}
