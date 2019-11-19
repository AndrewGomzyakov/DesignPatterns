using System;

namespace Decorator
{
    interface IChatClient
    {
        void SendMessage(Message message);
        Message GetMessage();
    }

    public class Message
    {
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string Body { get; set; }
    }

    class ChatClient : IChatClient
    {
        private Message sendedMessage { get; set; }
        
        public void SendMessage(Message message)
        {
            sendedMessage = message;
        }

        public Message GetMessage()
        {
            return sendedMessage;
        }
    }

    class AnonymousChatClient : IChatClient
    {
        private readonly IChatClient _chatClient;

        public AnonymousChatClient(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }
        
        public void SendMessage(Message message)
        {
            message.SenderName = "anonymous";
            _chatClient.SendMessage(message);
        }

        public Message GetMessage()
        {
            var answer = _chatClient.GetMessage();
            answer.RecipientName = "you";
            return answer;
        }
    }

    class SecretChatClient : IChatClient
    {
        private readonly IChatClient _chatClient;

        public SecretChatClient(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }
        
        public void SendMessage(Message message)
        {
            message.Body = "<encrypted>" + message.Body + "</encrypted>";
            _chatClient.SendMessage(message);
        }

        public Message GetMessage()
        {
            var answer = _chatClient.GetMessage();
            answer.Body = answer.Body
                                .Replace("<encrypted>", "")
                                .Replace("</encrypted>", "");
            return answer;
        }
    }
}