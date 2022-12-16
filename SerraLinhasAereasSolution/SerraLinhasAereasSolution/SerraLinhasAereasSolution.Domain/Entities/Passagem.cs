using System;

namespace SerraLinhasAereasSolution.Domain.Entities
{
    public class Passagem
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataOrigem { get; set; }
        public DateTime DataDestino { get; set; }
        public Passagem()
        {

        }

        public Passagem(int id, string origem, string destino, decimal valor, DateTime dataOrigem, DateTime dataDestino)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            Valor = valor;
            DataOrigem = dataOrigem;
            DataDestino = dataDestino;
        }

        public static bool DataValida(DateTime dataOrigem, DateTime dataDestino)
        { 
            return dataDestino > dataOrigem;
        }
    }
}
