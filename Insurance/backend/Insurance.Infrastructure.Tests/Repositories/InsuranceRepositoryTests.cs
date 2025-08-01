using Insurance.ApplicationCore.Interfaces;
using Insurance.ApplicationCore.Models;
using Insurance.Infrastructure.Repositories;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class InsuranceRepositoryTests
{
    private readonly Mock<IMongoCollection<Insurer>> _mockCollection;
    private readonly Mock<IMongoClient> _mockClient;
    private readonly InsuranceRepository _repository;

    public InsuranceRepositoryTests()
    {
        _mockCollection = new Mock<IMongoCollection<Insurer>>();
        _mockClient = new Mock<IMongoClient>();

        var mockDatabase = new Mock<IMongoDatabase>();
        mockDatabase.Setup(db => db.GetCollection<Insurer>(It.IsAny<string>(), null))
                    .Returns(_mockCollection.Object);

        _mockClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null))
                   .Returns(mockDatabase.Object);

        _repository = new InsuranceRepository(_mockClient.Object);
    }

    [Fact]
    public async Task Create_ShouldInsertInsurer()
    {
        var insurer = new Insurer { Id = "1", Name = "Test", Email = "test@example.com", Phone = "1234567890" };

        await _repository.Create(insurer);

        _mockCollection.Verify(c => c.InsertOneAsync(insurer, null, default), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldReturnTrue_WhenDeleted()
    {
        var deleteResult = Mock.Of<DeleteResult>(r => r.DeletedCount == 1);
        _mockCollection.Setup(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<Insurer>>(), default))
                       .ReturnsAsync(deleteResult);

        var result = await _repository.Delete("1");

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenNotDeleted()
    {
        var deleteResult = Mock.Of<DeleteResult>(r => r.DeletedCount == 0);
        _mockCollection.Setup(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<Insurer>>(), default))
                       .ReturnsAsync(deleteResult);

        var result = await _repository.Delete("1");

        Assert.False(result);
    }

   
  


    [Fact]
    public async Task Update_ShouldReturnTrue_WhenModified()
    {
        var updateResult = Mock.Of<UpdateResult>(r => r.ModifiedCount == 1);
        _mockCollection.Setup(c => c.UpdateOneAsync(It.IsAny<FilterDefinition<Insurer>>(), It.IsAny<UpdateDefinition<Insurer>>(), null, default))
                       .ReturnsAsync(updateResult);

        var result = await _repository.Update("1", new Insurer { Name = "Updated", Email = "u@example.com", Phone = "1234567890" });

        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenNotModified()
    {
        var updateResult = Mock.Of<UpdateResult>(r => r.ModifiedCount == 0);
        _mockCollection.Setup(c => c.UpdateOneAsync(It.IsAny<FilterDefinition<Insurer>>(), It.IsAny<UpdateDefinition<Insurer>>(), null, default))
                       .ReturnsAsync(updateResult);

        var result = await _repository.Update("1", new Insurer { Name = "Updated", Email = "u@example.com", Phone = "1234567890" });

        Assert.False(result);
    }

  

   

}
