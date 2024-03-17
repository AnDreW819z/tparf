using Microsoft.AspNetCore.Mvc;
using tparf.api.EmailSender;
using tparf.api.Extensions;
using tparf.api.Interfaces;
using tparf.dto.Orders;

namespace tparf.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;

        public OrderController(IOrderRepository orderRepository, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("/createOrder")]
        public async Task<IActionResult> CreateOreder([FromBody] CreateOrderDto orderDto)
        {
            try
            {
                var newOrder = await _orderRepository.CreateNewOrder(orderDto);
                if (newOrder != null)
                {
                    var order = newOrder.ConvertToDto();
                    var message = new Message(new string[] { $"{order.Email}" }, "TОРГОВО-ПРОМЫШЛЕННОЕ АГЕНТСТВО", $"{order.FirstName}, ваш заказ успешно создан на tparf.ru");
                    await _emailService.SendEmail(message);
                    return Ok(order);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            try
            {
                var orders = _orderRepository.GetOrders();
                if (orders == null)
                    return NoContent();
                else
                {
                    var orderDtos = (await orders).ConvertToDto();
                    return Ok(orderDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getOrdersByUser/{cartId:long}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUser(long cartId)
        {
            try
            {
                var orders = await _orderRepository.GetOrderByUser(cartId);
                if (orders == null)
                    return NoContent();
                else
                {
                    var orderDtos = orders.ConvertToDto();
                    return Ok(orderDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getOrderItems/{orderId:long}")]
        public async Task<ActionResult<List<OrderItemDto>>> GetOrderItems(long orderId)
        {
            try
            {
                var orderItems = await _orderRepository.GetOrderItems(orderId);
                if (orderItems == null)
                    return NoContent();
                else
                {
                    var orders = orderItems.ConvertToDto();
                    return Ok(orders);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }

        }
        [HttpDelete]
        [Route("deleteOrder/{orderId:long}")]
        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrder(orderId);
                if (order.StatusId == 1)
                {
                    await _orderRepository.DeleteOrder(orderId);
                    var result = order.ConvertToDto();
                    return Ok(result);
                }
                else
                {
                    return Ok("Ваша заявка уже была рассмотрена, поэтому заказ не может быть удален");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("getStatuses")]
        public async Task<ActionResult<List<OrderStatusDto>>> GetSatuses()
        {
            try
            {
                var statuses = await _orderRepository.GetStatuses();
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateOrderStatus/{id:long}")]
        public async Task<IActionResult> UpdateStatus(long id, int statusId)
        {
            try
            {
                var order = await _orderRepository.UpdateStatus(id, statusId);
                if (order != null)
                {
                    var result = order.ConvertToDto();
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
