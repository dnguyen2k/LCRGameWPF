using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRGame.DataModels
{
    public class Player : INotifyPropertyChanged
    {
        private int playerId;
        private string playerName;
        private int chips;
        private int dices;
        private int dice1;
        private int dice2;
        private int dice3;

        private bool gameOver = false;

        private string displayDice1;
        private string displayDice2;
        private string displayDice3;

        public int PlayerId
        {
            get
            {
                return playerId;
            }
            set
            {
                playerId = value;
                OnPropertyChanged("PlayerId");
            }
        }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        public int Chips
        {
            get
            {
                return chips;
            }
            set
            {
                chips = value;
                OnPropertyChanged("Chips");
            }
        }

        public int Dices
        {
            get
            {
                return dices;
            }
            set
            {
                dices = value;
                OnPropertyChanged("Dices");
            }
        }

        public int Dice1
        {
            get
            {
                return dice1;
            }
            set
            {
                dice1 = value;
                OnPropertyChanged("Dice1");
            }
        }

        public int Dice2
        {
            get
            {
                return dice2;
            }
            set
            {
                dice2 = value;
                OnPropertyChanged("Dice2");
            }
        }

        public int Dice3
        {
            get
            {
                return dice3;
            }
            set
            {
                dice3 = value;
                OnPropertyChanged("Dice3");
            }
        }

        public string DisplayDice1
        {
            get
            {
                return displayDice1;
            }
            set
            {
                displayDice1 = value;
                OnPropertyChanged("DisplayDice1");
            }
        }

        public string DisplayDice2
        {
            get
            {
                return displayDice2;
            }
            set
            {
                displayDice2 = value;
                OnPropertyChanged("DisplayDice2");
            }
        }

        public string DisplayDice3
        {
            get
            {
                return displayDice3;
            }
            set
            {
                displayDice3 = value;
                OnPropertyChanged("DisplayDice3");
            }
        }

        public bool GameOver
        {
            get
            {
                return gameOver;
            }
            set
            {
                gameOver = value;
                OnPropertyChanged("GameOver");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
