using System.Collections.Generic;

namespace Builder
{
    public class EMail
    {
        public string Recipient { get; set; }
        public string Body { get; set; }
        public string[] Copies { get; set; }
        public string[] Themes { get; set; }
    }

    public class EMailBuilder
    {
        public EmailWithRecipientBuilder AddRecipient(string recipient)
        {
            return new EmailWithRecipientBuilder(recipient);
        }

        public class EmailWithRecipientBuilder
        {
            private readonly string _recipient;

            public EmailWithRecipientBuilder(string recipient)
            {
                _recipient = recipient;
            }
            
        }

        public class EmailFinalBuilder
        {
            private readonly string _recipient;
            private readonly string _body;
            private readonly List<string> _copies;
            private readonly List<string> _themes;
            
            public EmailFinalBuilder(string recipient, string body)
            {
                _copies = new List<string>();
                _themes = new List<string>();
                _recipient = recipient;
                _body = body;
            }

            public void AddCopy(string copy)
            {
                _copies.Add(copy);
            }

            public void AddTheme(string theme)
            {
                _themes.Add(theme);
            }

            public EMail Build()
            {
                return new EMail
                {
                    Body = _body,
                    Copies = _copies.ToArray(),
                    Themes = _themes.ToArray(),
                    Recipient = _recipient
                };
            }
        }
    }
}