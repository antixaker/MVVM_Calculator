using System;
using Xamarin.Forms;

namespace Calculator
{
    public partial class CalculatorView : ContentPage
    {
        //IInputManager displayManager;
        //ICaclProcessor mathProcessor;

        public CalculatorView()
        {
            InitializeComponent();

            //displayManager = new InputManager();
            //mathProcessor = new CaclProcessor();
        }

        //void OnButtonClicked(object sender, EventArgs e)
        //{
        //    Button tmp = sender as Button;
        //    if (tmp == null)
        //        return;

        //    Display.Text = displayManager.AddSymbol(Display.Text, tmp.Text);
        //}

        //void OnResetClicked(object sender, EventArgs e)
        //{
        //    Button tmp = sender as Button;
        //    if (tmp == null)
        //        return;

        //    Display.Text = displayManager.ClearAll();
        //}

        //void OnResultClicked(object sender, EventArgs e)
        //{
        //    Button tmp = sender as Button;
        //    if (tmp == null)
        //        return;

        //    var res = mathProcessor.Calculate(Display.Text);
        //    if (res != null)
        //        Display.Text = res.ToString();
        //}

        //void OnRemoveLastSymbolClicked(object sender, EventArgs e)
        //{
        //    Button tmp = sender as Button;
        //    if (tmp == null)
        //        return;

        //    Display.Text = displayManager.RemoveLastSymbol(Display.Text);
        //}
    }
}