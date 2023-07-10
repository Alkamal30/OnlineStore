using Microsoft.Extensions.Configuration;
using OnlineStore.Core.Abstractions.Services.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Core.Services.RabbitMq;

public class RabbitMqService : IRabbitMqService {

	public RabbitMqService(IConfiguration configuration) { 
		_configuration = configuration.GetSection("RabbitMQ");
	}


	private IConfigurationSection _configuration;



	public void SendMessage(object message) {
		SendMessage(JsonSerializer.Serialize(message));
	}

	public void SendMessage(string message) {

		var factory = new ConnectionFactory() { HostName = _configuration["Host"] };
		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();

		channel.QueueDeclare(
			queue: _configuration["Queue"],
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);

		var body = Encoding.UTF8.GetBytes(message);

		channel.BasicPublish(
			exchange: "",
			routingKey: _configuration["Queue"],
			basicProperties: null,
			body: body
		);
	}
}
