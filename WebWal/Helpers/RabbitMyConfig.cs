using System;

namespace FakeUserApi.Models
{
    public class RabbitMqConfig
    {
        /// <summary>
        /// Server address
        /// </summary>
        public string Server = "localhost";

        /// <summary>
        /// Username
        /// </summary>
        public string User = "guest";

        /// <summary>
        /// Password
        /// </summary>
        public string Password = "guest";

        /// <summary>
        /// Server port
        /// </summary>
        public int Port = 15672;

        /// <summary>
        /// Get complete url
        /// </summary>
        /// <returns></returns>
        public Uri GetServiceUri()
        {
            return new Uri($"rabbitmq://{Server}:{Port}/");
        }

        /// <summary>
        /// Stringify connection information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"server {GetServiceUri()} connection configuration, username {User} {(string.IsNullOrEmpty(Password) ? "without" : "with")} password";
        }
    }
}
