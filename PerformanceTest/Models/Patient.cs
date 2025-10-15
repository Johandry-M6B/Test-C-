

namespace PerformanceTest.Models
{
    public class Patient(Guid id,int age, string name, string document, string email, string phone) : Person(name, document, email, phone ,id)
    {
        public int Age { get; set; } = age;       
            
    }

   
}