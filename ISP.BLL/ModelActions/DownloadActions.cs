using ISP.DAL.DBModels;
using SautinSoft;
using SautinSoft.Document;
using SautinSoft.Document.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.BLL.ModelActions
{
    public class DownloadActions
    {
        public Stream DownloadTXT(InternetPackage item)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);

            streamWriter.WriteLine("Информация предоставлена (UTC): {0}", DateTime.UtcNow);
            streamWriter.WriteLine("Название пакета интернет услуг: {0}", item.Name);
            streamWriter.WriteLine("Скорость приёма: {0}", item.DownloadSpeed);
            streamWriter.WriteLine("Скорость отдачи: {0}", item.UploadSpeed);
            streamWriter.WriteLine("Стоимость в месяц: {0}", item.Price);

            streamWriter.Flush();
            return stream;
        }
        public Stream DownloadTXT(IEnumerable<InternetPackage> items)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);

            streamWriter.WriteLine("Информация предоставлена (UTC): {0}", DateTime.UtcNow);
            foreach (InternetPackage internetPackage in items)
            {
                streamWriter.WriteLine("\tНазвание пакета интернет услуг: {0}", internetPackage.Name);
                streamWriter.WriteLine("\tСкорость приёма: {0}", internetPackage.DownloadSpeed);
                streamWriter.WriteLine("\tСкорость отдачи: {0}", internetPackage.UploadSpeed);
                streamWriter.WriteLine("\tСтоимость в месяц: {0}", internetPackage.Price);
                streamWriter.WriteLine();
            }

            streamWriter.Flush();
            return stream;
        }
        public Stream DownloadTXT(IEnumerable<TVChannel> items)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);

            streamWriter.WriteLine("Информация предоставлена (UTC): {0}", DateTime.UtcNow);
            foreach (TVChannel tvChannel in items)
            {
                string packages = string.Join(",", tvChannel.Packages.Select(item => item.Name));
                streamWriter.WriteLine("\tНазвание ТВ канала: {0}", tvChannel.Name);
                streamWriter.WriteLine("\tНаличие IPTV: {0}", (tvChannel.IsIPTV ? "Да" : "Нет"));
                streamWriter.WriteLine("\tНаличие TP: {0}", (tvChannel.IsIPTV ? "Да" : "Нет"));
                streamWriter.WriteLine("\tВходит в пакеты: {0}", packages);
                streamWriter.WriteLine("\tСтоимость в месяц: {0}", tvChannel.Price);
                streamWriter.WriteLine();
            }

            streamWriter.Flush();
            return stream;
        }
        public Stream DownloadTXT(TVChannelPackage item)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);

            streamWriter.WriteLine("Информация предоставлена (UTC): {0}", DateTime.UtcNow);
            streamWriter.WriteLine("Название пакета ТВ: {0}", item.Name);
            streamWriter.WriteLine("Количество каналов в пакете: {0}", item.Channels.Count());
            streamWriter.WriteLine("Стоимость в месяц: {0}", item.Price);
            streamWriter.WriteLine("Список каналов:");
            foreach (TVChannel tvChannel in item.Channels)
            {
                streamWriter.WriteLine("\tНазвание канала: {0}", tvChannel.Name);
                streamWriter.WriteLine("\t\tНаличие IPTV: {0}", (tvChannel.IsIPTV ? "Да" : "Нет"));
                streamWriter.WriteLine("\t\tНаличие TV: {0}", (tvChannel.IsTV ? "Да" : "Нет"));
            }

            streamWriter.Flush();
            return stream;
        }

        public Stream DownloadPDF(InternetPackage item)
        {
            PdfMetamorphosis docxConverter = new PdfMetamorphosis();

            if (docxConverter == null)
                return null;

            MemoryStream docxFileStream = (DownloadDOCX(item) as MemoryStream);
            byte[] pdfFile = docxConverter.DocxToPdfConvertByte(docxFileStream.ToArray());
            MemoryStream pdfFileStream = new MemoryStream(pdfFile);

            return pdfFileStream;
        }
        public Stream DownloadPDF(IEnumerable<InternetPackage> items)
        {
            PdfMetamorphosis docxConverter = new PdfMetamorphosis();

            if (docxConverter == null)
                return null;

            MemoryStream docxFileStream = (DownloadDOCX(items) as MemoryStream);
            byte[] pdfFile = docxConverter.DocxToPdfConvertByte(docxFileStream.ToArray());
            MemoryStream pdfFileStream = new MemoryStream(pdfFile);

            return pdfFileStream;
        }
        public Stream DownloadPDF(IEnumerable<TVChannel> items)
        {
            PdfMetamorphosis docxConverter = new PdfMetamorphosis();

            if (docxConverter == null)
                return null;

            MemoryStream docxFileStream = (DownloadDOCX(items) as MemoryStream);
            byte[] pdfFile = docxConverter.DocxToPdfConvertByte(docxFileStream.ToArray());
            MemoryStream pdfFileStream = new MemoryStream(pdfFile);

            return pdfFileStream;
        }
        public Stream DownloadPDF(TVChannelPackage item)
        {
            PdfMetamorphosis docxConverter = new PdfMetamorphosis();

            if (docxConverter == null)
                return null;

            MemoryStream docxFileStream = (DownloadDOCX(item) as MemoryStream);
            byte[] pdfFile = docxConverter.DocxToPdfConvertByte(docxFileStream.ToArray());
            MemoryStream pdfFileStream = new MemoryStream(pdfFile);

            return pdfFileStream;
        }

        public Stream DownloadDOCX(InternetPackage item)
        {
            MemoryStream stream = new MemoryStream();

            DocumentCore docx = new DocumentCore();
            Section section = new Section(docx);
            docx.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;

            section.Blocks.Add(new Paragraph(docx, string.Format("Информация предоставлена (UTC): {0}", DateTime.UtcNow)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Название пакета интернет услуг: {0}", item.Name)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Скорость приёма: {0}", item.DownloadSpeed)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Скорость отдачи: {0}", item.UploadSpeed)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Стоимость в месяц: {0}", item.Price)));

            docx.Save(stream, SaveOptions.DocxDefault);
            return stream;
        }
        public Stream DownloadDOCX(IEnumerable<InternetPackage> items)
        {
            MemoryStream stream = new MemoryStream();

            DocumentCore docx = new DocumentCore();
            Section section = new Section(docx);
            docx.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;

            section.Blocks.Add(new Paragraph(docx, string.Format("Информация предоставлена (UTC): {0}", DateTime.UtcNow)));

            Table table = new Table(docx);
            table.TableFormat.Alignment = HorizontalAlignment.Left;

            TableRow tableRowHeader = new TableRow(docx);
            TableCell tableRowCellNameHeader = AddCellToDOCXTable("Название пакета интернет услуг", ref docx, ref tableRowHeader);
            TableCell tableRowCellDownloadSpeedHeader = AddCellToDOCXTable("Скорость приёма", ref docx, ref tableRowHeader);
            TableCell tableRowCellUploadSpeedHeader = AddCellToDOCXTable("Скорость отдачи", ref docx, ref tableRowHeader);
            TableCell tableRowCellPriceHeader = AddCellToDOCXTable("Стоимость в месяц", ref docx, ref tableRowHeader);
            table.Rows.Add(tableRowHeader);

            foreach (InternetPackage internetPackage in items)
            {
                TableRow tableRow = new TableRow(docx);
                TableCell tableRowCellName = AddCellToDOCXTable(internetPackage.Name, ref docx, ref tableRow);
                TableCell tableRowCellDownloadSpeed = AddCellToDOCXTable(internetPackage.DownloadSpeed.ToString(), ref docx, ref tableRow);
                TableCell tableRowCellUploadSpeed = AddCellToDOCXTable(internetPackage.UploadSpeed.ToString(), ref docx, ref tableRow);
                TableCell tableRowCellPrice = AddCellToDOCXTable(internetPackage.Price.ToString(), ref docx, ref tableRow);
                table.Rows.Add(tableRow);
            }
            section.Blocks.Add(table);

            docx.Save(stream, SaveOptions.DocxDefault);
            return stream;
        }
        public Stream DownloadDOCX(IEnumerable<TVChannel> items)
        {
            MemoryStream stream = new MemoryStream();

            DocumentCore docx = new DocumentCore();
            Section section = new Section(docx);
            docx.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;

            section.Blocks.Add(new Paragraph(docx, string.Format("Информация предоставлена (UTC): {0}", DateTime.UtcNow)));

            Table table = new Table(docx);
            table.TableFormat.Alignment = HorizontalAlignment.Left;

            TableRow tableRowHeader = new TableRow(docx);
            TableCell tableRowCellNameHeader = AddCellToDOCXTable("Название ТВ канала", ref docx, ref tableRowHeader);
            TableCell tableRowCellIPTVHeader = AddCellToDOCXTable("Наличие IPTV", ref docx, ref tableRowHeader);
            TableCell tableRowCellTVHeader = AddCellToDOCXTable("Наличие TV", ref docx, ref tableRowHeader);
            TableCell tableRowCellPackagesHeader = AddCellToDOCXTable("Входит в пакеты", ref docx, ref tableRowHeader);
            TableCell tableRowCellPriceHeader = AddCellToDOCXTable("Стоимость в месяц", ref docx, ref tableRowHeader);
            table.Rows.Add(tableRowHeader);

            foreach (TVChannel tvChannel in items)
            {
                TableRow tableRow = new TableRow(docx);
                string packages = string.Join(",", tvChannel.Packages.Select(item => item.Name));
                TableCell tableRowCellName = AddCellToDOCXTable(tvChannel.Name, ref docx, ref tableRow);
                TableCell tableRowCellIPTV = AddCellToDOCXTable((tvChannel.IsIPTV ? "Да" : "Нет"), ref docx, ref tableRow);
                TableCell tableRowCellTV = AddCellToDOCXTable((tvChannel.IsIPTV ? "Да" : "Нет"), ref docx, ref tableRow);
                TableCell tableRowCellPackages = AddCellToDOCXTable(packages, ref docx, ref tableRow);
                TableCell tableRowCellPrice = AddCellToDOCXTable(tvChannel.Price.ToString(), ref docx, ref tableRow);
                table.Rows.Add(tableRow);
            }
            section.Blocks.Add(table);

            docx.Save(stream, SaveOptions.DocxDefault);
            return stream;
        }
        public Stream DownloadDOCX(TVChannelPackage item)
        {
            MemoryStream stream = new MemoryStream();

            DocumentCore docx = new DocumentCore();
            Section section = new Section(docx);
            docx.Sections.Add(section);
            section.PageSetup.PaperType = PaperType.A4;
            section.Blocks.Add(new Paragraph(docx, string.Format("Информация предоставлена (UTC): {0}", DateTime.UtcNow)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Название пакета ТВ: {0}", item.Name)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Количество каналов в пакете: {0}", item.Channels.Count())));
            section.Blocks.Add(new Paragraph(docx, string.Format("Стоимость в месяц: {0}", item.Price)));
            section.Blocks.Add(new Paragraph(docx, string.Format("Список каналов:")));

            Table table = new Table(docx);
            table.TableFormat.Alignment = HorizontalAlignment.Left;

            TableRow tableRowHeader = new TableRow(docx);
            TableCell tableRowCellNameHeader = AddCellToDOCXTable("Название канала", ref docx, ref tableRowHeader);
            TableCell tableRowCellIPTVHeader = AddCellToDOCXTable("Наличие IPTV", ref docx, ref tableRowHeader);
            TableCell tableRowCellTVHeader = AddCellToDOCXTable("Наличие TV", ref docx, ref tableRowHeader);
            table.Rows.Add(tableRowHeader);

            foreach (TVChannel tvChannel in item.Channels)
            {
                TableRow tableRow = new TableRow(docx);
                TableCell tableRowCellName = AddCellToDOCXTable(tvChannel.Name, ref docx, ref tableRow);
                TableCell tableRowCellIPTV = AddCellToDOCXTable((tvChannel.IsIPTV ? "Да" : "Нет"), ref docx, ref tableRow);
                TableCell tableRowCellTV = AddCellToDOCXTable((tvChannel.IsTV ? "Да" : "Нет"), ref docx, ref tableRow);
                table.Rows.Add(tableRow);
            }
            section.Blocks.Add(table);

            docx.Save(stream, SaveOptions.DocxDefault);
            return stream;
        }
        private TableCell AddCellToDOCXTable(string text, ref DocumentCore docx, ref TableRow tableRow)
        {
            TableCell tableRowCell = new TableCell(docx);
            tableRowCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0);
            tableRow.Cells.Add(tableRowCell);
            Paragraph paragraph = new Paragraph(docx);
            paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            paragraph.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
            paragraph.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point); ;
            paragraph.Content.Start.Insert(String.Format("{0}", text));
            tableRowCell.Blocks.Add(paragraph);

            return tableRowCell;
        }
    }
}