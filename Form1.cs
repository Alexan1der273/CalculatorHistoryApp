using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private string currentExpression = "";
        private List<HistoryItem> historyList = new List<HistoryItem>();
        private readonly string historyFilePath = "history.json";

        public Form1()
        {
            InitializeComponent(); // Вызов метода из Form1.Designer.cs
            LoadHistory();
        }

        // ---------- Работа с историей ----------
        private void AddToHistory(string expression, string result)
        {
            historyList.Add(new HistoryItem
            {
                Expression = expression,
                Result = result,
                Timestamp = DateTime.Now
            });
            SaveHistory();
            UpdateHistoryDisplay();
        }

        private void SaveHistory()
        {
            try
            {
                string json = JsonConvert.SerializeObject(historyList, Formatting.Indented);
                File.WriteAllText(historyFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения истории: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistory()
        {
            if (File.Exists(historyFilePath))
            {
                try
                {
                    string json = File.ReadAllText(historyFilePath);
                    historyList = JsonConvert.DeserializeObject<List<HistoryItem>>(json) ?? new List<HistoryItem>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    historyList = new List<HistoryItem>();
                }
            }
            UpdateHistoryDisplay();
        }

        private void UpdateHistoryDisplay()
        {
            lstHistory.Items.Clear();
            foreach (var item in historyList)
            {
                lstHistory.Items.Add($"{item.Expression} = {item.Result}  [{item.Timestamp:yyyy-MM-dd HH:mm:ss}]");
            }
        }

        // ---------- Вычисления ----------
        private string Calculate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return "";

            string exprForCompute = expression.Replace("%", " Mod ");
            try
            {
                var result = new DataTable().Compute(exprForCompute, null);
                return result.ToString();
            }
            catch (DivideByZeroException)
            {
                return "Ошибка: деление на ноль";
            }
            catch (Exception)
            {
                return "Ошибка";
            }
        }

        // ---------- Логика ввода ----------
        private void AppendToExpression(string text)
        {
            if (txtResult.Text.StartsWith("Ошибка"))
                ClearExpression();

            if (text == ".")
            {
                string[] parts = currentExpression.Split('+', '-', '*', '/', '%');
                string lastNumber = parts[parts.Length - 1];
                if (lastNumber.Contains("."))
                    return;
            }

            if (IsOperator(text))
            {
                if (string.IsNullOrEmpty(currentExpression))
                {
                    if (text == "-")
                        currentExpression += text;
                    else
                        return;
                }
                else
                {
                    char lastChar = currentExpression[currentExpression.Length - 1];
                    if (IsOperator(lastChar.ToString()))
                        currentExpression = currentExpression.Remove(currentExpression.Length - 1) + text;
                    else
                        currentExpression += text;
                }
            }
            else
            {
                currentExpression += text;
            }
            txtResult.Text = currentExpression;
        }

        private bool IsOperator(string s) => s == "+" || s == "-" || s == "*" || s == "/" || s == "%";

        private void ClearExpression()
        {
            currentExpression = "";
            txtResult.Text = "0";
        }

        private void Backspace()
        {
            if (txtResult.Text.StartsWith("Ошибка"))
            {
                ClearExpression();
                return;
            }
            if (currentExpression.Length > 0)
                currentExpression = currentExpression.Remove(currentExpression.Length - 1);
            txtResult.Text = string.IsNullOrEmpty(currentExpression) ? "0" : currentExpression;
        }

        private void ToggleSign()
        {
            if (string.IsNullOrEmpty(currentExpression)) return;

            int lastOpIndex = -1;
            for (int i = currentExpression.Length - 1; i >= 0; i--)
            {
                char c = currentExpression[i];
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%')
                {
                    lastOpIndex = i;
                    break;
                }
            }
            string lastNumber = currentExpression.Substring(lastOpIndex + 1);
            if (double.TryParse(lastNumber, out double num))
            {
                num = -num;
                string newLast = num.ToString();
                currentExpression = currentExpression.Substring(0, lastOpIndex + 1) + newLast;
                txtResult.Text = currentExpression;
            }
        }

        private void HandleEquals()
        {
            if (string.IsNullOrEmpty(currentExpression))
                return;

            string result = Calculate(currentExpression);
            if (result == "Ошибка: деление на ноль" || result == "Ошибка")
            {
                txtResult.Text = result;
                currentExpression = "";
                return;
            }

            AddToHistory(currentExpression, result);
            txtResult.Text = result;
            currentExpression = result;
        }

        // ---------- Обработчики событий ----------
        private void ButtonDigit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            AppendToExpression(btn.Text);
        }

        private void ButtonOperator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            AppendToExpression(btn.Text);
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            HandleEquals();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearExpression();
        }

        private void ButtonBackspace_Click(object sender, EventArgs e)
        {
            Backspace();
        }

        private void ButtonSign_Click(object sender, EventArgs e)
        {
            ToggleSign();
        }
    }

    public class HistoryItem
    {
        public string Expression { get; set; }
        public string Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}