using System;

namespace SerraLinhasAereasSolution.Domain.Entities
{
    public class Viagem
    {
        public int Id { get; set; }
        public string CodigoReserva { get; set; }
        public DateTime DataCompra { get { return DateTime.Now; } }
        public decimal ValorTotal => CalculaTotal(); 
        public Cliente Cliente { get; set; }
        public bool EhSohIda { get; set; }
        public Passagem PassagemIda { get; set; }
        #nullable enable
        public Passagem? PassagemVolta { get; set; }
        public string ResumoDaViagem => ViagemResumida();



        public Viagem(int id, string codigoReserva, Cliente cliente, bool sohIda, Passagem passagemIda, Passagem? passagemVolta)
        {
            Id = id;
            CodigoReserva = codigoReserva;
            Cliente = cliente;
            EhSohIda = sohIda;
            PassagemIda = passagemIda;
            PassagemVolta = passagemVolta;
        }
        #nullable disable
        public Viagem()
        {
            
        }

        private string ViagemResumida()
        {
            if (this.EhSohIda)
            {
                return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataOrigem.ToShortDateString()}" +
                       $" as {PassagemIda.DataOrigem.ToShortTimeString()}h";
            }
            return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataOrigem.ToShortDateString()}" +
                       $" as {PassagemIda.DataOrigem.ToShortTimeString()}h e seu voo de volta de {PassagemVolta.Origem}" +
                       $" a {PassagemVolta.Destino} será dia {PassagemVolta.DataOrigem.ToShortDateString()}" +
                       $" as {PassagemVolta.DataOrigem.ToShortTimeString()}h";
        }

        public decimal CalculaTotal()
        {
            return EhSohIda == true ? PassagemIda.Valor : this.PassagemIda.Valor + this.PassagemVolta.Valor;
        }

        public bool PassagemValida(Passagem passagem)
        {
            return passagem == null ? false : true;
        }

        public static bool DataViagemValida(Passagem passagemIda, Passagem passagemVolta)
        {
            if (passagemIda == null || passagemVolta == null)
            {
                return false;
            }
            else
            {
                if (passagemIda.DataOrigem < passagemVolta.DataOrigem)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public static bool CodigoValido(string codigo)
        { 
            return codigo.Length == 6 ? true : false;
        }
    }

}
