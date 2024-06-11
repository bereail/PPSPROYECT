using Microsoft.AspNetCore.Mvc;
using server_side.Model.DTO;
using server_side.Model.Models;

namespace server_side.Data.Interfaces
{
    public interface IProductRepository
    {
        int SetPreferenceProduct(int productId, PreferenceProductDTO preferenceProductDTO);
    }


}


