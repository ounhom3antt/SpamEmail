using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpamEmail
{
    class Email
    {
        private string emailBot;
        private string password;

        public Email(string emailBot, string password)
        {
            this.emailBot = emailBot;
            this.password = password;
        }

        public string EmailBot { get => emailBot; set => emailBot = value; }
        public string Password { get => password; set => password = value; }
    }
}
