using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMarket.Services.CartAPI.Models.Entities;

public class Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserId { get; set; }
    public string CouponCode { get; set; }

    [NotMapped]
    public decimal Discount { get; set; }

    [NotMapped]
    public decimal Total { get; set; }

    public List<CartDetail> CartDetails { get; set; }
}