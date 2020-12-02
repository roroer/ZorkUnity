using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public interface IOutputService
    {
        void WriteLine(string value, bool isBold = false);

        void WriteLine(object value);

        void Write(string value);

        void Write(object value);
    }
}
