using System;
using System.Data;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SQLite;
using System.IO;

namespace cargo_transportation.Classes
{
    public static class DataProcessor
    {

        public static void ExportOrdersToWord(DataTable dt)
        {
            try
            {
                // Создаем документ Word
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Add();

                Word.Paragraph title = document.Content.Paragraphs.Add();
                title.Range.Text = "Отчет по заказам и водителям";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                foreach (DataRow row in dt.Rows)
                {
                    Word.Paragraph orderSection = document.Content.Paragraphs.Add();
                    orderSection.Range.Text = $"Заказ {row["OrderID"]}";
                    orderSection.Range.Font.Size = 14;
                    orderSection.Range.Font.Bold = 1;
                    orderSection.Range.InsertParagraphAfter();

                    orderSection.Range.InsertParagraphAfter();
                    orderSection.Range.Text = $@"
                - Дата: {row["Date"]}
                - Отправитель: {row["Sender"]}
                - Адрес отправителя: {row["SenderAddress"]}
                - Адрес получателя: {row["RecipientAddress"]}
                - Длина поездки: {row["TripLength"]} км
                - Стоимость: {row["Cost"]} руб.";

                    orderSection.Range.InsertParagraphAfter();

                    Word.Paragraph driverSection = document.Content.Paragraphs.Add();
                    driverSection.Range.Text = "Водитель:";
                    driverSection.Range.Font.Size = 12;
                    driverSection.Range.Font.Bold = 0;
                    driverSection.Range.InsertParagraphAfter();

                    driverSection.Range.Text = $@"
                - Имя: {row["DriverName"]}
                - Табельный номер: {row["TableNumber"]}
                - Дата рождения: {row["DateOfBirth"]}
                - Опыт: {row["Experience"]} лет";

                    driverSection.Range.InsertParagraphAfter();
                    driverSection.Range.InsertParagraphAfter();
                }

                // Сохраняем документ
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Word Document (*.docx)|*.docx",
                    Title = "Сохранить отчет"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        document.SaveAs2(saveFileDialog.FileName);
                        wordApp.Visible = true;
                    }
                    else
                    {
                        document.Close();
                        wordApp.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportOrdersToExcel(DataTable dt)
        {
            try
            {

                // Создаем Excel файл
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = workbook.ActiveSheet;

                // Записываем заголовки
                worksheet.Cells[1, 1] = "ID заказа";
                worksheet.Cells[1, 2] = "Дата";
                worksheet.Cells[1, 3] = "Отправитель";
                worksheet.Cells[1, 4] = "Адрес отправителя";
                worksheet.Cells[1, 5] = "Адрес получателя";
                worksheet.Cells[1, 6] = "Длина поездки";
                worksheet.Cells[1, 7] = "Стоимость";
                worksheet.Cells[1, 8] = "Имя водителя";
                worksheet.Cells[1, 9] = "Табельный номер";
                worksheet.Cells[1, 10] = "Дата рождения";
                worksheet.Cells[1, 11] = "Опыт";

                int row = 2;

                foreach (DataRow dataRow in dt.Rows)
                {
                    worksheet.Cells[row, 1] = dataRow["OrderID"];
                    worksheet.Cells[row, 2] = dataRow["Date"];
                    worksheet.Cells[row, 3] = dataRow["Sender"];
                    worksheet.Cells[row, 4] = dataRow["SenderAddress"];
                    worksheet.Cells[row, 5] = dataRow["RecipientAddress"];
                    worksheet.Cells[row, 6] = dataRow["TripLength"];
                    worksheet.Cells[row, 7] = dataRow["Cost"];
                    worksheet.Cells[row, 8] = dataRow["DriverName"];
                    worksheet.Cells[row, 9] = dataRow["TableNumber"];
                    worksheet.Cells[row, 10] = dataRow["DateOfBirth"];
                    worksheet.Cells[row, 11] = dataRow["Experience"];
                    row++;
                }

                // Сохраняем Excel файл
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    Title = "Сохранить отчет"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        excelApp.Visible = true;
                    }
                    else
                    {
                        workbook.Close(false);
                        excelApp.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportTripsToWord(DataTable dt)
        {
            try
            {
                // Создаем документ Word
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Add();

                Word.Paragraph title = document.Content.Paragraphs.Add();
                title.Range.Text = "Отчет по поездкам и машинам";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                foreach (DataRow row in dt.Rows)
                {
                    Word.Paragraph tripSection = document.Content.Paragraphs.Add();
                    tripSection.Range.Text = $"Поездка №{row["TripID"]}";
                    tripSection.Range.Font.Size = 14;
                    tripSection.Range.Font.Bold = 1;
                    tripSection.Range.InsertParagraphAfter();

                    tripSection.Range.InsertParagraphAfter();
                    tripSection.Range.Text = $@"
                - Государственный номер машины: {row["CarStateNumber"]}
                - Бренд машины: {row["CarBrandName"]}
                - Модель машины: {row["CarModel"]}
                - Принадлежность: {row["CarUsage"]}
                - Дата выпуска: {row["CarIssueDate"]}
                - Дата ремонта: {row["CarRepairDate"]}
                - Пробег: {row["CarMileage"]}
                - Дата прибытия: {row["TripArrivalDate"]}";
                    
                    tripSection.Range.InsertParagraphAfter();
                    tripSection.Range.InsertParagraphAfter();
                }

                // Сохраняем документ
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Word Document (*.docx)|*.docx",
                    Title = "Сохранить отчет"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        document.SaveAs2(saveFileDialog.FileName);
                        wordApp.Visible = true;
                    }
                    else
                    {
                        document.Close();
                        wordApp.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportTripsToExcel(DataTable dt)
        {
            try
            {

                // Создаем Excel файл
                var excelApp = new Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = workbook.ActiveSheet;

                // Записываем заголовки
                worksheet.Cells[1, 1] = "ID поездки";
                worksheet.Cells[1, 2] = "Государственный номер машины";
                worksheet.Cells[1, 3] = "Бренд машины";
                worksheet.Cells[1, 4] = "Модель машины";
                worksheet.Cells[1, 5] = "Принадлежность";
                worksheet.Cells[1, 6] = "Дата выпуска";
                worksheet.Cells[1, 7] = "Дата ремонта";
                worksheet.Cells[1, 8] = "Пробег";
                worksheet.Cells[1, 9] = "Дата прибытия";

                int row = 2;

                foreach (DataRow dataRow in dt.Rows)
                {
                    worksheet.Cells[row, 1] = dataRow["TripID"];
                    worksheet.Cells[row, 2] = dataRow["CarStateNumber"];
                    worksheet.Cells[row, 3] = dataRow["CarBrandName"];
                    worksheet.Cells[row, 4] = dataRow["CarModel"];
                    worksheet.Cells[row, 5] = dataRow["CarUsage"];
                    worksheet.Cells[row, 6] = dataRow["CarIssueDate"];
                    worksheet.Cells[row, 7] = dataRow["CarRepairDate"];
                    worksheet.Cells[row, 8] = dataRow["CarMileage"];
                    worksheet.Cells[row, 9] = dataRow["TripArrivalDate"];
                    row++;
                }

                // Сохраняем Excel файл
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    Title = "Сохранить отчет"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        excelApp.Visible = true;
                    }
                    else
                    {
                        workbook.Close(false);
                        excelApp.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
