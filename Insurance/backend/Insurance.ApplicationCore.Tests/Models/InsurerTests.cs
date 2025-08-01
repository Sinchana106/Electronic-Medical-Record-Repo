using Insurance.ApplicationCore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class InsurerModelTests
{
    private List<ValidationResult> ValidateModel(Insurer insurer)
    {
        var context = new ValidationContext(insurer, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(insurer, context, results, true);
        return results;
    }

    [Fact]
    public void ValidInsurer_ShouldPassValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "valid@example.com",
            Phone = "1234567890"
        };

        var results = ValidateModel(insurer);
        Assert.Empty(results);
    }

    [Fact]
    public void MissingName_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = null,
            Email = "valid@example.com",
            Phone = "1234567890"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Name is required"));
    }

    [Fact]
    public void LongName_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = new string('A', 101),
            Email = "valid@example.com",
            Phone = "1234567890"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Name can't be longer than 100 characters"));
    }

    [Fact]
    public void InvalidEmail_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "invalid-email",
            Phone = "1234567890"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Invalid email address format"));
    }

    [Fact]
    public void MissingEmail_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = null,
            Phone = "1234567890"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Email is required"));
    }

    [Fact]
    public void InvalidPhone_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "valid@example.com",
            Phone = "abc123"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Invalid phone number format"));
    }

    [Fact]
    public void ShortPhone_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "valid@example.com",
            Phone = "123"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Phone number must be between 7 and 10 digits"));
    }

    [Fact]
    public void LongPhone_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "valid@example.com",
            Phone = "12345678901"
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Phone number must be between 7 and 10 digits"));
    }

    [Fact]
    public void MissingPhone_ShouldFailValidation()
    {
        var insurer = new Insurer
        {
            Id = "123",
            Name = "Valid Name",
            Email = "valid@example.com",
            Phone = null
        };

        var results = ValidateModel(insurer);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Phone number is required"));
    }
}
