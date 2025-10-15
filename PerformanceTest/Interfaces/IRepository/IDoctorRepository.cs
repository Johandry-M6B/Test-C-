using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Models;

namespace PerformanceTest.Interfaces.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor? GetByDocument(string document);
        bool DocumentExists(string document);
        IEnumerable<Doctor> GetBySpecialty(string specialty);
    }
}