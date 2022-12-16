using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereasSolution.Domain.Entities;
using SerraLinhasAereasSolution.Domain.Interfaces;
using SerraLinhasAereasSolution.Infra.Data.Repository;
using System;

namespace SerraLinhasAereasSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("api/viagens")]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemRepository _viagemRepository;

        public ViagemController()
        {
            _viagemRepository = new ViagemRepository();
        }

        [HttpPost]
        public IActionResult PostViagem([FromBody]Viagem novaViagem)
        {
            try
            {
                _viagemRepository.MarcarViagem(novaViagem);
                return Ok(new Resposta(200, $"Sua viagem foi marcada com sucesso, seu código de reserva é {novaViagem.CodigoReserva}."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("viagem-por-cliente")]
        public IActionResult GetViagensPorCliente([FromQuery] string cpf)
        {
            try
            {
                return Ok(_viagemRepository.BuscarViagensPorCliente(cpf));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("passagem-ida")]
        public IActionResult PatchViagemIda([FromQuery] int idViagem, [FromQuery] DateTime dataOrigem, DateTime dataDestino)
        {
            try
            {
                _viagemRepository.RemarcarViagemIda(idViagem, dataOrigem, dataDestino);
                return Ok(new Resposta(200, $"Viagem {idViagem} remarcada, passagem de ida alterada com sucesso. Agora você sairá em {dataOrigem} e chegará em {dataDestino}."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("passagem-volta")]
        public IActionResult PatchViagemVolta([FromQuery] int idViagem, [FromQuery] DateTime dataOrigem, DateTime dataDestino)
        {
            try
            {
                _viagemRepository.RemarcarViagemVolta(idViagem, dataOrigem, dataDestino);
                return Ok(new Resposta(200, $"Viagem {idViagem} remarcada, passagem de volta alterada com sucesso. Agora você sairá em {dataOrigem} e chegará em {dataDestino}."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }
    }
}