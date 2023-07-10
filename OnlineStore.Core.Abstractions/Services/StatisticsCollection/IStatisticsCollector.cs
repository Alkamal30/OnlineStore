namespace OnlineStore.Core.Abstractions.Services.StatisticsCollection;

public interface IStatisticsCollector {
    Task CollectAndSendAsync();
}
