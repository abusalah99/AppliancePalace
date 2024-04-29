using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace AppliancePalaceWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        public CartController(IOrderRepository orderRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            int userId = 0;
            int.TryParse(User.FindFirst("Id")?.Value, out userId);

            Order? cart = await _orderRepository.GetUserCart(userId);

            if (cart == null)
                return NotFound();

            return View(cart);
        }

        // POST: OrderController/AddToCart/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId)
        {
            int userId = 0;
            int.TryParse(User.FindFirst("Id")?.Value, out userId);

            Product? product = await _productRepository.GetById(productId);

            if (product == null)
                return BadRequest("Worng Product Id");

            if (product.Qunatity == 0)
                return BadRequest("Out Of Stock");

            Order? cart = null;

            cart = await _orderRepository.GetUserCart(userId);

            if (cart == null)

                cart = new()
                {
                    OrderType = OrderTypeEnum.Cart,
                    UserId = userId,
                    TotalPrice = 0
                };

            cart.TotalPrice += product.Price;

            product.Qunatity -= 1;

            List<OrderProduct>? ordersProducts = null;

            if (cart.Id > 0)
            {
                ordersProducts = cart.ordersProducts.ToList();

                OrderProduct? orderProduct = null;
                orderProduct = ordersProducts.FirstOrDefault(o => o.ProductId == productId && o.OrderId == cart.Id);

                if (orderProduct != null)
                {
                    ordersProducts.Remove(orderProduct);
                    orderProduct.Quantity += 1;
                    orderProduct.Price = product.Price;
                }
                else
                {
                    orderProduct = new()
                    {
                        OrderId = cart.Id,
                        ProductId = productId,
                        Quantity = 1,
                        Price = product.Price
                    };
                }
                ordersProducts.Add(orderProduct);

                await _orderRepository.Edit(cart);

                await _orderProductRepository.Edit(orderProduct);

            }
            else
            {
                cart.ordersProducts = new List<OrderProduct>()
                {
                    new()
                    {
                        ProductId = productId,
                        Price = product.Price,
                        Quantity = 1
                    }
                };

                await _orderRepository.Add(cart);
            }

            await _productRepository.Edit(product);

            //Modfiy Redirect
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutCart()
        {
            int userId = 0;
            int.TryParse(User.FindFirst("Id")?.Value, out userId);

            Order? cart = await _orderRepository.GetUserCart(userId);

            if (cart == null) 
                return NotFound("Here is no active cart");

            cart.OrderType = OrderTypeEnum.Invoice;

            await _orderRepository.Edit(cart);

            //Modfiy Redirect
            return View();
        }

        /* // GET: OrderController/Edit/5
         public ActionResult Edit(int id)
         {
             return View();
         }

         // POST: OrderController/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }

         // GET: OrderController/Delete/5
         public ActionResult Delete(int id)
         {
             return View();
         }

         // POST: OrderController/Delete/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/

    }
}
