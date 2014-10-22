using System;
using System.Collections.Generic;

namespace Meta.Net
{

    /// <summary>
    /// A delegate method for subscribing to DataTimer status events.
    /// </summary>
    /// <param name="status">status message</param>
    public delegate void DataTimerStatus(string status);

    /// <summary>
    /// Class that tracks and provides status update messages on timing intervals
    /// for all data synchronization tasks. This includes the timing intervals for
    /// connecting to source and target databases to collect metadata, building and
    /// comparing the metadata analysis objects, and synchronization timeframes for
    /// both metadata and data for the source and target databases.
    /// </summary>
    public class DataTimer
    {
		#region Properties (2) 

        /// <summary>
        /// A list of exceptions that were thrown during CompareForSync() or Sync()
        /// methods. This list is cleared at the beginning of either call.
        /// </summary>
        public List<Exception> Exceptions { get; private set; }

        /// <summary>
        /// Friendly format advisor to display at the top of lists.
        /// </summary>
        public string StatusMessageFormat { get; private set; }

		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Constructor meant to be used internally within the Data assembly.
        /// </summary>
        public DataTimer()
        {
            Exceptions = new List<Exception>();
            StatusMessageFormat = "[ddd hh:mm:ss.mss] - Current Status.";
        }

		#endregion Constructors 

		#region Delegates and Events (1) 

		#region Events (1) 

        /// <summary>
        /// Event to subscribe to for receiving DataTimer status updates.
        /// </summary>
        public event DataTimerStatus TimerStatus;

		#endregion Events 

		#endregion Delegates and Events 

		#region Methods (2) 

		#region Public Methods (2) 

        /// <summary>
        /// Method for internal classes to call when they wish to publish a
        /// status message to any TimerStatus subscribers when starting a task.
        /// </summary>
        /// <param name="status">status message.</param>
        public void RaiseTimerStatusEvent(string status)
        {
            try
            {
                if (TimerStatus == null)
                    return;

                status = string.Format(
                    "[--- --:--:--.---] - {0}"
                    , status
                    );

                TimerStatus(status);
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

        /// <summary>
        /// Method for internal classes to call when they wish to publish a
        /// status message to any TimerStatus subscribers when finishing a task.
        /// </summary>
        /// <param name="interval">timing interval</param>
        /// <param name="status">status message.</param>
        public void RaiseTimerStatusEvent(TimeSpan interval, string status)
        {
            try
            {
                if (TimerStatus == null)
                    return;

                status = string.Format(
                    "[{0} {1}:{2}:{3}.{4}] - {5}"
                    , interval.Days.ToString("D3")
                    , interval.Hours.ToString("D2")
                    , interval.Minutes.ToString("D2")
                    , interval.Seconds.ToString("D2")
                    , interval.Milliseconds.ToString("D3")
                    , status
                    );

                TimerStatus(status);
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
