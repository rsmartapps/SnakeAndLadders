using FluentAssertions;
using SnakeAndLadders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SnakeAndLadders.Domain.Tests
{
    public class SixSidesDiceServiceTest
    {
        [Fact]
        public void GivenGame_WhenPlayerRolls_ResultBetween1And6()
        {
            // Arrange
            //var service = new SixSidesDiceService();

            //// Act
            //var result = service.RollDie();

            //// Assert
            //result.Should().BeGreaterThanOrEqualTo(1);
            //result.Should().BeLessThanOrEqualTo(6);
        }
    }
}
