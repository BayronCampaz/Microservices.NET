﻿using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> Get(string userName);
        Task<ShoppingCart> Update(ShoppingCart basket);
        Task Delete(string userName);
    }
}
