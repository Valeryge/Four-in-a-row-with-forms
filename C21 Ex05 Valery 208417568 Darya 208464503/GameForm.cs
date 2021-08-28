using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
//using GameLogic;

namespace C21_Ex05_Valery_208417568_Darya_208464503
{
    public class GameForm : Form
    {
        private readonly SettingsForm m_SettingsForm = new SettingsForm();
        private readonly Label m_LabelPlayer1 = new Label();
        private readonly Label m_LabelPlayer2 = new Label();
        private readonly Label m_LabelPlayerScore1 = new Label();
        private readonly Label m_LabelPlayerScore2 = new Label();
       // private Game m_Game;

        public GameForm()
        {
            m_SettingsForm.ShowDialog();
            initGameForm();
        }

        private void initGameForm()
        {
            int rows = m_SettingsForm.Rows;
            int columns = m_SettingsForm.Columns;
            int startLeft = 20;
            int startTop = 20;
            bool isAgainstComputer = !m_SettingsForm.IsTwoPlayers;

            this.Size = new Size(columns * 40 + 50, rows * 60 + 50);
            for (int i = 0; i < columns; i++)
            {
                createButtonAtLocation(startLeft + 40 * i, startTop, 20, (i + 1).ToString());
                for (int j = 0; j < rows; j++)
                {
                    createButtonAtLocation(startLeft + 40 * i, startTop + 40 * (j + 1), 30);
                } 
            }

            initScorePanel(rows);
          //  m_Game = new Game(columns, rows, isAgainstComputer);
        }

        private void initScorePanel(int i_Rows)
        {
            this.Controls.AddRange(new Control[] { m_LabelPlayer1, m_LabelPlayer2, m_LabelPlayerScore1, m_LabelPlayerScore2 });
            int labelsTop = i_Rows * 40 + 60;
            m_LabelPlayer1.Text = m_SettingsForm.Player1 + ":";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayerScore1.AutoSize = true;
            m_LabelPlayerScore2.AutoSize = true;
            m_LabelPlayer1.Location = new Point(20, labelsTop);
            m_LabelPlayerScore1.Location = new Point(m_LabelPlayer1.Right, labelsTop);
          //  m_LabelPlayerScore1.Text = m_Game.GetFirstPlayerScore().ToString();
            m_LabelPlayer2.Text = m_SettingsForm.IsTwoPlayers ? m_SettingsForm.Player2 : "Computer";
            m_LabelPlayer2.Text += ":";
            m_LabelPlayer2.Location = new Point(m_LabelPlayerScore1.Right + 10, labelsTop);
            m_LabelPlayerScore2.Location = new Point(m_LabelPlayer2.Right, labelsTop);
           // m_LabelPlayerScore2.Text = m_Game.GetSecondPlayerScore().ToString();
        }

        private void createButtonAtLocation(int i_Left, int i_Top, int i_Height, string i_Text = "")
        {
            Button button = new Button();

            if (i_Text != "")
            {
                button.Text = i_Text;
            }

            button.Height = i_Height;
            button.Location = new Point(i_Left, i_Top);
            button.Width = 30;
            this.Controls.Add(button);
        }


    }
}
