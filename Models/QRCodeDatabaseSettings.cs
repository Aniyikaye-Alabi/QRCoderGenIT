using System;
namespace QR_code_Api.Models;

public class QRCodeDatabaseSettings
{
	public string ConnectionString { get; set; } = null!;

	public string DatabaseName { get; set; } = null!;

	public string QRCodeCollectionName { get; set; } = null!;
}

