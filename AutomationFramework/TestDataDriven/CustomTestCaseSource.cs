//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using System.Linq;


namespace AutomationFramework.TestDataDriven
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class CustomTestCaseSource : TestCaseSourceAttribute, ITestBuilder, IImplyFixture
    {
        private NUnitTestCaseBuilder _builder = new NUnitTestCaseBuilder();

        public string SourceName
        {
            get;
            private set;
        }

        public Type SourceType
        {
            get;
            private set;
        }

        public CustomTestCaseSource(Type sourceType, [CallerMemberName] string name = null)
            : base(sourceType)
        {
            this.SourceType = sourceType;
        }

        public IEnumerable<string[]> BuildFrom(string name, string path)
        {
            TestDataFactory.filepath = path;
            TestDataFactory.methodName = name;
            TestDataFactory.GetExcelData();
            IEnumerable<string[]> test = TestDataFactory.ReadTestCases();
            return test;
        }
    }

}

