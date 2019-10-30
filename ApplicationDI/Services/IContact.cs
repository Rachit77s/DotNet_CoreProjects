using ApplicationDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationDI.Services
{
    public interface IContact
    {
        IEnumerable<ContactModel> getAll();
        void Save();
        void Insert(ContactModel contact);
        void Delete(int id);
        void Update(ContactModel contact);
        ContactModel getById(int id);

    }
}
