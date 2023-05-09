using SalesWebMvc.Models;
using System.Collections.Generic;

namespace SalesWebMvc.Interfaces
{
    public interface ISellerService
    {
        List<Seller> GetSellers();

        int CreateSeller(Seller seller);
    }
}
