using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Common.POCO.ValueObject;
using Wiki.Payment.Repository;

namespace Wiki.Payment.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private readonly IPaymentRepository _repository;

        public RepositoryTest()
        {
            _repository = new PaymentRepository(@"Data Source=192.168.0.208;Initial Catalog=Wiki;Integrated Security=false;
                                            User ID=sa;Password=masterkey;");
        }

        [TestMethod]
        public void get_client_byId()
        {
            var client = Client.CreateClient(new User(6127,1, "Склады РИК"), null);
            var client2 =_repository.GetClient(6127);
            Assert.AreEqual(client.User.ClientCode,client2.User.ClientCode);
        }
        [TestMethod]
        public void get_payment_byId()
        {
            var payment = Common.POCO.Payment.CreatePayment(1, new User(46088, 4, "Корнеев Егор"),
                new User(3324, 2200, "Корнеев Егор"), 2200, DateTime.Now, PaymentType.auto, 1, DateTime.Now);
            var payment2 = _repository.GetPayment(1);
            Assert.AreEqual(payment.Operator.ClientId, payment2.Operator.ClientId);
        }
        [TestMethod]
        public void get_payments()
        {
            var payments = new List<Common.POCO.Payment>
            {
                 Common.POCO.Payment.CreatePayment(1, new User(46088, 4, "Корнеев Егор"),
                new User(3324, 2200, "Корнеев Егор"), 2200, DateTime.Now, PaymentType.auto, 1, DateTime.Now)
            };
            var payments2 = _repository.GetPayments();
            Assert.AreEqual(payments[0].Operator.ClientId,payments2[0].Operator.ClientId);
        }
        [TestMethod]
        public void insert_payment()
        {
            var payment = new DTOInsertPayment()
            {
                ClientCode = 6130,
                OperatorId = 46088,
                Price = 2200,
                Type = PaymentType.auto
            };
            var result = _repository.InsertPayment(payment);
            Assert.AreEqual(1,1);
        }
        [TestMethod]
        public void update_payment()
        {
            var payment = new DTOUpdatePayment()
            {
                Price = 1222
            };
            var result = _repository.UpdatePayment(1, payment);
            Assert.AreEqual(result.Id,1);
        }

        [TestMethod]
        public void get_clients()
        {
            var clients = _repository.GetClients(new[] {6127, 6130});
            Assert.AreEqual(clients[0].User.Name.Trim(), "КАССА".Trim());
        }

    }
}
