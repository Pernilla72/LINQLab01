using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLabExercice
{
    internal class NamnsdagSummering
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            // Skapar en sträng som visar månad, dag och antal personer
            string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
            return $"{Day.ToString().PadRight(3)} {monthName.PadRight(10)}: {Count} personer";
        }
    }
}
