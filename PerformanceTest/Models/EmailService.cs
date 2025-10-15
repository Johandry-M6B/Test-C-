using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PerformanceTest.Models;

namespace PerformanceTest.Service
{
    public class EmailService
    {
        private SmtpClient _smtpClient;
        private string _fromEmail;

        public EmailService(string smtpHost, int port, string fromEmail, string password, bool enableSsl = true)
        {
            _fromEmail = fromEmail;
            _smtpClient = new SmtpClient(smtpHost)
            {
                Port = port,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = enableSsl,
                Timeout = 10000
            };
        }

        public async Task<bool> SendAppointmentConfirmationAsync(Appointment appointment, Patient patient, Doctor doctor)
        {
            try
            {
                string subject = "Medical Appointment Confirmation";
                string body = $@"
Hello {patient.Name},

Your medical appointment has been successfully confirmed.

📅 **Appointment Details:**
• **Doctor:** Dr. {doctor.Name}
• **Specialty:** {doctor.Specialty}
• **Date:** {appointment.Date:MM/dd/yyyy}
• **Time:** {appointment.Time:hh:mm tt}

📍 **Clinic Address:**
Main Medical Center
Main Street #123

📞 **Contact:**
Phone: (555) 123-4567
Email: info@medicalclinic.com

⚠️ **Important:**
- Arrive 15 minutes early
- Bring your ID document
- Cancel with 24 hours notice if you cannot attend

Thank you for trusting us!

*This is an automated message, please do not reply.*";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(patient.Email);

                await _smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($" Email sent successfully to: {patient.Email}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error sending email to {patient.Email}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendAppointmentCancellationAsync(Appointment appointment, Patient patient, Doctor doctor)
        {
            try
            {
                string subject = " Medical Appointment Cancellation";
                string body = $@"
Hello {patient.Name},

Your medical appointment has been cancelled.

📅 **Cancelled Appointment:**
• **Doctor:** Dr. {doctor.Name}
• **Date:** {appointment.Date:MM/dd/yyyy}
• **Time:** {appointment.Time:hh:mm tt}

📞 **To Reschedule:**
Contact our clinic at (555) 123-4567

We apologize for any inconvenience.

*This is an automated message, please do not reply.*";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(patient.Email);

                await _smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($" Cancellation email sent to: {patient.Email}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error sending cancellation email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendAppointmentCompletedAsync(Appointment appointment, Patient patient, Doctor doctor)
        {
            try
            {
                string subject = " Appointment Completed - Follow Up";
                string body = $@"
Hello {patient.Name},

Your medical appointment has been marked as completed.

📅 **Appointment Details:**
• **Doctor:** Dr. {doctor.Name}
• **Specialty:** {doctor.Specialty}
• **Date:** {appointment.Date:MM/dd/yyyy}

💊 **Next Steps:**
- Follow the doctor's recommendations
- Take prescribed medications as directed
- Schedule a follow-up if recommended

📞 **For Questions:**
Contact our clinic at (555) 123-4567

We hope you feel better soon!

*This is an automated message, please do not reply.*";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(patient.Email);

                await _smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($" Completion email sent to: {patient.Email}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error sending completion email: {ex.Message}");
                return false;
            }
        }
    }
}