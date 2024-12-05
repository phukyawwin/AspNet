using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Domain.Entities;

namespace AppointmentBookingSystem.Application.Services.Interface
{
    public interface ISlotService
    {
        IEnumerable<Slot> GetAllSlot();
        Slot GetSlotById(int id);
        void CreateSlot(Slot slot);
        void UpdateSlot(Slot slot);
        bool DeleteSlot(int id);

        bool CheckSlotExists(int slotId);
    }
}
