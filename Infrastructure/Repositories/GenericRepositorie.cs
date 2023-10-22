using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepositorie<T> : IRepository<T> where T : TEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual T GetById(ulong id)
        {
            var entity = _context.Set<T>().SingleOrDefault(x => x.Id == id);

            return entity;
        }

        public void Save(T entity)
        {
            try
            {
                //Salva todos os tipos de alterações/Adções no contexto
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
