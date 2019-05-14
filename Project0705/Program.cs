using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project0705
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAOProvider Mssql = new DAOMSSQLProvider();
            IDAOProvider Firebase = new DAOFirebasePOrovider();

            Console.WriteLine("All customers:");
            //Mssql.GetAllCustomers().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
            Console.WriteLine("All orders:");
           // Mssql.GetAllOrders().ForEach(o => Console.WriteLine(JsonConvert.SerializeObject(o)));
            Console.WriteLine("Orders by customer id:");
            //Mssql.GetAllOrdersByCumstomerId(1).ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
            Console.WriteLine("Order by id:");
           // Console.WriteLine(JsonConvert.SerializeObject(Mssql.GetOrderById(8)));
            Console.WriteLine("Customer by id");
           // Console.WriteLine(JsonConvert.SerializeObject(Mssql.GetCustomerById(3)));
            Console.WriteLine("Add customer:");
            Customer addcustomer = new Customer
            {
                Name = "Yron",
                Age = 43,
                Country = "Germany"
            };
            // Console.WriteLine(Mssql.AddCustomer(addcustomer));
            Console.WriteLine("Remove customer:");
            //Console.WriteLine(Mssql.RemoveCustomer(2));
            Console.WriteLine("Update customer:");
            //Console.WriteLine(Mssql.UpdateCustomer(new Customer { Name = "Amir", Age = 45, Country = "Israel", Id = 5 }));
            Console.WriteLine("Add order:");
            //Console.WriteLine(Mssql.AddOrder(new Order { Customer_Id = 3, Date = "06.05", Price = 800 }));
            Console.WriteLine("Remove order:");
            //Console.WriteLine(Mssql.RemoveOrder(3));
            Console.WriteLine("Updte Order:");
            //Console.WriteLine(Mssql.UpdateOrder(new Order { Customer_Id = 3, Date = "09.04", Price = 350, Id = 4 }));
            Console.WriteLine("Get all orders and customers:");
            //Console.WriteLine(JsonConvert.SerializeObject(Mssql.GetAllOrderCustomer()));

            Console.WriteLine("Add customer firebase");
            Customer addcustomer1 = new Customer
            {
                Name = "Yaron",
                Age = 47,
                Country = "Germany",
                Id = 8
            };
            //Console.WriteLine(Firebase.AddCustomer(addcustomer1));
            Console.WriteLine("Add order firebase");
            Order addorder1 = new Order
            {
                Customer_Id = 7,
                Date = "08.09",
                Id = 6,
                Price = 600
            };
            // Console.WriteLine(Firebase.AddOrder(addorder1));
            Console.WriteLine("All customers firebase");
          // Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetAllCustomers()));
            Console.WriteLine("All orders firebase");
            //Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetAllOrders()));
            Console.WriteLine("All Customers and Orders firebase");
            //Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetAllOrderCustomer()));
            Console.WriteLine("Get order by customer Id firebase");
            // Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetAllOrdersByCumstomerId(1)));
            Console.WriteLine("Get customer by id firebase");
             //Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetCustomerById(12)));
            Console.WriteLine("Get order by id firebase");
            //Console.WriteLine(JsonConvert.SerializeObject(Firebase.GetOrderById(22)));
            Console.WriteLine("Remove Customer firebase");
            // Console.WriteLine(Firebase.RemoveCustomer(6));
            Console.WriteLine("Remove Order firebase");
           // Console.WriteLine(Firebase.RemoveOrder(7));
            Console.WriteLine("Update Order firebase");
            Order o1 = new Order
            {
                Customer_Id = 1,
                Date = "08.03",
                Id = 1,
                Price = 1000
            };
            Console.WriteLine(Firebase.UpdateOrder(o1));



        }
    }
}
