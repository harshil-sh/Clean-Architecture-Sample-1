﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAllCustomers(long id);
    }
}
