using System;
using System.Drawing;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QR_code_Api.Models;
using QRCoder;

namespace QR_code_project.Models
{
	public class BusinessService
	{
        private readonly IMongoCollection<Business> _qrcodecollection;


        public BusinessService(
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

        public async Task<Business?> GetAsync(string id) =>
            await _qrcodecollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(string Businessname, string Phonenumber, string CompanyAddress, string Website)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Website, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(qrCodeImage, typeof(byte[]));
            Business _business = new Business();
            _business.Businessname = Businessname;
            _business.Phonenumber = Phonenumber;
            _business.CompanyAddress = CompanyAddress;
            _business.Website = Website;
            _business.QRCode = Convert.ToBase64String(bytes);
            await _qrcodecollection.InsertOneAsync(_business);
        }



        public async Task UpdateAsync(string id, Business updatedBusiness) =>
            await _qrcodecollection.ReplaceOneAsync(x => x.Id == id, updatedBusiness);

        public async Task RemoveAsync(string id) =>
            await _qrcodecollection.DeleteOneAsync(x => x.Id == id);

        //internal Task CreateQRCode(QRCoder.QRCodeGenerator qRCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

