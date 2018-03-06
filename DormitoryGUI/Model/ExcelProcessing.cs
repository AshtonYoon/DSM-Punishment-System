using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
namespace DormitoryGUI.Model
{
    class ExcelProcessing
    {
        // 확장명 XLS (Excel 97~2003 용)
        private const string ConnectStrFrm_Excel97_2003 =
            "Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 8.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";
        // 확장명 XLSX (Excel 2007 이상용)
        private const string ConnectStrFrm_Excel =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 12.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";
        /// <summary>
        ///    Excel 파일의 형태를 반환한다.
        ///    -2 : Error  
        ///    -1 : 엑셀파일아님
        ///     0 : 97-2003 엑셀 파일 (xls)
        ///     1 : 2007 이상 파일 (xlsx)
        /// </summary>
        /// <param name="XlsFile">
        ///    Excel File 명 전체 경로입니다.
        /// </param>
        public static int ExcelFileType(string XlsFile)
        {
            byte[,] ExcelHeader = {
                { 0xD0, 0xCF, 0x11, 0xE0, 0xA1 }, // XLS  File Header
                { 0x50, 0x4B, 0x03, 0x04, 0x14 }  // XLSX File Header
            };

            // result -2=error, -1=not excel , 0=xls , 1=xlsx

            int result = -1;

            FileInfo FI = new FileInfo(XlsFile);
            FileStream FS = FI.Open(FileMode.Open);

            try
            {
                byte[] FH = new byte[5];

                FS.Read(FH, 0, 5);

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (FH[j] != ExcelHeader[i, j]) break;
                        else if (j == 4) result = i;
                    }

                    if (result >= 0) break;
                }
            }
            catch
            {
                result = (-2);
            }
            finally
            {
                FS.Close();
            }

