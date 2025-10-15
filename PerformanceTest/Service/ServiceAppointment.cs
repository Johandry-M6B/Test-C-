using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Models;

namespace PerformanceTest.Service;

public class ServiceAppointment
{
    private List<Appointment> Appointments = [];
    private int nextAppointmentId = 1;

    public bool ScheduledAppointment(Patient patient, Doctor doctor, DateTime date, DateTime time)
    {
        try
        {
            bool doctorHasConflict = Appointments.Any(a =>
            a.Doctor == doctor.Name
            && a.Date.Date == date.Date
            && a.Time.TimeOfDay == time.TimeOfDay
            && a.Status == AppointmentStatus.Scheduled);

            if (doctorHasConflict)
            {
                Console.WriteLine("The doctor has another appointment at this date and time.");
                return false;
            }

            bool patientHasConflict = Appointments.Any(a =>
                a.Patient == patient.Name
                && a.Date.Date == date.Date
                && a.Time.TimeOfDay == time.TimeOfDay
                && a.Status == AppointmentStatus.Scheduled);
            if (patientHasConflict)
            {
                Console.WriteLine("The patient has another appointment at this date and time.");
                return false;
            }
            Appointment newAppointment = new(nextAppointmentId++, patient.Name, doctor.Name, date, time);
            Appointments.Add(newAppointment);
            Console.WriteLine($"Appointment scheduled successfully with ID: {newAppointment.Id}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling appointment: {ex.Message}");
            return false;
        }
    }
    public bool CancelAppointment(int appointmentId)
    {
        try
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return false;
            }
            if (appointment.Status != AppointmentStatus.Canceled)
            {
                Console.WriteLine("Only scheduled appointments can be canceled.");
                return true;
            }
            appointment.Status = AppointmentStatus.Canceled;
            Console.WriteLine($"Appointment ID {appointmentId} has been canceled.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error canceling appointment: {ex.Message}");
            return false;
        }
    }
    public bool MarkAsAttended(int appointmentId)
    {
        try
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return false;
            }
            if (appointment.Status != AppointmentStatus.Completed)
            {
                Console.WriteLine("Only scheduled appointments can be marked as attended.");
                return true;
            }
            appointment.Status = AppointmentStatus.Completed;
            Console.WriteLine($"Appointment ID {appointmentId} has been marked as attended.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error marking appointment as attended: {ex.Message}");
            return false;
        }
    }

    public void ListAppointmentsByPatient(string patientName)
    {
        var patientAppointments = Appointments.Where(a => a.Patient == patientName).ToList();
        if (patientAppointments.Count == 0)
        {
            Console.WriteLine("No appointments found for this patient.");
            return;
        }
        foreach (var appointment in patientAppointments)
        {
            Console.WriteLine($"ID: {appointment.Id}, Doctor: {appointment.Doctor}, Date: {appointment.Date.ToShortDateString()}, Time: {appointment.Time.ToShortTimeString()}, Status: {appointment.Status}");
        }
    }
    public void ListAppointmentsByDoctor(string doctorName)
    {
        var doctorAppointments = Appointments.Where(a => a.Doctor == doctorName).ToList();
        if (doctorAppointments.Count == 0)
        {
            Console.WriteLine("No appointments found for this doctor.");
            return;
        }
        foreach (var appointment in doctorAppointments)
        {
            Console.WriteLine($"ID: {appointment.Id}, Patient: {appointment.Patient}, Date: {appointment.Date.ToShortDateString()}, Time: {appointment.Time.ToShortTimeString()}, Status: {appointment.Status}");
        }
    }
    public Appointment? GetAppointmentById(int appointmentId)
    {
        return Appointments.FirstOrDefault(a => a.Id == appointmentId);
    }
}
