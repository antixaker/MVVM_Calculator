using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModel
{
    class CalcViewModel : INotifyPropertyChanged
    {
        readonly char[] actionSymbols = { '+', '-', '/', '*' };

        readonly ICalculatorModel _model;

        string displayString = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public CalcViewModel()
        {
            _model = new CalculatorModel();

            this.AddSymbolCommand = new Command<string>(symbol =>
            {
                //for first button press
                if (string.IsNullOrEmpty(displayString))
                {
                    if (char.IsDigit(symbol[0]))
                    {
                        DisplayString += symbol;
                        return;
                    }
                    else
                        return;

                }
                //constraint for input only two numbers and action symbol
                if (!char.IsDigit(symbol[0]) && symbol != "." && CommandIsFull(displayString))
                    return;

                char lastSymbol = GetLastSymbol(displayString);
                //check point enter
                if (symbol == "." && !CanAddPoint(displayString, lastSymbol))
                    return;

                //check re-enter mathoperator
                else if (!Char.IsDigit(symbol[0]) && !CanAddMath(displayString, lastSymbol))
                {
                    DisplayString = ReplaceLastSymbol(displayString, symbol);
                    return;
                }

                DisplayString += symbol;

            });

            this.RemoveLastSymbolCommand = new Command(obj =>
            {
                if (!string.IsNullOrEmpty(displayString))
                    this.DisplayString = displayString.Substring(0, displayString.Length - 1);
            });

            this.ClearAllCommand = new Command(obj => { this.DisplayString = string.Empty; });

            this.CalculateCommand = new Command(obj =>
           {
               var result = _model.Calculate(displayString);
               if (result != null)
                   DisplayString = result.ToString();
           });
        }

        public string DisplayString
        {
            protected set
            {
                if (displayString != value)
                {
                    displayString = value;
                    OnPropertyChanged("DisplayString");
                }
            }

            get { return displayString; }
        }

        public ICommand AddSymbolCommand { protected set; get; }

        public ICommand RemoveLastSymbolCommand { protected set; get; }

        public ICommand ClearAllCommand { protected set; get; }

        public ICommand CalculateCommand { protected set; get; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Servise methods
        string ReplaceLastSymbol(string valueToChange, string symbolToReplace)
        {
            return valueToChange.Substring(0, valueToChange.Length - 1) + symbolToReplace;
        }

        bool CommandIsFull(string valueToTest)
        {
            if (valueToTest.Split(actionSymbols, StringSplitOptions.RemoveEmptyEntries).Length == 2)
                return true;
            else
                return false;
        }

        bool CanAddPoint(string currentDisplayValue, char lastSymbol)
        {
            if (string.IsNullOrEmpty(lastSymbol.ToString()) || !Char.IsDigit(lastSymbol))
                return false;
            string[] arr = StringSplit(currentDisplayValue, actionSymbols);
            string last = arr[arr.Length - 1];
            if (last.Contains("."))
                return false;
            return true;
        }

        bool CanAddMath(string currentDisplayValue, char lastSymbol)
        {
            return (!string.IsNullOrEmpty(lastSymbol.ToString()) && Char.IsDigit(lastSymbol) && currentDisplayValue[currentDisplayValue.Length - 1] != '.');
        }

        char GetLastSymbol(string value)
        {
            return value[value.Length - 1];
        }

        string[] StringSplit(string stringToSplit, char[] separatorArray)
        {
            return stringToSplit.Split(separatorArray);
        }
        #endregion

    }
}
