using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Calca.Api.Infrastructure.Models
{
    public class CalculationResult
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }
    }
}