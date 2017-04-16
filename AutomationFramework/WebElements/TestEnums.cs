﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.WebElements
{
    /// <summary>
    ///     The test type you want to run. It will help in Environment setup.It has not been implemented and kept for furture considertion.
    /// </summary>
    [Description("Test Type")]
    public enum TestType
    {
        [Description("Standalone Browser")]
        StandaloneBrowser,
        [Description("Installed Browser")]
        InstalledBrowser,
        [Description("SauceLabs Browsers")]
        SauceLabsBrowsers,
        [Description("Emulator Browser")]
        EmulatorBrowser,
        [Description("Android Browser")]
        AndroidBrowser
    }

    /// <summary>
    ///     The webdriver to use for the test
    /// </summary>
    [Description("Driver")]
    public enum WebDriverType
    {
        [Description("Chrome")]
        ChromeDriver,
        [Description("Firefox")]
        FirefoxDriver,
        [Description("IE")]
        InternetExplorerDriver,
        [Description("Edge")]
        EdgeDriver,
        [Description("Opera")]
        OperaDriver,
        //PhantomJsDriver,
        [Description("Remote")]
        RemoteWebDriver,
        [Description("Safari")]
        SafariDriver,
        [Description("Emulator")]
        EmulatorDriver,
    }

    //public enum TestBrowser
    //{
    //    Chrome,
    //    Edge,
    //    MultiBrowserEmulator,
    //    Firefox,
    //    InternetExplorer,
    //    Opera,
    //    Safari,
    //    Android,
    //    IPhone,
    //    IPad,
    //    HtmlUnit,
    //    HtmlUnitWithJavaScript,
    //    PhantomJs
    //}


    [Description("Orientation")]
    public enum DeviceOrientation
    {
        [Description("Landscape")]
        Landscape,
        [Description("Portrait")]
        Portrait
    }

    [Description("MultiBrowser Emulator")]
    public enum Emulator
    {
        [Description("Apple iPhone4")]
        // ReSharper disable InconsistentNaming
        iPhone4,
        [Description("Apple iPhone5")]
        iPhone5,
        [Description("Apple iPhone6")]
        iPhone6,
        [Description("Apple iPhone6 Plus")]
        iPhone6Plus,
        [Description("Apple iPad")]
        iPad,
        // ReSharper restore InconsistentNaming
        [Description("Samsung Galaxy Tab")]
        SamsungGalaxyTab,
        [Description("Samsung Galaxy S5")]
        SamsungGalaxyS5,
        [Description("Samsung Galaxy S6")]
        SamsungGalaxyS6,
        [Description("Sony Xperia Z3")]
        SonyXperiaZ3
    }

    public enum ElementType
    {
        Button,
        CheckBox,
        Div,
        Img,
        Label,
        A,
        Radio,
        Select,
        Span,
        Tbody,
        Td,
        Thead,
        Tr,
        Table,
        Text,
        Password,
        Submit,
        DateTime,
        DateTimeLocal,
        Date,
        Color,
        Email,
        Month,
        Number,
        Range,
        Search,
        Tel,
        Time,
        Url,
        Week
    }

    public enum AlertAction
    {
        Accept,
        Dismiss,
        SendKeys,
        SetCredentials

    }

    public enum LogLevel
    {
        None,
        Basic,
        Verbose
    }

    public enum MediaAction
    {
        Play,
        Pause,
        VolumeChange,
        RateChange,
        Seeked
    }
}
