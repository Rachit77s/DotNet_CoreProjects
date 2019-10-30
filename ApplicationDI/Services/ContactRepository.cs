using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDI.Data;
using ApplicationDI.Models;

namespace ApplicationDI.Services
{
    public class ContactRepository : IContact
    {
        //Dependency Injection
        private readonly ApplicationContext _context;

        public ContactRepository(ApplicationContext context)
        {
            _context = context;
        }




        public void Delete(int id)
        {
            ContactModel contactDelete = getById(id);
            _context.Remove(contactDelete);
            //throw new NotImplementedException();

        }

        public IEnumerable<ContactModel> getAll()
        {
            return _context.contacts.ToList();

            //throw new NotImplementedException();
        }

        public ContactModel getById(int id)
        {
            return _context.contacts.Where(a => a.Id == id).SingleOrDefault();
            // throw new NotImplementedException();
        }

        public void Insert(ContactModel contact)
        {
            _context.Add(contact);
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
            //        throw new NotImplementedException();
        }

        public void Update(ContactModel contact)
        {
            _context.contacts.Update(contact);
            //throw new NotImplementedException();
        }
    }
}
