using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CarParts.Data.Componets;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Common.POCO.ValueObject;

namespace Wiki.Payment.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;
        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IWikiDbCommand GetCommand(string sql)
        {
            var cmd = new LafSqlCommand(sql, new SqlConnection(_connectionString))
            {
                CommandType = CommandType.StoredProcedure,
                UseTransaction = true,
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            return cmd;
        }
        public Client GetClient(int clientId)
        {
            Client client = null;
            using (var cmd = GetCommand("get_client_byId"))
            {
                cmd.AddParameter("Id", clientId);
                cmd.ExecuteReader(x =>
                {
                    client = Client.CreateClient(
                        new User(
                            x.GetValue<int>("Id"),
                            x.GetValue<int>("ClientCode"),
                            x.GetValue<string>("Name")
                        ),                                            
                        x.GetValue<string>("Phone")
                    );
                });
            }
            return client;
        }

        public Client GetClientByCode(int clientCode)
        {
            Client client = null;
            using (var cmd = GetCommand("get_client_byCode"))
            {
                cmd.AddParameter("ClientCode", clientCode);
                cmd.ExecuteReader(x =>
                {
                    client = Client.CreateClient(
                        new User(
                            x.GetValue<int>("Id"),
                            x.GetValue<int>("ClientCode"),
                            x.GetValue<string>("Name")
                        ),
                        x.GetValue<string>("Phone")
                    );
                });
            }
            return client;
        }

        public List<Client> GetClients(int[] clientIds)
        {
            return clientIds.Aggregate(new List<Client>(), ((list, i) =>
            {
                list.Add(GetClient(i));
                return list;
            }));
        }

        public List<Manager> GetAllowedManagers()
        {
            var managers = new List<Manager>();
            using (var cmd = GetCommand("Payments_get_allowed_contacts"))
            {
                cmd.ExecuteReader(x =>
                {
                    managers.Add(new Manager(
                        x.GetValue<int>("ClientId"),
                        x.GetValue<string>("Phone"),
                        x.GetValue<string>("Name"),
                        x.GetValue<DateTime?>("LastModified")));
                });
            }
            return managers;          
        }
        public ReturnModel AddManager(DTOManager manager)
        {
            ReturnModel result = null;
            using (var cmd = GetCommand("Payments_insert_manager"))
            {
                cmd.AddParameter("ClientCode", manager.Id);
                cmd.AddParameter("Phone", manager.Phone);
                cmd.ExecuteReader(x =>
                {
                    result = new ReturnModel()
                    {
                        Id = manager.Id,
                        LastModified = DateTime.Now
                    };
                });
            }
            return result;
        }

        public ReturnModel UpdateManager(int id,DTOManager manager)
        {
            ReturnModel result = null;
            using (var cmd = GetCommand("Payments_update_allowed_contacts"))
            {
                cmd.AddParameter("ClientId", id);
                cmd.AddParameter("Phone", manager.Phone);
                cmd.ExecuteReader(x =>
                {
                    result = new ReturnModel()
                    {
                        Id = x.GetValue<int>("ClientId"),
                        LastModified = x.GetValue<DateTime>("LastModified")
                    };
                });
            }
            return result;
        }

        public void DeleteManager(int id)
        {
            using (var cmd = GetCommand("Payments_delete_allowed_contacts"))
            {
                cmd.AddParameter("ClientId", id);
                cmd.Execute();
            }
        }

        public ReturnModel InsertPayment(DTOInsertPayment payment)
        {
            ReturnModel result = null;
            using (var cmd = GetCommand("Payments_insert_payment"))
            {
                cmd.AddParameter("Type", payment.Type);
                cmd.AddParameter("OperatorId", payment.OperatorId);
                cmd.AddParameter("ClientCode", payment.ClientCode);
                cmd.AddParameter("Price", payment.Price);
                cmd.ExecuteReader(x =>
                {
                    result = new ReturnModel()
                    {
                        Id = x.GetValue<int>("Id"),
                        LastModified = x.GetValue<DateTime>("CreatedDate")
                    };
                });
            }
            return result;
            
        }

        public ReturnModel UpdatePayment(int id, DTOUpdatePayment payment)
        {
            ReturnModel result = null;
            using (var cmd = GetCommand("Payments_update_payment"))
            {
                cmd.AddParameter("Id", id);
                cmd.AddParameter("Type", payment.Type);
                cmd.AddParameter("OperatorId", payment.OperatorId);
                cmd.AddParameter("ClientId", payment.ClientId);
                cmd.AddParameter("Price", payment.Price);
                cmd.AddParameter("DocCode", payment.ErpCode);
                cmd.ExecuteReader(x =>
                {
                    result = new ReturnModel()
                    {
                        Id = x.GetValue<int>("Id"),
                        LastModified = x.GetValue<DateTime>("LastModified")
                    };
                });
            }
            return result;
        }

        public void DeletePayment(int id)
        {
            using (var cmd = GetCommand("Payments_delete_payment"))
            {
                cmd.AddParameter("Id", id);
                cmd.Execute();
            }
        }

        public Common.POCO.Payment GetPayment(int paymentId)
        {
            Common.POCO.Payment payment = null;
            using (var cmd = GetCommand("Payments_get_payment_byId"))
            {
                cmd.AddParameter("Id", paymentId);
                cmd.ExecuteReader(x =>
                {
                    payment = Common.POCO.Payment.CreatePayment(
                        x.GetValue<int>("Id"),
                        new User(
                            x.GetValue<int>("OperatorId"),
                            x.GetValue<int>("OperatorCode"),
                            x.GetValue<string>("OperatorName")),
                        new User(
                            x.GetValue<int>("OperatorId"),
                            x.GetValue<int>("ClientCode"),
                            x.GetValue<string>("ClientName").Trim()),
                        x.GetValue<decimal>("Price"),
                        x.GetValue<DateTime>("CreatedDate"),
                        x.GetValue<PaymentType>("Type"),
                        x.GetValue<int>("DocCode"),
                        x.GetValue<DateTime>("LastModified")
                    );
                });
                return payment;
            }
        }

        public List<Common.POCO.Payment> GetPayments(int? operatorId = null)
        {
            var payments = new List<Common.POCO.Payment>();
            using (var cmd = GetCommand("Payments_get_payments"))
            {
                cmd.AddParameter("OperatorId", operatorId);
                cmd.ExecuteReader(x =>
                {
                    payments.Add(Common.POCO.Payment.CreatePayment(
                        x.GetValue<int>("Id"),
                        new User(
                            x.GetValue<int>("OperatorId"),
                            x.GetValue<int>("OperatorCode"),
                            x.GetValue<string>("OperatorName")),
                        new User(
                            x.GetValue<int>("OperatorId"),
                            x.GetValue<int>("ClientCode"),
                            x.GetValue<string>("ClientName").Trim()),
                        x.GetValue<decimal>("Price"),
                        x.GetValue<DateTime>("CreatedDate"),
                        x.GetValue<PaymentType>("Type"),
                        x.GetValue<int>("DocCode"),
                        x.GetValue<DateTime>("LastModified")
                    ));
                });
                return payments;
            }
        }
    }
}
