using System.Text.Json;
using Application.UseCases;
using Confluent.Kafka;
using Domain.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka
{
    public class CurityKafkaConsumer : BackgroundService
    {
        private readonly ILogger<CurityKafkaConsumer> _logger;
        private readonly CreateCurityCredentialUseCase _useCase;
        private readonly IConsumer<string, string> _consumer;

        public CurityKafkaConsumer(ILogger<CurityKafkaConsumer> logger,
                                   CreateCurityCredentialUseCase useCase,
                                   IConfiguration config)
        {
            _logger = logger;
            _useCase = useCase;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = "curity-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _consumer.Subscribe("create-curity-credential");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = _consumer.Consume(stoppingToken);

                try
                {
                    var eventData = JsonSerializer.Deserialize<CurityEvent>(cr.Message.Value);
                    var credential = await _useCase.ExecuteAsync(eventData!);
                    _logger.LogInformation($"Credential created: {credential.ClientId}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro processando evento Kafka.");
                }
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}