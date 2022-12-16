using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereasSolution.Domain.Interfaces
{
    public interface IPassagemRepository
    {
        void AdicionarPassagem(Passagem passagem);
        List<Passagem> BuscarPassagens();
        Passagem BuscarPassagemPorId(int id);
        List<Passagem> BuscarPassagensPorData(DateTime data);
        List<Passagem> BuscarPassagemPorOrigem(string origem);
        List<Passagem> BuscarPassagemPorDestino(string destino);
        void AtualizarPassagem(Passagem passagemAtualizada);
    }
}
