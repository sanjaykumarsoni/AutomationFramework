using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using AutomationFramework.Extensions;
using AutomationFramework.KendoWrapper;
using AutomationFramework.Base;
using AutomationFramework.Helpers;

namespace TradeProTests.Pages
{
    public class HomeDashboardPage
    {
        #region WebElements
        [FindsBy(How = How.Id, Using = "manufacturers")]
        public IWebElement ddlManufacturers { get; set; }

        [FindsBy(How = How.Id, Using = "suggestion")]
        public IWebElement AddSuggestionBtn { get; set; }

        [FindsBy(How = How.Id, Using = "suggestionText")]
        public IWebElement FillSuggestionTextAreaMsg { get; set; }

        [FindsBy(How = How.Id, Using = "addSuggestion")]
        public IWebElement ClickSubmit { get; set; }

        [FindsBy(How = How.Id, Using = "cancelSuggestion")]
        public IWebElement ClickCancel { get; set; }

        [FindsBy(How = How.Id, Using = "operatorsgrid")]
        public IWebElement OperatorsGridId { get; set; }

        [FindsBy(How = How.Id, Using = "afscommentsgrid")]
        public IWebElement AfsCommentsGrid { get; set; }

        [FindsBy(How = How.Id, Using = "transactionFromDate")]
        public IWebElement DatePickers { get; set; }

        [FindsBy(How = How.Id, Using = "commentSearchTypes")]
        public IWebElement CommentSearchTypes { get; set; }

       // [FindsBy(How = How.Id, Using = "status")]
        //public IWebElement StatusTypes { get; set; }

       // [FindsBy(How = How.Id, Using = "actiontype")]
       // public IWebElement ActionType { get; set; }

       // [FindsBy(How = How.Id, Using = "datetype")]
       // public IWebElement DateType { get; set; }

        [FindsBy(How = How.Id, Using = "Export")]
        public IWebElement ExportButton { get; set; }

        [FindsBy(How = How.Id, Using = "tabstrip")]
        public IWebElement MessagesTabStrip { get; set; }

        //[FindsBy(How = How.Id, Using = "ddl-46136")]
        //public IWebElement StatusDDL { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".col-md-5 .panel-title")]
        public IWebElement NewsFeedAlignMent { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".k-grid-update")]
        public IWebElement UpdateOptGrid{ get; set; }

