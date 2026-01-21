using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsCore
{
    //TODO: XML
    internal class ParameterExceptionUnitTests
    {
        //TODO: description
        [Test]
        public void ParameterExceptionConstructorTests()
        {
            ParameterException param = new ParameterException(
                ExceptionType.InvalidException,
                ParameterType.BindingLength
                );
            Assert.Multiple(() =>
            {
                Assert.That(param.ExceptionType, Is.EqualTo(
                    ExceptionType.InvalidException));
                Assert.That(param.ParameterType, Is.EqualTo(
                    ParameterType.BindingLength));
            });
        }
    }
}
