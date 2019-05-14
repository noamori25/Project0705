using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Project0705
{
    class DAOFirebasePOrovider : IDAOProvider
    {
        static IFirebaseClient firebaseClient;
        static IFirebaseConfig config;

        static DAOFirebasePOrovider ()
        {
            config = new FirebaseConfig
            {
                AuthSecret = ConfigurationManager.AppSettings["Secret"],
                BasePath = ConfigurationManager.AppSettings["Url"]
            };

            firebaseClient = new FireSharp.FirebaseClient(config);
            if (firebaseClient != null)
            {
                Console.WriteLine("Conection succeeded!");
            }
        }

        public bool AddCustomer(Customer c)
        {
            FirebaseResponse response = firebaseClient.Get($"Customers/{c.Id}");
            if (response.Body == "null")
            {
                SetResponse response1 = firebaseClient.Set($"Customers/{c.Id}", c);
                DataCust result = response1.ResultAs<DataCust>();
                Console.WriteLine($"Customer inserted {c.Id}");
                return true;
            }
            else 
            {
                Console.WriteLine("Customer ID already exist in data");
                return false;
            }

        }

        public bool AddOrder(Order o)
        {
            FirebaseResponse response = firebaseClient.Get($"Orders/{o.Id}");
            if (response.Body == "null")
            {
                FirebaseResponse response1 = firebaseClient.Get($"Customers/{o.Customer_Id}");
                if (response1.Body == "null")
                {
                    Console.WriteLine("Customer does not exist");
                    return false;
                }
                else
                {
                    SetResponse response2 = firebaseClient.Set($"Orders/{o.Id}", o);
                    DataOrd result = response1.ResultAs<DataOrd>();
                    Console.WriteLine($"Order inserted {o.Id}");
                    return true;
                }
               
            }
            else
            {
                Console.WriteLine("Order ID already exist in data");
                return false;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            FirebaseResponse response = firebaseClient.Get("Customers");
            List<Customer> allCustomers = response.ResultAs<List<Customer>>();
            allCustomers.RemoveAt(0);
           
            return allCustomers;
        }

        public List<OrderCustomer> GetAllOrderCustomer()
        {
            List<Customer> allCustomers = GetAllCustomers();
            List<Order> allOrders = GetAllOrders();

            var result = (
                from Customer in allCustomers
                join Order in allOrders
                on Customer.Id equals Order.Customer_Id
                select new
                {
                    resultID = Order.Id,
                    resultCustomerName = Customer.Name,
                    resultCustomer_Id = Customer.Id,
                    resultPrice = Order.Price,
                    resultDate = Order.Date
                }).ToList();

            List<OrderCustomer> allOrdersCustomers = new List<OrderCustomer>();

            foreach (var r in result)
            {
                allOrdersCustomers.Add(new OrderCustomer
                {
                    CustomerName = r.resultCustomerName,
                    Customer_Id = r.resultCustomer_Id,
                    Date = r.resultDate,
                    Id = r.resultID,
                    Price = r.resultPrice
                });
            }

            return allOrdersCustomers;
        }

        public List<Order> GetAllOrders()
        {
            FirebaseResponse response = firebaseClient.Get("Orders");
            List<Order> allOrders = response.ResultAs<List<Order>>();
            allOrders.RemoveAt(0);

            return allOrders;
        }

        public List<Order> GetAllOrdersByCumstomerId(int customerId)
        {
            List<Order> allOrders = GetAllOrders();
            List<Order> ordersByCustomerId = new List<Order>();
           foreach (Order o in allOrders)
            {
                if (o.Customer_Id == customerId)
                    ordersByCustomerId.Add(o);
            }
            return ordersByCustomerId;

        }

        public Customer GetCustomerById(int customerId)
        {
            Customer c = new Customer();
            List<Customer> allCustomers = GetAllCustomers();
            foreach (Customer c1 in allCustomers)
            {
                if (c1.Id == customerId)
                    c = c1;
            }
            if (c.Id == 0)
                Console.WriteLine("Customer does not exist");
            return c;
        }

        public Order GetOrderById(int orderId)
        {
            Order o = new Order();
            List<Order> allOrders = GetAllOrders();
            foreach (Order o1 in allOrders)
            {
                if (o1.Id == orderId)
                    o = o1;
            }
            if (o.Id == 0)
                Console.WriteLine("Order does not exist");
            return o;
        }

        public bool RemoveCustomer(int customerId)
        {
            List<Order> orderlist = GetAllOrdersByCumstomerId(customerId);
             if (orderlist.Count == 0)
             {
                DeleteResponse response = firebaseClient.Delete($"Customers/{customerId}");
                return true;
             }
            else
            {
                Console.WriteLine("Customer has orders");
                return false;
            }

        }

        public bool RemoveOrder(int orderId)
        {
           
            DeleteResponse response = firebaseClient.Delete($"Orders/{orderId}");
            return true;

        }

        public bool UpdateCustomer(Customer c)
        {
            FirebaseResponse response = firebaseClient.Update($"Customer/{c.Id}", c);

            Customer result = response.ResultAs<Customer>();
            return true;
        }

        public bool UpdateOrder(Order o)
        {
            Customer c = GetCustomerById(o.Customer_Id);
            if (c.Id != 0)
            {
                FirebaseResponse respone = firebaseClient.Update($"Order/{o.Id}", o);
                DataOrd or = respone.ResultAs<DataOrd>();
                return true;
            }
            else
            {
                Console.WriteLine("Customer does not exist");
                return false;
            }
           
        }
    }
}
