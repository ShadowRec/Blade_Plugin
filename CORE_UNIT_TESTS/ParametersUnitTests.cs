
using CORE;
using Newtonsoft.Json.Linq;

namespace CORE_UNIT_TESTS
{
    public class ParametersUnitTests
    {
        private Parameters _parameters = new Parameters();

        /// <summary>
        /// Выставление значений для работы тестов  
        /// </summary>
        private void FillValues()
        {
            _parameters.NumericalParameters[ParameterType.BladeWidth].Value = 40;
        }

        /// <summary>
        /// Тестирование функции SetDependensies класса Parameters
        /// </summary>
        /// <param name="maxratio"> Соотношение величин для максимального значения</param>
        /// <param name="minratio">Соотношение величин для минимального значения</param>
        [Theory]
        [InlineData((double)3 / 4, (double)1 / 4)]
        [InlineData((double)2 / 4,(double) 0)]
        public void SetDependenciesMaxMinTest( double maxratio, double minratio) 
        { 
            FillValues();
            _parameters.SetDependencies(_parameters.NumericalParameters[ParameterType.BladeWidth],
                _parameters.NumericalParameters[ParameterType.EdgeWidth], maxratio, minratio);

            double expectedMaxValue = _parameters.NumericalParameters[ParameterType.BladeWidth].Value*maxratio;
            double expectedMinValue = 0;
            if (minratio != 0)
            {
                 expectedMinValue = _parameters.NumericalParameters[ParameterType.BladeWidth].Value * minratio;
            }
            else
            {
                 expectedMinValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue * 0.1;
            }
                
            double resultMaxValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MaxValue;
            double resultMinValue = _parameters.NumericalParameters[ParameterType.EdgeWidth].MinValue;

            Assert.Equal(expectedMaxValue, resultMaxValue);
            Assert.Equal(expectedMinValue, resultMinValue);
        }
        /// <summary>
        /// Проверка валидации значения maxratio и minratio
        /// </summary>
        [Fact]
        public void SetDependenciesExceptionTest() 
        {
            FillValues();
           
            var exception = Assert.ThrowsAny<Exception>(() => {
                _parameters.SetDependencies(_parameters.NumericalParameters[ParameterType.BladeWidth],
                _parameters.NumericalParameters[ParameterType.EdgeWidth], 0, 0);
            });
            Assert.Equal("ratios_negative", exception.Message);
        }

        /// <summary>
        /// Проверка присвоения свойству BindingType значения
        /// </summary>
        [Fact]
        public void SetBindingTypeTest()
        {
            FillValues();
            _parameters.BindingType = BindingType.Through;
            Assert.Equal(BindingType.Through, _parameters.BindingType);
        }

        /// <summary>
        /// Проверка присвоения свойству BladeExistance значения
        /// </summary>
        [Fact]
        public void SetBladeExistenceTest()
        {
            FillValues();
            _parameters.BladeExistence = true;
            Assert.True(_parameters.BladeExistence);
        }

        /// <summary>
        /// Проверка присвоения свойству BladeType значения
        /// </summary>
        [Fact]
        public void SetBladeTypeTest()
        {
            FillValues();
            _parameters.BladeType = true;
            Assert.True(_parameters.BladeType);
        }
    }
}