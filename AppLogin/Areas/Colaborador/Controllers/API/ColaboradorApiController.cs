using AppLayoutAspCore.Models;
using AppLayoutAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLayoutAspCore.Areas.Colaborador.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorApiController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;

        public ColaboradorApiController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        // Listar todos os colaboradores
        [HttpGet]
        public IActionResult ListarColaboradores()
        {
            var colaboradores = _colaboradorRepository.ObterTodosColaboradores();

            return Ok(colaboradores);
        }

        // Cadastrar usuário
        [HttpPost]
        public IActionResult Post(Colaborador colaborador)
        {
            if (colaborador is null)
            {
                return BadRequest(new
                {
                    mensagem = "Os dados do colaborador são obrigatórios."
                });
            }
            _colaboradorRepository.Cadastrar(colaborador);

            return Ok(new
            {
                mensagem = "Cadastro realizado com sucesso.",
                colaborador = colaborador
            });
        }
        // GET: api/ColaboradorApi/1
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);

            if (colaborador == null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado."
                });
            }

            return Ok(colaborador);
        }

        // PUT: api/ColaboradoresApi/1
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Colaborador colaborador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var colaboradorExistente = _colaboradorRepository.ObterColaborador(id);

            if (colaboradorExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado."
                });
            }

            colaborador.Id = id;

            _colaboradorRepository.Atualizar(colaborador);

            return Ok(new
            {
                mensagem = "Colaborador atualizado com sucesso."
            });
        }
        // DELETE: api/ColaboradoresApi/1
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);

            if (colaborador == null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado."
                });
            }

            _colaboradorRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Colaborador excluído com sucesso."
            });
        }
    }
}