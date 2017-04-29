using System.Collections.Generic;
using System.Data;
using System.IO;

using Excel;
using TeacherDiary.Data.Entities;
using TeacherDiary.Data.Services.Contracts;
using TeacherDiary.Services.Contracts;

namespace TeacherDiary.Services
{
    public class ExelParser : IExelParser
    {
        private readonly IClassService _classService;

        public ExelParser(IClassService classService)
        {
            _classService = classService;
        }

        public void ReadFile(string filePath)
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
            var clases = new List<Class>();

            for (int i = 0; i < result.Tables.Count; i++)
            {
                var @class = new Class();
                @class.Name = result.Tables[i].TableName;

                DataTable sheet = result.Tables[i];

                
                // skip first rows
                for (int j = 2; j < sheet.Rows.Count; j++)
                {
                    if (sheet.Rows[j].ItemArray[0].ToString() == "")
                    {
                        break;
                    }

                    var student = new Student
                    {
                        Number = int.Parse(sheet.Rows[j].ItemArray[0].ToString()),
                        FirstName = sheet.Rows[j].ItemArray[1].ToString(),
                        MiddleName = sheet.Rows[j].ItemArray[2].ToString(),
                        LastName = sheet.Rows[j].ItemArray[3].ToString()
                    };

                    var monthId = 1;

                    for (var k = 4; k <= sheet.Rows[j].ItemArray.Length; k += 2)
                    {
                        if (sheet.Rows[j].ItemArray[k].ToString() == "" && sheet.Rows[j].ItemArray[k+1].ToString() == "")
                        {
                            break;
                        }

                        var absence = new Absence();

                        double excusedAbsence;
                        string excusedAbsenceAsString = sheet.Rows[j].ItemArray[k].ToString();

                        if (double.TryParse(excusedAbsenceAsString, out excusedAbsence))
                        {
                            absence.Excused = excusedAbsence;
                        }
                        else
                        {
                            absence.Excused = 0;
                        }

                        double notExcusedAbsence;
                        string notExcusedAbsenceAsString = sheet.Rows[j].ItemArray[k + 1].ToString();

                        if (double.TryParse(notExcusedAbsenceAsString, out notExcusedAbsence))
                        {
                            absence.NotExcused = notExcusedAbsence;
                        }
                        else
                        {
                            absence.NotExcused = 0;
                        }

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
