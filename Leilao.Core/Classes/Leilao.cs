using LeilaoOnline.Core.Interface;
using System;
using System.Collections.Generic;

namespace LeilaoOnline.Core.Classes
{
    public class Leilao
    {
        private IList<Lance> _lances;

        public IEnumerable<Lance> Lances => _lances;

        private IModalidadeAvaliacao _avaliador;

        public string Peca { get; }

        public Lance Ganhador { get; private set; }

        public EstadoLeilao Estado { get; set; }

        private Interessado _ultimoCliente;

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.Nao_Iniciado;
            _avaliador = avaliador;
        }

        private bool NovoLanceAceito(Interessado cliente, double valor)
        {
            return (Estado == EstadoLeilao.Em_Andamento)
                    && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessado cliente, double valor)
        {
            if (NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void Iniciar()
        {
            Estado = EstadoLeilao.Em_Andamento;
        }

        public void Finalizar()
        {
            if (Estado != EstadoLeilao.Em_Andamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método Iniciar()");
            }

            Ganhador = _avaliador.Avalia(this);

            Estado = EstadoLeilao.Finalizado;
        }
    }

    public enum EstadoLeilao
    {
        Nao_Iniciado,
        Em_Andamento,
        Finalizado
    }
}
