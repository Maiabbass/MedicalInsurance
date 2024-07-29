using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Services;

namespace api.Services
{
    public class ExcelService : IExcelService
    {
       private readonly IEngineeringUnitsService _engineeringUnitsService;
        private readonly IWorkplaceService _workplaceService;
        private readonly IHospitalService _hospitalService;
        private readonly ICityService _cityService;
        private readonly ISpecializationService _specializationService;
        private readonly IEngineeringeDeparService _engineeringeDeparService;

        private readonly ISearchService _searchService;

        public ExcelService(
            IEngineeringUnitsService engineeringUnitsService,
            IWorkplaceService workplaceService,
            IHospitalService hospitalService,
            ICityService cityService,
            ISpecializationService specializationService,
            IEngineeringeDeparService engineeringeDeparService,
            ISearchService searchService)
        {
            _engineeringUnitsService = engineeringUnitsService;
            _workplaceService = workplaceService;
            _hospitalService = hospitalService;
            _cityService = cityService;
            _specializationService = specializationService;
            _engineeringeDeparService = engineeringeDeparService;
            _searchService = searchService;
        }

        public async Task<Stream> GenerateExcelReportAsync()
        {
            var units = await _engineeringUnitsService.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("EngineeringUnits");

                // تعيين الكتابة من اليمين إلى اليسار
                worksheet.View.RightToLeft = true;

                // إضافة العناوين بحجم أكبر
                worksheet.Cells[1, 1].Value = "الرقم";
                worksheet.Cells[1, 2].Value = "الاسم";
                worksheet.Cells[1, 3].Value = "اسم رئيس الوحدة";
                worksheet.Cells[1, 4].Value = "رقم رئيس الوحدة";
                worksheet.Cells[1, 5].Value = "ايميل رئيس الوحدة";

                // ضبط حجم الخط للعناوين
                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Size = 14; // يمكنك تغيير الحجم حسب الحاجة
                    range.Style.Font.Bold = true; // جعل الخط عريضاً للعناوين
                }

                // إضافة البيانات
                int row = 2;
                foreach (var unit in units)
                {
                    worksheet.Cells[row, 1].Value = unit.Number;
                    worksheet.Cells[row, 2].Value = unit.Name;
                    worksheet.Cells[row, 3].Value = unit.Namepresident;
                    worksheet.Cells[row, 4].Value = unit.Phonepresident;
                    worksheet.Cells[row, 5].Value = unit.Emailpresident;
                    row++;
                }

                // تنسيق الجدول
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                stream.Position = 0;

