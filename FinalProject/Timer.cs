using System;
using System.Threading;
using Android.OS;

namespace FinalProject
{
    public class Timer
    {
        private TimerHandler timerHandler;
        private int counter;
        BoardGame.Turn turn;
        public Timer(TimerHandler timerHandler, int counter, BoardGame.Turn turn)
        {
            this.turn = turn;
            this.timerHandler = timerHandler;
            this.counter = counter;
        }

        public void Run()
        {
            while (GameActivity.boardGame.turn == turn)
            {
                counter++;
                Thread.Sleep(1000);
                Message m = new Message();
                m.Arg1 = counter;
                timerHandler.HandleMessage(m);
            }
        }
        public void Start()
        {
            ThreadStart startThread = new ThreadStart(() => Run());
            Thread t = new Thread(startThread);
            t.Start();
        }

        public bool IsOver(int limit)
        {
            return counter >= 60 * limit;
        }
    }
}
