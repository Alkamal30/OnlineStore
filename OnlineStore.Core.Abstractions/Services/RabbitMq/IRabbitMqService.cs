namespace OnlineStore.Core.Abstractions.Services.RabbitMq;

public interface IRabbitMqService {

	void SendMessage(object message);
	void SendMessage(string message);
}
