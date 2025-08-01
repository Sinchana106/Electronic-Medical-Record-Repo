using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Patient
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
    public string ContactNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required.")]
    [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
    public string Type { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "InsuredBy is required.")]
    [StringLength(100, ErrorMessage = "InsuredBy cannot exceed 100 characters.")]
    public string InsuredBy { get; set; } = string.Empty;

    [Required(ErrorMessage = "VisitType is required.")]
    [MinLength(1, ErrorMessage = "At least one visit type must be specified.")]
    public List<string> VisitType { get; set; } = new();

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;
}
