using Core;
using NUnit.Framework;
using System;

namespace CoreUnitTests
{
    [TestFixture]
    public class ParametersUnitTests
    {
        //TODO: XML
        private Parameters _parameters;

        //TODO: refactor
        [SetUp]
        public void Setup()
        {
            _parameters = new Parameters();
            _parameters.NumericalParameters[
                ParameterType.BladeWidth].Value = 40;
        }

        //TODO: incoding
        /// <summary>
        /// Тестирование функции SetDependencies класса Parameters
        /// </summary>
        /// <param name="maxRatio">
        /// Соотношение величин для максимального значения</param>
        /// <param name="minRatio">
        /// Соотношение величин для минимального значения</param>
        [Test]
        [TestCase(3.0 / 4, 1.0 / 4)]
        [TestCase(2.0 / 4, 0.0)]
        public void SetDependenciesMaxMinTest(
            double maxRatio, double minRatio)
        {
            _parameters.SetDependencies(
                _parameters.NumericalParameters[ParameterType.BladeWidth],
                _parameters.NumericalParameters[ParameterType.EdgeWidth],
                maxRatio,
                minRatio);

            double expectedMaxValue =
                _parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value * maxRatio;
            double expectedMinValue;

            if (minRatio != 0)
            {
                expectedMinValue =
                    _parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value * minRatio;
            }
            else
            {
                expectedMinValue =
                    _parameters.NumericalParameters[
                        ParameterType.EdgeWidth].MaxValue * 0.1;
            }

            double resultMaxValue =
                _parameters.NumericalParameters[
                    ParameterType.EdgeWidth].MaxValue;
            double resultMinValue =
                _parameters.NumericalParameters[
                    ParameterType.EdgeWidth].MinValue;

            Assert.That(resultMaxValue,
                Is.EqualTo(expectedMaxValue));
            Assert.That(resultMinValue,
                Is.EqualTo(expectedMinValue));
        }

        /// <summary>
        /// Проверка валидации значения maxratio и minratio
        /// </summary>
        [Test]
        [TestCase(3.0 / 4, -1.0 / 4)]
        [TestCase(-2.0 / 4, 0.0)]
        [TestCase(-2.0 / 4, 1 / 8)]
        [TestCase(-2.0 / 4, -1 / 2)]
        public void SetDependenciesExceptionTest(
            double minratio, double maxratio)
        {
            var exception = Assert.Throws<ParameterException>(() =>
            {
                _parameters.SetDependencies(
                    _parameters.NumericalParameters[
                        ParameterType.BladeWidth],
                    _parameters.NumericalParameters[
                        ParameterType.EdgeWidth],
                    maxratio,
                    minratio);
            });

            Assert.That(exception.ExceptionType,
                Is.EqualTo(ExceptionType.RatioNegativeException));
        }

        /// <summary>
        /// Проверка присвоения свойству BindingType значения
        /// </summary>
        [Test]
        public void SetBindingTypeTest()
        {
            _parameters.BindingType = BindingType.Through;
            Assert.That(_parameters.BindingType,
                Is.EqualTo(BindingType.Through));
        }

        /// <summary>
        /// Проверка присвоения свойству BladeExistence значения
        /// </summary>
        [Test]
        public void SetBladeExistenceTest()
        {
            _parameters.BladeExistence = true;
            Assert.That(_parameters.BladeExistence, Is.True);
        }

        /// <summary>
        /// Проверка присвоения свойству BladeType значения
        /// </summary>
        [Test]
        public void SetBladeTypeTest()
        {
            _parameters.BladeType = true;
            Assert.That(_parameters.BladeType, Is.True);
        }

        /// <summary>
        /// Пример дополнительного теста 
        /// с разными типами утверждений NUnit
        /// </summary>
        [Test]
        public void AdditionalNUnitFeaturesExample()
        {
            _parameters.BladeExistence = true;
            _parameters.BindingType = BindingType.Through;

            Assert.Multiple(() =>
            {
                Assert.That(_parameters.BladeExistence, Is.True);
                Assert.That(_parameters.BindingType,
                    Is.EqualTo(BindingType.Through));
            });
        }
    }
}