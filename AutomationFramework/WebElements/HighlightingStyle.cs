using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.WebElements
{
    /// <summary>
    /// High light style and background colour. 
    /// </summary>
    public class HighlightingStyle
    {
        private string _backgroundColor;
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                try
                {
                    ColorTranslator.FromHtml(value);
                }
                catch (Exception)
                {
                    throw new ArgumentException("The color " + value + " is not a valid background html color");
                }
                _backgroundColor = value;
            }
        }


        public int BorderSizeInPixels { get; set; }

        public BorderStyle BorderStyle { get; set; }

        private string _borderColor;
        public string BorderColor
        {
            get { return _borderColor; }
            set
            {
                try
                {
                    ColorTranslator.FromHtml(value);
                }
                catch (Exception)
                {
                    throw new ArgumentException("The color " + value + " is not a valid border html color");
                }
                _borderColor = value;
            }
        }
    }

    public enum BorderStyle
    {
        None,
        Hidden,
        Dotted,
        Dashed,
        Solid,
        Double,
        Groove,
        Ridge,
        Inset,
        Outset,
        Initial,
        Inherit
    }
}
