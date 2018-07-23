using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.MainModule
{
    [Serializable]
    public class NotValidInvoiceMonthException : Exception
    {
    public NotValidInvoiceMonthException() : base() { }
    public NotValidInvoiceMonthException(string message) : base(message) { }
    public NotValidInvoiceMonthException(string message, System.Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected NotValidInvoiceMonthException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }    
    }
}
