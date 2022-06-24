using System.IO;
using Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infraesctructura
{
    public class DocumentoPdf
    {

        public void GuardarPdf(Tarifa tarifa, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 40, 40, 40, 40);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.AddAuthor("Aplicacion Pulsacion");
            document.Open();
            document.Add(new Paragraph("INFORME DE TARIFA REGISTRADA"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph($"Placa: {tarifa.NombreTipodeVehiculo}"));
            document.Add(new Paragraph($"Placa: {tarifa.ValorMinimo}"));
            document.Add(new Paragraph($"Placa: {tarifa.ValorNormal}"));
            document.Add(new Paragraph($"Placa: {tarifa.TiempoMinimo}"));

            document.Close();
        }
    }
}