using AutomationFramework.Base;
namespace TradeProTests.Pages
{
    /// <summary>
    /// Page generator using framework GetPage genric method to create instance for each page class with the help of BasePage class which comes from framework.
    /// </summary>
    public abstract class PageGenerator :BasePage
    {
        public static HomeDashboardPage HomeDashBoardPage
        {
            get { return GetPage<HomeDashboardPage>(); }
        }

        public static LoginPage ApplicationLoginPage
        {
            get { return GetPage<LoginPage>(); }
        }

        public static ContractsDashboardPage ContractsDashBoardPage
        {
            get { return GetPage<ContractsDashboardPage>(); }
        }
        public static ClaimsDashboardPage ClaimsDashboardPage
        {
            get { return GetPage<ClaimsDashboardPage>(); }
        }
    }
}
