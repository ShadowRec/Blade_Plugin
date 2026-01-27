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
        //TODO:XML DONE
        /// <summary>
        /// Тесты для класса NumerialParameter
        /// </summary>
        [TestFixture]
        public class NumericalParameterTest
        {
            /// <summary>
            /// Поле хранящее параметры
            /// </summary>
            private Parameters _parameters;
            //TODO: refactor done
            /// <summary>
            /// Функция инициализации
            /// поля параметров
            /// </summary>
            public void Initialize()
            {
                _parameters = new Parameters();
            }

            /// <summary>
            /// Выставление значений для работы тестов  
            /// </summary>
            private void FillValues()
            {
                _parameters.NumericalParameters[
                      ParameterType.PeakLenght].SetMinAndMax(5, 30);
                  _parameters.NumericalParameters[
                    ParameterType.PeakLenght].Value = 15;
            }

            
            [Test]
            [TestCase(ExceptionType.TooSmallException, 5)]
            [TestCase(ExceptionType.TooBigException, 100)]
            [Description("Данный тест проверяет" +
                "правильность возникающих исключений")]
            public void ValidateExceptionTest(
                ExceptionType exceptionType, double value)
            {
                Initialize();
                ParameterException exception =
                    Assert.Throws<ParameterException>(() =>
                    {
                        _parameters.NumericalParameters[
                            ParameterType.BladeWidth].Value = value;
                    });

                Assert.That(exception.ExceptionType,
                    Is.EqualTo(exceptionType));
            }

            
            [Test]
            [Description("Данный тест проверяет" +
                "правильность присваивания значений")]
            public void ValidValueTest()
            {
                Initialize();
                double validValue = 50;
                _parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value = validValue;

                Assert.That(_parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value,
                    Is.EqualTo(validValue));
            }

            [Description("Данный тест проверяет" +
               "правильность возникающих исключений" +
                "при присваивании значений")]
            [Test]
            [TestCase(ExceptionType.TooSmallException, 0)]
            [TestCase(ExceptionType.TooBigException, 1000)]
            public void BoundaryValueTests(
                ExceptionType exceptionType, double boundaryValue)
            {
                Initialize();
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
            [TestCase(6, 7, ExceptionType.MinGreaterMaxException, false)]
            [TestCase(-20, 40,
                ExceptionType.MaxValueNegativeException, true)]
            [TestCase(20, -40,
                ExceptionType.MinValueNegativeException, true)]
            [Description("Данный тест проверяет" +
                "правильность возникающих исключений" +
                "при присваивании Min Max значений")]
            public void MinMaxValueValidationTest(
                double maxValue, double minValue,
                ExceptionType exceptionType, bool valuesNULL)
            {
                Initialize();
                if (!valuesNULL)
                {
                    FillValues();
                }

                ParameterException exception =
                    Assert.Throws<ParameterException>(() =>
                    {
                        _parameters.NumericalParameters[
                            ParameterType.PeakLenght].SetMinAndMax(minValue,
                            maxValue);
                    });

                Assert.That(exception.ExceptionType,
                    Is.EqualTo(exceptionType));
            }
        }
    }
}