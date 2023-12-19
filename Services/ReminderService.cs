namespace Q1Q2Q4.Services
{
 public class ReminderService : IReminderService
    {
        public void SendReminder()
        {
			try
			{
                Console.WriteLine("Please make payment" + DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
			{
                //By default hangfire retries jobs 10 times

                throw new JobFailedException("Job failed", ex);
            }
        }

        public class JobFailedException : Exception
        {
            public JobFailedException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}
