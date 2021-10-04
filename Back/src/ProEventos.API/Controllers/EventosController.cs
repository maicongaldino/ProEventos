using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contrato;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includePalestrantes)
        {
            try
            {
                 var eventos = await _eventoService.GetAllEventosAsync(includePalestrantes);
                 if (eventos == null) return NotFound("Nenhum evento encontrado !");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("/id/{id}")]
        public async Task<IActionResult> Get(int id, bool includePalestrantes)
        {
            try
            {
                 var evento = await _eventoService.GetEventoByIdAsync(id, includePalestrantes);
                 if (evento == null) return NotFound("Evento por id não encontrados !");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("/tema/{tema}")]
        public async Task<IActionResult> Get(string tema, bool includePalestrantes)
        {
            try
            {
                 var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, includePalestrantes);
                 if (eventos == null) return NotFound("Eventos por tema não encontrados !");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                 var evento = await _eventoService.AddEventos(model);
                 if (evento == null) return BadRequest("Erro ao tentar adicionar evento !");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                 var evento = await _eventoService.Update(id, model);

                 if (evento == null) return BadRequest("Erro ao tentar atualizar o evento !");

                 return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete(("{id}"))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id)? Ok("Deletado") : BadRequest("Evento não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
