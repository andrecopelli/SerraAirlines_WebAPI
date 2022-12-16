using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.DAO;
using System;
using System.Collections.Generic;


namespace SerraLinhasAereasSolution.Infra.Data.Repository
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly ViagemDAO _viagemDAO;
        private readonly ClienteDAO _clienteDAO;
        private readonly PassagemDAO _passagemDAO;
        public ViagemRepository()
        {
            _viagemDAO = new ViagemDAO();
            _clienteDAO = new ClienteDAO();
            _passagemDAO = new PassagemDAO();
        }
        public List<Viagem> BuscarViagensPorCliente(string cpf)
        {
            var clienteBuscado = _clienteDAO.BuscarClientePorCpf(cpf);
            if (clienteBuscado == null)
            {
                throw new Exception("O cliente buscado não existe.");
            }
            else
            {
                var listaViagens = _viagemDAO.BuscaViagensPorCliente(clienteBuscado);
                if (listaViagens.Count == 0)
                {
                    throw new Exception($"O cliente {clienteBuscado.Nome} {clienteBuscado.Sobrenome} não possui nenhuma viagem.");
                }
                else
                {
                    return listaViagens;
                }
            }
        }

        public void MarcarViagem(Viagem viagem)
        {
            var passagemIda = _passagemDAO.BuscarPassagemPorId(viagem.PassagemIda.Id); //Busca a passagem de ida.
            if (passagemIda != null) //Verifica se a passagem de ida existe.
            {
                var listaViagens = _viagemDAO.BuscaViagens(); //Adiciona todas as viagens a lista.
                var verificacaoCodigoReserva = Viagem.CodigoValido(viagem.CodigoReserva); //Verifica se o código tem 6 caracteres.
                var codigoReservaUtilizado = listaViagens.Find(v => v.CodigoReserva == viagem.CodigoReserva); //Verifica se o código já foi utilizado.
                var passagemIdaUtilizada = listaViagens.Find(vIda => vIda.PassagemIda.Id == viagem.PassagemIda.Id); //Verifica se a passagem já foi utilizada em outra viagem.
                var cliente = _clienteDAO.BuscarClientePorCpf(viagem.Cliente.Cpf); //Verifica se o cliente existe.
                Viagem passagemVoltaUtilizada = null;
                bool viagemValida = true;
                if (!viagem.EhSohIda) //Se a viagem tiver volta entra nesta estrutura.
                {   
                    var passagemVolta = _passagemDAO.BuscarPassagemPorId(viagem.PassagemVolta.Id); //Busca a passagem de volta.
                    passagemVoltaUtilizada = listaViagens.Find(vVolta => vVolta.PassagemVolta.Id == viagem.PassagemVolta.Id); //Verifica se a passagem já foi utilizada.
                    viagemValida = Viagem.DataViagemValida(passagemIda, passagemVolta); //Valida se as passagens existem e estão em um intervalo de datas válido.
                }
                if (viagemValida) //Se as passagens forem válidas e estiverem na data correta.
                {
                    if (codigoReservaUtilizado == null && verificacaoCodigoReserva == true) //Se o código da reserva for válido e não utilizado anteriormente.
                    {
                        if (passagemIdaUtilizada == null && passagemVoltaUtilizada == null) //Se as passagens não foram utilizadas em outra viagem.
                        {
                            if (cliente != null) //E se o cliente existir.
                            {
                                _viagemDAO.MarcarViagem(viagem); //Marca a viagem.
                            }
                            else
                            {
                                throw new Exception("O cliente não existe.");
                            }
                        }
                        else
                        {
                            throw new Exception("A passagem já foi utilizada em outra viagem.");
                        }
                    }
                    else
                    {
                        throw new Exception($"O código de reserva {viagem.CodigoReserva} é inválido ou já foi utilizado.");
                    }
                }
                else
                {
                    throw new Exception($"As passagens não são compativeis.");
                }
            }
            else
            {
                throw new Exception($"A passagem informada não foi encontrada.");
            }
            
        }

        public void RemarcarViagemIda(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            var viagemRemarcada = _viagemDAO.BuscaViagensPorId(idViagem);
            if (viagemRemarcada == null)
            {
                throw new Exception("Não é possível remarcar pois a viagem não encontrada.");
            }
            bool passagemValida = Passagem.DataValida(dataOrigem, dataDestino);
            if (passagemValida)
            {
                _viagemDAO.RemarcarViagemIda(viagemRemarcada.Id, dataOrigem, dataDestino);
            }
            else
            {
                throw new Exception("As datas da remarcação não são válidas.");
            }
        }

        public void RemarcarViagemVolta(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            var viagemRemarcada = _viagemDAO.BuscaViagensPorId(idViagem);
            if (viagemRemarcada == null)
            {
                throw new Exception("Não é possível remarcar pois a viagem não encontrada.");
            }
            bool passagemValida = Passagem.DataValida(dataOrigem, dataDestino);
            if (passagemValida)
            {
                _viagemDAO.RemarcarViagemVolta(viagemRemarcada.Id, dataOrigem, dataDestino);
            }
            else
            {
                throw new Exception("As datas da remarcação não são válidas.");
            }
        }
    }
}
