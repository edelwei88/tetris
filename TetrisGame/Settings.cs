using System.Drawing;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Settings : Form
    {
        //цветовые схемы
        public Color[][] colorScheme = new Color[2][]
        {
            new Color[]
            {
                Color.LightSkyBlue,
                Color.Bisque,
                Color.LightPink,
                Color.LightBlue,
                Color.Orange,
                Color.LightGreen,
                Color.Purple
            },
            new Color[]
            {
                Color.DarkBlue,
                Color.DarkGoldenrod,
                Color.Fuchsia,
                Color.DarkOrange,
                Color.DarkGreen,
                Color.DarkRed,
                Color.DarkTurquoise
            }
        };

        //свойство цветовой схемы
        public Color[] SetColor
        {
            get => colorScheme[comboBox1.SelectedIndex];
        }

        //свойство музыки
        public bool Music
        {
            get => checkBox1.Checked;
        }

        public Settings()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
    }
}
