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
            
            // Skapar mockobjektf�r ICustomer interface 
            var mockCustomer = new Mock<ICustomer>();

            // Act
            // S�tter att id f�rv�ntas vara 42
            
            var expectedId = 42;
                mockCustomer.SetupProperty(c => c.Id, expectedId);
            
            //H�mtar egentliga v�rdet p� Id propertyn fr�n mocken
            var actualId = mockCustomer.Object.Id;

            // Assert
            // kollar s� f�rvantade och faktiska v�rdet �verensst�mmer
           
            Assert.Equal(expectedId, actualId);
            }
        }
    }