using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using api.Services;
using api.Entities;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PDF : ControllerBase
    {
        private readonly IEngineeringUnitsService _engineeringUnitsService;

        public PDF(IEngineeringUnitsService engineeringUnitsService)
        {
            _engineeringUnitsService = engineeringUnitsService;
        }

        private async Task<QuestPDF.Infrastructure.IDocument> CreateDocument()
        {
            var engineeringUnits = await _engineeringUnitsService.GetAll();

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .AlignCenter()
                        .Text("تقرير الوحدات الهندسية")
                        .SemiBold().FontSize(36).FontColor(Colors.Black)
                        .DirectionFromRightToLeft();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            foreach (var unit in engineeringUnits)
                            {
                                x.Item().Text($"الاسم: {unit.Name}").DirectionFromRightToLeft();
                                x.Item().Text($"الرقم: {unit.Number}").DirectionFromRightToLeft();
                                x.Item().Text($"اسم رئيس الوحدة: {unit.Namepresident}").DirectionFromRightToLeft();
                                x.Item().Text($"رقم رئيس الوحدة: {unit.Phonepresident}").DirectionFromRightToLeft();
                                x.Item().Text($"ايميل رئيس الوحدة: {unit.Emailpresident}").DirectionFromRightToLeft();
                                x.Item().PaddingBottom(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("الصفحة ").DirectionFromRightToLeft();
                            x.CurrentPageNumber();
                        });
                });
            });
        }

        [HttpGet("GeneratePdf")]
        public async Task<FileContentResult> GeneratePdf()
        {
            var document = await CreateDocument();

            var pdf = document.GeneratePdf();
            
            var stream = new MemoryStream(pdf);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };

            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "EN1.pdf"
            };

            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            FileContentResult file = new FileContentResult(stream.ToArray(), "application/pdf")
            {
                FileDownloadName = "EN1.pdf"
            };

            return file;
        }
    }
}