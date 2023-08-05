using Newtonsoft.Json;

namespace Api_Users.Services.Users
{
    public class DebitOperationService<K,T>
    {
        private readonly KafkaService<K> _kafkaService;

        public DebitOperationService(KafkaService<K> kafkaService) { 
            this._kafkaService = kafkaService;
        }

        public async Task<T> Execute(K request) {
            var response = await _kafkaService.ProduceAsync(request, "OPERATIONS.DebitOperation.create");
            T result = JsonConvert.DeserializeObject<T>(response.Value);

            return result;
        }
    }
}
