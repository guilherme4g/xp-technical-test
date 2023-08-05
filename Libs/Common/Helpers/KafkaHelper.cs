namespace Common.Helpers
{
    public interface IKafkaConsumerService
    {
        Task Consume(CancellationToken cancellationToken);
    }
}
