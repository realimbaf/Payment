using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Repository;

namespace Wiki.Payment.Test
{
    [TestClass]
    public class ExportRepositoryTest
    {
        private readonly IExportRepository _repository;

        public ExportRepositoryTest()
        {
            _repository = new ExportRepository(@"Data Source=192.168.0.208;Initial Catalog=Wiki;Integrated Security=false;
                                            User ID=sa;Password=masterkey;");
        }
        [TestMethod]
        public void erp_test()
        {
            var erpCode = new DTOErp
            {
                Ids = new long[]
                {
                    1,
                    2,
                    3,
                    4
                }
            };
            _repository.SendIdsToErp(erpCode);
        }
    }
}
