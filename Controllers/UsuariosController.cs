using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository
        _usuarioRepository;
        public UsuariosController(UsuarioRepository
        projetoRepository)
        {
            _usuarioRepository = projetoRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            Usuario usuario = _usuarioRepository.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound("O usuário não foi encontrado!");
            }
            return Ok(usuario);
        }

        // [HttpPost]
        // public IActionResult Cadastrar(Usuario usuario)
        // {
        //     try
        //     {
        //         var erros = isValido(usuario);
        //         if (erros.Count != 0)
        //         {
        //             return BadRequest(new
        //             {
        //                 status = 400,
        //                 error = "Bad Request",
        //                 message = "Falha na validação dos dados enviados.",
        //                 errors = erros
        //             });
        //         }
        //         Usuario usuarioExistente = _usuarioRepository.ObterPorEmail(usuario.Email);
        //         if (usuarioExistente != null)
        //         {
        //             List<string> errosEmail = new() { "O email já está sendo usado por outro usuário" };
        //             return BadRequest(new
        //             {
        //                 status = 400,
        //                 error = "Bad Request",
        //                 message = "Falha na validação dos dados enviados.",
        //                 errors = errosEmail
        //             });
        //         }
        //         _usuarioRepository.Cadastrar(usuario);
        //         return StatusCode(201);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, new
        //         {
        //             Sucesso = false,
        //             Mensagem = "Erro interno no servidor.",
        //             error = ex.Message,
        //         });
        //     }
        // }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email,
            usuario.Senha);
            if (usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos!");
            }
            // Se o usuário for encontrado, segue a criação do token.
            // Define os dados que serão fornecidos no token - Payload.
            var claims = new[]
            {
                // Armazena na claim o e-mail usuário autenticado.
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                // Armazena na claim o id do usuário autenticado.
                new Claim(JwtRegisteredClaimNames.Jti,
                usuarioBuscado.Id.ToString()),
            };
            // Define a chave de acesso ao token.
            var key = new
            SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("+gA8pSW2cH8kLfFEUk1IeD1JGdQB5gohXAjILVlAvXM="));
            // Define as credenciais do token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Gera o token.
            var token = new JwtSecurityToken(
            issuer: "exoapi.webapi", // Emissor do token.
            audience: "exoapi.webapi", // Destinatário do token.
            claims: claims, // Dados definidos acima.
            expires: DateTime.Now.AddMinutes(30), // Tempo de expiração.
            signingCredentials: creds // Credenciais do token.
            );
            // Retorna ok com o token.
            return Ok(
            new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            try
            {
                var erros = isValido(usuario);

                if (erros.Count != 0)
                {
                    return BadRequest(new
                    {
                        status = 400,
                        error = "Bad Request",
                        message = "Falha na validação dos dados enviados.",
                        errors = erros
                    });
                }
                Usuario usuarioExistente = _usuarioRepository.ObterPorEmail(usuario.Email);
                if (usuarioExistente != null && usuarioExistente.Id != id)
                {
                    List<string> errosEmail = new() { "O email já está sendo usado por outro usuário" };
                    return BadRequest(new
                    {
                        status = 400,
                        error = "Bad Request",
                        message = "Falha na validação dos dados enviados.",
                        errors = errosEmail
                    });
                }
                _usuarioRepository.Atualizar(id, usuario);
                return StatusCode(204);
            }
            catch (System.Exception)
            {
                return NotFound("O usuário não foi encontrado!");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _usuarioRepository.Excluir(id);
                return StatusCode(204);
            }
            catch (System.Exception)
            {
                return NotFound("O usuário não foi encontrado!");
            }
        }

        private List<string> isValido(Usuario usuario)
        {
            List<string> minhaLista = new List<string>();

            if (usuario == null)
            {
                minhaLista.Add("Objeto usuário não informado");
                return minhaLista;
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                minhaLista.Add("O campo Email é obrigatório.");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                minhaLista.Add("O campo Senha é obrigatório.");
            }
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                minhaLista.Add("O campo Nome é obrigatório.");
            }

            return minhaLista;
        }

    }
}