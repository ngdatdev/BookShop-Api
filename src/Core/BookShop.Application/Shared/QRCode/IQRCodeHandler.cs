﻿using System.Threading.Tasks;

namespace BookShop.Application.Shared.QRCode;

/// <summary>
///     Represent interface of generate qr code handler.
/// </summary>
public interface IQRCodeHandler
{
    /// <summary>
    ///     Generate url QR code.
    /// </summary>
    /// <param name="qRCodeModel">
    ///     Model depends on service QR API.
    /// </param>
    /// <returns>
    ///     String contain url qr code.
    /// </returns>
    Task<string> CreateUrlQRCode(QRCodeModel qRCodeModel);
}
