using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerformanceTest.Models;

namespace PerformanceTest.Interfaces.IRepository
{
    public interface IAppointmentsRespository: IRepository<Appointment>
    {
        IEnumerable<Appointment> GetByPatientName(string patientName);
        IEnumerable<Appointment> GetByDoctorName(string doctorName);
        IEnumerable<Appointment> GetByDate(DateTime date);
        bool HasDoctorConflict(string doctorName, DateTime date, DateTime time);
        bool HasPatientConflict(string patientName, DateTime date, DateTime time);
    }
}