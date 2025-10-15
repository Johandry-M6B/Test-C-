

using System.Dynamic;

namespace PerformanceTest.Models;

public class Person(string name, string document, string email, string phone, Guid id)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; set; } = name;
    public string Document { get; private set; } = document;
    public string Email { get; private set; } = email;
    public string Phone { get;private set; } = phone;

}
