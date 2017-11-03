using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormCommand.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implmentation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var changed = this.PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        #region Command interface

        public ICommand SquareRootCommand { get; private set; }
        public ICommand SquareRootWithParameterCommand { get; private set; }
        public ICommand SimulateDownloadCommand { get; private set; }

        #endregion


        #region ViewModel Properties

        public int Number { get; set; }

        public double SquareRootResult { get; private set; }
        public double SquareRootWithParameterResult { get; private set; }

        #endregion

        public MyViewModel()
        {
            Number = 25;
            SquareRootCommand = new Command(CalculateSquerRoot);
            SquareRootWithParameterCommand = new Command<string>(CalculateSquerRoot);
            SimulateDownloadCommand = new Command(async () => await SimulateDownloadAsync(), () => canDownload);
        }


        #region Command Actions

        private void CalculateSquerRoot()
        {
            SquareRootResult = Math.Sqrt(Number);
            OnPropertyChanged("SquareRootResult");
        }

        private void CalculateSquerRoot(string value)
        {
            double num = Convert.ToDouble(value);
            SquareRootWithParameterResult = Math.Sqrt(num);
            OnPropertyChanged("SquareRootWithParameterResult");
        }

        #endregion

        #region Async Command Demo
        bool canDownload = true;
        string simulatedDownloadResult;

        public string SimulatedDownloadResult
        {
            get { return simulatedDownloadResult; }
            private set
            {
                if (simulatedDownloadResult != value)
                {
                    simulatedDownloadResult = value;
                    OnPropertyChanged("SimulatedDownloadResult");
                }
            }
        }

        async Task SimulateDownloadAsync()
        {
            CanInitiateNewDownload(false);
            SimulatedDownloadResult = string.Empty;
            await Task.Run(() => SimulateDownload());
            SimulatedDownloadResult = "Simulated download complete";
            CanInitiateNewDownload(true);
        }

        void CanInitiateNewDownload(bool value)
        {
            canDownload = value;
            ((Command)SimulateDownloadCommand).ChangeCanExecute();
        }

        void SimulateDownload()
        {
            // Simulate a 5 second pause
            var endTime = DateTime.Now.AddSeconds(5);
            while (true)
            {
                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }
        }

        #endregion

    }
}
