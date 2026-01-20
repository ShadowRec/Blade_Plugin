using Core;
using NUnit.Framework;
using System;

namespace CoreUnitTests
{
    [TestFixture]
    public class ParametersUnitTests
    {
        private Parameters _parameters;

        [SetUp]
        public void Setup()
        {
            _parameters = new Parameters();
            FillValues();
        }

        /// <summary>
        /// ¬ыставление значений дл€ работы тестов  
        /// </summary>
        private void FillValues()
        {
            _parameters.NumericalParameters[ParameterType.BladeWidth].Value = 40;
        }

        /// <summary>
        /// “естирование функции SetDependencies класса Parameters
        /// </summary>
        /// <param name="maxRatio">—оотношение величин дл€ максимального значени€</param>
        /// <param name="minRatio">—оотношение величин дл€ минимального значени€</param>
        [Test]
        [TestCase(3.0 / 4, 1.0 / 4)]
        [TestCase(2.0 / 4, 0.0)]
        public void SetDependenciesMaxMinTest(double maxRatio, double minRatio)
        {
            // Act
        
            _parameters.SetDependencies(
                _parameters.NumericalParameters[ParameterType.BladeWidth],
                _parameters.NumericalParameters[ParameterType.EdgeWidth],
                maxRatio,
                minRatio);

            // Assert
            double expectedMaxValue = _parameters.NumericalParameters[ParameterType.BladeWidth].Value * maxRatio;
            double expectedMinValue;

            if (minRatio != 0)
            {
                expectedMinValue = _parameters.NumericalParameters[ParameterType.BladeWidth].Value * minRatio;
            }
            else
            {
                expectedMinValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue * 0.1;
            }

            double resultMaxValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue;
            double resultMinValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MinValue;

            Assert.That(resultMaxValue, Is.EqualTo(expectedMaxValue));
            Assert.That(resultMinValue, Is.EqualTo(expectedMinValue));
        }

        /// <summary>
        /// ѕроверка валидации значени€ maxratio и minratio
        /// </summary>
        [Test]
        [TestCase(3.0 / 4, -1.0 / 4)]
        [TestCase(-2.0 / 4, 0.0)]
        [TestCase(-2.0 / 4, 1/8)]
        [TestCase(-2.0 / 4, -1/2)]
        public void SetDependenciesExceptionTest(double minratio, double maxratio)
        {
            // Act & Assert
            var exception = Assert.Throws<ParameterException>(() =>
            {
                _parameters.SetDependencies(
                    _parameters.NumericalParameters[ParameterType.BladeWidth],
                    _parameters.NumericalParameters[ParameterType.EdgeWidth],
                    maxratio,
                    minratio);
            });

            Assert.That(exception.ExceptionType, Is.EqualTo(ExceptionType.RatioNegativeException));
        }

        /// <summary>
        /// ѕроверка присвоени€ свойству BindingType значени€
        /// </summary>
        [Test]
        public void SetBindingTypeTest()
        {
            // Act
            
            _parameters.BindingType = BindingType.Through;

            // Assert
            Assert.That(_parameters.BindingType, Is.EqualTo(BindingType.Through));
        }

        /// <summary>
        /// ѕроверка присвоени€ свойству BladeExistence значени€
        /// </summary>
        [Test]
        public void SetBladeExistenceTest()
        {
            // Act
            
            _parameters.BladeExistence = true;

            // Assert
            Assert.That(_parameters.BladeExistence, Is.True);
        }

        /// <summary>
        /// ѕроверка присвоени€ свойству BladeType значени€
        /// </summary>
        [Test]
        public void SetBladeTypeTest()
        {
            // Act
            
            _parameters.BladeType = true;

            // Assert
            Assert.That(_parameters.BladeType, Is.True);
        }

        /// <summary>
        /// ѕример дополнительного теста с разными типами утверждений NUnit
        /// </summary>
        [Test]
        public void AdditionalNUnitFeaturesExample()
        {
            // Arrange
           
            _parameters.BladeExistence = true;
            _parameters.BindingType = BindingType.Through;

            // Assert с использованием нескольких утверждений
            Assert.Multiple(() =>
            {
                Assert.That(_parameters.BladeExistence, Is.True);
                Assert.That(_parameters.BindingType, Is.EqualTo(BindingType.Through));
            });
        }
    }
}