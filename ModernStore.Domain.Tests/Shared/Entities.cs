﻿using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests.Shared
{
    public class Entities
    {
        public readonly Name name = new Name("Natan", "Dutra");
        public readonly Name nameOnlyFirst = new Name("Natan", "");
        public readonly Name nameOnlyLast = new Name("", "Dutra");

        public readonly Email email = new Email("natan_r.dutra@hotmail.com");
        public readonly Email invalidEmail = new Email("");

        public readonly Document invaliDocument = new Document("1234567890");
        public readonly Document validDocument = new Document("492281269");

        public readonly User user = new User("natan.dutra", "123456789");

        protected Customer GenerateNewCustomer()
        {
            return new Customer(this.name, this.email, this.validDocument, this.user);
        }
    }
}