using System;

namespace LeilaoOnline.Core.Classes
{
    public class Lance
    {
        public Interessado Cliente { get; set; }

        public double Valor { get; set; }

        public Lance(Interessado cliente, double valor)
        {
            Cliente = cliente;

            if (valor < 0)
            {
                throw new ArgumentException("Valor do lance deve ser maior ou igual a zero.");
            }

            Valor = valor;
        }
    }
}
