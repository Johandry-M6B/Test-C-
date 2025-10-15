using PerformanceTest.Models;
using PerformanceTest.Service;
using PerformanceTest.Utils;

class Program
{
    static void Main()
    {
        ServicePatient servicePatient = new();
        ServiceAppointment serviceAppointment = new();
        AppointmentMenu appointmentMenu = new(serviceAppointment, servicePatient, []);

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Menus Patients");
            Console.WriteLine("2. Menus Doctors");
            Console.WriteLine("3. Appointment Menu");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    ShowPatientsMenu(servicePatient);
                    break;
                case "2":
                    ShowDoctorsMenu(new ServiceDoctor());
                    break;
                case "3":
                    appointmentMenu.ShowMenu();
                    break;
                case "4":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    static void ShowPatientsMenu(ServicePatient service)
    {
        while (true)
        {
            Console.WriteLine("\nPatient Menu:");
            Console.WriteLine("1. Register Patient");
            Console.WriteLine("2. Edit Patient");
            Console.WriteLine("3. List Patients");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    service.Register();
                    break;
                case "2":
                    Console.WriteLine("Enter the document of the patient to edit:");
                    string doc = Console.ReadLine() ?? "";
                    ServicePatient.EditPatient(doc);
                    break;
                case "3":
                    var patients = ServicePatient.GetAllPatients();
                    if (patients.Count == 0)
                    {
                        Console.WriteLine("No patients registered.");
                    }
                    else
                    {
                        foreach (var patient in patients)
                        {
                            Console.WriteLine($"Name: {patient.Name}, Age: {patient.Age}, Document: {patient.Document}");
                        }
                    }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        }
    }
    static void ShowDoctorsMenu(ServiceDoctor service)
    {
        while (true)
        {
            Console.WriteLine("\nDoctor Menu:");
            Console.WriteLine("1. Register Doctor");
            Console.WriteLine("2. Edit Doctor");
            Console.WriteLine("3. List Doctors");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    service.Register();
                    break;
                case "2":
                    Console.WriteLine("Enter the document of the doctor to edit:");
                    string doc = Console.ReadLine() ?? "";
                    service.EditDoctor(doc);
                    break;
                case "3":
                    service.GetAllDoctors();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        }
    }
}