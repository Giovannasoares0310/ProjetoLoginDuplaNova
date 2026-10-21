using AppLogin.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLogin.Areas.Colaborador.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteApiControlller : Controller
    {
        private IClienteRepository _clienteRepository;

        public ClienteApiControlller(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        [HttpGet]
        public IActionResult ListarClientes()
        {
            var clientes = _clienteRepository.ObterTodosClientes();
            return Ok(clientes);
        }
       
        [HttpPost]
        public IActionResult Post( Models.Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest(new
                {
                    mensagem = "Os dados do cliente são obrigatorios"
                });     
            }
            _clienteRepository.Cadastrar(cliente);

            return Ok(new
            {
                mensagem = "Cadastro realizado com sucesso.",
                cliente = cliente
            });
        }

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

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Models.Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteExistente = _clienteRepository.ObterCliente(id);

            if(clienteExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente não encontrado."
                });
            }

            cliente.Id = id;

            return Ok(new
            {
                mensagem = "Cliente atualizado com sucesso."
            });
        }
        

    }
}
