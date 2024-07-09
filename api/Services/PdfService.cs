using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using api.Entities;
using api.DTOS;
using api.Data;

namespace api.Services
{
    public class PdfService : IPdfService
    {

        private readonly DataContext _dataContext;

         public PdfService(DataContext dataContext)
         {
            _dataContext=dataContext;
         } 


        public string GetRelationTypeNameById(int relationTypeId)
    {
        var relationType = _dataContext.RelationTypes
            .FirstOrDefault(rt => rt.Id == relationTypeId);
        return relationType?.Name;
    }

    
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


 

public byte[] GeneratePdfEngineerReport(IEnumerable<SimpleEngineer> engineers)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(stream);
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);

                // Title of the report
                document.Add(new Paragraph("Engineers and their families Report").SetFontSize(20).SetBold());

                foreach (var engineer in engineers)
                {
                    // Engineer details
                    document.Add(new Paragraph($"Engineer Number: {engineer.EngNumber}").SetFontSize(12));
                    document.Add(new Paragraph($"Sub Number: {engineer.SubNumber}").SetFontSize(12));

                    var person = engineer.Person;
                    if (person != null)
                    {
                        document.Add(new Paragraph("Person Details:").SetBold());
                        document.Add(new Paragraph($"Name: {person.FirstName} {person.FatherName} {person.LastName}").SetFontSize(12));
                        document.Add(new Paragraph($"National ID: {person.NationalId}").SetFontSize(12));
                        document.Add(new Paragraph($"Phone: {person.Phone}").SetFontSize(12));
                        document.Add(new Paragraph($"Mobile: {person.Mobile}").SetFontSize(12));
                        document.Add(new Paragraph($"Email: {person.Email}").SetFontSize(12));
                        document.Add(new Paragraph($"Address: {person.Address}").SetFontSize(12));
                    }

                    // Relations details
                    if (engineer.Relations != null && engineer.Relations.Any())
                    {
                        document.Add(new Paragraph("Relations:").SetBold());
                        foreach (var relation in engineer.Relations)
                        {
                           // document.Add(new Paragraph($"Relation Name: {relation.Name}").SetFontSize(12));

                            // الحصول على اسم نوع العلاقة من الخدمة
                            string relationTypeName = GetRelationTypeNameById(relation.RelationTypeId);
                            document.Add(new Paragraph($"Relation Type: {relationTypeName}").SetFontSize(12));

                            var relatedPerson = relation.Person;
                            if (relatedPerson != null)
                            {
                                document.Add(new Paragraph("Related Person Details:").SetBold());
                                document.Add(new Paragraph($"Name: {relatedPerson.FirstName} {relatedPerson.FatherName} {relatedPerson.LastName}").SetFontSize(12));
                                document.Add(new Paragraph($"National ID: {relatedPerson.NationalId}").SetFontSize(12));
                                document.Add(new Paragraph($"Phone: {relatedPerson.Phone}").SetFontSize(12));
                                document.Add(new Paragraph($"Mobile: {relatedPerson.Mobile}").SetFontSize(12));
                                document.Add(new Paragraph($"Email: {relatedPerson.Email}").SetFontSize(12));
                                document.Add(new Paragraph($"Address: {relatedPerson.Address}").SetFontSize(12));
                            }

                            document.Add(new Paragraph(new string('-', 30))); // Separator line
                        }
                    }

                    document.Add(new Paragraph(new string('=', 30))); // Separator line between engineers
                }

                document.Close();
            }

            return stream.ToArray();
        }
    }
    }}