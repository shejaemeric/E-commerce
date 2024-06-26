using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface ITokenServices
    {
        public string GenerateToken(string userId);

        public bool IsTokenExpired(string token);

    }
}
