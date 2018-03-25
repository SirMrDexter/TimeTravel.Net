# TimeTravel.Net
This library provides abstraction between .Net's System DateTime and System.DateTimeOffset classes and enabled application time to be changed as desired during runtime.

It's expected to be used only for testing time sensitve application code.

# Usage
Enable time travel as

    SystemTimeOffset.IsTimeTravelEnabled = true;

Change time by calling

    await SystemTimeOffset.TimeTravelAsync(TimeSpan.FromDays(30))

Swap all usages of

DateTime.Now with SystemTime.Now

DateTime.UtcNow with SystemTime.UtcNow

DateTime.Today with SystemTime.Today

DateTimeOffset.Now with SystemTimeOffset.Now

DateTimeOffset.UtcNow with SystemTimeOffset.UtcNow

Rest back to real time

    await SystemTimeOffset.ResetToHomeTimeAsync()
