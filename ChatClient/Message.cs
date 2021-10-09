using System.Text.Json;

namespace ChatClient
{
    public class Message
    {
        public string ClientName { get; set; }
        public string Content { get; set; }
        
        public Message (string clientName, string content)
        {
            ClientName = clientName;
            Content = content;
        }


        public string toJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Message fromJson(string jsonedMessage)
        {
            jsonedMessage = jsonedMessage.Replace("\0", string.Empty);
            return JsonSerializer.Deserialize<Message>(jsonedMessage);
        }
    }
}
