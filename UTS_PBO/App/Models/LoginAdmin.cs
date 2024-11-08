using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS_PBO.Models;

namespace UTS_PBO.Models
{
    internal class LoginAdmin
    {
        private string email { get; set; }
        private string password { get; set; }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public LoginAdmin(string username, string password)
        {
            this.email = username;
            this.password = password;
        }
    }
}
