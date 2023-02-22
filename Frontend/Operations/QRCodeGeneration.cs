using QRCoder;

namespace Frontend.Operations
{
    public class QRCodeGeneration
    {
        public string GenerateQrCodeImage(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCodeImage = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCodeImage.GetGraphic(50);
            string qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
            return $"data:image/png;base64,{qrCodeBase64}";
        }
    }
}
