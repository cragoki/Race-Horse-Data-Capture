using Infrastructure.PunterAdmin.Services;
using Microsoft.Extensions.Hosting;

namespace MissingDataRetriever
{
    public class MissingDataRetriever : BackgroundService
    {
        public IAdminRaceService _adminRaceService;
        public MissingDataRetriever(IAdminRaceService adminRaceService)
        {
            _adminRaceService = adminRaceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Initializing...");
            bool stop = false;
            while (!stoppingToken.IsCancellationRequested || stop)
            {
                var result = await _adminRaceService.RunResultRetrieval(null);

                if (result == 0)
                {
                    stop = true;
                }
                Console.WriteLine($"Processed {result} Races");

            }
        }
    }
}
