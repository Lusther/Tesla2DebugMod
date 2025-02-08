using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tesla2DebugMod
{
    internal class Chrono
    {
        private Stopwatch stopWatch;

        public Chrono() {
            stopWatch = new Stopwatch();
        }

        public void Start()
        {
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
        }

        public void Reset()
        {
            stopWatch.Reset();
        }
        
        public string GetTime()
        {
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedTime;
        }
    }
}
