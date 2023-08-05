using Newtonsoft.Json;

namespace Api_Users.Services.Users
{
    public class CreditOperationService<K,T>
    {
        private readonly KafkaService<K> _kafkaService;

        public CreditOperationService(KafkaService<K> kafkaService) { 
            this._kafkaService = kafkaService;
        }

        public async Task<T> Execute(K request) {
            var response = await _kafkaService.ProduceAsync(request, "OPERATIONS.CreditOperation.create");
            T result = JsonConvert.DeserializeObject<T>(response.Value);

            return result;
        }
    }
}
