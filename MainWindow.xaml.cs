using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_Game
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startGame();
        }

      private void startGame()
        {
            List<string> memorycards = new List<string>() {
    "😊","😊","❤","❤","🌹","🌹",
    "🐉","🐉",
    "🐱","🐱",
    "🚀","🚀",
    "👽","👽","🧠","🧠"
};  //"👁","👁","👵","👵","🎈","🎈","🎨","🎨","🎪","🎪","⚽","⚽","🏆","🏆","💰","💰","✂","✂"
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(memorycards.Count);
                string nextCard = memorycards[index];
                textBlock.Text = nextCard;
                memorycards.RemoveAt(index);
                    
            }


        }
        bool matchFound = false;
        TextBlock textblockclicked;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (matchFound == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                textblockclicked = textBlock;
                matchFound = true;
            }
            else if (textBlock.Text == textblockclicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                matchFound = false;

            }
            else
            {
                textblockclicked.Visibility = Visibility.Hidden;
                matchFound = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
