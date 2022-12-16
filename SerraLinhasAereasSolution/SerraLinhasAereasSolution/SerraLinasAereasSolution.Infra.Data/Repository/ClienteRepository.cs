using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.DAO;
using System;
using System.Collections.Generic;


namespace SerraLinhasAereasSolution.Infra.Data.Repository
{   
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDAO _clienteDAO;
        
        public ClienteRepository()
        {
            _clienteDAO = new ClienteDAO();
        }
        public void AtualizarCliente(Cliente clienteAtualizado)
        {
            bool cpfValido = Cliente.CpfValido(clienteAtualizado.Cpf);
            if (cpfValido)
            {
                if (clienteAtualizado == null)
                {
                    throw new Exception($"Cliente não encontrado.");
                }
                else
                {
                    _clienteDAO.AtualizarCliente(clienteAtualizado);
                }
            }
            else
            {
                throw new Exception($"O CPF {clienteAtualizado.Cpf} é inválido.");
            }
        }

        public Cliente BuscarClientePorCpf(string cpf)
        {
            bool cpfValido = Cliente.CpfValido(cpf);
            if (cpfValido)
            {
                var clienteBuscado = _clienteDAO.BuscarClientePorCpf(cpf);
                if (clienteBuscado == null)
                {
                    throw new Exception($"O cliente com o CPF {cpf} não foi encontrado.");
                }
                else
                {
                   return clienteBuscado;
                }
            }
            else
            {
                throw new Exception($"O CPF {cpf} é inválido.");
            }
        }

        public List<Cliente> BuscarClientes()
        {
            var listaClientes = _clienteDAO.BuscarClientes();
            if (listaClientes.Count == 0)
            {
                throw new Exception("Nenhum cliente encontrado.");
            }
            return listaClientes;
        }

        public void DeletarCliente(string cpf)
        {
            bool cpfValido = Cliente.CpfValido(cpf);
            if (cpfValido)
            {
                var clienteBuscado = _clienteDAO.BuscarClientePorCpf(cpf);
                if (clienteBuscado == null)
                {
                    throw new Exception($"O cliente com o CPF {cpf} não foi encontrado.");
                }
                else
                {
                    _clienteDAO.DeletarCliente(clienteBuscado.Cpf);
                }
            }
            else
            {
                throw new Exception($"O CPF {cpf} é inválido.");
            }
        }

        public void RegistrarCliente(Cliente cliente)
        {
            bool cpfValido = Cliente.CpfValido(cliente.Cpf);
            if (cpfValido)
            {
                var clienteBuscado = _clienteDAO.BuscarClientePorCpf(cliente.Cpf);
                if (clienteBuscado == null)
                {
                    _clienteDAO.InserirCliente(cliente);
                }
                else
                {
                    throw new Exception($"O cliente {cliente.Nome} {cliente.Sobrenome} já foi cadastrado.");
                }
            }
            else
            {
                throw new Exception($"O CPF {cliente.Cpf} é inválido.");
            }
        }
    }
}
