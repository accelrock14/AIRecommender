using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Exceptions
{
    public class KeyNotPresentException : ApplicationException
    {
        public KeyNotPresentException(string message) : base(message) { }
    }
}
