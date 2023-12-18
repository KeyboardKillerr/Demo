using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Exceptions;

public class BadImageException : Exception
{
    public BadImageException(string? message = null) : base(message) { }
}
