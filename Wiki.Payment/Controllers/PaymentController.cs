using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using CarParts.Common.Log;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Repository;
using Wiki.Payment.Utils;


namespace Wiki.Payment.Controllers
{
    [RoutePrefix("api/Payments")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentRepository _repository;
        private readonly FileLogger _logger;
        private readonly IMapper _mapper;

        public PaymentController()
        {
            _repository = new PaymentRepository(Constants.CONNECTIONSTRING);
            _logger = new FileLogger("payment");
            _mapper = AutoMapperWebConfiguration.MapperConfiguration.CreateMapper();
        }

        [Route("")]
        [HttpGet]
        public List<DTOPayment> GetPayments()
        {
            try
            {
                var operatorId = Request.GetQueryString("operatorId").ToNullableInt();
                var payments =  _repository.GetPayments(operatorId);
                return _mapper.Map<List<Common.POCO.Payment>, List<DTOPayment>>(payments);           
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetPayments", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public DTOPayment GetPayment(int id)
        {
            try
            {
                var result = _repository.GetPayment(id);
                return _mapper.Map<DTOPayment>(result);

            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:GetPayment", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        [Route("")]
        [HttpPost]
        public ReturnModel InsertPayment([FromBody] DTOInsertPayment payment)
        {
            try
            {
                 return _repository.InsertPayment(payment);
            }
            catch (Exception ex)
            {
                _logger.WriteError("api error.method:InsertPayment", ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("{id}")]
        [HttpPut]
        public ReturnModel PutPayment(int id, [FromBody] DTOUpdatePayment payment)
        {
            try
            {
                return _repository.UpdatePayment(id, payment);

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public void DeletePayment(int id)
        {
            try
            {
                _repository.DeletePayment(id);

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
