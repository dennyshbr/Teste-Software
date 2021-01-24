using LeilaoOnline.Core.Classes;
using LeilaoOnline.Core.Regra;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorMenorQueZero()
        {
            //Arrange
            var valorNegativo = -10;

            //Assert
            Assert.Throws<ArgumentException>(

                //Act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
