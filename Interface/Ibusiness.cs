using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QR_code_Api.Models;

namespace QR_code_project.Interface
{
    public class Ibusiness
    {
        private readonly IMongoCollection<Business> _qrcodecollection;

        public Ibusiness(
        IOptions<QRCodeDatabaseSettings> qrcodeDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                qrcodeDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                qrcodeDatabaseSettings.Value.DatabaseName);

            _qrcodecollection = mongoDatabase.GetCollection<Business>(
                qrcodeDatabaseSettings.Value.QRCodeCollectionName);
        }

        public async Task<List<Business>> GetAsync() =>
        await _qrcodecollection.Find(_ => true).ToListAsync();

        public async Task CreateQRCode(Business qRCode) =>
        await _qrcodecollection.InsertOneAsync(qRCode);
    }
}

