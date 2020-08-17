using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace server.Util
{
    public class TimerHandler
    {

        // Use for example like: this.timer = new Util.TimerHandler(() =>{this.giveGo();}, 222);

        private Timer timer { get; set; }
        private Action callback { get; set; }

        public TimerHandler(Action callback, int milliseconds, int repeat = Timeout.Infinite)
        {
            this.callback = callback;

            this.timer = new Timer(
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
            this.callback();
            timer.Change(
                Timeout.Infinite,
                Timeout.Infinite);
        }

    }
}