                return stream;
            }
        }



             public async Task<Stream> GenerateExcelReportAsync2()
        {
            var units = await _workplaceService.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("WorkPlace");

                // تعيين الكتابة من اليمين إلى اليسار
                worksheet.View.RightToLeft = true;

                // إضافة العناوين بحجم أكبر
                worksheet.Cells[1, 1].Value = "الاسم";
                worksheet.Cells[1, 2].Value = "الموقغ";
                worksheet.Cells[1, 3].Value = "الرقم";
               
                

                // ضبط حجم الخط للعناوين
                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Size = 14; // يمكنك تغيير الحجم حسب الحاجة
                    range.Style.Font.Bold = true; // جعل الخط عريضاً للعناوين
                }

                // إضافة البيانات
                int row = 2;
                foreach (var unit in units)
                {
                   
                    worksheet.Cells[row, 1].Value = unit.Name;
                    worksheet.Cells[row, 2].Value = unit.Location;
                    worksheet.Cells[row, 3].Value = unit.Phone;
                    
                    row++;
                }

                // تنسيق الجدول
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                stream.Position = 0;

                return stream;
            }
        }


        

             public async Task<Stream> GenerateExcelReportAsync3()

        {
            var hospitals = await _hospitalService.GetAll();

            // استخدم قاموس لتخزين أسماء المدن المرتبطة بمعرف المدينة
            var cityNames = new Dictionary<int, string>();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Hospitals");

                // تعيين الكتابة من اليمين إلى اليسار
                worksheet.View.RightToLeft = true;

                // إضافة العناوين بحجم أكبر
                worksheet.Cells[1, 1].Value = "الاسم";
                worksheet.Cells[1, 2].Value = "رقم الهاتف";
                worksheet.Cells[1, 3].Value = "البريد الإلكتروني";
                worksheet.Cells[1, 4].Value = "العنوان";
                worksheet.Cells[1, 5].Value = "خارج الشبكة";
                worksheet.Cells[1, 6].Value = "داخل الشبكة";
                worksheet.Cells[1, 7].Value = "اسم المدينة";

                // ضبط حجم الخط للعناوين
                using (var range = worksheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Size = 14; // يمكنك تغيير الحجم حسب الحاجة
                    range.Style.Font.Bold = true; // جعل الخط عريضاً للعناوين
                }

                // إضافة البيانات
                int row = 2;
                foreach (var hospital in hospitals)
                {
                    // جلب اسم المدينة من قاموس أو باستخدام الدالة Get من CityService
                    string cityName;
                    if (!cityNames.TryGetValue(hospital.CityId, out cityName))
                    {
                        var city = await _cityService.Get(hospital.CityId);
                        cityName = city?.Name;
                        cityNames[hospital.CityId] = cityName;
                    }

                    worksheet.Cells[row, 1].Value = hospital.Name;
                    worksheet.Cells[row, 2].Value = hospital.Phone;
                    worksheet.Cells[row, 3].Value = hospital.Email;
                    worksheet.Cells[row, 4].Value = hospital.Address;
                    worksheet.Cells[row, 5].Value = hospital.Enabled;
                    worksheet.Cells[row, 6].Value = hospital.Inside;
                    worksheet.Cells[row, 7].Value = cityName; // جلب اسم المدينة
                    row++;
                }

                // تنسيق الجدول
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                stream.Position = 0;

                return stream;
            }
        }

        
     public async Task<Stream> GenerateExcelReportSpecializationsAndDepartmentsAsync()
        {
            var specializations = await _specializationService.GetAll();
            var departments = await _engineeringeDeparService.GetAll();

            using (var package = new ExcelPackage())
            {
                var specializationSheet = package.Workbook.Worksheets.Add("Specializations");

                // تعيين الكتابة من اليمين إلى اليسار
                specializationSheet.View.RightToLeft = true;

                // إضافة العناوين بحجم أكبر
                
                specializationSheet.Cells[1, 1].Value = "الاسم";
                specializationSheet.Cells[1, 2].Value = "القسم الهندسي";

                // ضبط حجم الخط للعناوين
                using (var range = specializationSheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Size = 14;
                    range.Style.Font.Bold = true;
                }

                // إضافة البيانات
                int row = 2;
                var departmentNames = new Dictionary<int, string>();

                foreach (var specialization in specializations)
                {
                    string departmentName;
                    if (!departmentNames.TryGetValue(specialization.EngineeringeDeparId, out departmentName))
                    {
                        var department = await _engineeringeDeparService.Get(specialization.EngineeringeDeparId);
                        departmentName = department?.Name;
                        departmentNames[specialization.EngineeringeDeparId] = departmentName;
                    }

                    //specializationSheet.Cells[row, 1].Value = specialization.Id;
                    specializationSheet.Cells[row, 1].Value = specialization.Name;
                    specializationSheet.Cells[row, 2].Value = departmentName;
                    row++;
                }

                // تنسيق الجدول
                specializationSheet.Cells[specializationSheet.Dimension.Address].AutoFitColumns();

                var departmentSheet = package.Workbook.Worksheets.Add("Departments");

                // تعيين الكتابة من اليمين إلى اليسار
                departmentSheet.View.RightToLeft = true;

                // إضافة العناوين بحجم أكبر
               
                departmentSheet.Cells[1, 1].Value = "الاسم";

                // ضبط حجم الخط للعناوين
                using (var range = departmentSheet.Cells[1, 1, 1, 2])
                {
                    range.Style.Font.Size = 14;
                    range.Style.Font.Bold = true;
                }

                // إضافة البيانات
                row = 2;
                foreach (var department in departments)
                {
                    //departmentSheet.Cells[row, 1].Value = department.Id;
                    departmentSheet.Cells[row, 1].Value = department.Name;
                    row++;
                }

                // تنسيق الجدول
                departmentSheet.Cells[departmentSheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                stream.Position = 0;

                return stream;
            }
        }
    
    

   public async Task<Stream> GenerateExcelReportForClaimsEng(string insuranceNumber)
{
    var claims = await _searchService.GetClaim(insuranceNumber);

    using (var package = new ExcelPackage())
    {
        // إنشاء ورقة العمل للمطالبات
        var worksheet = package.Workbook.Worksheets.Add("Claims");

        // تعيين الكتابة من اليمين إلى اليسار
        worksheet.View.RightToLeft = true;

        // إضافة العناوين
        worksheet.Cells[1, 1].Value = "رقم المطالبة";
        worksheet.Cells[1, 2].Value = "رقم التأمين";
        worksheet.Cells[1, 3].Value = "الاسم الكامل";
        worksheet.Cells[1, 4].Value = "المبلغ الإجمالي";
        worksheet.Cells[1, 5].Value = "رسوم الشركة";
        worksheet.Cells[1, 6].Value = "المبلغ المعتمد";
        worksheet.Cells[1, 7].Value = "غير المضافة";
        worksheet.Cells[1, 8].Value = "غير المضافة للشخص";
        worksheet.Cells[1, 9].Value = "نسبة التحمل";
        worksheet.Cells[1, 10].Value = "المستشفى";
        worksheet.Cells[1, 11].Value = "الحالة";
        worksheet.Cells[1, 12].Value = "تاريخ الدخول";
        worksheet.Cells[1, 13].Value = "تاريخ الخروج";

        // ضبط حجم الخط للعناوين
        using (var range = worksheet.Cells[1, 1, 1, 13])
        {
            range.Style.Font.Size = 14;
            range.Style.Font.Bold = true;
        }

        // زيادة عرض الأعمدة وارتفاع الصفوف
        worksheet.Column(1).Width = 15;
        worksheet.Column(2).Width = 20;
        worksheet.Column(3).Width = 25;
        worksheet.Column(4).Width = 20;
        worksheet.Column(5).Width = 15;
        worksheet.Column(6).Width = 20;
        worksheet.Column(7).Width = 15;
        worksheet.Column(8).Width = 20;
        worksheet.Column(9).Width = 15;
        worksheet.Column(10).Width = 25;
        worksheet.Column(11).Width = 15;
        worksheet.Column(12).Width = 20;
        worksheet.Column(13).Width = 20;

        // إضافة البيانات
        int row = 2;
        decimal totalAmount = 0;
        decimal totalCompanyFees = 0;
        decimal totalApprovedPrice = 0;
        decimal totalNonAdd = 0;
        decimal totalNonAddForPerson = 0;

        foreach (var claim in claims)
        {
            worksheet.Cells[row, 1].Value = claim.Id;
            worksheet.Cells[row, 2].Value = claim.EnsuranceNumber;
            worksheet.Cells[row, 3].Value = claim.FullName;
            worksheet.Cells[row, 4].Value = claim.TotalPrice;
            worksheet.Cells[row, 5].Value = claim.Company_fees;
            worksheet.Cells[row, 6].Value = claim.ApprovedPrice;
            worksheet.Cells[row, 7].Value = claim.non_Add;
            worksheet.Cells[row, 8].Value = claim.non_AddForPerson;
            worksheet.Cells[row, 9].Value = claim.EnduranceRatio;
            worksheet.Cells[row, 10].Value = claim.Hospital?.Name;
            worksheet.Cells[row, 11].Value = claim.Trust;
            worksheet.Cells[row, 12].Value = claim.LoginDate?.ToString("yyyy-MM-dd");
            worksheet.Cells[row, 13].Value = claim.ExitDate?.ToString("yyyy-MM-dd");

            totalAmount += claim.TotalPrice;
            totalCompanyFees += claim.Company_fees;
            totalApprovedPrice += claim.ApprovedPrice;
            totalNonAdd += claim.non_Add;
            totalNonAddForPerson += claim.non_AddForPerson;

            row++;
        }

        // إضافة صفحة ملخص
        var summarySheet = package.Workbook.Worksheets.Add("Summary");
        summarySheet.View.RightToLeft = true;
        summarySheet.Cells[1, 1].Value = "العمود";
        summarySheet.Cells[1, 2].Value = "مجموع القيم";

        summarySheet.Cells[2, 1].Value = "المبلغ الإجمالي";
        summarySheet.Cells[2, 2].Value = totalAmount;

        summarySheet.Cells[3, 1].Value = "رسوم الشركة";
        summarySheet.Cells[3, 2].Value = totalCompanyFees;

        summarySheet.Cells[4, 1].Value = "المبلغ المعتمد";
        summarySheet.Cells[4, 2].Value = totalApprovedPrice;

        summarySheet.Cells[5, 1].Value = "غير المضافة";
        summarySheet.Cells[5, 2].Value = totalNonAdd;

        summarySheet.Cells[6, 1].Value = "غير المضافة للشخص";
        summarySheet.Cells[6, 2].Value = totalNonAddForPerson;

        summarySheet.Cells[7, 1].Value = "عدد المطالبات";
        summarySheet.Cells[7, 2].Value = claims.Count();

        // ضبط حجم الخط للعناوين في صفحة الملخص
        using (var range = summarySheet.Cells[1, 1, 1, 2])
        {
            range.Style.Font.Size = 14;
            range.Style.Font.Bold = true;
        }

        // زيادة عرض الأعمدة في صفحة الملخص
        summarySheet.Column(1).Width = 20;
        summarySheet.Column(2).Width = 25;

        var stream = new MemoryStream(package.GetAsByteArray());
        stream.Position = 0;

        return stream;
    }
}


       
        /*
       // دالة لإنشاء تقرير المطالبات في نطاق تاريخ محدد
       public async Task<Stream> GenerateExcelReportForClimsDateRange(DateTime startDate, DateTime endDate)
       {
           var claims = await _searchService.GetClaimsByDateRange(startDate, endDate);

           using (var package = new ExcelPackage())
           {
               var worksheet = package.Workbook.Worksheets.Add("Claims");

               // تعيين الكتابة من اليمين إلى اليسار
               worksheet.View.RightToLeft = true;

               // إضافة العناوين
               worksheet.Cells[1, 1].Value = "رقم المطالبة";
               worksheet.Cells[1, 2].Value = "رقم التأمين";
               worksheet.Cells[1, 3].Value = "الاسم الكامل";
               worksheet.Cells[1, 4].Value = "المبلغ الإجمالي";
               worksheet.Cells[1, 5].Value = "رسوم الشركة";
               worksheet.Cells[1, 6].Value = "المبلغ المعتمد";
               worksheet.Cells[1, 7].Value = "غير المضافة";
               worksheet.Cells[1, 8].Value = "غير المضافة للشخص";
               worksheet.Cells[1, 9].Value = "نسبة التحمل";
               worksheet.Cells[1, 10].Value = "المستشفى";
               worksheet.Cells[1, 11].Value = "موثوق";
               worksheet.Cells[1, 12].Value = "تاريخ الدخول";
               worksheet.Cells[1, 13].Value = "تاريخ الخروج";

               // ضبط حجم الخط للعناوين
               using (var range = worksheet.Cells[1, 1, 1, 13])
               {
                   range.Style.Font.Size = 14;
                   range.Style.Font.Bold = true;
               }

               // إضافة البيانات
               int row = 2;
               decimal totalAmount = 0;
               foreach (var claim in claims)
               {
                   worksheet.Cells[row, 1].Value = claim.Id;
                   worksheet.Cells[row, 2].Value = claim.EnsuranceNumber;
                   worksheet.Cells[row, 3].Value = claim.FullName;
                   worksheet.Cells[row, 4].Value = claim.TotalPrice;
                   worksheet.Cells[row, 5].Value = claim.Company_fees;
                   worksheet.Cells[row, 6].Value = claim.ApprovedPrice;
                   worksheet.Cells[row, 7].Value = claim.non_Add;
                   worksheet.Cells[row, 8].Value = claim.non_AddForPerson;
                   worksheet.Cells[row, 9].Value = claim.EnduranceRatio;
                   worksheet.Cells[row, 10].Value = claim.Hospital?.Name;
                   worksheet.Cells[row, 11].Value = claim.Trust;
                   worksheet.Cells[row, 12].Value = claim.LoginDate?.ToString("yyyy-MM-dd");
                   worksheet.Cells[row, 13].Value = claim.ExitDate?.ToString("yyyy-MM-dd");

                   totalAmount += claim.TotalPrice;
                   row++;
               }

               // إضافة صفحة ملخص
               var summarySheet = package.Workbook.Worksheets.Add("Summary");
               summarySheet.View.RightToLeft = true;
               summarySheet.Cells[1, 1].Value = "مجموع المبالغ";
               summarySheet.Cells[1, 2].Value = "عدد المطالبات";
               summarySheet.Cells[2, 1].Value = totalAmount;
               summarySheet.Cells[2, 2].Value = claims.Count();

               var stream = new MemoryStream(package.GetAsByteArray());
               stream.Position = 0;

               return stream;
           }
       }
       */
       


       // دالة لإنشاء تقرير جميع المطالبات
       public async Task<Stream> GenerateExcelReportAllClaimsAsync()
{
    var claims = await _searchService.GetAllClaims();

    using (var package = new ExcelPackage())
    {
        var worksheet = package.Workbook.Worksheets.Add("Claims");

        // تعيين الكتابة من اليمين إلى اليسار
        worksheet.View.RightToLeft = true;

        // إضافة العناوين
        worksheet.Cells[1, 1].Value = "رقم المطالبة";
        worksheet.Cells[1, 2].Value = "رقم التأمين";
        worksheet.Cells[1, 3].Value = "الاسم الكامل";
        worksheet.Cells[1, 4].Value = "المبلغ الإجمالي";
        worksheet.Cells[1, 5].Value = "رسوم الشركة";
        worksheet.Cells[1, 6].Value = "المبلغ المعتمد";
        worksheet.Cells[1, 7].Value = "غير المضافة";
        worksheet.Cells[1, 8].Value = "غير المضافة للشخص";
        worksheet.Cells[1, 9].Value = "نسبة التحمل";
        worksheet.Cells[1, 10].Value = "المستشفى";
        worksheet.Cells[1, 11].Value = "موثوق";
        worksheet.Cells[1, 12].Value = "تاريخ الدخول";
        worksheet.Cells[1, 13].Value = "تاريخ الخروج";

        // ضبط حجم الخط للعناوين
        using (var range = worksheet.Cells[1, 1, 1, 13])
        {
            range.Style.Font.Size = 14;
            range.Style.Font.Bold = true;
        }

        worksheet.Column(1).Width = 15;
        worksheet.Column(2).Width = 15;
        worksheet.Column(3).Width = 15;
        worksheet.Column(4).Width = 15;
        worksheet.Column(5).Width = 20;
        worksheet.Column(6).Width = 15;
        worksheet.Column(7).Width = 15;
        worksheet.Column(8).Width = 15;
        worksheet.Column(9).Width = 20;
        worksheet.Column(10).Width = 20;
        worksheet.Column(11).Width = 20;
        worksheet.Column(12).Width = 20;
        worksheet.Column(13).Width = 20;
       

        // إضافة البيانات
        int row = 2;
        decimal totalAmount = 0;
        decimal totalCompanyFees = 0;
        decimal totalApprovedPrice = 0;
        decimal totalNonAdd = 0;
        decimal totalNonAddForPerson = 0;

        foreach (var claim in claims)
        {
            worksheet.Cells[row, 1].Value = claim.Id;
            worksheet.Cells[row, 2].Value = claim.EnsuranceNumber;
            worksheet.Cells[row, 3].Value = claim.FullName;
            worksheet.Cells[row, 4].Value = claim.TotalPrice;
            worksheet.Cells[row, 5].Value = claim.Company_fees;
            worksheet.Cells[row, 6].Value = claim.ApprovedPrice;
            worksheet.Cells[row, 7].Value = claim.non_Add;
            worksheet.Cells[row, 8].Value = claim.non_AddForPerson;
            worksheet.Cells[row, 9].Value = claim.EnduranceRatio;
            worksheet.Cells[row, 10].Value = claim.Hospital?.Name;
            worksheet.Cells[row, 11].Value = claim.Trust;
            worksheet.Cells[row, 12].Value = claim.LoginDate?.ToString("yyyy-MM-dd");
            worksheet.Cells[row, 13].Value = claim.ExitDate?.ToString("yyyy-MM-dd");

            totalAmount += claim.TotalPrice;
            totalCompanyFees += claim.Company_fees;
            totalApprovedPrice += claim.ApprovedPrice;
            totalNonAdd += claim.non_Add;
            totalNonAddForPerson += claim.non_AddForPerson;

            row++;
        }

        // إضافة صفحة ملخص
        var summarySheet = package.Workbook.Worksheets.Add("Summary");
        summarySheet.View.RightToLeft = true;
        summarySheet.Cells[1, 1].Value = "المجموع";
        summarySheet.Cells[1, 2].Value = "عدد المطالبات";
        summarySheet.Cells[1, 3].Value = "مجموع المبالغ الإجمالية";
        summarySheet.Cells[1, 4].Value = "مجموع رسوم الشركة";
        summarySheet.Cells[1, 5].Value = "مجموع المبالغ المعتمدة";
        summarySheet.Cells[1, 6].Value = "مجموع غير المضافة";
        summarySheet.Cells[1, 7].Value = "مجموع غير المضافة للشخص";
        summarySheet.Cells[2, 1].Value = "مجموع الأعمدة المالية";
        summarySheet.Cells[2, 2].Value = claims.Count();
        summarySheet.Cells[2, 3].Value = totalAmount;
        summarySheet.Cells[2, 4].Value = totalCompanyFees;
        summarySheet.Cells[2, 5].Value = totalApprovedPrice;
        summarySheet.Cells[2, 6].Value = totalNonAdd;
        summarySheet.Cells[2, 7].Value = totalNonAddForPerson;

        // تنسيق الخلايا
        using (var range = summarySheet.Cells[1, 1, 2, 7])
        {
            range.Style.Font.Size = 14;
            range.Style.Font.Bold = true;
            range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        }


        summarySheet.Column(1).Width = 20;
        summarySheet.Column(2).Width = 20;
        summarySheet.Column(3).Width = 25;
        summarySheet.Column(4).Width = 25;
        summarySheet.Column(5).Width = 25;
        summarySheet.Column(6).Width = 25;
        summarySheet.Column(7).Width = 25;
       
        

        var stream = new MemoryStream(package.GetAsByteArray());
        stream.Position = 0;

        return stream;
    }
}
    }}