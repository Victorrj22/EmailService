using EmailService.Repository;

namespace EmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Service _service = new Service();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _service.StartServiceEmail();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}