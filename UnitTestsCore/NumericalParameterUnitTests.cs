using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsCore
{
    using System;
    using Core;
    using NUnit.Framework;

    namespace CORE_UNIT_TESTS
    {
        [TestFixture]
        public class NumericalParameterTest
        {
            private Parameters _parameters;

            [SetUp]
            public void Setup()
            {
                _parameters = new Parameters();
            }

            /// <summary>
            /// Проверка валидации значения
            /// </summary>
            /// <param name="expectedMessage">Ожидаемое сообщение об ошибке</param>
            /// <param name="value">Значение для тестирования</param>
            [Test]
            [TestCase(ExceptionType.TooSmallException, 5)]
            [TestCase(ExceptionType.TooBigException, 100)]
            public void ValidateExceptionTest(ExceptionType exceptionType, double value)
            {
                ParameterException exception = Assert.Throws<ParameterException>(() =>
                {
                    _parameters.NumericalParameters[ParameterType.BladeWidth].Value = value;
                });
                Assert.That(exception.ExceptionType, Is.EqualTo(exceptionType));
            }

            /// <summary>
            /// Дополнительный тест для проверки корректного значения
            /// </summary>
            [Test]
            public void ValidValueTest()
            {
                double validValue = 50; 
                _parameters.NumericalParameters[ParameterType.BladeWidth].Value = validValue;
                Assert.That(_parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                    Is.EqualTo(validValue));
            }

            /// <summary>
            /// Тест граничных значений 
            /// </summary>
            [Test]
            [TestCase(ExceptionType.TooSmallException, 0)]
            [TestCase(ExceptionType.TooBigException, 1000)]
            public void BoundaryValueTests(ExceptionType exceptionType, double boundaryValue)
            {
                ParameterException exception = Assert.Throws<ParameterException>(() =>
                {
                    _parameters.NumericalParameters[ParameterType.BladeWidth].Value = boundaryValue;
                });
                Assert.That(exception.ExceptionType, Is.EqualTo(exceptionType));
            }
        }
    }
}
