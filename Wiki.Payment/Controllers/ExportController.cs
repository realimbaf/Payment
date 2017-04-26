using System.Web.Http;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Repository;
using Wiki.Payment.Utils;

namespace Wiki.Payment.Controllers
{
    [RoutePrefix("api/Export")]
    public class ExportController : ApiController
    {
        private readonly IExportRepository _repository;

        public ExportController()
        {
            _repository = new ExportRepository(Constants.CONNECTIONSTRING);
        }
        [HttpPost]
        [Route("Erp")]
        public void ErpReport(DTOErp ids)
        {
            _repository.SendIdsToErp(ids);
        }

    }
}
