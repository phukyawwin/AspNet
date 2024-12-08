using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Contract;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace AppointmentBookingSystem.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _appPassword;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _appPassword = configuration["Email:AppPassword"];
            _senderEmail = configuration["Email:SenderEmail"];
            _senderName = configuration["Email:SenderName"];
            _unitOfWork = unitOfWork;
        }

        public async Task<bool>  SendEmailReminder(ApplicationUser? user, Booking booking)
        {
            if (user == null)
            {
            }
            var template = _unitOfWork.EmailTemplateRepository.GetTemplateByName("Appointment Reminder");
            prepareEmailTemplateAndSendEmail(user, booking, template);
            return true;
        }
        public Task<bool> SendEmailCancle(ApplicationUser user, Booking booking)
        {
            var template = _unitOfWork.EmailTemplateRepository.GetTemplateByName("Booking Cancellation");
            prepareEmailTemplateAndSendEmail(user, booking, template);
            return Task.FromResult(true);
        }
        public Task<bool> SendEmailConfirmation(ApplicationUser user, Booking booking)
        {
            var template = _unitOfWork.EmailTemplateRepository.GetTemplateByName("Appointment Confirmation");
            prepareEmailTemplateAndSendEmail(user, booking,template);
            return Task.FromResult(true);
        }

        public async Task<bool> prepareEmailTemplateAndSendEmail(ApplicationUser user, Booking booking,EmailTemplate template)
        {
            if (template != null)
            {
                var placeholders = new Dictionary<string, string>
                {
                    { "CustomerName", user.Name },
                    { "AppointmentDate", booking.AppointmentDate.ToString("MMMM dd, yyyy") },
                    { "SlotStartTime", $"{(new DateTime(2000, 1, 1).Add(booking.Slot.StartTime)):hh:mm tt}"},
                    { "SlotEndTime", $"{(new DateTime(2000, 1, 1).Add(booking.Slot.EndTime)):hh:mm tt}"},
                    { "DoctorName", booking.Slot.Doctor.Name }
                };

                string subject = ReplacePlaceholders(template.Subject, placeholders);
                string body = ReplacePlaceholders(template.Body, placeholders);
                SendEmailAsync(user.Email, user.Name, subject, body);
                return true;
            }
            return false;
        }
        public async Task<bool> SendEmailAsync(string email, string receiverName, string subject, string message)
        {
            var senderEmail = new MailAddress(_senderEmail, _senderName);
            var receiverEmail = new MailAddress(email, receiverName);
            var appPassword = _appPassword; // Replace with your Gmail App Password

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, appPassword)
            };

            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true // Set to true if sending HTML content
            })
            {
                smtp.Send(mess);
            }
            return true;
        }

        public string ReplacePlaceholders(string template, Dictionary<string, string> placeholders)
        {
            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }
            return template;
        }
        
        
    }
}
