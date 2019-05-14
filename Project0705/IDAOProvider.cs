﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project0705
{
    interface IDAOProvider
    {
        List<Customer> GetAllCustomers();

        List<Order> GetAllOrders();

        List<Order> GetAllOrdersByCumstomerId(int customerId);

        Order GetOrderById(int orderId);

        Customer GetCustomerById(int customerId);

        bool AddCustomer(Customer c);

        bool RemoveCustomer(int customerId);

        bool UpdateCustomer(Customer c);

        bool AddOrder(Order o);

        bool RemoveOrder(int orderId);

        bool UpdateOrder(Order o);

        List<OrderCustomer> GetAllOrderCustomer();
    }
}
