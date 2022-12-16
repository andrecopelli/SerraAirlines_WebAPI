using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SerraLinhasAereasSolution.Infra.Data.DAO
{
    public class PassagemDAO
    {
        private readonly string _connectionString = @"data source=.\SQLEXPRESS;initial catalog= SERRA_LINHAS_AEREAS_BD;uid= sa;pwd= trop;";

        public void InserirPassagem(Passagem novaPassagem)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT PASSAGENS VALUES (@ORIGEM, @DESTINO, @VALOR, @DATA_ORIGEM, @DATA_DESTINO);";
                    comando.Parameters.AddWithValue("@ORIGEM", novaPassagem.Origem);
                    comando.Parameters.AddWithValue("@DESTINO", novaPassagem.Destino);
                    comando.Parameters.AddWithValue("@VALOR", novaPassagem.Valor);
                    comando.Parameters.AddWithValue("@DATA_ORIGEM", novaPassagem.DataOrigem);
                    comando.Parameters.AddWithValue("@DATA_DESTINO", novaPassagem.DataDestino);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizaPassagem(Passagem passagemEditada)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PASSAGENS SET ORIGEM = @ORIGEM, DESTINO = @DESTINO, VALOR = @VALOR,
                                   DATA_ORIGEM = @DATA_ORIGEM, DATA_DESTINO = @DATA_DESTINO WHERE ID = @ID;";
                    comando.Parameters.AddWithValue("@ID", passagemEditada.Id);
                    comando.Parameters.AddWithValue("@ORIGEM", passagemEditada.Origem);
                    comando.Parameters.AddWithValue("@DESTINO", passagemEditada.Destino);
                    comando.Parameters.AddWithValue("@VALOR", passagemEditada.Valor);
                    comando.Parameters.AddWithValue("@DATA_ORIGEM", passagemEditada.DataOrigem);
                    comando.Parameters.AddWithValue("@DATA_DESTINO", passagemEditada.DataDestino);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Passagem> BuscarPassagens()
        { 
            var listaPassagens = new List<Passagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATA_ORIGEM, DATA_DESTINO FROM PASSAGENS";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var idPassagem = Convert.ToInt32(leitor["ID"].ToString());
                        var origem = leitor["ORIGEM"].ToString();
                        var destino = leitor["DESTINO"].ToString();
                        var valor = Convert.ToDecimal(leitor["VALOR"].ToString());
                        var dataOrigem = Convert.ToDateTime(leitor["DATA_ORIGEM"].ToString());
                        var dataDestino = Convert.ToDateTime(leitor["DATA_DESTINO"].ToString());
                        Passagem passagem = new Passagem(idPassagem, origem, destino, valor, dataOrigem, dataDestino);
                        listaPassagens.Add(passagem);
                    }
                }
            }
            return listaPassagens;
        }

        public Passagem BuscarPassagemPorId(int id)
        {
            var listaPassagens = new List<Passagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATA_ORIGEM, DATA_DESTINO FROM PASSAGENS
                                   WHERE ID = @ID";
                    comando.Parameters.AddWithValue("@ID", id);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var idPassagem = Convert.ToInt32(leitor["ID"].ToString());
                        var origem = leitor["ORIGEM"].ToString();
                        var destino = leitor["DESTINO"].ToString();
                        var valor = Convert.ToDecimal(leitor["VALOR"].ToString());
                        var dataOrigem = Convert.ToDateTime(leitor["DATA_ORIGEM"].ToString());
                        var dataDestino = Convert.ToDateTime(leitor["DATA_DESTINO"].ToString());
                        Passagem passagem = new Passagem(idPassagem, origem, destino, valor, dataOrigem, dataDestino);
                        return passagem;
                    }
                }
            }
            return null;
        }

        public List<Passagem> BuscarPassagensPorData(DateTime dataBuscada)
        {
            var listaPassagens = new List<Passagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATA_ORIGEM, DATA_DESTINO FROM PASSAGENS
                                   WHERE CONVERT(DATE, DATA_ORIGEM) = CONVERT(DATE, @DATA_BUSCADA) OR
                                   CONVERT(DATE, DATA_DESTINO) = CONVERT(DATE, @DATA_BUSCADA);";
                    comando.Parameters.AddWithValue("@DATA_BUSCADA", dataBuscada);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var idPassagem = Convert.ToInt32(leitor["ID"].ToString());
                        var origem = leitor["ORIGEM"].ToString();
                        var destino = leitor["DESTINO"].ToString();
                        var valor = Convert.ToDecimal(leitor["VALOR"].ToString());
                        var dataOrigem = Convert.ToDateTime(leitor["DATA_ORIGEM"].ToString());
                        var dataDestino = Convert.ToDateTime(leitor["DATA_DESTINO"].ToString());
                        Passagem passagem = new Passagem(idPassagem, origem, destino, valor, dataOrigem, dataDestino);
                        listaPassagens.Add(passagem);
                    }
                }
            }
            return listaPassagens;
        }

        public List<Passagem> BuscarPassagensPorOrigem(string origemBuscada)
        {
            var listaPassagens = new List<Passagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATA_ORIGEM, DATA_DESTINO FROM PASSAGENS
                                    WHERE ORIGEM = @ORIGEM_BUSCADA;";
                    comando.Parameters.AddWithValue("@ORIGEM_BUSCADA", origemBuscada);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var idPassagem = Convert.ToInt32(leitor["ID"].ToString());
                        var origem = leitor["ORIGEM"].ToString();
                        var destino = leitor["DESTINO"].ToString();
                        var valor = Convert.ToDecimal(leitor["VALOR"].ToString());
                        var dataOrigem = Convert.ToDateTime(leitor["DATA_ORIGEM"].ToString());
                        var dataDestino = Convert.ToDateTime(leitor["DATA_DESTINO"].ToString());
                        Passagem passagem = new Passagem(idPassagem, origem, destino, valor, dataOrigem, dataDestino);
                        listaPassagens.Add(passagem);
                    }
                }
            }
            return listaPassagens;
        }

        public List<Passagem> BuscarPassagensPorDestino(string destinoBuscado)
        {
            var listaPassagens = new List<Passagem>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATA_ORIGEM, DATA_DESTINO FROM PASSAGENS
                                    WHERE DESTINO = @DESTINO_BUSCADO;";
                    comando.Parameters.AddWithValue("@DESTINO_BUSCADO", destinoBuscado);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var idPassagem = Convert.ToInt32(leitor["ID"].ToString());
                        var origem = leitor["ORIGEM"].ToString();
                        var destino = leitor["DESTINO"].ToString();
                        var valor = Convert.ToDecimal(leitor["VALOR"].ToString());
                        var dataOrigem = Convert.ToDateTime(leitor["DATA_ORIGEM"].ToString());
                        var dataDestino = Convert.ToDateTime(leitor["DATA_DESTINO"].ToString());
                        Passagem passagem = new Passagem(idPassagem, origem, destino, valor, dataOrigem, dataDestino);
                        listaPassagens.Add(passagem);
                    }
                }
            }
            return listaPassagens;
        }
    }
}
