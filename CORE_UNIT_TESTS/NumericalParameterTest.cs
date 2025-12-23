using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE;

namespace CORE_UNIT_TESTS
{
    public class NumericalParameterTest
    {
        private Parameters _parameters = new Parameters();

       
   
        /// <summary>
        /// Проверка валидации значения
        /// </summary>
        /// <param name="expectedstring"></param>
        /// <param name="value"></param>
        [Theory]
        [InlineData("Value_small",5)]
        [InlineData("Value_TooBig", 100)]
        public void ValidateExeptionTest(string expectedstring,double value)
        {
            var exception = Assert.ThrowsAny<Exception>(() => {
                _parameters.NumericalParameters[ParameterType.BladeWidth].Value=value;
            });
            Assert.Equal(expectedstring, exception.Message);
        }
    }
}
