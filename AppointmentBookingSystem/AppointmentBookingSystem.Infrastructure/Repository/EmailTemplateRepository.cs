using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingSystem.Infrastructure.Repository
{
    public class EmailTemplateRepository:IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _db;
        public EmailTemplateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public EmailTemplate GetTemplateByName(string name)
        {
            return _db.EmailTemplates.FirstOrDefault(e => e.Name == name);
        }
    }
}
