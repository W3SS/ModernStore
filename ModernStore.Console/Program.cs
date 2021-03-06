﻿using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ModernStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new RegisterOrderCommand
            {
                Customer = Guid.NewGuid(),
                DeliveryFee = 9,
                Discount = 30,
                Items = new List<RegisterOrderItemCommand>
                {
                    new RegisterOrderItemCommand
                    {
                        Product = Guid.NewGuid(),
                        Quantity = 3
                    }
                }
            };

            GenerateOrder(
                new FakeCustomerRepository(),
                new FakeProductRepository(),
                new FakeOrderRepository(),
                command);

            Console.ReadKey();
        }

        public static void GenerateOrder(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository, RegisterOrderCommand command)
        {
            var handler = new OrderCommandHandler(customerRepository, productRepository, orderRepository);
            handler.Handle(command);

            if (handler.IsValid())
                Console.WriteLine("Your order has been submitted");
        }

        public class FakeProductRepository : IProductRepository
        {
            public Product Get(Guid id)
            {
                return new Product("Mouse", 299, string.Empty, 50);
            }
        }

        public class FakeOrderRepository : IOrderRepository
        {
            public void Save(Order order)
            {

            }
        }

        public class FakeCustomerRepository : ICustomerRepository
        {
            public bool DocumentExists(string document)
            {
                throw new NotImplementedException();
            }

            public Customer Get(Guid id)
            {
                return null;
            }

            public Customer Get(string document)
            {
                throw new NotImplementedException();
            }

            public Customer GetByUserId(Guid id)
            {
                return new Customer(
                    new Name("Natan", "Dutra"),
                    new Email("natan_r.dutra@hotmail.com"),
                    new Document("71936805286"),
                    new User("natan.dutra", "123456789", "123456789")
                    );
            }

            public void Save(Customer customer)
            {
                throw new NotImplementedException();
            }

            public void Update(Customer customer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
