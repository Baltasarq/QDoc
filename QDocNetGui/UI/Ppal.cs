// QDocNet - (c) 2018 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetGui.UI {
    using System.Windows.Forms;

    class Ppal
    {
        [System.STAThread]
        static void Main()
        {
            Application.Run( new MainFormController().Form );
        }
    }
}
