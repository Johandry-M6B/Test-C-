using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceTest.Service;
    public class ValidateAge
    {
    public static bool ValidAge(int age)
    {
        return age > 0 && age < 100;
    }
    public static bool ValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 2;
    }

    }
