using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class PatientModelTests
{
    private List<ValidationResult> ValidateModel(Patient model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }

    [Fact]
    public void PatientModel_ValidModel_PassesValidation()
    {
        var patient = new Patient
        {
            Id = "P001",
            Name = "John Doe",
            ContactNo = "123-456-7890",
            Type = "Outpatient",
            Age = 30,
            InsuredBy = "HealthCare Inc.",
            VisitType = new List<string> { "Consultation" },
            Description = "Regular check-up"
        };

        var results = ValidateModel(patient);

        Assert.Empty(results); // No validation errors
    }

 
    [Fact]
    public void PatientModel_InvalidPhoneNumber_FailsValidation()
    {
        var patient = new Patient
        {
            Name = "Jane",
            ContactNo = "invalid-phone",
            Type = "Inpatient",
            Age = 25,
            InsuredBy = "InsureCo",
            VisitType = new List<string> { "Emergency" }
        };

        var results = ValidateModel(patient);

        Assert.Contains(results, r => r.ErrorMessage.Contains("Invalid phone number format"));
    }

    [Fact]
    public void PatientModel_AgeOutOfRange_FailsValidation()
    {
        var patient = new Patient
        {
            Name = "Elder",
            ContactNo = "1234567890",
            Type = "Inpatient",
            Age = 150, // Invalid
            InsuredBy = "InsureCo",
            VisitType = new List<string> { "Emergency" }
        };

        var results = ValidateModel(patient);

        Assert.Contains(results, r => r.ErrorMessage.Contains("Age must be between 0 and 120"));
    }

    [Fact]
    public void PatientModel_DescriptionTooLong_FailsValidation()
    {
        var patient = new Patient
        {
            Name = "John",
            ContactNo = "1234567890",
            Type = "Inpatient",
            Age = 40,
            InsuredBy = "InsureCo",
            VisitType = new List<string> { "Routine" },
            Description = new string('a', 600) // Exceeds 500
        };

        var results = ValidateModel(patient);

        Assert.Contains(results, r => r.ErrorMessage.Contains("Description cannot exceed 500 characters"));
    }
}
