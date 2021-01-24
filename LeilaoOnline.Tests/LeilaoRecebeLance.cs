using LeilaoOnline.Core.Classes;
using LeilaoOnline.Core.Regra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoLeilaoFinalizado()
        {
            //Arrange
            var modalidade = new MaiorValor();

            var leilao = new Leilao(peca: "Notebook HP", modalidade);

            var vinicius = new Interessado("Vinicius", leilao);
            var pamela = new Interessado("Pamela", leilao);

            leilao.Iniciar();

            leilao.RecebeLance(vinicius, 500);
            leilao.RecebeLance(pamela, 600);
            leilao.Finalizar();

            //Act

            leilao.RecebeLance(vinicius, 700);

            //Assert

            var qtdLancesObtido = leilao.Lances.Count();
            var qtdLancesEsperados = 2;

            Assert.Equal(qtdLancesEsperados, qtdLancesObtido);
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteUltimoLance()
        {
            //Arrange

            var modalidade = new MaiorValor();

            var leilao = new Leilao("Geladeira", modalidade);

            var vinicius = new Interessado("Vinicius", leilao);

            leilao.Iniciar();

            leilao.RecebeLance(vinicius, 800);

            //Act
            leilao.RecebeLance(vinicius, 900);

            //Assert
            double qtdLancesEsperado = 1;
            double qtdLancesObtido = leilao.Lances.Count();

            Assert.Equal(qtdLancesEsperado, qtdLancesObtido);
        }
    }
}
