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

    }
}
