using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("phone")]
        public string phone { get; set; }

        [BsonElement("website")]
        public string website { get; set; }

        [BsonElement("address")]
        public Address address { get; set; }

        [BsonElement("company")]
        public Company company { get; set; }
    }

    public class Address
    {
        [BsonElement("street")]
        public string street { get; set; }

        [BsonElement("suite")]
        public string suite { get; set; }

        [BsonElement("city")]
        public string city { get; set; }

        [BsonElement("zipcode")]
        public string zipcode { get; set; }

        [BsonElement("geo")]
        public GEO geo { get; set; }

    }

    public class GEO
    {
        [BsonElement("lat")]
        public string lat { get; set; }

        [BsonElement("lng")]
        public string lng { get; set; }
    }

    public class Company
    {
        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("catchPhrase")]
        public string catchPhrase { get; set; }

        [BsonElement("bs")]
        public string bs { get; set; }
    }
}
