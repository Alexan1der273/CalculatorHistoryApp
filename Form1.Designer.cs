namespace CalculatorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем элементы управления (доступны из Form1.cs)
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Button[] digitButtons;
        private System.Windows.Forms.Button[] operatorButtons;
        private System.Windows.Forms.Button btnEquals, btnClear, btnBackspace, btnSign;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Калькулятор с историей";
            this.Size = new System.Drawing.Size(320, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Поле результата
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtResult.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtResult.Location = new System.Drawing.Point(12, 12);
            this.txtResult.Size = new System.Drawing.Size(280, 39);
            this.txtResult.ReadOnly = true;
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtResult.Text = "0";

            // Список истории
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.lstHistory.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstHistory.Location = new System.Drawing.Point(12, 220);
            this.lstHistory.Size = new System.Drawing.Size(280, 150);
            this.lstHistory.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Таблица для кнопок
            System.Windows.Forms.TableLayoutPanel tlp = new System.Windows.Forms.TableLayoutPanel();
            tlp.Location = new System.Drawing.Point(12, 57);
            tlp.Size = new System.Drawing.Size(280, 150);
            tlp.ColumnCount = 5;
            tlp.RowCount = 4;
            tlp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            for (int i = 0; i < 5; i++)
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            for (int i = 0; i < 4; i++)
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));

            // Создание кнопок
            string[] digitLabels = { "7", "8", "9", "4", "5", "6", "1", "2", "3", "0", "." };
            this.digitButtons = new System.Windows.Forms.Button[digitLabels.Length];
            for (int i = 0; i < digitLabels.Length; i++)
            {
                var btn = new System.Windows.Forms.Button();
                btn.Text = digitLabels[i];
                btn.Dock = System.Windows.Forms.DockStyle.Fill;
                btn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                btn.Click += new System.EventHandler(this.ButtonDigit_Click);
                this.digitButtons[i] = btn;
            }

            string[] opLabels = { "/", "*", "-", "+", "%" };
            this.operatorButtons = new System.Windows.Forms.Button[opLabels.Length];
            for (int i = 0; i < opLabels.Length; i++)
            {
                var btn = new System.Windows.Forms.Button();
                btn.Text = opLabels[i];
                btn.Dock = System.Windows.Forms.DockStyle.Fill;
                btn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                btn.Click += new System.EventHandler(this.ButtonOperator_Click);
                this.operatorButtons[i] = btn;
            }

            this.btnEquals = new System.Windows.Forms.Button();
            this.btnEquals.Text = "=";
            this.btnEquals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEquals.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnEquals.Click += new System.EventHandler(this.ButtonEquals_Click);

            this.btnClear = new System.Windows.Forms.Button();
            this.btnClear.Text = "C";
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClear.Click += new System.EventHandler(this.ButtonClear_Click);

            this.btnBackspace = new System.Windows.Forms.Button();
            this.btnBackspace.Text = "⌫";
            this.btnBackspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBackspace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackspace.Click += new System.EventHandler(this.ButtonBackspace_Click);

            this.btnSign = new System.Windows.Forms.Button();
            this.btnSign.Text = "±";
            this.btnSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSign.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSign.Click += new System.EventHandler(this.ButtonSign_Click);

            // Размещение в таблице
            // Ряд 0
            tlp.Controls.Add(this.digitButtons[0], 0, 0); // 7
            tlp.Controls.Add(this.digitButtons[1], 1, 0); // 8
            tlp.Controls.Add(this.digitButtons[2], 2, 0); // 9
            tlp.Controls.Add(this.operatorButtons[0], 3, 0); // /
            tlp.Controls.Add(this.btnClear, 4, 0);
            // Ряд 1
            tlp.Controls.Add(this.digitButtons[3], 0, 1); // 4
            tlp.Controls.Add(this.digitButtons[4], 1, 1); // 5
            tlp.Controls.Add(this.digitButtons[5], 2, 1); // 6
            tlp.Controls.Add(this.operatorButtons[1], 3, 1); // *
            tlp.Controls.Add(this.btnBackspace, 4, 1);
            // Ряд 2
            tlp.Controls.Add(this.digitButtons[6], 0, 2); // 1
            tlp.Controls.Add(this.digitButtons[7], 1, 2); // 2
            tlp.Controls.Add(this.digitButtons[8], 2, 2); // 3
            tlp.Controls.Add(this.operatorButtons[2], 3, 2); // -
            tlp.Controls.Add(this.operatorButtons[4], 4, 2); // %
            // Ряд 3
            tlp.Controls.Add(this.digitButtons[9], 0, 3); // 0
            tlp.Controls.Add(this.digitButtons[10], 1, 3); // .
            tlp.Controls.Add(this.btnEquals, 2, 3);
            tlp.Controls.Add(this.operatorButtons[3], 3, 3); // +
            tlp.Controls.Add(this.btnSign, 4, 3);

            // Добавляем всё на форму
            this.Controls.Add(tlp);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.txtResult);
        }
    }
}