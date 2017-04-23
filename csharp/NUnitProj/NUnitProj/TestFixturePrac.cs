using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProj
{
    [TestFixture("a=1,b=2")]
    [TestFixture("a=2")]
    public class TestFixturePrac
    {
        public TestFixturePrac(string data)
        {
            var array = data.Split(',');
            if(array.Length > 0)
            {

            }
        }

    }
}
