using Wiki.Payment.Common.POCO.DTO;

namespace Wiki.Payment.Repository
{
    public interface IExportRepository
    {
        void SendIdsToErp(DTOErp ids);
    }
}
