using System;

namespace TimeTravel
{
    public class TimeTravelEventArgs : EventArgs
    {
        public TimeSpan CurrentOffset { get; }
        public TimeSpan TravelBy { get; }

        public TimeTravelEventArgs(TimeSpan currentOffset, TimeSpan travelBy)
        {
            CurrentOffset = currentOffset;
            TravelBy = travelBy;
        }

    }
}