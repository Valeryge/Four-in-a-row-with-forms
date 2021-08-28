using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C21_Ex05_Valery_208417568_Darya_208464503
{
    public class UserInterface
    {
        private readonly GameForm m_GameForm = new GameForm();

        public UserInterface() { }

        public void StartGame()
        {
            m_GameForm.ShowDialog();
        }
    }
}
