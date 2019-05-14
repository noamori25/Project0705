using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project0705
{
    class DAOMSSQLProvider : IDAOProvider
    {
        public bool AddCustomer(Customer c)
        {
            using (Entities1 entities = new Entities1())
            {
                entities.Customers.Add(c);
                int res = entities.SaveChanges();

                if (res > 0)
                return true;
            }
            return false;
        }

        public bool AddOrder(Order o)
        {
            using (Entities1 entities = new Entities1())
            {
                entities.Orders.Add(o);
                int res = entities.SaveChanges();

                if (res > 0)
                    return true;
            }
            return false;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            using (Entities1 entities = new Entities1())
            {
                entities.Customers.ToList().ForEach(c => customerList.Add(c));
            }
            return customerList;
                
        }

        public List<OrderCustomer> GetAllOrderCustomer()
        {
            List<OrderCustomer> orderCustomer = new List<OrderCustomer>();
            using (Entities1 entities = new Entities1())
            {
                entities.Customers.Join(entities.Orders,
                   customer => customer.Id,
                   order => order.Customer_Id,
                   (customer, order) => new OrderCustomer
                   {
                       CustomerName = customer.Name,
                       Date = order.Date,
                       Customer_Id = customer.Id,
                       Id = order.Id,
                       Price = order.Price

                   }).ToList().ForEach(oc => orderCustomer.Add(oc));
            }
            return orderCustomer;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orderList = new List<Order>();
            using (Entities1 entities = new Entities1())
            {
                entities.Orders.ToList().ForEach(o => orderList.Add(o));
            }
            return orderList;
        }

        public List<Order> GetAllOrdersByCumstomerId(int customerId)
        {
            List<Order> orderList = new List<Order>();
            using (Entities1 entities = new Entities1())
            {
                entities.Orders.Where(o => o.Customer_Id == customerId).ToList().ForEach(o => orderList.Add(o));
            }
            return orderList;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = new Customer();
            using (Entities1 entities = new Entities1())
            {
                customer = entities.Customers.SingleOrDefault(c => c.Id == customerId);
                return customer;
            }
            return null;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = new Order();
            using (Entities1 entities = new Entities1())
            {
                order = entities.Orders.SingleOrDefault(o => o.Id == orderId);
                    return order;
            }
            return null;
        }

        public bool RemoveCustomer(int customerId)
        {
            using (Entities1 entities = new Entities1())
            {
                if (entities.Customers.Any(c => c.Id == customerId))
                {
                    var orders = entities.Orders.Where(o => o.Customer_Id == customerId).ToList();
                    orders.ForEach(o => entities.Orders.Remove(o));
                    entities.Customers.Remove(entities.Customers.Single(c => c.Id == customerId));
                    int res = entities.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            return false;
        }

        public bool RemoveOrder(int orderId)
        {
            using (Entities1 entities = new Entities1())
            {
                if (entities.Orders.Any(o => o.Id == orderId))
                {
                    entities.Orders.Remove(entities.Orders.Single(o1 => o1.Id == orderId));
                    int res = entities.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            return false;
        }

        public bool UpdateCustomer(Customer c)
        {
            using (Entities1 entities = new Entities1())
            {
                Customer updateCustomer = entities.Customers.SingleOrDefault(c2 => c2.Id == c.Id);
                if (updateCustomer.Id > 0)
                {
                    updateCustomer.Age = c.Age;
                    updateCustomer.Country = c.Country;
                    updateCustomer.Name = c.Name;
                    entities.SaveChanges();
                    return true;
                }
                
            }
            return false;
        }

        public bool UpdateOrder(Order o)
        {
            using (Entities1 entities = new Entities1())
            {
                Order updateOrder = entities.Orders.SingleOrDefault(o2 => o2.Id == o.Id);
                if (updateOrder.Id > 0)
                {
                    updateOrder.Date = o.Date;
                    updateOrder.Price = o.Price;
                    updateOrder.Customer_Id = o.Customer_Id;
                    entities.SaveChanges();
                    return true;
                }

            }
            return false;
        }
    }
}
