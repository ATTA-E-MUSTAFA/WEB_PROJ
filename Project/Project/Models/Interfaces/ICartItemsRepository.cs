using Project.Models.Entities;
using System.Collections.Generic;

namespace Project.Models.Interfaces
{
    public interface ICartItemsRepository 
    {
        void Add(CartItems cartItem);
        public void ClearCartForUser(string userId);
        void RemoveCartItem(int id);
        List<CartItems> GetCartItemsByUserId(string userId);
        CartItems GetById(int id);
       
    }
}
