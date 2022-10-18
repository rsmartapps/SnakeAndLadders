using SnakeAndLadders.Domain.Entities;
using SnakeAndLadders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadders.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IBoardService _boardService;

        public GameService(IBoardService boardService)
        {
            _boardService = boardService;
            _boardService.SubscribeTokenUpdated(TokenUpdated);
        }

        private void TokenUpdated(object sender, Token args)
        {

        }
    }
}
