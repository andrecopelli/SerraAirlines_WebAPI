using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.DAO;
using System;
using System.Collections.Generic;


namespace SerraLinhasAereasSolution.Infra.Data.Repository
{
    public class PassagemRepository : IPassagemRepository
    {
        private readonly PassagemDAO _passagemDAO;

        public PassagemRepository()
        {
            _passagemDAO = new PassagemDAO();
        }
        public void AdicionarPassagem(Passagem passagem)
        {
            bool passagemValida = Passagem.DataValida(passagem.DataOrigem, passagem.DataDestino);
            if (passagemValida)
            {
                _passagemDAO.InserirPassagem(passagem);
            }
            else
            {
                throw new Exception("As datas da passagem não são válidas.");
            }
        }

        public void AtualizarPassagem(Passagem passagemAtualizada)
        {
            if (passagemAtualizada == null)
            {
                throw new Exception("Passagem não localizada.");
            }
            _passagemDAO.AtualizaPassagem(passagemAtualizada);
        }

        public List<Passagem> BuscarPassagemPorDestino(string destino)
        {
            var listaPassagens = _passagemDAO.BuscarPassagensPorDestino(destino);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem localizada para {destino}.");
            }
            return listaPassagens;
        }

        public Passagem BuscarPassagemPorId(int id)
        {
            var passagemBuscada = _passagemDAO.BuscarPassagemPorId(id);
            if (passagemBuscada == null)
            {
                throw new Exception($"Nenhuma passagem localizada com id {id}.");
            }
            return passagemBuscada;
        }

        public List<Passagem> BuscarPassagemPorOrigem(string origem)
        {
            var listaPassagens = _passagemDAO.BuscarPassagensPorOrigem(origem);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem originária de {origem} localizada.");
            }
            return listaPassagens;
        }

        public List<Passagem> BuscarPassagens()
        {
            var listaPassagens = _passagemDAO.BuscarPassagens();
            if (listaPassagens.Count == 0)
            {
                throw new Exception("Nenhuma passagem localizada.");
            }
            return listaPassagens;
        }

        public List<Passagem> BuscarPassagensPorData(DateTime data)
        {
            var listaPassagens = _passagemDAO.BuscarPassagensPorData(data);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem localizada {data}.");
            }
            return listaPassagens;
        }
    }
}
