using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IEmailService
    {
         Task SendVerificationEmail(User user,string token);
    }
}
