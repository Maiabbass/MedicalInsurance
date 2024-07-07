using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using api.Entities;

namespace api.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdfEngUnits(IEnumerable<EngineeringUnits> units)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    // عنوان التقرير
                    document.Add(new Paragraph("Engineering Units Report").SetFontSize(20).SetBold());

                    // إضافة بيانات الوحدات الهندسية
                    foreach (var unit in units)
                    {
                        document.Add(new Paragraph($"Name: {unit.Name}").SetFontSize(12));
                        document.Add(new Paragraph($"President Name: {unit.Namepresident}").SetFontSize(12));
                        document.Add(new Paragraph($"President Phone: {unit.Phonepresident}").SetFontSize(12));
                        document.Add(new Paragraph($"President Email: {unit.Emailpresident}").SetFontSize(12));
                        document.Add(new Paragraph($"Number: {unit.Number}").SetFontSize(12));
                        document.Add(new Paragraph(new string('-', 30))); // خط فاصل بين الوحدات
                    }

                    document.Close();
                }

                return stream.ToArray();
            }
        }


         public byte[] GeneratePdfWorkPlace(IEnumerable<WorkPlace> workPlaces, IEnumerable<EngineeringUnits> engineeringUnits)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                
                    document.Add(new Paragraph("WorkPlaces Report").SetFontSize(20).SetBold());

                
                    foreach (var workPlace in workPlaces)
                    {
                        document.Add(new Paragraph($"Name: {workPlace.Name}").SetFontSize(12));
                        document.Add(new Paragraph($"Location: {workPlace.Location}").SetFontSize(12));
                        document.Add(new Paragraph($"Phone: {workPlace.Phone}").SetFontSize(12));
                        //document.Add(new Paragraph($"EngineeringUnitsId: {workPlace.EngineeringUnitsId}").SetFontSize(12));
                        
                        
                        var engineeringUnit = engineeringUnits.FirstOrDefault(u => u.Id == workPlace.EngineeringUnitsId);
                        if (engineeringUnit != null)
                        {
                            document.Add(new Paragraph($"Engineering Unit Name: {engineeringUnit.Name}").SetFontSize(12));
                        }
                        else
                        {
                            document.Add(new Paragraph($"Engineering Unit Name: Not Found").SetFontSize(12));
                        }

                        document.Add(new Paragraph(new string('-', 30))); // خط فاصل بين الوحدات
                    }

                    document.Close();
                }

                
          
        return stream.ToArray();
    }
        }
    }
}