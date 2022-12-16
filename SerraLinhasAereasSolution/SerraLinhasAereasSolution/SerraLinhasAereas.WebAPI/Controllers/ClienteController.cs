using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.Repository;
using System;

namespace SerraLinhasAereasSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController()
        {
            _clienteRepository = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult PostCliente(Cliente novoCliente)
        {
            try
            {
                _clienteRepository.RegistrarCliente(novoCliente);
                return Ok(new Resposta(200, "Cliente cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientes());

            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("cpf")]
        public IActionResult GetClientePorCpf([FromQuery] string cpf)
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientePorCpf(cpf));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpPut]
        public IActionResult PutCliente([FromBody] Cliente clienteEditado)
        {
            try
            {
                var clienteBuscado = _clienteRepository.BuscarClientePorCpf(clienteEditado.Cpf);
                clienteBuscado.Nome = clienteEditado.Nome;
                clienteBuscado.Sobrenome = clienteEditado.Sobrenome;
                clienteBuscado.Cep = clienteEditado.Cep;
                clienteBuscado.Rua = clienteEditado.Rua;
                clienteBuscado.Numero = clienteEditado.Numero;
                clienteBuscado.Bairro = clienteEditado.Bairro;
                clienteBuscado.Cidade =clienteEditado.Cidade;
                clienteBuscado.Estado = clienteEditado.Estado;
                clienteBuscado.Complemento = clienteEditado.Complemento;
                _clienteRepository.AtualizarCliente(clienteBuscado);
                return Ok(new Resposta(200, "Cliente atualizado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpDelete]
        public IActionResult DeleteCliente([FromQuery] string cpf)
        {
            try
            {
                var clienteBuscado = _clienteRepository.BuscarClientePorCpf(cpf);
                _clienteRepository.DeletarCliente(clienteBuscado.Cpf);
                return Ok(new Resposta(200, "Cliente deletado com suceso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
