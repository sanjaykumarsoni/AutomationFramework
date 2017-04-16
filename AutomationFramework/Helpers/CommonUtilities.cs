using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Helpers
{
    public class CommonUtilities
    {
        /// <summary>
        /// To count the word in given text.
        /// </summary>
        /// <param name="text">text input</param>
        /// <returns></returns>
        public static int WordCount(string text)
        {
            return text.Length;
        }
    }
}
