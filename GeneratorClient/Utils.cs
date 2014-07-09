using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace GeneratorClient
{
    public static class Utils
    {
        public static System.Windows.Forms.SaveFileDialog createSaveDialog(String title, String defaultExtension)
        {
            var saveDialog = new System.Windows.Forms.SaveFileDialog();
            saveDialog.CheckFileExists = false;
            saveDialog.CheckPathExists = true;
            saveDialog.DereferenceLinks = true;
            saveDialog.OverwritePrompt = true;
            saveDialog.ValidateNames = true;
            saveDialog.Title = title;
            saveDialog.AddExtension = true;
            saveDialog.DefaultExt = defaultExtension;
            return saveDialog;
        }

        public class Timer : INotifyPropertyChanged
        {
            private TimeSpan _countdown = TimeSpan.Zero;
            public TimeSpan Countdown
            {
                get
                {
                    return _countdown;
                }

                set
                {
                    if (value != _countdown)
                    {
                        _countdown = value;
                        OnPropertyChanged("Countdown");
                    }
                }
            }

            private TimeSpan duration;
            private DispatcherTimer timer;

            public Timer(TimeSpan duration)
            {
                this.duration = duration;
                Countdown = duration;

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(timer_Tick);
            }

            public void Start()
            {
                timer.Start();
            }

            public void Stop()
            {
                timer.Stop();
            }

            public void Restart()
            {
                Countdown = duration;
                if (!timer.IsEnabled)
                    timer.Start();
            }

            void timer_Tick(object sender, EventArgs e)
            {
                Countdown = Countdown.Subtract(TimeSpan.FromSeconds(1));

                if (Countdown == TimeSpan.Zero)
                {
                    timer.Stop();
                    NotifyCountdownEnd();
                }
            }

            public event EventHandler CountdownEnd;
            private void NotifyCountdownEnd()
            {
                EventHandler handler = CountdownEnd;
                if (null != handler)
                {
                    handler(this, new EventArgs());
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }

        }
    }
}

