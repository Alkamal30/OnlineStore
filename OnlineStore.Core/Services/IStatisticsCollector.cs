namespace OnlineStore.Core.Services;

public interface IStatisticsCollector {
    Task CollectAndSendAsync();
}
