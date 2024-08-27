using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace EV_HACIENDA.Servicios
{
    public class GenerarFirmaXml
    {
        public string FirmarXml(string xmlFactura, string certificatePath, string certificatePassword)
        {
            if (string.IsNullOrEmpty(certificatePath) || string.IsNullOrEmpty(certificatePassword))
            {
                throw new ArgumentException("La ruta del certificado o la contraseña no pueden estar vacías.");
            }

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlFactura);

                var certificate = new X509Certificate2(certificatePath, certificatePassword);
                var signedXml = new SignedXml(xmlDoc)
                {
                    SigningKey = certificate.GetRSAPrivateKey()
                };

                var reference = new Reference { Uri = "" };
                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                signedXml.AddReference(reference);

                signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

                var keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(certificate));
                signedXml.KeyInfo = keyInfo;

                signedXml.ComputeSignature();
                var xmlDigitalSignature = signedXml.GetXml();
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al firmar el XML.", ex);
            }
        }

    }
}
