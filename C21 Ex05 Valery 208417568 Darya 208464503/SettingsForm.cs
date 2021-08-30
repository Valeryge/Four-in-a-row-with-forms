using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace C21_Ex05_Valery_208417568_Darya_208464503
{
    class SettingsForm : Form
    {
        TextBox m_TextboxPlayer1 = new TextBox();
        TextBox m_TextboxPlayer2 = new TextBox();
        NumericUpDown m_NumericRows = new NumericUpDown();
        NumericUpDown m_NumericCols = new NumericUpDown();
        Label m_LabelPlayers = new Label();
        Label m_LabelPlayer1 = new Label();
        Label m_LabelPlayer2 = new Label();
        Label m_LabelBoardSize = new Label();
        Label m_LabelRows = new Label();
        Label m_LabelCols = new Label();
        Button m_ButtonStart = new Button();
        CheckBox m_CheckBoxPlayer2 = new CheckBox();

        public string Player1
        {
            get { return m_TextboxPlayer1.Text; }
        }

        public string Player2
        {
            get { return m_TextboxPlayer2.Text; }
        }

        public bool IsTwoPlayers
        {
            get { return m_CheckBoxPlayer2.Checked; }
        }

        public int Columns
        {
            get { return (int)m_NumericCols.Value; }
        }

        public int Rows
        {
            get { return (int)m_NumericRows.Value; }
        }

        public SettingsForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "4 in a row - Settings";
            this.BackColor = Color.AliceBlue;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            int leftLocationX = 10;
            int tiltedLocationX = 20;

            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new Point(leftLocationX, 20);

            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Location = new Point(tiltedLocationX, 50);
            m_TextboxPlayer1.Location = new Point(m_LabelPlayer1.Right, 50);

            int secondPlayerY = m_LabelPlayer1.Top + 30;
            m_CheckBoxPlayer2.Location = new Point(tiltedLocationX, secondPlayerY - 5);
            m_LabelPlayer2.Text = "Player 2:";
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Location = new Point(tiltedLocationX + 15, secondPlayerY);
            m_TextboxPlayer2.Enabled = false;
            m_TextboxPlayer2.Location = new Point(m_TextboxPlayer1.Left, secondPlayerY);

            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(leftLocationX, m_LabelPlayer2.Bottom + 30);

            int rowsColsY = m_LabelBoardSize.Top + 30;
            m_LabelRows.Text = "Rows:";
            m_LabelRows.AutoSize = true;
            m_LabelRows.Location = new Point(tiltedLocationX, rowsColsY);
            m_LabelRows.Width = 40;
            m_NumericRows.Minimum = 4;
            m_NumericRows.Maximum = 10;
            m_NumericRows.Width = 35;
            m_NumericRows.Location = new Point(m_LabelRows.Right, rowsColsY);

            m_NumericCols.Minimum = 4;
            m_NumericCols.Width = 35;
            m_NumericCols.Maximum = 10;
            m_NumericCols.Top = rowsColsY;
            m_NumericCols.Left = m_TextboxPlayer2.Right - 40;
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Location = new Point(m_NumericCols.Left - 35, rowsColsY);
            m_LabelCols.AutoSize = true;

            m_ButtonStart.Location = new Point(leftLocationX, rowsColsY + 30);
            m_ButtonStart.Text = "Start!";

            this.Controls.AddRange(new Control[] { m_LabelPlayers, m_LabelPlayer1, m_LabelPlayer2,
                m_CheckBoxPlayer2, m_TextboxPlayer1, m_TextboxPlayer2, m_LabelRows, m_NumericCols,
                m_NumericRows, m_LabelCols, m_LabelBoardSize, m_ButtonStart });

            this.m_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
            this.m_CheckBoxPlayer2.CheckedChanged += new EventHandler(m_CheckBoxPlayer2_Click);

            this.Width = m_TextboxPlayer1.Right + 40;
            m_ButtonStart.Width = this.Width - 40;
            this.Height = m_ButtonStart.Bottom + 50;
        }

        void m_ButtonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void m_CheckBoxPlayer2_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                m_TextboxPlayer2.Enabled = true;
            }
            else
            {
                m_TextboxPlayer2.Enabled = false;
            }
        }
    }
}
