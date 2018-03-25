using System;
using System.Threading.Tasks;

namespace TimeTravel
{
    public static class SystemTimeOffset
    {
        private static TimeSpan _offset = TimeSpan.Zero;

        /// <summary>
        /// Event triggered before time travel is initiated
        /// </summary>
        public static event Func<TimeTravelEventArgs, Task> TimeTravelling;

        /// <summary>
        /// Event triggered after time travel is complete
        /// </summary>
        public static event Func<TimeTravelEventArgs, Task> TimeTravelled;

        /// <summary>
        /// Identifies if time travel is enabled in the application
        /// </summary>
        public static bool IsTimeTravelEnabled { get; set; }

        /// <summary>
        /// Current time travel offset
        /// </summary>
        public static TimeSpan OffSet => _offset;

        /// <summary>
        /// Now according to current time line
        /// </summary>
        public static DateTimeOffset Now
        {
            get
            {
                if (IsTimeTravelEnabled)
                {
                    return DateTimeOffset.Now.Add(_offset);
                }

                return DateTimeOffset.Now;
            }
        }

        /// <summary>
        /// UTC now according to current time line
        /// </summary>
        public static DateTimeOffset UtcNow
        {
            get
            {
                if (IsTimeTravelEnabled)
                {
                    return DateTimeOffset.UtcNow.Add(_offset);
                }

                return DateTimeOffset.UtcNow;
            }
        }

        /// <summary>
        /// Moves the application in time by the given interval
        /// </summary>
        /// <param name="interval"></param>
        public static async Task TimeTravelAsync(TimeSpan interval)
        {
            if (!IsTimeTravelEnabled)
            {
                // Fail-Safe code
                throw new InvalidOperationException("Time travel is not enabled");
            }

            if (TimeTravelling != null)
            {
                await TimeTravelling(new TimeTravelEventArgs(OffSet, interval));
            }

            _offset = _offset.Add(interval);

            if (TimeTravelled != null)
            {
                await TimeTravelled(new TimeTravelEventArgs(OffSet, interval));
            }
        }

        /// <summary>
        /// Reset time to home time
        /// </summary>
        public static Task ResetToHomeTimeAsync()
        {
            return TimeTravelAsync(TimeSpan.Zero - OffSet);
        }

        /// <summary>
        /// Use this method to get the real time in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="appTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ToRealTime(this DateTimeOffset appTime)
        {
            if (IsTimeTravelEnabled)
            {
                return appTime.Add(TimeSpan.Zero - _offset);
            }

            return appTime;
        }

        /// <summary>
        /// Use this method to convert real time into AppTime, in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="realTime"></param>
        /// <returns></returns>
        public static DateTimeOffset FromRealTime(this DateTimeOffset realTime)
        {
            if (IsTimeTravelEnabled)
            {
                return realTime.Add(_offset);
            }

            return realTime;
        }

        /// <summary>
        /// Use this method to get the real time in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="appTime"></param>
        /// <returns></returns>
        public static DateTimeOffset? ToRealTime(this DateTimeOffset? appTime)
        {
            if (!appTime.HasValue)
            {
                return null;
            }

            if (IsTimeTravelEnabled)
            {
                return appTime.Value.Add(TimeSpan.Zero - _offset);
            }

            return appTime;
        }

        /// <summary>
        /// Use this method to convert real time into AppTime, in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="realTime"></param>
        /// <returns></returns>
        public static DateTimeOffset? FromRealTime(this DateTimeOffset? realTime)
        {
            if (!realTime.HasValue)
            {
                return null;
            }

            if (IsTimeTravelEnabled)
            {
                return realTime.Value.Add(_offset);
            }

            return realTime;
        }
    }
}
