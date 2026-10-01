using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class timerFunction
{
    private readonly ILogger _logger;

    public timerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<timerFunction>();
    }

    [Function("timerFunction")]
    public void Run([TimerTrigger("%TIMER_SCHEDULE%")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        if (myTimer.IsPastDue)
        {
            _logger.LogWarning("The timer is running late!");
        }

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}
