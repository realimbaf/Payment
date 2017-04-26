using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Wiki.Core.Exceptions;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Service.Common.Clients;

namespace Wiki.Payment.Client
{
    public class PaymentClient : ServiceClientBase
    {
        public PaymentClient(string discoveryAdress, string clientId, string clientSecret) : base(discoveryAdress, clientId, clientSecret)
        { }

        public PaymentClient() : this(ConfigurationManager.AppSettings["DiscoveryUrl"],
                                  ConfigurationManager.AppSettings["ClientId"],
                                  ConfigurationManager.AppSettings["ClientSecret"])
        { }

        public PaymentClient(string discoveryUrl) : this(discoveryUrl,
                                                     ConfigurationManager.AppSettings["ClientId"],
                                                     ConfigurationManager.AppSettings["ClientSecret"])
        { }

        public override string ServiceId { get { return "Wiki.Payment"; } }

        public async Task<List<DTOPayment>> GetAllPayments(int? operatorId = null)
        {
            var cl = this.GetClient();
            var requestUri = "api/Payments";
            if (operatorId != null)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["operatorId"] = operatorId.ToString();
                requestUri += "/?" + query;
            }
            
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DTOPayment>>(content);

        }
        public async Task<DTOPayment> GetPaymentById(int id)
        {
            var cl = this.GetClient();
            var requestUri = "api/Payments/" + id;
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTOPayment>(content);

        }

        public async Task<DTOClient> GetClientById(int id)
        {
            var cl = this.GetClient();
            var requestUri = "api/Clients/" + id;
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTOClient>(content);

        }
        public async Task<DTOClient> GetClientByCode(int clientCode)
        {
            var cl = this.GetClient();
            var requestUri = "api/Clients/Code/" + clientCode;
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTOClient>(content);

        }
        public async Task<List<DTOClient>> GetClients(DTOClients clients)
        {
            var cl = this.GetClient();
            const string requestUri = "api/Clients";
            var stringContent = new StringContent(JsonConvert.SerializeObject(clients),
                                                    Encoding.UTF8,
                                                    "application/json");
            var result = await cl.PostAsync(requestUri, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DTOClient>>(content);
        }
        public async Task<ReturnModel> AddPayment(DTOInsertPayment payment)
        {
            var cl = this.GetClient();
            const string requestUri = "api/Payments";
            var stringContent = new StringContent(JsonConvert.SerializeObject(payment),
                                                     Encoding.UTF8,
                                                     "application/json");
            var result = await cl.PostAsync(requestUri, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel>(content);
        }
        public async Task<ReturnModel> UpdatePayment(int paymentId,DTOUpdatePayment payment)
        {
            var cl = this.GetClient();
            var requestUri = "api/Payments/"+ paymentId;
            var stringContent = new StringContent(JsonConvert.SerializeObject(payment),
                                                     Encoding.UTF8,
                                                     "application/json");
            var result = await cl.PutAsync(requestUri, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel>(content);
        }
        public async Task DeletePayment(int paymentId)
        {
            var cl = this.GetClient();
            var requestUri = "api/Payments/" + paymentId;
            var result = await cl.DeleteAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
        }
        public async Task<List<DTOManager>> GetAllowedManagers()
        {
            var cl = this.GetClient();
            var requestUri = "api/Clients/Managers";
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DTOManager>>(content);

        }
        public async Task<ReturnModel> AddManager(DTOManager manager)
        {
            var cl = this.GetClient();
            const string requestUri = "api/Clients/Managers";
            var stringContent = new StringContent(JsonConvert.SerializeObject(manager),
                                                     Encoding.UTF8,
                                                     "application/json");
            var result = await cl.PostAsync(requestUri, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel>(content);
        }
        public async Task<ReturnModel> UpdateManager(int id, DTOManager manager)
        {
            var cl = this.GetClient();
            var requestUri = "api/Clients/Managers/" + id;
            var stringContent = new StringContent(JsonConvert.SerializeObject(manager),
                                                     Encoding.UTF8,
                                                     "application/json");
            var result = await cl.PutAsync(requestUri, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel>(content);
        }
        public async Task<ReturnModel> DeleteManager(int id)
        {
            var cl = this.GetClient();
            var requestUri = "api/Clients/Managers/" + id;
            var result = await cl.DeleteAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ReturnModel>(content);
        }
        public async Task ReportToErp(DTOErp erpCodes)
        {
            var cl = this.GetClient();
            const string requestUri = "api/Export/Erp";
            var stringContent = new StringContent(JsonConvert.SerializeObject(erpCodes),
                                                    Encoding.UTF8,
                                                    "application/json");
            var result = await cl.PostAsync(requestUri,stringContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
        }
    }
}
