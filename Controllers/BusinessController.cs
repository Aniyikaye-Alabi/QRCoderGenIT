using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using IronBarCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using QR_code_Api.Models;
using QR_code_project.Models;
using QRCoder;
//using QRCodeGenerator = QRCoder.QRCodeGenerator;

namespace QR_code_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessController : ControllerBase
{
    private readonly BusinessService _qrcodecollection;
    private readonly IWebHostEnvironment _environment;


    public BusinessController(BusinessService qRCodeService) =>
        _qrcodecollection = qRCodeService;
    // GET: api/Business



    [HttpGet]
    public async Task<List<Business>> Get() =>
    await _qrcodecollection.GetAsync();


    // GET: api/Business/5
    [HttpGet("{id}", Name = "Get")]
    public async Task<ActionResult<Business>> Get(string id)
    {
        var Business = await _qrcodecollection.GetAsync(id);

        if (Business is null)
        {
            return NotFound();
        }

        return Ok(Business);
    }

    // POST: api/Business
    [HttpPost]
    public async Task<IActionResult> Post(string Businessname, string Phonenumber, string CompanyAddress, string Website)
    {
        await _qrcodecollection.CreateAsync(Businessname, Phonenumber, CompanyAddress, Website);

        return Ok("Post Successfully");


    }


    //[HttpPost]
    //public IActionResult CreateQRCode(string generatorBarCode)
    //{
    //    //QRCodeGenerator QrGenerator = new QRCodeGenerator();
    //    //QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode.QRCode, QRCodeGenerator.ECCLevel.Q);
    //    //QRCode QrCode = new QRCode(QrCodeInfo);
    //    //imgBarCode.Height = 150;
    //    //imgBarCode.Width = 150;
    //    //using (Bitmap bitMap = qrCode.GetGraphic(20))
    //    //{
    //    //    using (MemoryStream ms = new MemoryStream())
    //    //    {
    //    //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    //    //        byte[] byteImage = ms.ToArray();
    //    //        return byteImage;
    //    //    }
    //        QRCodeGenerator QrGenerator = new QRCodeGenerator();
    //        QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(generatorBarCode, QRCodeGenerator.ECCLevel.Q);
    //        QRCode QrCode = new QRCode(QrCodeInfo);

    //        Bitmap qr1 = QrCode.GetGraphic(20);


    //    }


    [HttpPost("CreateQRCode")]
    public IActionResult CreateQRCode(QrcodeGen generateQRCode)
    {
        try
        {
            //GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(generateQRCode.QRCodeText, 200);
            //barcode.AddBarcodeValueTextBelowBarcode();
            //// Styling a QR code and adding annotation text
            //barcode.SetMargins(10);
            //barcode.ChangeBarCodeColor(Color.BlueViolet);
            //string path = Path.Combine(_environment.WebRootPath, "GeneratedQRCode");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //string filePath = Path.Combine(_environment.WebRootPath, "GeneratedQRCode/qrcode.png");
            //barcode.SaveAsPng(filePath);
            //string fileName = Path.GetFileName(filePath);
            //string imageUrl = $ "{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedQRCode/" + fileName;
            //ViewBag.QrCodeUri = imageUrl;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Website", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(qrCodeImage, typeof(byte[]));
            return Ok(bytes);

        }
        catch (Exception)
        {
            throw;
        }
        //return View();
    }

        // PUT: api/Business/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Business updatedBusiness)
        {
            var business = await _qrcodecollection.GetAsync(id);

            if (business is null)
            {
                return NotFound();
            }

            updatedBusiness.Id = business.Id;

            await _qrcodecollection.UpdateAsync(id, updatedBusiness);

            return NoContent();
        }

        // DELETE: api/Business/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var business = await _qrcodecollection.GetAsync(id);

            if (business is null)
            {
                return NotFound();
            }

            await _qrcodecollection.RemoveAsync(id);

            return NoContent();
        }
}
