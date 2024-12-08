using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Common.Contract
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string receiverName, string subject, string message);
        Task<bool> SendEmailConfirmation(ApplicationUser user,Booking booking);
        Task<bool> SendEmailCancle(ApplicationUser user, Booking booking);
        Task<bool> SendEmailReminder(ApplicationUser user, Booking booking);
    }
}
