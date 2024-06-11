using server_side.Data.Interfaces;
using server_side.Model.DTO;
using server_side.Model.Models;

namespace server_side.Data.Respositories
{
    public class PreferenceProductService : IProductRepository
    {
        public int SetPreferenceProduct(int productId, PreferenceProductDTO preferenceProductDTO)
        {
            try
            {
                PreferenceProduct newPreferenceProduct = new PreferenceProduct
                {
                    Id = productId,
                    Title = preferenceProductDTO.Title,
                    Quantity = preferenceProductDTO.Quantity,
                    Price = (int)preferenceProductDTO.Price
                };

                // Aquí agregarías la lógica para guardar el producto en la base de datos y devolver el Id del producto.

                return newPreferenceProduct.Id;
            }
            catch (Exception ex)
            {
                // Manejar cualquier error aquí
                throw new Exception("Error al establecer el producto de preferencia", ex);
            }
        }

    }
}