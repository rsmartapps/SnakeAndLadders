using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadders.Domain.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public int Tile { get; set; } = 1;

        public bool IsPlayerToken(int tokenId)
        {
            return Id == tokenId;
        }
    }
}
