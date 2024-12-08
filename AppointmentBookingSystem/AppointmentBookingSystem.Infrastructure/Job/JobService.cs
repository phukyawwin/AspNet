using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Contract;
using AppointmentBookingSystem.Application.Common.Job;
using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AppointmentBookingSystem.Infrastructure.Job
{
    public class JobService : IJobService
    {
        private readonly IBookingService _bookingService;
        private readonly IEmailService _emailService;
        private readonly ILogger<JobService> _logger;
        public JobService(IBookingService bookingService, IEmailService emailService, ILogger<JobService> logger)
        {
            _bookingService= bookingService;
            _emailService= emailService;
            _logger= logger;
        }
        public async Task StartSendReminderEmailAsync()
        {
            try
            {
                IEnumerable<Booking> bookingList = _bookingService.GetAllBookingToSendReminderEmail();
                foreach (var booking in bookingList)
                {

                    if (await _emailService.SendEmailReminder(booking.Customer, booking))
                    {
                        booking.IsSendReminderNoti = true;
                        _bookingService.UpdateBooking(booking);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while sending reminder emails: {ex.Message}");
            }

        }
           
    }
}

