﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingSystem.Application.Common.Job
{
    public interface IJobService
    {
        Task StartSendReminderEmailAsync();
    }
}