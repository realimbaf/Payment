using System.Collections.Generic;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;

namespace Wiki.Payment.Repository
{
    public interface IPaymentRepository
    {
        Client GetClient(int clientId);
        Client GetClientByCode(int clientCode);
        List<Client> GetClients(int[] clientIds);
        List<Manager> GetAllowedManagers();
        ReturnModel AddManager(DTOManager manager);
        ReturnModel UpdateManager(int id,DTOManager manager);
        void DeleteManager(int id);
        ReturnModel InsertPayment(DTOInsertPayment payment);
        ReturnModel UpdatePayment(int id, DTOUpdatePayment payment);
        void DeletePayment(int id);
        Common.POCO.Payment GetPayment(int paymentId);
        List<Common.POCO.Payment> GetPayments(int? operatorId = null);

    }
}
