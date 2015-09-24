namespace Calculator
{
    interface IInputManager
    {
        string AddSymbol(string valueToChange, string symbolToAdd);
        string ClearAll();
        string RemoveLastSymbol(string value);
    }
}