        [FindsBy(How = How.CssSelector, Using = ".k-grid-cancel")]
        public IWebElement CancelOptGrid { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".pull-right.manufacturer-container")]
        public IWebElement ManufactureDDLAlignment { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".label.label-default")]
        public IWebElement SuggestionLabel { get; set; }

        [FindsBy(How = How.Id, Using = "to")]
        public IWebElement SuggestionTo { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".col-md-7 .panel-title")]
        public IWebElement TransactionLabel { get; set; }

        [FindsBy(How = How.Id, Using = "transactionschart")]
        public IWebElement Graph { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".row .col-md-12 .panel-title")]
        public IWebElement MessageLabel { get; set; }

        [FindsBy(How = How.Id, Using = "transactionschartWatermark")]
        public IWebElement TransactionPanelCount { get; set; }

        [FindsBy(How = How.Id, Using = "newsfeed-panel")]
        public IWebElement NewsFeedPanelCount { get; set; }

        //need to remove absolute path once find the way to get realtive path.
        [FindsBy(How = How.XPath, Using = "html/body/div[1]/section/div[3]/div[2]/div/div/div[1]")]
        public IWebElement MessagesPanelCount { get; set; }
        
        [FindsBy(How = How.Id, Using = "commentTypes")]
        public IWebElement CommentTypesAFSCommentsTab { get; set; }
       
        
        [FindsBy(How = How.CssSelector, Using = ".k-button.k-button-icontext.k-grid-edit.btn.btn-default")]
        public IWebElement OperatorEdit { get; set; }

        [FindsBy(How = How.Id, Using = "CompanyNumber")]
        public IWebElement CompanyNo { get; set; }

        [FindsBy(How = How.Id, Using = "masterCheckBox")]
        public IWebElement MasterCheckBox { get; set; }

        [FindsBy(How = How.Id, Using = "to")]
        public IWebElement AddSuggestionTo { get; set; }

        [FindsBy(How = How.Id, Using = "subject")]
        public IWebElement AddSuggestionSubject { get; set; }

        /*------Elements of New operator----------------------*/

        [FindsBy(How = How.XPath, Using = ".//*[@id='operatorsgrid']/div[2]/table/tbody/tr[2]/td[2]")]
        public IWebElement OperatorName { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".k-button.k-button-icontext.k-primary.k-grid-update.btn.btn-default")]
        public IWebElement OperatorUpdate { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='operatorsgrid']/div[2]/table/tbody/tr[2]/td[3]")]
        public IWebElement OperatorAddress { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='operatorsgrid']/div[2]/table/tbody/tr[2]/td[4]")]
        public IWebElement OperatorGroup { get; set; }

        [FindsBy(How = How.ClassName, Using = "postLink")]
        public IWebElement OperatorView { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#advance-search-close")]
        public IWebElement Return { get; set; }

     







        /*----Elements of User message tab-------------------*/

        [FindsBy(How = How.ClassName, Using = "postLink")]
        public IWebElement UserMessagePostLink { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='rowDelete']")]
        public IWebElement UserDelete { get; set; }


        /*-------RECOVERY Part Elements of Comments tab-----------*/
        
        [FindsBy(How = How.Id, Using ="status")]
        public IWebElement Status { get; set; }

        [FindsBy(How = How.Id, Using = "actionedtype")]
        public IWebElement ActionedType { get; set; }

        [FindsBy(How = How.Id, Using = "datetype")]
        public IWebElement  DateType{ get; set; }
        
        [FindsBy(How = How.Id, Using = "start")]
        public IWebElement StartDate { get; set; }
 
        [FindsBy(How = How.Id, Using = "end")]
        public IWebElement EndDate { get; set; }

        [FindsBy(How = How.Id, Using = "name")]
        public IWebElement NameField { get; set; }

        [FindsBy(How = How.Id, Using = "comment")]
        public IWebElement CommentField { get; set; }

        [FindsBy(How = How.Id, Using = "claimId")]
        public IWebElement ClaimIdField { get; set; }

        [FindsBy(How = How.Id, Using = "afscommentsearch")]
        public IWebElement Search { get; set; }

        [FindsBy(How = How.Id, Using = "afscommentsearchclear")]
        public IWebElement Clear { get; set; }

        [FindsBy(How = How.Id, Using = "msgModalBody")]
        public IWebElement SubmitAlert { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='msgModalBody']/div")]
        public IWebElement DateAlert { get; set; }

      

        

        #endregion

        #region Enums
        public enum Enums
        {
            NewOperators = 0,
            UserMessages = 1,
            AfsComments = 2,
            CommentsSearch = 3

        }
        #endregion

        #region Webelment methods
        public void HomeDatePicker()
        {
            var datePicker = new DatePicker(DatePickers);
            datePicker.ClickCalendarIcon();
            datePicker.KendoDatePicker("June 2014");
            //DatePickers.Click();
            //var element = DatePickers.NextSibling("..//span[@class='k-select']");
            //element.Click();
            Hooks.Driver.WaitForJQuery(100);
            Console.WriteLine("Value entered in date field");
        }

        public string CheckBGColor()
        {
            var color = SuggestionLabel.Colour();
            return color;
        }

        public void AddSuggestionClick()
        {
            Hooks.Driver.ActionDrivers("suggestion");
            Hooks.Driver.WaitForJQuery(1000);
            AddSuggestionBtn.WaitForElementToLoad(100);
            AddSuggestionBtn.JavaScriptClick();
            Console.WriteLine("Clicked on the Add Suggestion Button");
        }

        public bool SubmitSugesstion()
        {
            ClickSubmit.Click();
            var item = KendoCommonUtilities.IsModelPopUpClosed();
            Console.WriteLine("Clicked on the Submit Suggestion Button");
            return item;
        }

        public bool CancelSuggestionBox()
        {
            ClickCancel.Click();
            var item=  KendoCommonUtilities.IsModelPopUpClosed();
            Console.WriteLine("Clicked on the Cancel Suggestion Button");
            return item;
        }

        public string AddSuggestionToText()
        {
            var item = AddSuggestionTo.GetTextByPlaceholder();
            Console.WriteLine("Fetching text from to textbox");
            return item;
        }

        public string AddSuggestionSubjectText()
        {
            var item = AddSuggestionSubject.GetText();
            Console.WriteLine("Fetching text from subject textbox");
            return item;
        }
        public bool AddSuggestionToEnable()
        {
            var item = AddSuggestionTo.Enabled;
            Console.WriteLine("Checking to textbox Enabled/ Disabled");
            return item;
        }
        public bool AddSuggestionSubjectEnable()
        {
            var item = AddSuggestionSubject.Enabled;
            Console.WriteLine("Checking subject textbox Enabled/ Disabled");
            return item;
        }
        public string FillSuggestionTextArea(string value)
        {
            var item = FillSuggestionTextAreaMsg.EnterText(value);
            Console.WriteLine("Filled suggestion Area");
            return item;
        }
        public void SelectManufacturesDDL(string manufactureName)
        {
            var kendoDDL = new KendoDropDownList(ddlManufacturers);
            kendoDDL.Select(manufactureName);

        }
        public string Suggestion()
        {
            var item = SuggestionLabel.Text;
            return item;
        }

        public string TransactionDollar()
        {
            var item = TransactionLabel.Text;
            return item;
        }
        public bool GraphView()
        {
            Graph.DefaultWait(200);
            var item = Graph.IsDisplayed();
            return item;
        }

        public string MessagePanel()
        {
            var item = MessageLabel.Text;
            return item;
        }

        #region Operators Grid related testcases

        public int OperatorsGridPageSize()
        {
            //ToDo:  var kendoGrid = new KendoGrid(OperatorsGridId); has been repeated for below methods. Should find the way to fix as common.
            var kendoGrid = new KendoGrid(OperatorsGridId);
            return kendoGrid.GetPageSize();
        }

        public void OperatorGridSortDescByID()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("ID", SortType.Desc);
        }

        public void OperatorGridSortAscByID()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("ID", SortType.Asc);
        }

        public void OperatorGridPageSortAscByOperator()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("CompanyName", SortType.Asc);
        }
        public void OperatorGridPageSortDscByOperator()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("CompanyName", SortType.Desc);
        }

        public void OperatorGridPageSortAscByAddress()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("AddressComplete", SortType.Asc);
        }
        public void OperatorGridPageSortDscByAddress()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("AddressComplete", SortType.Desc);
        }

        public void OperatorGridPageSortAscByGroup()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("Group", SortType.Asc);
        }
        public void OperatorGridPageSortDscByGroup()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Sort("Group", SortType.Desc);
        }

        public void ChangeGridOperatorPageSize()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.ChangePageSize(10);
        }

        public void ReloadOperatorGridPage()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.Reload();
        }

        public void NavigationToPageInOpertorGrid()
        {
            var kendoGrid = new KendoGrid(OperatorsGridId);
            kendoGrid.NavigateToPage(5);
        }


        public void ClickNewOperator()
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            kendoTab.TabSelect((int)Enums.NewOperators);
            Console.WriteLine("Clicked New Operator Tab");
        }

        public void ClickUserMessages()
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            kendoTab.TabSelect((int)Enums.UserMessages);
            Console.WriteLine("Clicked user messages Tab");
        }

        public void ClickAFSComments()
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            kendoTab.TabSelect((int)Enums.AfsComments);
            Console.WriteLine("Clicked AFS comments Tab");
        }

        public void ClickCommentSearch()
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            kendoTab.TabSelect((int)Enums.CommentsSearch);
            Console.WriteLine("Clicked Comment search Tab");
        }

        public void NewOperatoreEdit()
        {
            OperatorEdit.Click();
            Console.WriteLine("Clicked Edit button");
        }


        public void CompanyNumber(string value)
        {
            CompanyNo.EnterText(value);
            Console.WriteLine("Entered value in Company No Text field");
        }

        public int MessagePanelTabCounts()
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            return kendoTab.TabCount();
        }

        public bool IsTabPresentMessagePanel(string tabName)
        {
            var kendoTab = new KendoTab(MessagesTabStrip);
            return kendoTab.IsTabNamePresent(tabName);
        }

        public bool IsSelectCommentSearchTypeDDLEnabled()
        {
             return CommentSearchTypes.Enabled;
        }
        public bool IsSelectCommentTypeDDLEnabled()
        {
            return CommentTypesAFSCommentsTab.Enabled;
        }

        //Method for New Operator//
        public string NewoperatorLabel()
        {
            var item = OperatorName.Text;
            return item;
        }
        public string NewoperatorAddress()
        {
            var item = OperatorAddress.Text;
            return item;
        }
        public string NewoperatorGroup()
        {
            var item = OperatorGroup.Text;
            return item;
        }
        public void NewoperatorContractsLink()
        {
            OperatorView.Click();
        }
        public void NewoperatorUpdate()
        {
            OperatorUpdate.Click();
        }
        public void ReturnDashboard()
        {
            Return.Click();
        }


        //Method for User Message//
        public void UsermessageID()
        {
            UserMessagePostLink.Click();
        }
        public void UserDeleteButton()
        {
            UserDelete.Click();
        }

        //Method for comment Search//
        public void SelectCommentSearchTypeDDL(string commentstypes)
        {
            var kendoDDL = new KendoDropDownList(CommentSearchTypes);
            kendoDDL.Select(commentstypes);
        }

        public void SelectStatusDDDL(string statustypes)
        {
            var kendoDDL = new KendoDropDownList(Status);
            kendoDDL.Select(statustypes);
        }

        public void SelectActionedDDL(string actiontype)
        {
            var kendoDDL = new KendoDropDownList(ActionedType);
            kendoDDL.Select(actiontype);
        }

        public void SelectDateTypeDDL(string date)
        {
            var kendoDDL = new KendoDropDownList(DateType);
            kendoDDL.Select(date);
        }

        public void CommentSearchStartDates()
        {
            var datePicker = new DatePicker(StartDate);
            datePicker.ClickCalendarIcon();
            datePicker.KendoDatePicker("12/02/2016");
            Hooks.Driver.WaitForJQuery(100);
            Console.WriteLine("Value entered in date field");
        }

        public void CommentSearchEndDates()
        {
            var datePicker = new DatePicker(EndDate);
            datePicker.ClickCalendarIcon();
            datePicker.KendoDatePicker("12/15/2016");
            Hooks.Driver.WaitForJQuery(100);
            Console.WriteLine("Value entered in date field");
        }

        public void CommentNameField(string value)
        {
           NameField.EnterText(value);
        }

        public void CommentTextField(string value)
        {
            CommentField.EnterText(value);         
        }

        public string SearchWarning()
        {
            var item = SubmitAlert.Text;
            return item; 
        }

        public string SearchWarningdate()
        {
            var item = DateAlert.Text;
            return item;
        }

        public void CommentClaimId(string id)
        {
            ClaimIdField.EnterText(id);
        }

        public void SearchButton()
        {
            Search.Click();
        }

        public void ClearButton()
        {
            Clear.Click();
        }
        public void ClickExport()
        {
            ExportButton.Click();
        }

        public int SuggestionWordsCount(string text)
        {
            var item = CommonUtilities.WordCount(text);
            return item;
        }

        public string GetSuggestionText()
        {
            var item = FillSuggestionTextAreaMsg.GetText();
            return item;
        }
        public IWebElement CheckCommentStatus()
        {
            var kendoGrid = new KendoGrid(AfsCommentsGrid);
            var status = kendoGrid.SelectByGridColumn("NEW", 10);
            if (status != null) { Console.WriteLine("Test Case Failed: Comment tab Status is not New for status grid row"); }
            return status;
        }

        
        #endregion


        #region Layouts


        public void NewsFeed()
        {

        }

        public string ManufactureDdlAlignment()
        {
            return ManufactureDDLAlignment.TextAlign();
        }

        public string NewsFeedLabAlignment()
        {
            string NewsAlign= NewsFeedAlignMent.TextAlign();
            if (NewsAlign == "start")
            {
                NewsAlign = "left";
            }
            return NewsAlign;
        }

        public void UserMsgMasterCheckBox()
        {
            MasterCheckBox.Click();
            Console.WriteLine("Clicked on the Cancle Suggestion Button");
        }
        #endregion

        #region User Messages grid related
        public void ClickPostLink()
        {
            KendoGrid kendo =new KendoGrid(OperatorsGridId);
           var gridColumn= kendo.SelectByGridColumn(4);
           gridColumn.ClickLinkByHref("<a href=/Home/GetUserMessagesLink?userId=5030402&amp;contractId=5399514&amp;claimId=5399514&amp;messageType=0></a>");
        }
        #endregion
        #endregion

        #region Panel
        public int TransactionPanelCountHomeDashboard()
        {
            KendoPanel kendoPanel = new KendoPanel();
            var item = kendoPanel.IsChildToParentCssClassPresent(TransactionPanelCount);
            return item;
        }
        public int NewsfeedPanelCountHomeDashboard()
        {
            KendoPanel kendoPanel = new KendoPanel();
            var item = kendoPanel.IsChildToParentCssClassPresent(NewsFeedPanelCount);
            return item;
        }
        public int MessagesPanelCountHomeDashboard()
        {
            //KendoPanel kendoPanel = new KendoPanel();
            //var item = kendoPanel.IsChildToParentCssClassPresent(MessagesPanelCount);
            //return item;
            string item = MessagesPanelCount.Text;
            int count = 0;
            if (item.Contains("Messages"))
            {
                count++;
            }
            return count;
        }
        #endregion
    }
}

