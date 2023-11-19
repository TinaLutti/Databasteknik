using Assignment.Contexts;
using Assignment.Entities;
using Assignment.Interfaces;
using Assignment.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

    namespace Assignment.Tests
    {
        public class CustomerTests
        {
            [Fact]
            public void SetId_GetId_ShouldReturnSameValue()
            {
            // Arrange
            
            // Skapar mockobjektför ICustomer interface 
            var mockCustomer = new Mock<ICustomer>();

            // Act
            // Sätter att id förväntas vara 42
            
            var expectedId = 42;
                mockCustomer.SetupProperty(c => c.Id, expectedId);
            
            //Hämtar egentliga värdet på Id propertyn från mocken
            var actualId = mockCustomer.Object.Id;

            // Assert
            // kollar så förvantade och faktiska värdet överensstämmer
           
            Assert.Equal(expectedId, actualId);
            }
        }
    }