using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IBookRepository
    {
        bool ValidateBookOrder(List<OrderItem> items);

        void UpdateBookQuantity(List<OrderItem> items, bool status);
    }
}
