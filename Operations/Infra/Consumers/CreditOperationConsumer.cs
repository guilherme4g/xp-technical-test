using Common.Helpers;
using Confluent.Kafka;
using Infra.EntityFramework;
using Infra.EntityFramework.Repositories;
using Interface.Controllers;
using Newtonsoft.Json;

namespace Infra.Consumers
{
    public class CreditOperationConsumer : IKafkaConsumerService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsumerConfig _config;

        public CreditOperationConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _config = new ConsumerConfig
            {
                GroupId = "operations-consumer-group",
                BootstrapServers = "127.0.0.1:9091",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
        public async Task Consume(CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config).Build();
            consumer.Subscribe("OPERATIONS.CreditOperation.create");

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

                        CreditOperationController creditOperationController = new CreditOperationController(walletRepository, operationRepository);


                        var request = JsonConvert.DeserializeObject<CreditOperationRequest>(message.Value);

                        
                        var transaction = dbContext.Database.BeginTransaction();

                        try
                        {
                            await creditOperationController.Execute(request);

                            transaction.Commit();
                        }
                        catch (Exception)
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
