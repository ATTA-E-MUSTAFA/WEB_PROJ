using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly IProductRepository _productRepository;
        public CartItemsController(ICartItemsRepository cartItemsRepository, IProductRepository productRepository)
        {
            _cartItemsRepository = cartItemsRepository;
            _productRepository = productRepository;
        }

        // GET: CartItem
        public IActionResult Index(string id)
        {

            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            id = ViewBag.UserId;
            if (id==null)
            {
                return View();
            }
            var cartItems = _cartItemsRepository.GetCartItemsByUserId(id);
            int count = 0;
            foreach (var item in cartItems)
            {
                count += 1;
                item.Product = _productRepository.GetById(item.ProductId); 
            }
            Console.WriteLine(count);
            return View(cartItems);
        }

        // GET: CartItem/Details/5
        public IActionResult Details(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            // Fetch the product for the cart item
            cartItem.Product = _productRepository.GetById(cartItem.ProductId);

            return View(cartItem);
        }


        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = new CartItems
            {
                UserId = userId
            };
            var products = _productRepository.GetAll();
            ViewBag.Products = products; // Pass products using ViewBag
            return View(cartItem);
        }

        // POST: CartItem/Create
        [HttpPost]
        public IActionResult Create(CartItems cartItem)
        {
            Console.WriteLine("received");
            if (ModelState.IsValid)
            {
                Console.WriteLine(cartItem.Id);
                _cartItemsRepository.Add(cartItem);

                return RedirectToAction("Index", "CartItems", new { userId = cartItem.UserId });
            }
            return View(cartItem);
        }


        // GET: CartItem/Delete/5
        public IActionResult Delete(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
           
            return View(cartItem);
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
            if (cartItem != null)
            {
                _cartItemsRepository.RemoveCartItem(id);
            }
            return RedirectToAction(nameof(Index), new { userId = cartItem?.UserId });
        }
    }
}
