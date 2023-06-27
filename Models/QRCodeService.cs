using QR_code_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace QR_code_project.Models;

public class QRCodeService
{
    private readonly Business _context;

    public QRCodeService(Business context)
    {
        _context = context;
    }

    public async Task Post(BusinessDto newBusiness)
    {
        //var createBus = _context.Business.
    }

}

