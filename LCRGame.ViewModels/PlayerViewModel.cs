using LCRGame.DataModels;
using LCRGame.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LCRGame.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private Player player;
        private int games;
        private int numberOfTurns;
        private bool gameOver = false;
        private ObservableCollection<Player> players;
        private ICommand rollDicesCommand;
        private ICommand addPlayerCommand;
        private ICommand resetGameCommand;

        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
                NotifyPropertyChanged("Player");
            }
        }

        public ObservableCollection<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                NotifyPropertyChanged("Players");
            }
        }

        public int NumberOfGames
        {
            get
            {
                return games;
            }
            set
            {
                games = value;
                NotifyPropertyChanged("NumberOfGames");
            }
        }

        public int NumberOfTurns
        {
            get { return numberOfTurns; }
            set
            {
                numberOfTurns = value;
                NotifyPropertyChanged("NumberOfTurns");
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
                NotifyPropertyChanged("GameOver");
            }
        }

        public PlayerViewModel()
        {
            Player = new Player();
            Players = new ObservableCollection<Player>();
            Players.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Players_CollectionChanged);
        }

        private void Players_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Players");
        }

        public ICommand AddPlayerCommand
        {
            get
            {
                if (addPlayerCommand == null)
                {
                    addPlayerCommand = new RelayCommand(param => this.AddPlayer(), null);
                }
                return addPlayerCommand;
            }
        }

        private void AddPlayer()
        {
            Player.Chips = 3;
            Player.Dices = 3;
            Player.PlayerId = Players.Count + 1;
            Players.Add(Player);
            Player = new Player();
        }

        public ICommand RollDicesCommand
        {
            get
            {
                if(rollDicesCommand == null)
                {
                    rollDicesCommand = new RelayCommand(param => this.RollTheDices(param), null);
                }
                return rollDicesCommand;
            }
        }

        public ICommand ResetGameCommand
        {
            get
            {
                if(resetGameCommand == null)
                {
                    resetGameCommand = new RelayCommand(param => this.ResetGame(), null);
                }
                return resetGameCommand;
            }

        }

        private void ResetGame()
        {
            this.Players = new ObservableCollection<Player>();
            this.NumberOfTurns = 0;
        }

        private Random random = new Random();
        private void RollTheDices(object id)
        {
            if (id == null) return;

            this.numberOfTurns++;
            int playerId = (int)id;
            this.Player = this.Players.FirstOrDefault(x => x.PlayerId == playerId);
            int[] dice = new int[player.Dices];
            for(int i=0; i < dice.Length; i++)
            {
                dice[i] = random.Next(1, 6 + 1);

                if (i == 0)
                {
                    this.Player.Dice1 = dice[i];
                    CalculateRemainderChipsAndDices(this.Player, dice[i]);
                    this.Player.DisplayDice1 = GetDiceCharacter(dice[i]);
                }
                else if (i == 1)
                {
                    this.Player.Dice2 = dice[i];
                    CalculateRemainderChipsAndDices(this.Player, dice[i]);
                    this.Player.DisplayDice2 = GetDiceCharacter(dice[i]);
                }
                else if (i == 2)
                {
                    this.Player.Dice3 = dice[i];
                    CalculateRemainderChipsAndDices(this.Player, dice[i]);
                    this.Player.DisplayDice3 = GetDiceCharacter(dice[i]);
                }
            }

            Array.Sort(dice);
        }

        private void CalculateRemainderChipsAndDices(Player player, int num)
        {
            Player currentPlayer = this.Players.Single(x => x.PlayerId == player.PlayerId);

            int index = this.Players.IndexOf(currentPlayer);
            Player previousPlayer = new Player();

            if (index == 0)
            {
                previousPlayer = this.Players[this.Players.Count - 1];
            }
            else
            {
                previousPlayer = index + 1 >= this.Players.Count ? this.Players[0] : this.Players[index - 1];
            }

            Player nextPlayer = index + 1 >= this.Players.Count ? this.Players[0] : this.Players[index + 1];

            if (num == 1)
            {
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Chips = (currentPlayer.Chips - 1);
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Dices = (currentPlayer.Dices - 1);

                this.Players.Single(x => x.PlayerId == previousPlayer.PlayerId).Chips = (previousPlayer.Chips + 1);
                this.Players.Single(x => x.PlayerId == previousPlayer.PlayerId).Dices = (previousPlayer.Dices + 1);
            }
            else if (num == 2)
            {
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Chips = (currentPlayer.Chips - 1);
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Dices = (currentPlayer.Dices - 1);
            }
            else if (num == 3)
            {
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Chips = (currentPlayer.Chips - 1);
                this.Players.Single(x => x.PlayerId == currentPlayer.PlayerId).Dices = (currentPlayer.Dices - 1);

                this.Players.Single(x => x.PlayerId == nextPlayer.PlayerId).Chips = (nextPlayer.Chips + 1);
                this.Players.Single(x => x.PlayerId == nextPlayer.PlayerId).Dices = (nextPlayer.Dices + 1);
            }

            if((currentPlayer.Chips == 0 && nextPlayer.Chips == 0) || (currentPlayer.Chips == 0 && previousPlayer.Chips == 0) || (nextPlayer.Chips == 0 && previousPlayer.Chips == 0))
            {
                this.player.GameOver = true;
                this.GameOver = true;
                this.NumberOfGames++;
            }
            else
            {
                this.player.GameOver = false;
            }
        }

        private string GetDiceCharacter(int num)
        {
            if (num == 1) return "L";
            else if (num == 2) return "C";
            else if (num == 3) return "R";
            else return "Dot";
        }
    }
}
