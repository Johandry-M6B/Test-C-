using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PerformanceTest.Interfaces.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    T? Find(Expression<Func<T, bool>> predicate);

    // Básico: Guardar datos
    void Add(T entity);
    void Update(T entity);
    void Delete(Guid id);

    // Básico: Verificar existencia
    bool Exists(Guid id);
    bool Any(Expression<Func<T, bool>> predicate);

}
