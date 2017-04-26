using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Payment.Utils;

namespace Wiki.Payment.Test
{
    [TestClass]
    public class MapperTest
    {
        private readonly IMapper _mapper;
        public MapperTest()
        {
            _mapper = AutoMapperWebConfiguration.MapperConfiguration.CreateMapper();
        }
        [TestMethod]
        public void map_client_to_dto()
        {
            var managers = new List<Manager>()
            {
                new Manager(1,"2","2")
            };
            var mapper = _mapper.Map<List<Manager>, List<DTOManager>>(managers);
            Assert.AreEqual(1,1);
        }

    }
}
