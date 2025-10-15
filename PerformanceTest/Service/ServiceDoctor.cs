using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Interfaces;
using PerformanceTest.Models;

namespace PerformanceTest.Service;

public class ServiceDoctor : IRegister
{
    public List<Doctor> Doctors = [];
    public bool Register()
    {

        try
        {
            string name = "";
            bool validName = false;
            while (!validName)
            {
                Console.WriteLine("Enter the doctor's name (at least 2 characters):");
                name = Console.ReadLine() ?? "";
                validName = ValidateAge.ValidName(name);
                if (!validName)
                {
                    Console.WriteLine("Invalid name. Please try again.");
                }
            }

            string document = "";
            bool ValidDocument = false;
            try
            {
                while (!ValidDocument)
                {
                    Console.WriteLine("Enter the doctor's document (at least 5 characters):");
                    document = Console.ReadLine() ?? "";
                    document = document.Trim();

                    if (string.IsNullOrEmpty(document) || document.Length < 5)
                    {
                        Console.WriteLine("Document must be at least 5 characters long. Please try again.");
                        continue;
                    }
                    if (IsDuplicateDocument(document))
                    {
                        Console.WriteLine("Document already exists. Please enter a unique document.");
                        continue;
                    }
                    ValidDocument = true;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            Console.WriteLine("Enter the doctor's specialty:");
            string specialty = Console.ReadLine() ?? "";
            Console.WriteLine("Enter the doctor's email:");
            string email = Console.ReadLine() ?? "";
            Console.WriteLine("Enter the doctor's phone:");
            string phone = Console.ReadLine() ?? "";
            Guid id = Guid.NewGuid();
            Doctor newDoctor = new(name, id, specialty, document, email, phone);
            Doctors.Add(newDoctor);
            Console.WriteLine($"Doctor {name} registered successfully with ID: {id}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during registration: {ex.Message}");
            return false;
        }
    }
    private bool IsDuplicateDocument(string document)
    {
        return Doctors.Any(d => d.Document == document);
    }

    public bool EditDoctor(string document)
    {
        try
        {
            var doctor = Doctors.FirstOrDefault(d => d.Document == document);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return false;
            }
            Console.WriteLine($"Editing doctor: {doctor.Name}");
            Console.WriteLine("Leave a field empty to keep the current value.");

            Console.Write($"Current Name: {doctor.Name}. New Name: ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName) && ValidateAge.ValidName(newName))
                doctor.Name = newName;


            Console.Write($"Current Specialty: {doctor.Specialty}. New Specialty: ");
            string newSpecialty = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newSpecialty))
                doctor.Specialty = newSpecialty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while editing the doctor: {ex.Message}");
            return false;
        }
        return true;
    }

    internal IEnumerable<object> GetAllDoctors()
    {
        return Doctors;
    }
}
