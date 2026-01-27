using Core;
using NUnit.Framework;
using System;

namespace CoreUnitTests
{
    [TestFixture]
    public class ParametersUnitTests
    {
        //TODO: XML DONE
        /// <summary>
        /// Поле хранящее в себе параметры
        /// </summary>
        private Parameters _parameters;

        //TODO: refactor DONE

        /// <summary>
        /// Функция инициализации
        /// поля параметров
        /// </summary>
        public void Initialize()
        {
            _parameters = new Parameters();
            _parameters.NumericalParameters[
                ParameterType.BladeWidth].Value = 40;
        }

        //TODO: incoding done...

        [Test]
        [Description("Тест установки зависимостей между числовыми параметрами " +
                     "и проверки корректности вычисления максимальных и минимальных значений")]
        [TestCase(3.0 / 4, 1.0 / 4)]
        [TestCase(2.0 / 4, 0.0)]
        public void SetDependenciesMaxMinTest(
            double maxRatio, double minRatio)
        {
            Initialize();
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


        [Test]
        [Description("Тест проверки выбрасывания исключения ParameterException " +
                     "при передаче отрицательных коэффициентов в метод SetDependencies")]
        [TestCase(3.0 / 4, -1.0 / 4)]
        [TestCase(-2.0 / 4, 0.0)]
        [TestCase(-2.0 / 4, 1 / 8)]
        [TestCase(-2.0 / 4, -1 / 2)]
        public void SetDependenciesExceptionTest(
            double minratio, double maxratio)
        {
            Initialize();
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


        [Test]
        [Description("Тест установки и проверки значения типа крепления (BindingType)")]
        public void SetBindingTypeTest()
        {
            Initialize();
            _parameters.BindingType = BindingType.Through;
            Assert.That(_parameters.BindingType,
                Is.EqualTo(BindingType.Through));
        }


        [Test]
        [Description("Тест установки и проверки наличия лезвия (BladeExistence)")]
        public void SetBladeExistenceTest()
        {
            Initialize();
            _parameters.BladeExistence = true;
            Assert.That(_parameters.BladeExistence, Is.True);
        }


        [Test]
        [Description("Тест установки и проверки типа лезвия (BladeType)")]
        public void SetBladeTypeTest()
        {
            Initialize();
            _parameters.BladeType = true;
            Assert.That(_parameters.BladeType, Is.True);
        }


        [Test]
        [Description("Тест одновременной установки и проверки наличия лезвия " +
                     "и типа крепления (BladeExistence и BindingType)")]
        public void SetBladeExistenceAndBindingTypeTest()
        {
            Initialize();
            _parameters.BladeExistence = true;
            _parameters.BindingType = BindingType.Through;

            Assert.Multiple(() =>
            {
                Assert.That(_parameters.BladeExistence, Is.True);
                Assert.That(_parameters.BindingType,
                    Is.EqualTo(BindingType.Through));
            });
        }

        [Test]
        [Description("Тест установки и проверки параметров серрейтора " +
                     "(SerreitorExistance и SerreitorType)")]
        public void SetSerreitorParametersTest()
        {
            Initialize();
            _parameters.SerreitorExistance = true;
            _parameters.SerreitorType = SerreitorType.ConstBigSerreitor;

            Assert.Multiple(() =>
            {
                Assert.That(_parameters.SerreitorExistance, Is.True);
                Assert.That(_parameters.SerreitorType,
                    Is.EqualTo(SerreitorType.ConstBigSerreitor));
            });
        }
    }
}