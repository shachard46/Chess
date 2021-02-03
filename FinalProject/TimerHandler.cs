using System;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.App;

namespace FinalProject
{
    public class TimerHandler : Handler
    {
        TextView text;
        Activity context;
        public TimerHandler(TextView text, Activity context)
        {
            this.context = context;
            this.text = text;
        }
        public override void HandleMessage(Message msg)
        {
            context.RunOnUiThread(() =>
            {
                if (msg.Arg1 / 60 < 10 && msg.Arg1 % 60 < 10)
                    text.Text = string.Format("0{0}:0{1}", msg.Arg1 / 60, msg.Arg1 % 60);
                else if (msg.Arg1 / 60 < 10)
                    text.Text = string.Format("0{0}:{1}", msg.Arg1 / 60, msg.Arg1 % 60);
                else if (msg.Arg1 % 60 < 10)
                    text.Text = string.Format("{0}:0{1}", msg.Arg1 / 60, msg.Arg1 % 60);
                else
                    text.Text = string.Format("{0}:{1}", msg.Arg1 / 60, msg.Arg1 % 60);
            });
        }

    }
}
