using System.Collections.Generic;
using System.Data;
using System.IO;

using Excel;

using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;

namespace TeachersDiary.Services.ExcelParser
{
    public class ExelParser : IExelParser
    {
        private readonly IClassService _classService;

        public ExelParser(IClassService classService)
        {
            _classService = classService;
        }

        public void CreateClassForUser(string filePath, string userId)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //Choose one of either 1 or 2
            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            //Choose one of either 3, 4, or 5
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();

            //4. DataSet - Create column names from first row
            //excelReader.IsFirstRowAsColumnNames = true;
            //DataSet result = excelReader.AsDataSet();

            //5. Data Reader methods
            var clases = new List<ClassDomain>();

            for (int sheetIndex = 0; sheetIndex < result.Tables.Count; sheetIndex++)
            {
                var @class = new ClassDomain
                {
                    Name = result.Tables[sheetIndex].TableName,
                    CreatedBy = userId,
                    // TODO works only for Blagoev
                    SchoolId = 1
                };

                var sheet = result.Tables[sheetIndex];

                // skip first rows
                for (var row = 2; row < sheet.Rows.Count; row++)
                {
                    if (sheet.Rows[row].ItemArray[0].ToString() == "")
                    {
                        break;
                    }

                    var student = new StudentDomain
                    {
                        Number = int.Parse(sheet.Rows[row].ItemArray[0].ToString()),
                        FirstName = sheet.Rows[row].ItemArray[1].ToString(),
                        MiddleName = sheet.Rows[row].ItemArray[2].ToString(),
                        LastName = sheet.Rows[row].ItemArray[3].ToString()
                    };

                    var monthId = 1;

                    // start from 4 because in exel file absenses starts from 4 column
                    for (var col = 4; col <= sheet.Rows[row].ItemArray.Length; col += 2)
                    {
                        if (sheet.Rows[row].ItemArray[col].ToString() == "" && sheet.Rows[row].ItemArray[col + 1].ToString() == "")
                        {
                            break;
                        }

                        var absence = new AbsenceDomain();

                        var excusedAbsenceAsString = sheet.Rows[row].ItemArray[col].ToString();
                        absence.Excused = double.TryParse(excusedAbsenceAsString, out double excusedAbsence) ? excusedAbsence : 0;

                        var notExcusedAbsenceAsString = sheet.Rows[row].ItemArray[col + 1].ToString();
                        absence.NotExcused = double.TryParse(notExcusedAbsenceAsString, out double notExcusedAbsence) ? notExcusedAbsence : 0;

                        absence.MonthId = monthId;

                        student.Absences.Add(absence);
                        monthId++;
                    }

                    @class.Students.Add(student);
                }

                clases.Add(@class);
            }

            _classService.AddRange(clases);

            excelReader.Close();
        }
    }
}
