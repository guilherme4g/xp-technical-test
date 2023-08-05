using Confluent.Kafka;
using Newtonsoft.Json;

namespace Api_Users.Services
{
    public class KafkaService<K>
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaService()
        {
            var config = new ProducerConfig { BootstrapServers = "127.0.0.1:9091" };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync(K request, string topicName)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                return await _producer.ProduceAsync(topicName, new Message<Null, string> { Value = jsonRequest });
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                throw;
            }
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
