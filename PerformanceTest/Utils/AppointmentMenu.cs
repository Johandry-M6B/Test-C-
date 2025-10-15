using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Models;
using PerformanceTest.Service;

namespace PerformanceTest.Utils;

public class AppointmentMenu
{
    private ServiceAppointment appointmentService = null!;
    private ServicePatient patientService = null!;
    private List<Doctor> doctors = new();

    public AppointmentMenu(ServiceAppointment appointmentService, ServicePatient patientService, List<Doctor> doctors)
    {
        this.appointmentService = appointmentService;
        this.patientService = patientService;
        InitializeDoctors();
    }
    private void InitializeDoctors()
    {
        doctors =
        [
            new Doctor("Dr. Smith", Guid.NewGuid(), "Cardiology", "123456789", "", ""),
             new Doctor("Dr. Johnson", Guid.NewGuid(), "Dermatology", "987654321", "", ""),
            new Doctor("Dr. Williams", Guid.NewGuid(), "Neurology", "456789123", "", "")
        ];
    }
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nAppointment Menu:");
            Console.WriteLine("1. Schedule Appointment");
            Console.WriteLine("2. Cancel Appointment");
            Console.WriteLine("3. View Appointments by Patient");
            Console.WriteLine("4. List Appointments by Doctor");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    ScheduleAppointment();
                    break;
                case "2":
                    CancelAppointment();
                    break;
                case "3":
                    ViewAppointmentsByPatient();
                    break;
                case "4":
                    ListAppointmentsByDoctor();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    private void ScheduleAppointment()
    {
        try
        {
            Console.Write("Enter patient name: ");
            string patientName = Console.ReadLine() ?? "";
            var patient = ServicePatient.GetAllPatients().FirstOrDefault(p => p.Name.Equals(patientName, StringComparison.OrdinalIgnoreCase));
            if (patient == null)
            {
                Console.WriteLine("Patient not found. Please register the patient first.");
                return;
            }

            Console.WriteLine("Available Doctors:");
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {doctors[i].Name} - {doctors[i].Specialty}");
            }
            Console.Write("Select a doctor by number: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorIndex) || doctorIndex < 1 || doctorIndex > doctors.Count)
            {
                Console.WriteLine("Invalid selection. Please try again.");
                return;
            }
            var selectedDoctor = doctors[doctorIndex - 1];

            Console.Write("Enter appointment date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format. Please try again.");
                return;
            }

            Console.Write("Enter appointment time (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime time))
            {
                Console.WriteLine("Invalid time format. Please try again.");
                return;
            }

            bool success = appointmentService.ScheduledAppointment(patient, selectedDoctor, date, time);
            if (success)
            {
                Console.WriteLine("Appointment scheduled successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling appointment: {ex.Message}");
        }
    }
    private void CancelAppointment()
    {
        try
        {
            Console.Write("Enter appointment ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int appointmentId))
            {
                Console.WriteLine("Invalid appointment ID. Please try again.");
                return;
            }

            bool success = appointmentService.CancelAppointment(appointmentId);
            if (success)
            {
                Console.WriteLine("Appointment canceled successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error canceling appointment: {ex.Message}");
        }
    }
    private void ViewAppointmentsByPatient()
    {
        try
        {
            Console.WriteLine("Insert the patient's name to view their appointments:");
            string name = Console.ReadLine() ?? "";
            appointmentService.ListAppointmentsByPatient(name);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving appointments: {ex.Message}");
        }
    }
    private void ListAppointmentsByDoctor()
    {
        try
        {
            Console.WriteLine("Insert the doctor's name to view their appointments:");
            string name = Console.ReadLine() ?? "";
            appointmentService.ListAppointmentsByDoctor(name);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving appointments: {ex.Message}");
        }
    }
}
