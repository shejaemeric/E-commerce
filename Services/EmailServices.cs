using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;

namespace E_Commerce_Api.Services
{
    public class EmailServices : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ITokenServices _tokenServices;

        public EmailServices(ITokenServices tokenServices, IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenServices = tokenServices;
        }
        public async Task SendVerificationEmail( User user,string token)
        {

            var emailInfo = _configuration.GetSection("Email");
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("E-commerce", emailInfo["AppFrom"]));
            email.To.Add(new MailboxAddress(user.Email, user.Email));
            email.Subject = "Verification Link";

            var confirmationLink = $"http://localhost:5136/api/Authentication/comfirmEmail?UserEmail={user.Email}&Token={token}";
            email.Body = new TextPart(TextFormat.Html) { Text = confirmationLink };

            using var smtp = new SmtpClient();
            smtp.Connect(emailInfo["Host"]);
            smtp.Authenticate(emailInfo["AppEmail"], emailInfo["AppPassword"]);

            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
