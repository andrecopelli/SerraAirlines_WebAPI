using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SerraLinhasAereasSolution.Infra.Data.DAO
{
    public class ClienteDAO
    {
        private readonly string _connectionString = @"data source=.\SQLEXPRESS;initial catalog= SERRA_LINHAS_AEREAS_BD;uid= sa;pwd= trop;";

        public void InserirCliente(Cliente novoCliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT CLIENTES(CPF, NOME, SOBRENOME, CEP, RUA, NUMERO, BAIRRO, CIDADE, ESTADO, COMPLEMENTO) 
                                   VALUES (@CPF, @NOME, @SOBRENOME, @CEP, @RUA, @NUMERO, @BAIRRO, @CIDADE,
                                   @ESTADO, @COMPLEMENTO)";
                    comando.Parameters.AddWithValue("@CPF", novoCliente.Cpf);
                    comando.Parameters.AddWithValue("@NOME", novoCliente.Nome);
                    comando.Parameters.AddWithValue("@SOBRENOME", novoCliente.Sobrenome);
                    comando.Parameters.AddWithValue("@CEP", novoCliente.Cep);
                    comando.Parameters.AddWithValue("@RUA", novoCliente.Rua);
                    comando.Parameters.AddWithValue("@NUMERO", novoCliente.Numero);
                    comando.Parameters.AddWithValue("@BAIRRO", novoCliente.Bairro);
                    comando.Parameters.AddWithValue("@CIDADE", novoCliente.Cidade);
                    comando.Parameters.AddWithValue("@ESTADO", novoCliente.Estado);
                    comando.Parameters.AddWithValue("@COMPLEMENTO", novoCliente.Complemento);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarCliente(Cliente clienteEditado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE CLIENTES SET CPF = @CPF, NOME = @NOME, SOBRENOME = @SOBRENOME, CEP = @CEP, RUA = @RUA,
                                 NUMERO = @NUMERO, BAIRRO = @BAIRRO, CIDADE = @CIDADE, ESTADO = @ESTADO, COMPLEMENTO = @COMPLEMENTO
                                 WHERE CPF = @CPF";
                    comando.Parameters.AddWithValue("@CPF", clienteEditado.Cpf);
                    comando.Parameters.AddWithValue("@NOME", clienteEditado.Nome);
                    comando.Parameters.AddWithValue("@SOBRENOME", clienteEditado.Sobrenome);
                    comando.Parameters.AddWithValue("@CEP", clienteEditado.Cep);
                    comando.Parameters.AddWithValue("@RUA", clienteEditado.Rua);
                    comando.Parameters.AddWithValue("@NUMERO", clienteEditado.Numero);
                    comando.Parameters.AddWithValue("@BAIRRO", clienteEditado.Bairro);
                    comando.Parameters.AddWithValue("@CIDADE", clienteEditado.Cidade);
                    comando.Parameters.AddWithValue("@ESTADO", clienteEditado.Estado);
                    comando.Parameters.AddWithValue("@COMPLEMENTO", clienteEditado.Complemento);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> BuscarClientes()
        { 
            var listaClientes = new List<Cliente>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT CPF, NOME, SOBRENOME, CEP, RUA, NUMERO, BAIRRO, CIDADE, ESTADO,
                                   COMPLEMENTO FROM CLIENTES;";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var cpf = leitor["CPF"].ToString();
                        var nome = leitor["NOME"].ToString();
                        var sobrenome = leitor["SOBRENOME"].ToString();
                        var cep = leitor["CEP"].ToString();
                        var rua = leitor["RUA"].ToString();
                        var numero = Convert.ToInt32(leitor["NUMERO"].ToString());
                        var bairro = leitor["BAIRRO"].ToString();
                        var cidade = leitor["CIDADE"].ToString();
                        var estado = leitor["ESTADO"].ToString();
                        var complemento = leitor["COMPLEMENTO"].ToString();
                        var cliente = new Cliente(cpf, nome, sobrenome, cep, rua, numero, bairro, cidade, estado, complemento);
                        listaClientes.Add(cliente);
                    }
                }
            }
            return listaClientes;
        }

        public Cliente BuscarClientePorCpf(string cpfDigitado)
        {   
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT CPF, NOME, SOBRENOME, CEP, RUA, NUMERO, BAIRRO, CIDADE, ESTADO, COMPLEMENTO
                                   FROM CLIENTES WHERE CPF = @CPF_CLIENTE;";
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", cpfDigitado);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        var cpf = leitor["CPF"].ToString();
                        var nome = leitor["NOME"].ToString();
                        var sobrenome = leitor["SOBRENOME"].ToString();
                        var cep = leitor["CEP"].ToString();
                        var rua = leitor["RUA"].ToString();
                        var numero = Convert.ToInt32(leitor["NUMERO"].ToString());
                        var bairro = leitor["BAIRRO"].ToString();
                        var cidade = leitor["CIDADE"].ToString();
                        var estado = leitor["ESTADO"].ToString();
                        var complemento = leitor["COMPLEMENTO"].ToString();
                        var clienteBuscado = new Cliente(cpf, nome, sobrenome, cep, rua, numero, bairro, cidade, estado, complemento);
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }

        public void DeletarCliente(string cpfDigitado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM CLIENTES WHERE CPF = @CPF_CLIENTE;";
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", cpfDigitado);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
