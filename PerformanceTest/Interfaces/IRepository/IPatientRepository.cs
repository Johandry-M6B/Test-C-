using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Models;

namespace PerformanceTest.Interfaces.IRepository
{
    public interface IPatientRepository: IRepository<Patient>
    {
        Patient?  GetByDocument(string document);
        bool DocumentExists(string document);
    }
}