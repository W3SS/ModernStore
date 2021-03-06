﻿using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Contexts;
using ModernStore.Shared;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context) => _context = context;

        public Customer Get(Guid id) => _context.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);

        public Customer Get(string document) => _context.Customers.Include(x => x.Document).FirstOrDefault(x => x.Document.Number == document);

        public Customer GetByUserId(Guid id) => _context.Customers.Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.User.Id == id);

        public void Update(Customer customer) => _context.Entry(customer).State = EntityState.Modified;

        public bool DocumentExists(string document) => _context.Customers.Include(x => x.Document).AsNoTracking().Any(x => x.Document.Number == document);

        public void Save(Customer customer) => _context.Customers.Add(customer);

        public Customer GetByUsername(string username) => _context.Customers.Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.User.Username == username);

        GetCustomerCommandResult ICustomerRepository.Get(string username)
        {
            using (SqlConnection conn = new SqlConnection(Runtime.ConnectionString))
            {
                conn.Open();
                return conn.Query<GetCustomerCommandResult>(
                    "SELECT * FROM [GetCustomerInfoView] WHERE [Active]=1 AND [Username]=@username",
                    new {username = username}).FirstOrDefault();
            }
        }
    }
}
