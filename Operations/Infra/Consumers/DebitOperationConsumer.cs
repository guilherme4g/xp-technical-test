using Common.Helpers;
using Confluent.Kafka;
using Infra.EntityFramework;
using Infra.EntityFramework.Repositories;
using Interface.Controllers;
using Newtonsoft.Json;

namespace Infra.Controllers
{
    public class DebitOperationConsumer : IKafkaConsumerService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsumerConfig _config;

        public DebitOperationConsumer(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._config = new ConsumerConfig
            {
                GroupId = "operations-consumer-group",
                BootstrapServers = "127.0.0.1:9091",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
        public async Task Consume(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config).Build();
            consumer.Subscribe("OPERATIONS.DebitOperation.create");

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var message = consumer.Consume(cancellationToken);

                        using var scope = _serviceProvider.CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        WalletRepository walletRepository = new WalletRepository(dbContext);
                        OperationRepository operationRepository = new OperationRepository(dbContext);

                        DebitOperationController debitOperationController = new DebitOperationController(walletRepository, operationRepository);

                        var request = JsonConvert.DeserializeObject<DebitOperationRequest>(message.Value);

                        var transaction = dbContext.Database.BeginTransaction();
                        
                        try
                        {
                            await debitOperationController.Execute(request);
                        
                            transaction.Commit();
                        } catch (Exception)
                        {
                            // Algum erro ocorreu, desfaça a transação.
                            transaction.Rollback();
                            throw;
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