            return result;
        }
        /// <summary>
        ///    Excel 파일을 DataSet 으로 변환하여 반환한다.
        /// </summary>
        /// <param name="FileName">
        ///    Excel File 명 PullPath
        /// </param>
        /// <param name="UseHeader">
        ///    첫번째 줄을 Field 명으로 사용할 것이지 여부
        /// </param>
        private static DataSet OpenExcel(string FileName, bool UseHeader)
        {
            DataSet DS = null;

            string[] HDROpt = { "NO", "YES" };
            string HDR = "";
            string ConnStr = "";

            if (UseHeader)
                HDR = HDROpt[1];
            else
                HDR = HDROpt[0];

            int ExcelType = ExcelFileType(FileName);

            switch (ExcelType)
            {
                case (-2): throw new Exception(FileName + "의 형식검사중 오류가 발생하였습니다.");
                case (-1): throw new Exception(FileName + "은 엑셀 파일형식이 아닙니다.");
                case (0):
                    ConnStr = string.Format(ConnectStrFrm_Excel97_2003, FileName, HDR, "1");
                    break;
                case (1):
                    ConnStr = string.Format(ConnectStrFrm_Excel, FileName, HDR, "1");
                    break;
            }

            OleDbConnection OleDBConn = null;
            OleDbDataAdapter OleDBAdap = null;

            DataTable Schema;

            try
            {
                OleDBConn = new OleDbConnection(ConnStr);
                OleDBConn.Open();

                Schema = OleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                DS = new DataSet();

                foreach (DataRow DR in Schema.Rows)
                {
                    OleDBAdap = new OleDbDataAdapter(DR["TABLE_NAME"].ToString(), OleDBConn);

                    OleDBAdap.SelectCommand.CommandType = CommandType.TableDirect;
                    OleDBAdap.AcceptChangesDuringFill = false;

                    string TableName = DR["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

                    if (DR["TABLE_NAME"].ToString().Contains("$")) OleDBAdap.Fill(DS, TableName);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (OleDBConn != null) OleDBConn.Close();
            }

            return DS;
        }
       
        /// <summary>
        ///    DataSet 을 Excel 파일로 저장한다.
        ///    [ NPOI 라이브러리를 이용한 컨버팅 작업 진행중 ]
        /// </summary>
        /// <param name="FileName">
        ///    Excel File 명 PullPath
        /// </param>
        /// <param name="DS">
        ///    Excel 로 저장할 대상 DataSet 객체.
        /// </param>
        /// <param name="ExistDel">
        ///    동일한 파일명이 있을 때 삭제 할 것인지 여부, 파일이 있고 false 면 저장안하고 그냥 false 를 리턴.
        /// </param>
        /// <param name="OldExcel">
        ///    xls 형태로 저장할 것인지 여부, false 이면 xlsx 형태로 저장함.
        /// </param>

        private static bool SaveExcel(string FileName, DataSet DS, bool ExistDel, bool OldExcel)
        {
            bool result = true;

            if (File.Exists(FileName))
            {
                if (ExistDel) File.Delete(FileName);
                else return result;
            }

            string TempFile = FileName;
            
            // 파일 확장자가 xls 이나 xlsx 가 아니면 아예 파일을 안만들어서
            // 템프파일로 생성후 지정한 파일명으로 변경..

            try
            {
                XSSFWorkbook WB = new XSSFWorkbook();

                foreach (DataTable DT in DS.Tables)
                {
                    XSSFSheet WS = WB.CreateSheet(DT.TableName) as XSSFSheet;
                    XSSFRow HR = WS.CreateRow(0) as XSSFRow;

                    XSSFCellStyle HeaderStyle = WB.CreateCellStyle() as XSSFCellStyle;
                    HeaderStyle.Alignment = HorizontalAlignment.Center;
                    HeaderStyle.VerticalAlignment = VerticalAlignment.Center;

                    XSSFFont HeaderFont = WB.CreateFont() as XSSFFont;
                    HeaderFont.FontHeightInPoints = 11;
                    HeaderFont.FontName = "맑은 고딕";
                    HeaderFont.Boldweight = (short)FontBoldWeight.Bold;

                    HeaderStyle.SetFont(HeaderFont);

                    int[] ColumnWidths = new int[] { 9, 12, 8, 8, 45, 45, 14 };
                    
                    // 엑셀의 헤더 부분(DataTable의 Columns 기록) 정의 및 출력

                    for (int i = 0; i < DT.Columns.Count; i++)
                    {
                        XSSFCell HC = HR.CreateCell(i) as XSSFCell;

                        HC.SetCellValue(DT.Columns[i].ColumnName);
                        HC.CellStyle = HeaderStyle;

                        WS.SetColumnWidth(i, (int)((ColumnWidths[i] + 0.72) * 256));
                    }

                    XSSFCellStyle BodyStyle = WB.CreateCellStyle() as XSSFCellStyle;
                    BodyStyle.Alignment = HorizontalAlignment.Center;
                    BodyStyle.VerticalAlignment = VerticalAlignment.Center;
                    BodyStyle.WrapText = true;

                    XSSFFont BodyFont = WB.CreateFont() as XSSFFont;
                    BodyFont.FontHeightInPoints = 11;
                    BodyFont.FontName = "맑은 고딕";
                    BodyFont.Boldweight = (short)FontBoldWeight.None;

                    BodyStyle.SetFont(BodyFont);
                    
                    // 엑셀의 바디 부분(Datable의 Rows 기록) 정의 및 출력

                    int RowCount = 0;

                    foreach (DataRow DR in DT.Rows)
                    {
                        XSSFRow BR = WS.CreateRow(++RowCount) as XSSFRow;
                        BR.HeightInPoints = 28 * Math.Max(DR["상점 내역"].ToString().Split('\n').Length, DR["벌점 내역"].ToString().Split('\n').Length);

                        int ColumnCount = 0;

                        foreach (DataColumn DC in DT.Columns)
                        {
                            XSSFCell BC = BR.CreateCell(ColumnCount++) as XSSFCell;

                            BC.SetCellValue(DR[DC.ColumnName] as string);
                            BC.CellStyle = BodyStyle;
                        }
                    }
                }

                using (FileStream FS = new FileStream(TempFile, FileMode.Create, FileAccess.Write))
                {
                    WB.Write(FS);
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        ///    Excel 파일을 DataSet 으로 변환하여 반환한다.
        /// </summary>
        /// <param name="ExcelFile">
        ///    읽어올 Excel File 명(전체경로)입니다.
        /// </param>

        public static DataSet OpenExcelDB(string ExcelFile)
        {
            return OpenExcel(ExcelFile, true);
        }

        /// <summary>
        ///    DataSet 을 Excel 파일로 저장한다. 동일 파일명이 있으면 Overwrite 됩니다.
        /// </summary>
        /// <param name="ExcelFile">
        ///    저장할 Excel File 명(전체경로)입니다.
        /// </param>
        /// <param name="DS">
        ///    저장할 대상 DataSet 입니다.
        /// </param>

        public static bool SaveExcelDB(string ExcelFile, DataSet DS)
        {
            return SaveExcel(ExcelFile, DS, true, false);
        }
    }
}