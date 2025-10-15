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
using System.Windows.Threading;

namespace Memory_Game
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSec = 0;
        int matchsFound;
        bool matchFound = false;
        TextBlock textblockclicked;



        public MainWindow()
        {
            InitializeComponent();
            
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            startGame();
        }

        private void Timer_Tick(object sender , EventArgs e)
        {
            tenthsOfSec++;
            timeTextBlock.Text = (tenthsOfSec / 10F).ToString("0.0s");
            if (matchsFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = " play again ?";
            }
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

            matchsFound = 0;
            matchFound = false;
            tenthsOfSec = 0;
            textblockclicked = null;
            timeTextBlock.Text = "0.0s";


            Random random = new Random(); // Zufall generator 
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) // Für jede TextBlock in Grid 
            {
                /* int index = random.Next(memorycards.Count); //Eine zufällige Position (Index) in der Liste bestimmen.
                 string nextCard = memorycards[index ]; //Das Emoji an dieser Position holen.
                 textBlock.Text = nextCard;  //Das Emoji in den TextBlock schreiben.
                 memorycards.RemoveAt(index); //Dieses Emoji aus der Liste entfernen,
                                              //damit es nicht nochmal verwendet wird.
 */

                if (textBlock == timeTextBlock) continue;
                {
                    
                    int index = random.Next(memorycards.Count); //Eine zufällige Position (Index) in der Liste bestimmen.
                    string nextCard = memorycards[index]; //Das Emoji an dieser Position holen.
                    textBlock.Tag = nextCard;
                    textBlock.Text = "?";  //Das Emoji in den TextBlock schreiben.
                    textBlock.Visibility = Visibility.Visible;
                    memorycards.RemoveAt(index); //Dieses Emoji aus der Liste entfernen,
                                                 //damit es nicht nochmal verwendet wird.
                }

            }
            timer.Start();
           // tenthsOfSec = 0;
            //matchsFound = 0;

        }

        /*private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock == timeTextBlock) return;

            if (textBlock.Text != "?") return;
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
        }*/
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock == null) return;
            if (textBlock == timeTextBlock) return;      // Zeit-Text ignorieren

            // Schon offen? Dann nichts tun
            if (textBlock.Text != "?") return;

            // Aktuelle Karte aufdecken (Symbol steckt in Tag, wurde in startGame gesetzt)
            string symbol = textBlock.Tag as string;
            if (symbol == null) return;
            textBlock.Text = symbol;

            if (matchFound == false)
            {
                // Erster Klick in der Runde
                textblockclicked = textBlock;
                matchFound = true;
                return;
            }

            // Zweiter Klick: vergleichen
            if (textblockclicked != null && (textblockclicked.Tag as string) == symbol)
            {
                // Treffer -> beide verschwinden
                textBlock.Visibility = Visibility.Hidden;
                textblockclicked.Visibility = Visibility.Hidden;
                matchsFound = matchsFound + 1;
            }
            else
            {
                // Kein Treffer -> beide wieder verdecken
                textBlock.Text = "?";
                if (textblockclicked != null) textblockclicked.Text = "?";
            }

            // Runde zurücksetzen
            textblockclicked = null;
            matchFound = false;
        }


        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchsFound == 8)
                startGame();
        }
    }
}
