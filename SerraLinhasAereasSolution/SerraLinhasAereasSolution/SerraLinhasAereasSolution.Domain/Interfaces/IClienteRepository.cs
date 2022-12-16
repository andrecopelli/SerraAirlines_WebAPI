using SerraLinhasAereasSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereasSolution.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void RegistrarCliente(Cliente cliente);
        Cliente BuscarClientePorCpf(string cpf);
        List<Cliente> BuscarClientes();
        void AtualizarCliente(Cliente clienteAtualizado);
        void DeletarCliente(string cpf);
    }
}
