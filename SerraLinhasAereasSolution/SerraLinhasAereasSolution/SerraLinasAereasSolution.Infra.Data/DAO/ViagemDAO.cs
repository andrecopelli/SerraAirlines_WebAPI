using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SerraLinhasAereasSolution.Infra.Data.DAO
{
    public class ViagemDAO
    {
        private readonly string _connectionString = @"data source=.\SQLEXPRESS;initial catalog= SERRA_LINHAS_AEREAS_BD;uid= sa;pwd= trop;";

        public void MarcarViagem(Viagem novaViagem)
        {
            
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT VIAGENS(CODIGO_RESERVA, DATA_COMPRA, EH_SOH_IDA, CLIENTE_ID, PASSAGEM_IDA_ID, PASSAGEM_VOLTA_ID) 
                                   VALUES (@CODIGO_RESERVA, @DATA_COMPRA, @EH_SOH_IDA, @CLIENTE_ID, @PASSAGEM_IDA_ID, @PASSAGEM_VOLTA_ID);";
                    comando.Parameters.AddWithValue("@CODIGO_RESERVA", novaViagem.CodigoReserva);
                    comando.Parameters.AddWithValue("@DATA_COMPRA", novaViagem.DataCompra);
                    comando.Parameters.AddWithValue("@EH_SOH_IDA", novaViagem.EhSohIda);
                    comando.Parameters.AddWithValue("@CLIENTE_ID", novaViagem.Cliente.Cpf);
                    comando.Parameters.AddWithValue("@PASSAGEM_IDA_ID", novaViagem.PassagemIda.Id);
                    comando.Parameters.AddWithValue("@PASSAGEM_VOLTA_ID", novaViagem.PassagemVolta == null ? DBNull.Value : novaViagem.PassagemVolta.Id);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Viagem> BuscaViagensPorCliente(Cliente clienteBuscado)
        {
            var listaViagens = new List<Viagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATA_ORIGEM AS IDA_DATA_ORIGEM, PI.DATA_DESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATA_ORIGEM AS VOLTA_DATA_ORIGEM, PV.DATA_DESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEM_IDA_ID 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEM_VOLTA_ID
                                   WHERE V.CLIENTE_ID = @CPF_DIGITADO;";
                    comando.Parameters.AddWithValue("@CPF_DIGITADO", clienteBuscado.Cpf);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var id = Convert.ToInt32(leitor["ID"].ToString());
                        var codigo = leitor["CODIGO_RESERVA"].ToString();
                        var ehSohIda = Convert.ToBoolean(leitor["EH_SOH_IDA"].ToString());
                        var cliente = new Cliente();
                        cliente.Cpf = leitor["CPF"].ToString();
                        cliente.Nome = leitor["NOME"].ToString();
                        cliente.Sobrenome = leitor["SOBRENOME"].ToString();
                        cliente.Cep = leitor["CEP"].ToString();
                        cliente.Rua = leitor["RUA"].ToString();
                        cliente.Bairro = leitor["BAIRRO"].ToString();
                        cliente.Numero = Convert.ToInt32(leitor["NUMERO"].ToString());
                        cliente.Cidade = leitor["CIDADE"].ToString();
                        cliente.Estado = leitor["ESTADO"].ToString();
                        cliente.Complemento = leitor["COMPLEMENTO"].ToString();
                        var passagemIda = new Passagem();
                        passagemIda.Id = Convert.ToInt32(leitor["IDA_ID"].ToString());
                        passagemIda.Valor = Convert.ToDecimal(leitor["IDA_VALOR"].ToString());
                        passagemIda.Origem = leitor["IDA_ORIGEM"].ToString();
                        passagemIda.Destino = leitor["IDA_DESTINO"].ToString();
                        passagemIda.DataOrigem = Convert.ToDateTime(leitor["IDA_DATA_ORIGEM"].ToString());
                        passagemIda.DataDestino = Convert.ToDateTime(leitor["IDA_DATA_DESTINO"].ToString());
                        var passagemVolta = new Passagem();
                        if (!ehSohIda)
                        {
                            passagemVolta.Id = Convert.ToInt32(leitor["VOLTA_ID"].ToString());
                            passagemVolta.Valor = Convert.ToDecimal(leitor["VOLTA_VALOR"].ToString());
                            passagemVolta.Origem = leitor["VOLTA_ORIGEM"].ToString();
                            passagemVolta.Destino = leitor["VOLTA_DESTINO"].ToString();
                            passagemVolta.DataOrigem = Convert.ToDateTime(leitor["VOLTA_DATA_ORIGEM"].ToString());
                            passagemVolta.DataDestino = Convert.ToDateTime(leitor["VOLTA_DATA_DESTINO"].ToString());
                        }
                        var viagem = new Viagem(id, codigo, cliente, ehSohIda, passagemIda, passagemVolta);
                        listaViagens.Add(viagem);
                    }
                }
            }
            return listaViagens;
        }

        public Viagem BuscaViagensPorId(int idViagem)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATA_ORIGEM AS IDA_DATA_ORIGEM, PI.DATA_DESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATA_ORIGEM AS VOLTA_DATA_ORIGEM, PV.DATA_DESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEM_IDA_ID 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEM_VOLTA_ID
                                   WHERE V.ID = @ID_DIGITADO";
                    comando.Parameters.AddWithValue("@ID_DIGITADO", idViagem);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var id = Convert.ToInt32(leitor["ID"].ToString());
                        var codigo = leitor["CODIGO_RESERVA"].ToString();
                        var ehSohIda = Convert.ToBoolean(leitor["EH_SOH_IDA"].ToString());
                        var cliente = new Cliente();
                        cliente.Cpf = leitor["CPF"].ToString();
                        cliente.Nome = leitor["NOME"].ToString();
                        cliente.Sobrenome = leitor["SOBRENOME"].ToString();
                        cliente.Cep = leitor["CEP"].ToString();
                        cliente.Rua = leitor["RUA"].ToString();
                        cliente.Bairro = leitor["BAIRRO"].ToString();
                        cliente.Numero = Convert.ToInt32(leitor["NUMERO"].ToString());
                        cliente.Cidade = leitor["CIDADE"].ToString();
                        cliente.Estado = leitor["ESTADO"].ToString();
                        cliente.Complemento = leitor["COMPLEMENTO"].ToString();
                        var passagemIda = new Passagem();
                        passagemIda.Id = Convert.ToInt32(leitor["IDA_ID"].ToString());
                        passagemIda.Valor = Convert.ToDecimal(leitor["IDA_VALOR"].ToString());
                        passagemIda.Origem = leitor["IDA_ORIGEM"].ToString();
                        passagemIda.Destino = leitor["IDA_DESTINO"].ToString();
                        passagemIda.DataOrigem = Convert.ToDateTime(leitor["IDA_DATA_ORIGEM"].ToString());
                        passagemIda.DataDestino = Convert.ToDateTime(leitor["IDA_DATA_DESTINO"].ToString());
                        var passagemVolta = new Passagem();
                        if (!ehSohIda)
                        {
                            passagemVolta.Id = Convert.ToInt32(leitor["VOLTA_ID"].ToString());
                            passagemVolta.Valor = Convert.ToDecimal(leitor["VOLTA_VALOR"].ToString());
                            passagemVolta.Origem = leitor["VOLTA_ORIGEM"].ToString();
                            passagemVolta.Destino = leitor["VOLTA_DESTINO"].ToString();
                            passagemVolta.DataOrigem = Convert.ToDateTime(leitor["VOLTA_DATA_ORIGEM"].ToString());
                            passagemVolta.DataDestino = Convert.ToDateTime(leitor["VOLTA_DATA_DESTINO"].ToString());
                        }
                        var viagem = new Viagem(id, codigo, cliente, ehSohIda, passagemIda, passagemVolta);
                        return viagem;
                    }
                }
            }
            return null;
        }

        public List<Viagem> BuscaViagens()
        {
            var listaViagens = new List<Viagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATA_ORIGEM AS IDA_DATA_ORIGEM, PI.DATA_DESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATA_ORIGEM AS VOLTA_DATA_ORIGEM, PV.DATA_DESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEM_IDA_ID 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEM_VOLTA_ID";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var id = Convert.ToInt32(leitor["ID"].ToString());
                        var codigo = leitor["CODIGO_RESERVA"].ToString();
                        var ehSohIda = Convert.ToBoolean(leitor["EH_SOH_IDA"].ToString());
                        var cliente = new Cliente();
                        cliente.Cpf = leitor["CPF"].ToString();
                        cliente.Nome = leitor["NOME"].ToString();
                        cliente.Sobrenome = leitor["SOBRENOME"].ToString();
                        cliente.Cep = leitor["CEP"].ToString();
                        cliente.Rua = leitor["RUA"].ToString();
                        cliente.Bairro = leitor["BAIRRO"].ToString();
                        cliente.Numero = Convert.ToInt32(leitor["NUMERO"].ToString());
                        cliente.Cidade = leitor["CIDADE"].ToString();
                        cliente.Estado = leitor["ESTADO"].ToString();
                        cliente.Complemento = leitor["COMPLEMENTO"].ToString();
                        var passagemIda = new Passagem();
                        passagemIda.Id = Convert.ToInt32(leitor["IDA_ID"].ToString());
                        passagemIda.Valor = Convert.ToDecimal(leitor["IDA_VALOR"].ToString());
                        passagemIda.Origem = leitor["IDA_ORIGEM"].ToString();
                        passagemIda.Destino = leitor["IDA_DESTINO"].ToString();
                        passagemIda.DataOrigem = Convert.ToDateTime(leitor["IDA_DATA_ORIGEM"].ToString());
                        passagemIda.DataDestino = Convert.ToDateTime(leitor["IDA_DATA_DESTINO"].ToString());
                        var passagemVolta = new Passagem();
                        if (!ehSohIda)
                        {   
                            passagemVolta.Id = Convert.ToInt32(leitor["VOLTA_ID"].ToString());
                            passagemVolta.Valor = Convert.ToDecimal(leitor["VOLTA_VALOR"].ToString());
                            passagemVolta.Origem = leitor["VOLTA_ORIGEM"].ToString();
                            passagemVolta.Destino = leitor["VOLTA_DESTINO"].ToString();
                            passagemVolta.DataOrigem = Convert.ToDateTime(leitor["VOLTA_DATA_ORIGEM"].ToString());
                            passagemVolta.DataDestino = Convert.ToDateTime(leitor["VOLTA_DATA_DESTINO"].ToString());
                        }
                        var viagem = new Viagem(id, codigo, cliente, ehSohIda, passagemIda, passagemVolta);
                        listaViagens.Add(viagem);
                    }
                }
            }
            return listaViagens;
        }

        public void RemarcarViagemIda(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            var viagemRemarcada = BuscaViagensPorId(idViagem);
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PASSAGENS SET DATA_ORIGEM = @DATA_ORIGEM, DATA_DESTINO = @DATA_DESTINO
                                   FROM PASSAGENS P INNER JOIN VIAGENS V ON P.ID = V.PASSAGEM_IDA_ID
                                   WHERE V.ID = @ID_VIAGEM;";
                    comando.Parameters.AddWithValue("@DATA_ORIGEM", dataOrigem);
                    comando.Parameters.AddWithValue("@DATA_DESTINO", dataDestino);
                    comando.Parameters.AddWithValue("@ID_VIAGEM", viagemRemarcada == null ? DBNull.Value : viagemRemarcada.Id);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void RemarcarViagemVolta(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            var viagemRemarcada = BuscaViagensPorId(idViagem);
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PASSAGENS SET DATA_ORIGEM = @DATA_ORIGEM, DATA_DESTINO = @DATA_DESTINO
                                   FROM PASSAGENS P INNER JOIN VIAGENS V ON P.ID = V.PASSAGEM_VOLTA_ID
                                   WHERE V.ID = @ID_VIAGEM;";
                    comando.Parameters.AddWithValue("@DATA_ORIGEM", dataOrigem);
                    comando.Parameters.AddWithValue("@DATA_DESTINO", dataDestino);
                    comando.Parameters.AddWithValue("@ID_VIAGEM", viagemRemarcada == null ? DBNull.Value : viagemRemarcada.Id);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
