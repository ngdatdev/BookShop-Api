namespace BookShop.Application.Shared.QRCode;

/// <summary>
///     Represent the QR code model.
/// </summary>
public sealed class QRCodeModel
{
    public int AccountNo { get; set; }
    public string AccountName { get; set; }
    public int AcqId { get; set; }
    public decimal Amount { get; set; }
    public string AddInfo { get; set; }
}
