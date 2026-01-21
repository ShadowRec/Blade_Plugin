
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Core;
using System.Threading.Tasks;

namespace Core
{
    //TODO: XML
    public class ParameterException:Exception
    {
        public ParameterException(ExceptionType exceptionType)
        {
            _exceptionType = exceptionType;
        }
        public ParameterException(ExceptionType exceptionType,
            ParameterType paramType)
        {
            _exceptionType = exceptionType;
            _parameterType = paramType;
        }
        private ParameterType _parameterType;
        private ExceptionType _exceptionType;
        public ExceptionType ExceptionType
        {
            get { return _exceptionType; }
        }
        public ParameterType ParameterType 
        { 
            get { return _parameterType; }
        }
    }
}
