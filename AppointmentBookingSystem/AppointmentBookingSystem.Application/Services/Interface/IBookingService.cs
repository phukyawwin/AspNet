﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Services.Interface
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAllBooking();
        IEnumerable<Booking> GetAllBookingByCustomer(string id);
        IEnumerable<Booking> GetAllBookingToSendReminderEmail();
        Booking GetBookingById(int id);

        bool checkBookingExitByCustomer(Booking booking);
        int getBookingCountOnDate(Booking booking);
        void CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        bool DeleteBooking(int id);

        bool CheckBookingExists(int bookingId);
    }
}
