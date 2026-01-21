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

    namespace CoreUnitTests
    {
        [TestFixture]
        public class NumericalParameterTest
        {
            //TODO: XML
            private Parameters _parameters;
            //TODO: refactor
            [SetUp]
            public void Setup()
            {
                _parameters = new Parameters();
            }

            /// <summary>
            /// Выставление значений для работы тестов  
            /// </summary>
            private void FillValues()
            {
                _parameters.NumericalParameters[
                    ParameterType.PeakLenght].MaxValue = 30;
                _parameters.NumericalParameters[
                    ParameterType.PeakLenght].MinValue = 5;
                _parameters.NumericalParameters[
                    ParameterType.PeakLenght].Value = 15;
            }

            //TODO: description
            /// <summary>
            /// Проверка валидации значения
            /// </summary>
            /// <param name="expectedMessage">
            /// Ожидаемое сообщение об ошибке</param>
            /// <param name="value">Значение для тестирования</param>
            [Test]
            [TestCase(ExceptionType.TooSmallException, 5)]
            [TestCase(ExceptionType.TooBigException, 100)]
            public void ValidateExceptionTest(
                ExceptionType exceptionType, double value)
            {
                ParameterException exception =
                    Assert.Throws<ParameterException>(() =>
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeWidth].Value = value;
                    });

                Assert.That(exception.ExceptionType,
                    Is.EqualTo(exceptionType));
            }

            /// <summary>
            /// Дополнительный тест для проверки 
            /// корректного значения
            /// </summary>
            [Test]
            public void ValidValueTest()
            {
                double validValue = 50;
                _parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value = validValue;

                Assert.That(_parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value,
                    Is.EqualTo(validValue));
            }

            /// <summary>
            /// Тест граничных значений 
            /// </summary>
            [Test]
            [TestCase(ExceptionType.TooSmallException, 0)]
            [TestCase(ExceptionType.TooBigException, 1000)]
            public void BoundaryValueTests(
                ExceptionType exceptionType, double boundaryValue)
            {
                ParameterException exception =
                    Assert.Throws<ParameterException>(() =>
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeWidth].Value =
                            boundaryValue;
                    });

                Assert.That(exception.ExceptionType,
                    Is.EqualTo(exceptionType));
            }

            [Test]
            [TestCase(4, 7, ExceptionType.MaxLesserrMinException, false)]
            [TestCase(6, 7, ExceptionType.MinGreaterMaxException, false)]
            [TestCase(-20, 40,
                ExceptionType.MaxValueNegativeException, true)]
            [TestCase(20, -40,
                ExceptionType.MinValueNegativeException, true)]
            public void MinMaxValueValidationTest(
                double maxValue, double minValue,
                ExceptionType exceptionType, bool valuesNULL)
            {
                if (!valuesNULL)
                {
                    FillValues();
                }

                ParameterException exception =
                    Assert.Throws<ParameterException>(() =>
                    {
                        _parameters.NumericalParameters[
                            ParameterType.PeakLenght].MaxValue = maxValue;
                        _parameters.NumericalParameters[
                            ParameterType.PeakLenght].MinValue = minValue;
                    });

                Assert.That(exception.ExceptionType,
                    Is.EqualTo(exceptionType));
            }

            [Test]
            [TestCase(40, false)]
            [TestCase(40, true)]
            public void MaxSetTest(double maxValue, bool valuesNULL)
            {
                if (!valuesNULL)
                {
                    FillValues();
                }

                _parameters.NumericalParameters[
                    ParameterType.PeakLenght].MaxValue = maxValue;

                Assert.That(_parameters.NumericalParameters[
                    ParameterType.PeakLenght].MaxValue,
                    Is.EqualTo(maxValue));
            }

            [Test]
            [TestCase(4, false)]
            [TestCase(4, true)]
            //TODO: rename
            public void MinSetTest(double minValue, bool valuesNULL)
            {
                if (!valuesNULL)
                {
                    FillValues();
                }

                _parameters.NumericalParameters[
                    ParameterType.PeakLenght].MinValue = minValue;

                Assert.That(_parameters.NumericalParameters[
                    ParameterType.PeakLenght].MinValue,
                    Is.EqualTo(minValue));
            }
        }
    }
}