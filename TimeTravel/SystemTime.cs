using System;

namespace TimeTravel
{
    public static class SystemTime
    {
        /// <summary>
        /// Now according to current time line
        /// </summary>
        public static DateTime Now
        {
            get
            {
                if (SystemTimeOffset.IsTimeTravelEnabled)
                {
                    return DateTime.Now.Add(SystemTimeOffset.OffSet);
                }

                return DateTime.Now;
            }
        }

        /// <summary>
        /// UTC now according to current time line
        /// </summary>
        public static DateTime UtcNow
        {
            get
            {
                if (SystemTimeOffset.IsTimeTravelEnabled)
                {
                    return DateTime.UtcNow.Add(SystemTimeOffset.OffSet);
                }

                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Today according to current time line
        /// </summary>
        public static DateTime Today
        {
            get
            {
                if (SystemTimeOffset.IsTimeTravelEnabled)
                {
                    return DateTime.Now.Add(SystemTimeOffset.OffSet).Date;
                }

                return DateTime.Today;
            }
        }

        /// <summary>
        /// Use this method to get the real time in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ToRealTime(this DateTime time)
        {
            if (SystemTimeOffset.IsTimeTravelEnabled)
            {
                return time.Add(TimeSpan.Zero - SystemTimeOffset.OffSet);
            }

            return time;
        }

        /// <summary>
        /// Use this method to get the real time in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? ToRealTime(this DateTime? time)
        {
            if (!time.HasValue)
            {
                return null;
            }

            if (SystemTimeOffset.IsTimeTravelEnabled)
            {
                return time.Value.Add(TimeSpan.Zero - SystemTimeOffset.OffSet);
            }

            return time;
        }

        /// <summary>
        /// Use this method to convert real time into AppTime, in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="realTime"></param>
        /// <returns></returns>
        public static DateTime FromRealTime(this DateTime realTime)
        {
            if (SystemTimeOffset.IsTimeTravelEnabled)
            {
                return realTime.Add(SystemTimeOffset.OffSet);
            }

            return realTime;
        }

        /// <summary>
        /// Use this method to convert real time into AppTime, in case the application uses TimeTravel.
        /// Needed when the application needs to communicate application time with third party applications.
        /// In case TimTravel is not enabled, the original input is returned as is.
        /// </summary>
        /// <param name="realTime"></param>
        /// <returns></returns>
        public static DateTime? FromRealTime(this DateTime? realTime)
        {
            if (!realTime.HasValue)
            {
                return null;
            }

            if (SystemTimeOffset.IsTimeTravelEnabled)
            {
                return realTime.Value.Add(SystemTimeOffset.OffSet);
            }

            return realTime;
        }
    }
}
