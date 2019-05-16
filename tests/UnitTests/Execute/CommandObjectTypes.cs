using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class CommandObjectTypes
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1(
            string _string,
            bool _bool1,
            bool _bool2,
            int _int,
            long _long,
            decimal _decimal,
            DateTime _dateTime,
            string[] _stringArray,
            bool[] _boolArray,
            int[] _intArray,
            long[] _longArray,
            decimal[] _decimalArray,
            DateTime[] _dateTimeArray)
        {
            return 1;
        }
    }
}
