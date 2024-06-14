using MiniMarket_API.Application.DTOs.DetailData;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IPriceStockService
    {
        Task<NewDetailResultsDto?> FormDetailData(Guid productId, int orderQuantity);
        Task<int?> HandleDetailDeletion(Guid detailId);
    }
}
