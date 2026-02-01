using System;
using System.Windows.Forms;

namespace _67Calculator
{
    public partial class Form1 : Form
    {
        private string currentInput = "";
        private string lastOperator = "";
        private double lastValue = 0;
        private string expression = ""; // For showing full expression in label
        private bool isResultShown = false; // flag for result displayed

        public Form1()
        {
            InitializeComponent();
            WireUpButtons();
            label1.Text = "0";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        private void WireUpButtons()
        {
            // Numbers
            Btn0.Click += BtnNumber_Click;
            Btn1.Click += BtnNumber_Click;
            Btn2.Click += BtnNumber_Click;
            Btn3.Click += BtnNumber_Click;
            Btn4.Click += BtnNumber_Click;
            Btn5.Click += BtnNumber_Click;
            Btn6.Click += BtnNumber_Click;
            Btn7.Click += BtnNumber_Click;
            Btn8.Click += BtnNumber_Click;
            Btn9.Click += BtnNumber_Click;
            BtnDecimal.Click += BtnNumber_Click;

            // Operators
            BtnAddition.Click += BtnOperator_Click;
            BtnSubtraction.Click += BtnOperator_Click;
            BtnMultiplication.Click += BtnOperator_Click;
            BtnDivision.Click += BtnOperator_Click;

            // Specials
            BtnClear.Click += BtnClear_Click;
            BtnBackspace.Click += BtnBackspace_Click;
            BtnEquals.Click += BtnEquals_Click;
            BtnPositiveNegative.Click += BtnPositiveNegative_Click;
        }

        // Number click
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            RoundedButton btn = sender as RoundedButton;

            // If result was just shown, start new input
            if (isResultShown)
            {
                currentInput = "";
                expression = "";
                isResultShown = false;
            }

            // Avoid multiple decimals
            if (btn.Text == "." && currentInput.Contains("."))
                return;

            currentInput += btn.Text;
            UpdateLabel();
        }

        // Operator click
        private void BtnOperator_Click(object sender, EventArgs e)
        {
            RoundedButton btn = sender as RoundedButton;

            // If result is currently shown and no current input, continue using lastValue
            if (isResultShown && string.IsNullOrEmpty(currentInput))
            {
                isResultShown = false;
            }

            if (!string.IsNullOrEmpty(currentInput))
            {
                double num;
                if (double.TryParse(currentInput, out num))
                {
                    if (!string.IsNullOrEmpty(lastOperator))
                    {
                        lastValue = Operate(lastValue, num, lastOperator);
                    }
                    else
                    {
                        lastValue = num;
                    }
                }
            }

            lastOperator = btn.Text;
            expression = lastValue.ToString() + " " + lastOperator;
            currentInput = "";
            UpdateLabel();
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentInput) && string.IsNullOrEmpty(lastOperator)) return;

            Calculate();

            // Show only the result after equals
            expression = "";
            currentInput = lastValue.ToString();
            label1.Text = currentInput;

            isResultShown = true; // mark that result is displayed
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            currentInput = "";
            lastOperator = "";
            lastValue = 0;
            expression = "";
            isResultShown = false;
            label1.Text = "0";
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                UpdateLabel();
            }
        }

        private void BtnPositiveNegative_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                if (currentInput.StartsWith("-"))
                    currentInput = currentInput.Substring(1);
                else
                    currentInput = "-" + currentInput;

                UpdateLabel();
            }
        }

        private void Calculate()
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                if (double.TryParse(currentInput, out double num))
                {
                    lastValue = Operate(lastValue, num, lastOperator);

                    // Show only the result
                    currentInput = lastValue.ToString();
                    label1.Text = currentInput;

                    lastOperator = "";

                    // Easter egg: trigger only on exactly 67 or -67
                    if (lastValue == 67 || lastValue == -67)
                    {
                        EasterEggForm egg = new EasterEggForm();
                        egg.ShowDialog();
                    }
                }
            }
        }

        private void UpdateLabel()
        {
            if (!string.IsNullOrEmpty(expression))
                label1.Text = expression + " " + currentInput;
            else
                label1.Text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
        }

        private double Operate(double a, double b, string op)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "x": return a * b;
                case "÷": return b != 0 ? a / b : 0;
                default: return b;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
