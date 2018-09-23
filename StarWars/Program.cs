using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWars
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form game_form = new Form()
            {
                Width = 800,
                Height = 600,
                FormBorderStyle = FormBorderStyle.FixedSingle
            };

            Game.Load();
            Game.Init(game_form);

            game_form.Show();

            Game.Draw();

            Application.Run(game_form);
        }
    }
}
