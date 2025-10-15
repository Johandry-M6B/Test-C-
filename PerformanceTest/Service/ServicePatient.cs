using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PerformanceTest.Interfaces;
using PerformanceTest.Models;

namespace PerformanceTest.Service;

public class ServicePatient : IRegister
{
    static List<Patient> Patients = [];
    public bool Register()
    {
        try
        {
            string name = "";
            bool validName = false;
            while (!validName)
            {
                Console.WriteLine("Enter the patient's name (at least 2 characters):");
                name = Console.ReadLine() ?? "";
                validName = ValidateAge.ValidName(name);
                if (!validName)
                {
                    Console.WriteLine("Invalid name. Please try again.");
                }
            }

            int age = 0;
            bool validAge = false;
            while (!validAge)
            {
                try
                {
                    Console.WriteLine("Enter the patient's age (between 1 and 99):");
                    age = int.Parse(Console.ReadLine() ?? "");
                    validAge = ValidateAge.ValidAge(age);
                    if (!validAge)
                    {
                        Console.WriteLine("Invalid age. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for age.");
                }
            }


            string document = "";
            bool ValidDocument = false;
            try
            {
                while (!ValidDocument)
                {
                    Console.WriteLine("Enter the patient's document (at least 5 characters):");
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
            Console.WriteLine("Enter the patient's email:");
            string email = Console.ReadLine() ?? "";
            Console.WriteLine("Enter the patient's phone:");
            string phone = Console.ReadLine() ?? "";
            Guid id = Guid.NewGuid();
            Patient newPatient = new(id, age, name, document, email, phone);
            Patients.Add(newPatient);
            Console.WriteLine($"Patient {name} registered successfully with ID: {id}");
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
        return Patients.Any(p => p.Document == document);
    }
    public static List<Patient> GetAllPatients()
    {
        return Patients;
    }
    public static bool EditPatient(string document)
    {
        try
        {
            var patient = Patients.FirstOrDefault(p => p.Document == document);
            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return false;
            }
            Console.WriteLine($"Editing patient: {patient.Name}");
            Console.WriteLine("Leave a field empty to keep the current value.");

            Console.Write($"Current Name: {patient.Name}. New Name: ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName) && ValidateAge.ValidName(newName))
                patient.Name = newName;
            

            Console.Write($"Current Age: {patient.Age}. New Age: ");
            string ageInput = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(ageInput) && int.TryParse(ageInput, out int newAge) && ValidateAge.ValidAge(newAge))
                patient.Age = newAge;
        }catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while editing the patient: {ex.Message}");
            return false;
        }
        return true;
    }
}