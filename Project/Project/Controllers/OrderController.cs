using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Collections.Generic;

public class OrderController : Controller
{
    private readonly ICartItemsRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository; // If needed for fetching product details

    public OrderController(
         IOrderRepository orderRepository,
         ICartItemsRepository cartItemRepository,
         IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
    }

    public ActionResult Index()
    {
        List<Order> orders = _orderRepository.GetAll();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var order = _orderRepository.GetById(id);
        
        return View(order);
    }
    public IActionResult PlaceOrder(string userId)
    {
        // Fetch cart items for the current user
        var cartItems = _cartItemRepository.GetCartItemsByUserId(userId);

        if (!cartItems.Any())
        {
            return RedirectToAction("Index", "CartItems"); // Redirect if the cart is empty
        }

        // Create a new order
        var order = new Order
        {
            OrderDate = DateTime.Now,
            UserId = userId
        };

        _orderRepository.Add(order);

        // Optionally associate the order with cart items

        // Clear the cart for the user
        _cartItemRepository.ClearCartForUser(userId);

        // Redirect to order confirmation or success page
        return RedirectToAction("OrderConfirmation");
    }


    // GET: /Order/OrderConfirmation
    public IActionResult OrderConfirmation()
    {
        return View();
    }
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.Add(order);
            return RedirectToAction("Index");
        }
        return View(order);
    }

    public ActionResult Edit(int id)
    {
        Order order = _orderRepository.GetById(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    [HttpPost]
    public ActionResult Edit(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.Update(order);
            return RedirectToAction("Index");
        }
        return View(order);
    }

    public ActionResult Delete(int id)
    {
        Order order = _orderRepository.GetById(id);
        
        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Console.WriteLine(id);
        _orderRepository.Delete(id);
        return RedirectToAction("Index");
    }
}
