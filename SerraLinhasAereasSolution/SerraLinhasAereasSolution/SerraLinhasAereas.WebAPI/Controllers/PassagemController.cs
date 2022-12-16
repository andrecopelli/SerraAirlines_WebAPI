using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.Repository;
using System;

namespace SerraLinhasAereasSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("api/passagens")]
    public class PassagemController : ControllerBase
    {
        private readonly IPassagemRepository _passagemRepository;
        public PassagemController()
        {
            _passagemRepository = new PassagemRepository();
        }

        [HttpPost]
        public IActionResult PostPassagem(Passagem novaPassagem)
        {
            try
            {
                _passagemRepository.AdicionarPassagem(novaPassagem);
                return Ok(new Resposta(200, $"Passagem comprada com sucesso."));

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
                return Ok(_passagemRepository.BuscarPassagens());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("por-data")]
        public IActionResult GetClientesPorData([FromQuery]DateTime dataBuscada)
        {
            try
            {
                return Ok(_passagemRepository.BuscarPassagensPorData(dataBuscada));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("por-origem")]
        public IActionResult GetClientesPorOrigem([FromQuery] string origem)
        {
            try
            {
                return Ok(_passagemRepository.BuscarPassagemPorOrigem(origem));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("por-destino")]
        public IActionResult GetClientesPorDestino([FromQuery] string destino)
        {
            try
            {
                return Ok(_passagemRepository.BuscarPassagemPorDestino(destino));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutCliente([FromBody] Passagem passagemAtualizada)
        {
            try
            {
                var passagemBuscada = _passagemRepository.BuscarPassagemPorId(passagemAtualizada.Id);
                passagemBuscada.Origem = passagemAtualizada.Origem;
                passagemBuscada.Destino = passagemAtualizada.Destino;
                passagemBuscada.Destino = passagemAtualizada.Destino;
                passagemBuscada.Valor = passagemAtualizada.Valor;
                passagemBuscada.DataOrigem = passagemAtualizada.DataOrigem;
                passagemBuscada.DataDestino = passagemAtualizada.DataDestino;
                _passagemRepository.AtualizarPassagem(passagemBuscada);
                return Ok(new Resposta(200, $"Passagem {passagemBuscada.Id} atualizada com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }
    }
}
