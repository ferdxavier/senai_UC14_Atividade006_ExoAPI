using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Exo.WebApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ExoContext _context;
        public UsuarioRepository(ExoContext context)
        {
            _context = context;
        }
        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }
        public Usuario ObterPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
        public Usuario ObterPorEmail(string email)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public void Atualizar(int id, Usuario usuarioCliente)
        {
            Usuario usuarioExistente = _context.Usuarios.Find(id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Email = usuarioCliente.Email;
                usuarioExistente.Senha = usuarioCliente.Senha;
                usuarioExistente.Nome = usuarioCliente.Nome;
                usuarioExistente.Telefone = usuarioCliente.Telefone;
                _context.Usuarios.Update(usuarioExistente);
                _context.SaveChanges();
            }
        }
        public void Excluir(int id)
        {
            Usuario usuarioExistente = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(usuarioExistente);
            _context.SaveChanges();
        }
    }
}