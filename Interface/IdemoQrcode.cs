using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QR_code_Api.Models;
using QR_code_project.Models;

namespace QR_code_project.Interface
{
	public class IdemoQrcode
    {
        private readonly IMongoCollection<BusinessDto> _bus;
        private readonly IMongoCollection<QrcodeGen> _qrcode;

        public IdemoQrcode(
        IOptions<QRCodeDatabaseSettings> qrcodeDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                qrcodeDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                qrcodeDatabaseSettings.Value.DatabaseName);


            _qrcode = mongoDatabase.GetCollection<QrcodeGen>(
                qrcodeDatabaseSettings.Value.QRCodeCollectionName);


            _bus = mongoDatabase.GetCollection<BusinessDto>(
                qrcodeDatabaseSettings.Value.QRCodeCollectionName);
        }


        public async Task CreateQRCode(QrcodeGen qRCode) =>
        await _qrcode.InsertOneAsync(qRCode);


        public async Task Post(BusinessDto newBusiness) =>
        await _bus.InsertOneAsync(newBusiness);
    }
}

