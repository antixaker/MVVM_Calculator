using System;

namespace Calculator
{
    class InputManager : IInputManager
    {
        readonly char[] actionSymbols = { '+', '-', '/', '*' };

        public string AddSymbol(string valueToChange, string symbolToAdd)
        {
            //for first button press
            if (string.IsNullOrEmpty(valueToChange))
            {
                if (char.IsDigit(symbolToAdd[0]))
                    return symbolToAdd;
                else
                    return string.Empty;
            }

            //constraint for input only two numbers and action symbol
            if (!char.IsDigit(symbolToAdd[0]) && symbolToAdd != "." && CommandIsFull(valueToChange))
                return valueToChange;
            char lastSymbol = GetLastSymbol(valueToChange);
            //check point enter
            if (symbolToAdd == "." && !CanAddPoint(valueToChange, lastSymbol))
                return valueToChange;
            //check re-enter mathoperator
            else if (!Char.IsDigit(symbolToAdd[0]) && !CanAddMath(valueToChange, lastSymbol))
                return ReplaceLastSymbol(valueToChange, symbolToAdd);
            return valueToChange += symbolToAdd;
        }

        public string RemoveLastSymbol(string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Remove(value.Length - 1, 1);
        }

        public string ClearAll()
        {
            return String.Empty;
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