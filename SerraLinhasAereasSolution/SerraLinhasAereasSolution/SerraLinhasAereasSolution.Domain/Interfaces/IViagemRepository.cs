using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;


namespace SerraLinhasAereasSolution.Domain.Interfaces
{
    public interface IViagemRepository
    {
        void MarcarViagem(Viagem viagem);
        List<Viagem> BuscarViagensPorCliente(string cpf);
        void RemarcarViagemIda(int idViagem, DateTime dataOrigem, DateTime dataDestino);
        void RemarcarViagemVolta(int idViagem, DateTime dataOrigem, DateTime dataDestino);
    }
}
