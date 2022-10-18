using SnakeAndLadders.Domain.Entities;
using SnakeAndLadders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeAndLadders.Domain.Services
{
    public class BoardService : IBoardService
    {
        public event EventHandler<Token> TokenUpdated;
        public event EventHandler<Token> WinnerFound;
        /// <summary>
        /// Used a list instead of IEnumerable because we don't store data and 
        /// IEnumerable doesn't update object state
        /// </summary>
        List<Token> _Tokens;
        public int BoardSize;
        public BoardService()
        {
            _Tokens = new List<Token>();
        }

        public BoardService(List<Token> tokens)
        {
            _Tokens = tokens;
        }

        public BoardService(int BoardSize)
        {
            this.BoardSize = BoardSize;
            _Tokens = new List<Token>();
        }

        public BoardService(int BoardSize, List<Token> tokens)
        {
            this.BoardSize = BoardSize;
            _Tokens = tokens;
        }

        public void MoveToken(int tokenId, int numTiles)
        {
            var token = GetToken(tokenId);
            if (token == null)
                throw new Exception($"Token {tokenId} not on board");
            token.Tile += numTiles;
            if(token.Tile > BoardSize)
                token.Tile -= numTiles;
            if (token.Tile == BoardSize)
                WinnerFound?.Invoke(this, token);
            TokenUpdated?.Invoke(this, token);
        }

        public Token GetToken(int tokenId)
        {
            return _Tokens.FirstOrDefault(f => f.Id == tokenId);
        }

        public void SubscribeTokenUpdated(EventHandler<Token> tokenUpdated)
        {
            TokenUpdated += tokenUpdated;
        }

        public void Dispose()
        {
        }

        public void UnSubscribeTokenUpdated(EventHandler<Token> tokenUpdated)
        {
            TokenUpdated -= tokenUpdated;
        }

        public void SubscribeWinnerFound(EventHandler<Token> winnerFound)
        {
            WinnerFound += winnerFound;
        }

        public void UnSubscribeWinnerFound(EventHandler<Token> winnerFound)
        {
            WinnerFound -= winnerFound;
        }
    }
}
