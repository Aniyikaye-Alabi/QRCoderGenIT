using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using QR_code_project.Models;

namespace QR_code_Api.Models;

public class Business
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Businessname")]
    public string Businessname { get; set; } = null!;

    public string Phonenumber { get; set; }

    public string CompanyAddress { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string QRCode { get; set; } = null!;

   
}


