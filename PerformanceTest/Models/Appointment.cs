using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceTest.Models;

public class Appointment(
int id,
string patient,
string doctor,
DateTime date,
DateTime time,
AppointmentStatus status = AppointmentStatus.Scheduled)
{
    public int Id { get; set; } = id;
    public string Patient { get; set; } = patient;
    public string Doctor { get; set; } = doctor;
    public DateTime Date { get; set; } = date;
    public DateTime Time { get; set; } = time;
    public AppointmentStatus Status { get; set; } = status;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Canceled
}