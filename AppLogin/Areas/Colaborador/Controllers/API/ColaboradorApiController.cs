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
            var clientes = _clienteRepository.ObterTodosClientes();

            return Ok(clientes);
        }

        // Cadastrar usuário
        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest(new
                {
                    mensagem = "Os dados do cliente são obrigatórios."
                });
            }
            _clienteRepository.Cadastrar(cliente);

            return Ok(new
            {
                mensagem = "Cadastro realizado com sucesso.",
                cliente = cliente
            });
        }
        // GET: api/ClienteApi/1
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }

            return Ok(cliente);
        }

        // PUT: api/ClientesApi/1
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteExistente = _clienteRepository.ObterCliente(id);

            if (clienteExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }

            cliente.Id = id;

            _clienteRepository.Atualizar(cliente);

            return Ok(new
            {
                mensagem = "Cliente atualizado com sucesso."
            });
        }
        // DELETE: api/ClientesApi/1
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }

            _clienteRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Cliente excluído com sucesso."
            });
        }
    }
}
