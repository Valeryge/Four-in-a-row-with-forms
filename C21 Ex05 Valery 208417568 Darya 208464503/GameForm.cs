using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameLogic;

namespace C21_Ex05_Valery_208417568_Darya_208464503
{
    public class GameForm : Form
    {
        private readonly SettingsForm m_SettingsForm = new SettingsForm();
        private readonly Label m_LabelPlayer1 = new Label();
        private readonly Label m_LabelPlayer2 = new Label();
        private readonly Label m_LabelPlayerScore1 = new Label();
        private readonly Label m_LabelPlayerScore2 = new Label();
        private readonly List<Button> m_ColumnButtons = new List<Button>();
        private Game m_Game;
        private Button[,] m_Buttons;

        public GameForm()
        {
            startGame();
        }

        private void startGame()
        {
            m_SettingsForm.FormClosed += new FormClosedEventHandler(settingsForm_FormClosed);
            m_SettingsForm.ShowDialog();
            initGameForm();
        }

        private void settingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void initGameForm()
        {
            int rows = m_SettingsForm.Rows;
            int columns = m_SettingsForm.Columns;
            int startLeft = 20;
            int startTop = 20;
            bool isAgainstComputer = !m_SettingsForm.IsTwoPlayers;

            m_Buttons = new Button[columns, rows];
            this.Size = new Size(columns * 40 + 50, rows * 60 + 50);
            this.StartPosition = FormStartPosition.CenterScreen;
            for (int i = 0; i < columns; i++)
            {
                Button columnButton = createButtonAtLocation(startLeft + 40 * i, startTop, 20, (i + 1).ToString());
                m_ColumnButtons.Add(columnButton);
                columnButton.Click += new EventHandler(columnButton_Click);
                for (int j = 0; j < rows; j++)
                {
                    m_Buttons[i, j] = createButtonAtLocation(startLeft + 40 * i, startTop + 40 * (j + 1), 30);
                } 
            }

            m_Game = new Game(columns, rows, isAgainstComputer);
            initScorePanel(rows);
        }

        void columnButton_Click(object sender, EventArgs e)
        {
            int colNum = int.Parse((sender as Button).Text);
            Board board = m_Game.MakeMove(colNum, out Board.MoveResponse moveResponse);

            printBoard(board);
            if (m_Game.winnerType != Game.WinnerType.None)
            {
                handleEndOfRound();
            }
           
            if (m_SettingsForm.IsTwoPlayers)
            {
                if (board.IsColumnFull(colNum))
                {
                    (sender as Button).Enabled = false;
                }
            } else
            {
                disableFullColumns();
            }
        }

        private void disableFullColumns()
        {
            foreach (Button button in m_ColumnButtons)
            {
                if (m_Game.Board.IsColumnFull(m_ColumnButtons.IndexOf(button) + 1))
                {
                    button.Enabled = false;
                }
            }
        }

        private void handleEndOfRound()
        {
            string message = "";

            switch (m_Game.winnerType)
            {
                case Game.WinnerType.Draw:
                    message = "Tie!!";
                    break;
                case Game.WinnerType.Player1:
                    message = string.Format("{0} won!", m_LabelPlayer1.Text.TrimEnd(':'));
                    m_LabelPlayerScore1.Text = (int.Parse(m_LabelPlayerScore1.Text) + 1).ToString();
                    break;
                case Game.WinnerType.Player2:
                    message = string.Format("{0} won!", m_LabelPlayer2.Text.TrimEnd(':'));
                    m_LabelPlayerScore2.Text = (int.Parse(m_LabelPlayerScore2.Text) + 1).ToString();
                    break;
            }

            if (MessageBox.Show(string.Format("{0}\nAnother round?", message), "Round Ended", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                startNewRound();
            } else
            {
                this.Close();
            }
        }

        private void startNewRound()
        {
            m_Game.Init();
            printBoard(m_Game.Board);
            foreach (Button button in m_ColumnButtons)
            {
                button.Enabled = true;
            }
        }

        private void printBoard(Board i_Board)
        {
            for (int i = 0; i < i_Board.Columns; i++)
            {
                for (int j = 0; j < i_Board.Rows; j++)
                {
                    m_Buttons[i, j].Text = getPlayersSign(m_Game.Board.GetCellPlayerType(i, j));
                }
            }
        }

        private string getPlayersSign(Player.PlayerType i_PlayerType)
        {
            string sign = "";

            switch (i_PlayerType)
            {
                case Player.PlayerType.Player1:
                    sign = "X";
                    break;
                case Player.PlayerType.Player2:
                    sign = "O";
                    break;
            }

            return sign;
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
            m_LabelPlayerScore1.Text = m_Game.GetFirstPlayerScore().ToString();
            m_LabelPlayer2.Text = m_SettingsForm.IsTwoPlayers ? m_SettingsForm.Player2 : "Computer";
            m_LabelPlayer2.Text += ":";
            m_LabelPlayer2.Location = new Point(m_LabelPlayerScore1.Right + 10, labelsTop);
            m_LabelPlayerScore2.Location = new Point(m_LabelPlayer2.Right, labelsTop);
            m_LabelPlayerScore2.Text = m_Game.GetSecondPlayerScore().ToString();
        }

        private Button createButtonAtLocation(int i_Left, int i_Top, int i_Height, string i_Text = "")
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

            return button;
        }
    }
}
