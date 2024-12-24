using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace cargo_transportation.Classes
{
    public static class DataProcessor
    {

        public static void ExportOrdersToWord()
        {
            try
            {
                DataTable dt = new DataTable();
                var query = $@"
                SELECT 
                    o.ID AS OrderID, 
                    o.Date, 
                    o.Sender, 
                    o.SenderAddress, 
                    o.RecipientAddress, 
                    o.TripLength, 
                    o.Cost, 
                    t.Car, 
                    d.FullName AS DriverName, 
                    d.TableNumber, 
                    d.DateOfBirth, 
                    d.Experience 
                FROM 'Order' o
                INNER JOIN Trip t ON o.Trip = t.ID
                INNER JOIN Trip_List tl ON tl.ID = t.ID
                INNER JOIN Driver d ON d.ID = tl.DriverID";

                Database.ReadData("Databases\\make.db", query, dt);

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

        public static void ExportOrdersToExcel()
        {
            try
            {

                DataTable dt = new DataTable();
                var query = $@"
                SELECT 
                    o.ID AS OrderID, 
                    o.Date, 
                    o.Sender, 
                    o.SenderAddress, 
                    o.RecipientAddress, 
                    o.TripLength, 
                    o.Cost, 
                    t.Car, 
                    d.FullName AS DriverName, 
                    d.TableNumber, 
                    d.DateOfBirth, 
                    d.Experience 
                FROM 'Order' o
                INNER JOIN Trip t ON o.Trip = t.ID
                INNER JOIN Trip_List tl ON tl.ID = t.ID
                INNER JOIN Driver d ON d.ID = tl.DriverID";

                Database.ReadData("Databases\\make.db", query, dt);

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

        public static void ExportAllCargoToWord()
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT ID, Name, Unit, Weight FROM Cargo;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем документ Word
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Add();

                // Заголовок документа
                Word.Paragraph title = document.Content.Paragraphs.Add();
                title.Range.Text = "Отчет о грузах";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                // Заполнение данными
                foreach (DataRow row in dt.Rows)
                {
                    Word.Paragraph cargoSection = document.Content.Paragraphs.Add();
                    cargoSection.Range.Text = $"Груз {row["ID"]}";
                    cargoSection.Range.Font.Size = 14;
                    cargoSection.Range.Font.Bold = 1;
                    cargoSection.Range.InsertParagraphAfter();

                    cargoSection.Range.Text = $@"
                - Название: {row["Name"]}
                - Единица измерения: {row["Unit"]}
                - Вес: {row["Weight"]} кг";
                    cargoSection.Range.InsertParagraphAfter();
                }

                // Сохранение документа
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

        public static void ExportCarsRepairedBeforeToWord(DateTime repairDate)
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = @"
                SELECT 
                    c.ID, 
                    c.StateNumber, 
                    b.Name AS BrandName, 
                    c.Model, 
                    c.WeightLimit, 
                    u.Info AS UsageInfo, 
                    c.IssueDate, 
                    c.RepairDate, 
                    c.Mileage
                FROM Car_List c
                INNER JOIN Car_Brand b ON c.BrandID = b.ID
                INNER JOIN Usage_List u ON c.Usage = u.ID
                WHERE date(substr(c.RepairDate, 7, 4) || '-' || substr(c.RepairDate, 4, 2) || '-' || substr(c.RepairDate, 1, 2)) <= date(@RepairDate);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RepairDate", repairDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем документ Word
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Add();

                // Заголовок документа
                Word.Paragraph title = document.Content.Paragraphs.Add();
                title.Range.Text = $"Отчет о машинах с ремонтом до {repairDate:dd.MM.yyyy}";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                // Заполнение данными
                foreach (DataRow row in dt.Rows)
                {
                    Word.Paragraph carSection = document.Content.Paragraphs.Add();
                    carSection.Alignment = Word.WdParagraphAlignment.wdAlignParagraphDistribute;
                    carSection.Range.Text = $"Машина {row["ID"]}";
                    carSection.Range.Font.Size = 14;
                    carSection.Range.Font.Bold = 1;
                    carSection.Range.InsertParagraphAfter();

                    carSection.Range.Text = $@"
- Гос. номер: {row["StateNumber"]}
- Марка: {row["BrandName"]}
- Модель: {row["Model"]}
- Грузоподъемность: {row["WeightLimit"]} кг
- Использование: {row["UsageInfo"]}
- Дата выпуска: {row["IssueDate"]}
- Дата последнего ремонта: {row["RepairDate"]}
- Пробег: {row["Mileage"]} км";
                    carSection.Range.InsertParagraphAfter();
                    carSection.Range.InsertParagraphAfter();
                }

                // Сохранение документа
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

        public static void ExportCarsRepairedBeforeToExcel(DateTime repairDate)
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";

                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных о машинах с фильтрацией по дате ремонта
                    string query = @"
                SELECT 
                    c.ID, 
                    cb.Name AS Brand, 
                    c.StateNumber, 
                    c.Model, 
                    c.WeightLimit, 
                    ul.Info AS Usage, 
                    c.IssueDate, 
                    c.RepairDate, 
                    c.Mileage
                FROM Car_List c
                JOIN Car_Brand cb ON c.BrandID = cb.ID
                JOIN Usage_List ul ON c.Usage = ul.ID
                WHERE date(substr(c.RepairDate, 7, 4) || '-' || substr(c.RepairDate, 4, 2) || '-' || substr(c.RepairDate, 1, 2)) <= date(@RepairDate);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Добавляем параметр для фильтрации по дате
                        command.Parameters.AddWithValue("@RepairDate", repairDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем Excel приложение
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                // Заголовок отчета
                worksheet.Cells[1, 1] = $"Отчет о машинах до заданной даты ремонта: {repairDate.ToString("dd.MM.yyyy")}";
                var headerRange = worksheet.Range["A1", "I1"];
                headerRange.Merge();
                headerRange.Font.Size = 16;
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Заголовки колонок
                string[] columns = { "ID", "Марка", "Гос. номер", "Модель", "Лимит веса", "Использование", "Дата выпуска", "Дата ремонта", "Пробег" };
                for (int i = 0; i < columns.Length; i++)
                {
                    worksheet.Cells[2, i + 1] = columns[i];
                }

                // Заполнение данных
                int rowIndex = 3;
                foreach (DataRow row in dt.Rows)
                {
                    worksheet.Cells[rowIndex, 1] = row["ID"].ToString();
                    worksheet.Cells[rowIndex, 2] = row["Brand"].ToString();
                    worksheet.Cells[rowIndex, 3] = row["StateNumber"].ToString();
                    worksheet.Cells[rowIndex, 4] = row["Model"].ToString();
                    worksheet.Cells[rowIndex, 5] = row["WeightLimit"].ToString();
                    worksheet.Cells[rowIndex, 6] = row["Usage"].ToString();
                    worksheet.Cells[rowIndex, 7] = row["IssueDate"].ToString();
                    worksheet.Cells[rowIndex, 8] = row["RepairDate"].ToString();
                    worksheet.Cells[rowIndex, 9] = row["Mileage"].ToString();
                    rowIndex++;
                }

                // Автоматическая настройка ширины колонок
                worksheet.Columns.AutoFit();

                // Сохранение файла Excel
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

        public static void ExportAllCargoToExcel()
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT ID, Name, Unit, Weight FROM Cargo;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем Excel приложение
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                // Заголовок отчета
                worksheet.Cells[1, 1] = "Отчет о грузах";
                var headerRange = worksheet.Range["A1", "D1"];
                headerRange.Merge();
                headerRange.Font.Size = 16;
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Заголовки колонок
                string[] columns = { "ID груза", "Название", "Единица измерения", "Вес" };
                for (int i = 0; i < columns.Length; i++)
                {
                    worksheet.Cells[2, i + 1] = columns[i];
                }

                // Заполнение данных
                int rowIndex = 3;
                foreach (DataRow row in dt.Rows)
                {
                    worksheet.Cells[rowIndex, 1] = row["ID"].ToString();
                    worksheet.Cells[rowIndex, 2] = row["Name"].ToString();
                    worksheet.Cells[rowIndex, 3] = row["Unit"].ToString();
                    worksheet.Cells[rowIndex, 4] = row["Weight"].ToString();
                    rowIndex++;
                }

                // Автоматическая настройка ширины колонок
                worksheet.Columns.AutoFit();

                // Сохранение файла Excel
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

        public static void ExportDeliveredOrdersToWord(DateTime deliveryDate)
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Подготовка DataTable
                DataTable dt = new DataTable();

                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    string query = @"
                    SELECT 
                        o.ID, 
                        o.Date, 
                        o.Sender, 
                        o.SenderAddress, 
                        o.RecipientAddress, 
                        o.TripLength, 
                        o.Cost, 
                        t.ArrivalDate 
                    FROM [Order] o 
                    INNER JOIN Trip t ON o.Trip = t.ID 
                    WHERE date(substr(t.ArrivalDate, 7, 4) || '-' || substr(t.ArrivalDate, 4, 2) || '-' || substr(t.ArrivalDate, 1, 2)) <= date(@DeliveryDate);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeliveryDate", deliveryDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }

                    connection.Close();
                }

                // Создаем документ Word
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Add();

                Word.Paragraph title = document.Content.Paragraphs.Add();
                title.Range.Text = $"Отчет о доставленных заказах до {deliveryDate:dd.MM.yyyy}";
                title.Range.Font.Size = 16;
                title.Range.Font.Bold = 1;
                title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                title.Range.InsertParagraphAfter();

                foreach (DataRow row in dt.Rows)
                {
                    DateTime arrivalDate = DateTime.ParseExact(row["ArrivalDate"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    Word.Paragraph orderSection = document.Content.Paragraphs.Add();
                    orderSection.Range.Text = $"Заказ {row["ID"]}";
                    orderSection.Range.Font.Size = 14;
                    orderSection.Range.Font.Bold = 1;
                    orderSection.Alignment = Word.WdParagraphAlignment.wdAlignParagraphDistribute;
                    orderSection.Range.InsertParagraphAfter();

                    orderSection.Range.Text = $@"
                    - Дата заказа: {row["Date"]}
                    - Отправитель: {row["Sender"]}
                    - Адрес отправителя: {row["SenderAddress"]}
                    - Адрес получателя: {row["RecipientAddress"]}
                    - Длина поездки: {row["TripLength"]} км
                    - Стоимость: {row["Cost"]} руб.
                    - Дата доставки: {arrivalDate:dd.MM.yyyy}";

                    orderSection.Range.InsertParagraphAfter();
                    orderSection.Range.InsertParagraphAfter();
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

        public static void ExportDeliveredOrdersToExcel(DateTime deliveryDate)
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = @"
                    SELECT 
                        o.ID, 
                        o.Date, 
                        o.Sender, 
                        o.SenderAddress, 
                        o.RecipientAddress, 
                        o.TripLength, 
                        o.Cost, 
                        t.ArrivalDate 
                    FROM [Order] o 
                    INNER JOIN Trip t ON o.Trip = t.ID 
                    WHERE date(substr(t.ArrivalDate, 7, 4) || '-' || substr(t.ArrivalDate, 4, 2) || '-' || substr(t.ArrivalDate, 1, 2)) <= date(@DeliveryDate);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeliveryDate", deliveryDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем Excel приложение
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                // Заголовок отчета
                worksheet.Cells[1, 1] = $"Отчет о доставленных заказах до {deliveryDate:dd.MM.yyyy}";
                var headerRange = worksheet.Range["A1", "G1"];
                headerRange.Merge();
                headerRange.Font.Size = 16;
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Заголовки колонок
                string[] columns = { "ID заказа", "Дата заказа", "Отправитель", "Адрес отправителя", "Адрес получателя", "Длина поездки (км)", "Стоимость (руб.)", "Дата доставки" };
                for (int i = 0; i < columns.Length; i++)
                {
                    worksheet.Cells[2, i + 1] = columns[i];
                }

                // Заполнение данных
                int rowIndex = 3;
                foreach (DataRow row in dt.Rows)
                {
                    worksheet.Cells[rowIndex, 1] = row["ID"].ToString();
                    worksheet.Cells[rowIndex, 2] = row["Date"].ToString();
                    worksheet.Cells[rowIndex, 3] = row["Sender"].ToString();
                    worksheet.Cells[rowIndex, 4] = row["SenderAddress"].ToString();
                    worksheet.Cells[rowIndex, 5] = row["RecipientAddress"].ToString();
                    worksheet.Cells[rowIndex, 6] = row["TripLength"].ToString();
                    worksheet.Cells[rowIndex, 7] = row["Cost"].ToString();
                    worksheet.Cells[rowIndex, 8] = DateTime.ParseExact(row["ArrivalDate"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    rowIndex++;
                }

                // Автоматическая настройка ширины колонок
                worksheet.Columns.AutoFit();

                // Сохранение файла Excel
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

        public static void ExportAllAvailableDriversToExcel()
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";
                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки свободных водителей
                    string query = @"
                SELECT D.ID, D.FullName, D.TableNumber, D.DateOfBirth, D.Experience, D.Category, D.Class
                FROM Driver D
                WHERE NOT EXISTS (
                    SELECT 1 FROM Trip_List T WHERE T.DriverID = D.ID
                );
            ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем Excel приложение
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                // Заголовок отчета
                worksheet.Cells[1, 1] = "Отчет о свободных водителях";
                var headerRange = worksheet.Range["A1", "G1"];
                headerRange.Merge();
                headerRange.Font.Size = 16;
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Заголовки колонок
                string[] columns = { "ID водителя", "ФИО", "Номер в таблице", "Дата рождения", "Стаж", "Категория", "Класс" };
                for (int i = 0; i < columns.Length; i++)
                {
                    worksheet.Cells[2, i + 1] = columns[i];
                }

                // Заполнение данных
                int rowIndex = 3;
                foreach (DataRow row in dt.Rows)
                {
                    worksheet.Cells[rowIndex, 1] = row["ID"].ToString();
                    worksheet.Cells[rowIndex, 2] = row["FullName"].ToString();
                    worksheet.Cells[rowIndex, 3] = row["TableNumber"].ToString();
                    worksheet.Cells[rowIndex, 4] = row["DateOfBirth"].ToString();
                    worksheet.Cells[rowIndex, 5] = row["Experience"].ToString();
                    worksheet.Cells[rowIndex, 6] = row["Category"].ToString();
                    worksheet.Cells[rowIndex, 7] = row["Class"].ToString();
                    rowIndex++;
                }

                // Автоматическая настройка ширины колонок
                worksheet.Columns.AutoFit();

                // Сохранение файла Excel
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

        public static void ExportAllAvailableDriversToWord()
        {
            try
            {
                string fullPath = $"Data Source='Databases\\make.db';Version=3; FailIfMissing=False";

                // Создаем DataTable для хранения данных
                DataTable dt = new DataTable();

                // Подключение к базе данных SQLite
                using (var connection = new SQLiteConnection(fullPath))
                {
                    connection.Open();

                    // SQL-запрос для выборки свободных водителей
                    string query = @"
                SELECT D.ID, D.FullName, D.TableNumber, D.DateOfBirth, D.Experience, D.Category, D.Class
                FROM Driver D
                WHERE NOT EXISTS (
                    SELECT 1 FROM Trip_List T WHERE T.DriverID = D.ID
                );
            ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Создаем Word приложение
                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var document = wordApp.Documents.Add();

                // Заголовок отчета
                var titleRange = document.Paragraphs.Add().Range;
                titleRange.Text = "Отчет о свободных водителях";
                titleRange.Font.Size = 16;
                titleRange.Font.Bold = 1;
                titleRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                titleRange.InsertParagraphAfter();

                // Создаем таблицу
                var table = document.Tables.Add(document.Paragraphs.Last.Range, dt.Rows.Count + 1, 7);
                table.Range.Font.Size = 12;

                // Заголовки колонок
                table.Cell(1, 1).Range.Text = "ID водителя";
                table.Cell(1, 2).Range.Text = "ФИО";
                table.Cell(1, 3).Range.Text = "Номер в таблице";
                table.Cell(1, 4).Range.Text = "Дата рождения";
                table.Cell(1, 5).Range.Text = "Стаж";
                table.Cell(1, 6).Range.Text = "Категория";
                table.Cell(1, 7).Range.Text = "Класс";

                // Заполнение данных
                int rowIndex = 2;
                foreach (DataRow row in dt.Rows)
                {
                    table.Cell(rowIndex, 1).Range.Text = row["ID"].ToString();
                    table.Cell(rowIndex, 2).Range.Text = row["FullName"].ToString();
                    table.Cell(rowIndex, 3).Range.Text = row["TableNumber"].ToString();
                    table.Cell(rowIndex, 4).Range.Text = row["DateOfBirth"].ToString();
                    table.Cell(rowIndex, 5).Range.Text = row["Experience"].ToString();
                    table.Cell(rowIndex, 6).Range.Text = row["Category"].ToString();
                    table.Cell(rowIndex, 7).Range.Text = row["Class"].ToString();
                    rowIndex++;
                }

                // Сохранение документа Word
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

    }
}
