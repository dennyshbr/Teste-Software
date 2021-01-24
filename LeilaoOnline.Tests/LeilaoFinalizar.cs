using LeilaoOnline.Core.Classes;
using LeilaoOnline.Core.Regra;
using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoFinalizar
    {
        [Theory]
        [InlineData(1000, new double[] { 800, 900, 950, 980, 1000 })]
        [InlineData(1200, new double[] { 800, 900, 950, 980, 1200 })]
        [InlineData(950, new double[] { 950 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmValor(
            double valorEsperado, double[] valores)
        {
            //Arrange

            var modalidade = new MaiorValor();

            var leilao = new Leilao(peca: "Tv Samsung", modalidade);

            leilao.Iniciar();

            var beatriz = new Interessado("Beatriz", leilao);
            var mario = new Interessado("Mario", leilao);

            for (int contador = 0; contador < valores.Length; contador++)
            {
                if ((contador % 2) == 0)
                    leilao.RecebeLance(beatriz, valores[contador]);
                else
                    leilao.RecebeLance(mario, valores[contador]);
            }

            //Act
            leilao.Finalizar();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoLeilaoNaoIniciado()
        {
            //Arrange
            var modalidade = new MaiorValor();

            var leilao = new Leilao(peca: "Tv Samsung", modalidade);

            //Assert
            var excecaoObtida = Assert.Throws<InvalidOperationException>(

                //Act
                () => leilao.Finalizar()
            );

            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método Iniciar()";

            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange - Cenários de entrada
            var modalidade = new MaiorValor();
            var leilao = new Leilao(peca: "Notebook HP", modalidade);

            leilao.Iniciar();

            //Act - método sob teste
            leilao.Finalizar();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1250, 1400 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double[] ofertas)
        {
            //Arrange
            var modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Cama Box com Colchão", modalidade);

            leilao.Iniciar();

            var marina = new Interessado("Marina", leilao);
            var maria = new Interessado("Maria", leilao);

            for (int contador = 0; contador < ofertas.Length; contador++)
            {
                if ((contador % 2) == 0)
                    leilao.RecebeLance(marina, ofertas[contador]);
                else
                    leilao.RecebeLance(maria, ofertas[contador]);
            }

            //Act
            leilao.Finalizar();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }
    }
}
