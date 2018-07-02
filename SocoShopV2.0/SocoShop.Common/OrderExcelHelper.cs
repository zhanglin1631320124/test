namespace SocoShop.Common
{
    using Microsoft.Office.Interop.Excel;
    using SkyCES.EntLib;
    using System;

    public class OrderExcelHelper : ExcelHelper
    {
        public OrderExcelHelper(string templetFilePath, string outputFilePath) : base(templetFilePath, outputFilePath)
        {
        }

        protected override void FillData(Workbook workBook, int sheetCount)
        {
            int count = base.Dt.Rows.Count;
            int num2 = base.Dt.Columns.Count;
            for (int i = 1; i <= sheetCount; i++)
            {
                int num4 = (i - 1) * base.Rows;
                int num5 = i * base.Rows;
                if (i == sheetCount) num5 = count;
                Worksheet sheet = (Worksheet) workBook.Worksheets.get_Item(i);
                sheet.Name = base.SheetPrefixName + "-" + i.ToString();
                for (int j = 0; j < num5 - num4; j++)
                {
                    for (int k = 0; k < num2; k++)
                    {
                        if (k < 2)
                            sheet.Cells[base.Top + j, base.Left + k] = base.Dt.Rows[num4 + j][k].ToString();
                        else
                            sheet.Cells[base.Top + j, base.Left + k + 2] = base.Dt.Rows[num4 + j][k].ToString();
                    }
                }
                base.SetCellParameters(sheet);
            }
        }
    }
}

