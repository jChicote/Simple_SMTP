using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_SMTP.Models
{

    public class SmtpSettings
    {

        #region - - - - - Properties - - - - -

        public int Port { get; set; } = 587; // Default port for SMTP with SSL. https://www.cloudflare.com/learning/email-security/smtp-port-25-587/

        public bool EnableSsl { get; set; } = true;

        public string FromEmail { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        #endregion Properties

    }

}
