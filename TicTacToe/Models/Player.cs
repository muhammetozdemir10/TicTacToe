using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{

    internal class Player
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public List<string> SelectedButtons { get; set; } = new List<string>();

        public Player(int id)
        {
            this.Id = id;
        }
    }
}
