using System;
using IronBarCode;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using QR_code_project.Models;
using QR_code_Api.Models;

namespace QR_code_project.Controllers
{
    public class demoQrController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly QrcodeGen _qrcode;
        //private readonly QRCodeService _bus;

        public demoQrController(IWebHostEnvironment environment, QrcodeGen qrcode,
            QRCodeService bus)
        {
            _environment = environment;
            _qrcode = qrcode;
            //_bus = bus;
        }

      
        //public IActionResult CreateQRCode(QrcodeGen qRCode)
        //{
        //    var createQrCode = _qrcode.CreateQRCode(qRCode);
        //    return createQrCode();
        //}

        
        private string CreateQRCode(QrcodeGen qRCode)
        {
            try
            {
                GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(qRCode.QRCodeText, 200);
                barcode.AddBarcodeValueTextBelowBarcode();
                // Styling a QR code and adding annotation text
                barcode.SetMargins(10);
                barcode.ChangeBarCodeColor(Color.BlueViolet);
                string path = Path.Combine(_environment.WebRootPath, "GeneratedQRCode");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(_environment.WebRootPath, "GeneratedQRCode/qrcode.png");
                barcode.SaveAsPng(filePath);
                string fileName = Path.GetFileName(filePath);
                //string imageUrl = $ "{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedQRCode/" + fileName;
                string imageUrl = fileName;
                //ViewBag.QrCodeUri = imageUrl;
                return imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
            //return View();
        }

        //[HttpPost]
        //public IActionResult Post(BusinessDto newBusiness)
        //{
        //    //var createBus = _qrcode.Post(newBusiness);
        //    return Ok(createBus);

        //   // return CreatedAtAction(nameof(Get), new { id = newBusiness.Id }, newBusiness);


        //}


    }
}

