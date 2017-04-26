using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using CarParts.Common.Log;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Repository;
using Wiki.Payment.Utils;

namespace Wiki.Payment.Controllers
{
    [RoutePrefix("api/Clients")]
    public class ClientController : ApiController
    {
        private readonly IPaymentRepository _repository;
        private readonly FileLogger _logger;
        private readonly IMapper _mapper;

        public ClientController()
        {
            _repository = new PaymentRepository(Constants.CONNECTIONSTRING);
            _logger = new FileLogger("client");
            _mapper = AutoMapperWebConfiguration.MapperConfiguration.CreateMapper();
        }

        [Route("")]
        [HttpPost]
        public List<Client> GetClients([FromBody] DTOClients clients)
        {
            try
            {
                return _repository.GetClients(clients.Ids);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetClients", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public DTOClient GetClient(int id)
        {
            try
            {
                var client = _repository.GetClient(id);
                return _mapper.Map<DTOClient>(client);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetClient", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Code/{clientCode}")]
        [HttpGet]
        public DTOClient GetClientByCode(int clientCode)
        {
            try
            {
                var client = _repository.GetClientByCode(clientCode);
                return _mapper.Map<DTOClient>(client);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetClient", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        [Route("Managers")]
        [HttpGet]
        public List<DTOManager> GetManagers()
        {
            try
            {
                var managers = _repository.GetAllowedManagers();
                return _mapper.Map<List<Manager>, List<DTOManager>>(managers);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetManagers", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("Managers")]
        [HttpPost]
        public ReturnModel AddManager(DTOManager manager)
        {
            try
            {
                return _repository.AddManager(manager);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:PostManagers", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("Managers/{id}")]
        [HttpDelete]
        public void DeleteManagers(int id)
        {
            try
            {
                _repository.DeleteManager(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("Managers/{id}")]
        [HttpPut]
        public ReturnModel PutManagers(int id,[FromBody]DTOManager manager)
        {
            try
            {
                var result= _repository.UpdateManager(id, manager);
                return result;               
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
