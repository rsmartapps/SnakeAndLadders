using SnakeAndLadders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadders.Domain.Interfaces
{
    public interface IBoardService : IDisposable
    {
        Token GetToken(int tokenId);
        void MoveToken(int tokenId, int numTiles);
        void SubscribeTokenUpdated(EventHandler<Token> tokenUpdated);
        void UnSubscribeTokenUpdated(EventHandler<Token> tokenUpdated);
        void SubscribeWinnerFound(EventHandler<Token> winnerFound);
        void UnSubscribeWinnerFound(EventHandler<Token> winnerFound);
    }
}
