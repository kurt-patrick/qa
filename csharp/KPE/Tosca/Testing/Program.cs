using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string theDate = "5/18/2021";
            var newDate = DateTime.ParseExact(theDate, "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var newDateString = newDate.ToString("dd.MM.yyyy");

            Console.WriteLine(newDateString);
            Console.ReadKey();

        }
    }
}
