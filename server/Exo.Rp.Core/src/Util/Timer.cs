using System;
using System.Threading;

namespace Exo.Rp.Core.Util
{
    public class TimerHandler
    {
        // Use for example like: this.timer = new Util.TimerHandler(() =>{this.giveGo();}, 222);
        private Timer timer { get; set; }
        private Action callback { get; set; }

        public TimerHandler(Action callback, int milliseconds, int repeat = Timeout.Infinite)
        {
            this.callback = callback;

            timer = new Timer(
                new TimerCallback(TimerCallback),
                null,
                milliseconds,
                repeat);
        }

        public void StopTimeout()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            callback();
            timer.Change(
                Timeout.Infinite,
                Timeout.Infinite);
        }

    }
}
