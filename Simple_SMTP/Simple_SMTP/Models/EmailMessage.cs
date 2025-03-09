using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_SMTP.Models
{

    public class EmailMessage
    {

        #region - - - - - Properties - - - - -

        public DateTime SentTime { get; set; } = DateTime.Now;

        public string Body { get; set; }

        public string Subject { get; set; }

        public Recipient From { get; set; }

        public List<Recipient> To { get; set; }

        public List<Recipient> Bcc { get; set; }

        public List<Recipient> Cc { get; set; }

        #endregion Properties

    }

}
