using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Exo.WebApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoContext _context;
        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }
        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }
        public Projeto ObterPorId(int id)
        {
            return _context.Projetos.Find(id);
        }
        public void Cadastrar(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            _context.SaveChanges();
        }
        public void Atualizar(int id, Projeto projetoCliente)
        {
            Projeto projetoExistente = _context.Projetos.Find(id);
            if (projetoExistente != null)
            {
                projetoExistente.NomeDoProjeto = projetoCliente.NomeDoProjeto;
                projetoExistente.Area = projetoCliente.Area;
                projetoExistente.Status = projetoCliente.Status;
                _context.Projetos.Update(projetoExistente);
                _context.SaveChanges();
            }
        }
        public void Excluir(int id)
        {
            Projeto projetoExistente = _context.Projetos.Find(id);
            _context.Projetos.Remove(projetoExistente);
            _context.SaveChanges();
        }
    }
}