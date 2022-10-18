using SnakeAndLadders.Domain.Entities;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Moq;
using SnakeAndLadders.Domain.Services;
using AutoFixture;
using System.Linq;

namespace SnakeAndLadders.Domain.Tests
{
    public class BoardServiceTest
    {
        Fixture fixture = new Fixture();
        public BoardServiceTest()
        {
        }

        [Fact]
        public void GivenGame_WhenTokenOnBoard_ThenTokenIsOnSquare1()
        {
            // Arrange
            var expects = 1;
            var player1 = fixture.Create<Player>();
            var player2 = fixture.Create<Player>();
            var board = new BoardService(100, new List<Token>
            {
                new Token(){ Id = player1.TokenId },
                new Token(){ Id = player2.TokenId }
            });
            // Act
            // Assert
            board.GetToken(player1.TokenId).Tile.Should().Be(expects);
            board.GetToken(player2.TokenId).Tile.Should().Be(expects);
        }

        [Fact]
        public void GivenGame_WhenTokenMoves3_ThenTokenIsOnSquare8()
        {
            // Arrange
            var expects = 4;
            var players = fixture.CreateMany<Player>(8);
            var board = new BoardService(100, players.Select(s => new Token { Id = s.TokenId }).ToList());
            
            // Act
            var player1 = players.First();
            board.MoveToken(player1.TokenId, 3);
            
            // Assert
            foreach (var player in players)
            {
                if (player.TokenId == player1.TokenId)
                {
                    board.GetToken(player.TokenId).Tile.Should().Be(expects);
                    continue;
                }
                board.GetToken(player.TokenId).Tile.Should().NotBe(expects);
            }
        }

        [Fact]
        public void GivenGame_WhenTokenMoves7_ThenTokenIsOnSquare8()
        {
            // Arrange
            var expects = 8;
            var players = fixture.CreateMany<Player>(8);
            var board = new BoardService(100, players.Select(s => new Token { Id = s.TokenId }).ToList());
            // Act
            var player1 = players.First();
            board.MoveToken(player1.TokenId, 3);
            board.MoveToken(player1.TokenId, 4);
            foreach(var player in players)
            {
                if (player.TokenId == player1.TokenId)
                    continue;
                board.GetToken(player.TokenId).Tile.Should().NotBe(expects);
            }

        }

        [Fact]
        public void GivenGameStarted_WhenTokenMoves_ThenIsSquare100_ThenPlayerWins()
        {
            bool winnerInvoked = false;
            // Arange
            var expects = 100;
            var players = fixture.Create<Player>();
            var token = new Token() { Id = players.TokenId, Tile = 97 };
            var board = new BoardService(100, new List<Token> { token, new Token { Id = 321 } });
            board.SubscribeWinnerFound((sender, args) =>
            {
                // Assert
                args.Id.Should().Be(token.Id);
                args.Tile.Should().Be(expects);
                winnerInvoked = true;
            });
            // Act
            board.MoveToken(players.TokenId, 3);

            // Assert
            winnerInvoked.Should().BeTrue();
        }

        [Fact]
        public void GivenGameStarted_WhenTokenMoves_ThenIsSquare100_ThenPlayerStillAtSquare97()
        {
            // Arange
            bool winnerInvoked = false;
            var expects = 97;
            var players = fixture.Create<Player>();
            var token = new Token() { Id = players.TokenId, Tile = 97 };
            var board = new BoardService(100, new List<Token> { token, new Token { Id = 321 } });
            board.SubscribeWinnerFound((sender, args) =>
            {
                // Assert
                args.Id.Should().Be(token.Id);
                args.Tile.Should().Be(expects);
                winnerInvoked = true;
            });
            // Act
            board.MoveToken(players.TokenId, 4);

            // Assert
            winnerInvoked.Should().BeFalse();
            board.GetToken(players.TokenId).Tile.Should().Be(expects);
        }
    }
}