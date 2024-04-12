using System.Timers;
using Timer = System.Timers.Timer;

namespace webapi
{
    public class RequestTimer
    {
        private Boolean isUpdated = false;
        private int secondsToUpdate;
        private System.Timers.Timer? timer;
        private Object item;

        public List<IListeners> listeners { get; set; }

        public RequestTimer(int seconds) 
        {
            secondsToUpdate = seconds * 1000;
            listeners = new List<IListeners>();
            item = new object();
        }

        public void startTimer(Object item)
        {
            this.item = item;
            timer = new Timer(secondsToUpdate);
            timer.Elapsed += TimeElapsed;
            timer.Start();            
        }

        public void stopTimer() 
        {
            if(timer != null)
            {
                timer.Stop();
            }            
            timer = null;            
        }

        public void RequestReceived()
        {
            isUpdated = true;
            foreach (var listener in listeners)
            {
                listener.Update();
            }
        }

        public void TimeElapsed(object sender, ElapsedEventArgs e)
        {            
            foreach (var listener in listeners)
            {
                listener.Reset(item);
            }
        }

        public Boolean IsUpdated()
        {
            return isUpdated;
        }
    }
}
