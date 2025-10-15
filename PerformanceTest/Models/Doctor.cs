using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceTest.Models
{
    public class Doctor(string name,Guid id,string specialty, string document, string email, string phone) : Person(name, document, email, phone,id)
    {
        public string Specialty { get; set; } = specialty;
    }
}