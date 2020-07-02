﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.DatabaseContext.DatabaseContext;

namespace Brainstorm.Repository.Repository
{
   public class CustomerRepository
    {

        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Customer aCustomer = _dbContext.Customers.FirstOrDefault((c => c.Id == id));
            _dbContext.Customers.Remove(aCustomer);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Update(Customer customer)
        {
            Customer aCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (aCustomer != null)
            {
                aCustomer.Code = customer.Code;
                aCustomer.Name = customer.Name;
                aCustomer.Address = customer.Address;
                aCustomer.Email = customer.Email;
                aCustomer.Contact = customer.Contact;
                aCustomer.LoyaltyPoint = customer.LoyaltyPoint;

            }

            return _dbContext.SaveChanges() > 0;
        }

        public List<Customer> GetAll()
        {

            return _dbContext.Customers.ToList();
        }
        public Customer GetById(int id)
        {

            return _dbContext.Customers.FirstOrDefault((c => c.Id == id));
        }


       

    }
}
