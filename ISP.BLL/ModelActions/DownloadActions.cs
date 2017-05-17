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
        public Stream DownloadDOCX(TVChannelPackage item)
        {
            MemoryStream stream = new MemoryStream();

            DocumentCore docx = new DocumentCore();
            Section textSection = new Section(docx);
            docx.Sections.Add(textSection);
            textSection.PageSetup.PaperType = PaperType.A4;
            textSection.Blocks.Add(new Paragraph(docx, string.Format("Информация предоставлена (UTC): {0}", DateTime.UtcNow)));
            textSection.Blocks.Add(new Paragraph(docx, string.Format("Название пакета ТВ: {0}", item.Name)));
            textSection.Blocks.Add(new Paragraph(docx, string.Format("Количество каналов в пакете: {0}", item.Channels.Count())));
            textSection.Blocks.Add(new Paragraph(docx, string.Format("Стоимость в месяц: {0}", item.Price)));
            textSection.Blocks.Add(new Paragraph(docx, string.Format("Список каналов:")));

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
            textSection.Blocks.Add(table);

            docx.Save(stream, SaveOptions.DocxDefault);
            return stream;
        }
        private TableCell AddCellToDOCXTable(string text, ref DocumentCore docx, ref TableRow tableRow)
        {
            TableCell tableRowCell = new TableCell(docx);
            tableRowCell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0);
            tableRow.Cells.Add(tableRowCell);
            Paragraph p = new Paragraph(docx);
            p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
            p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point); ;
            p.Content.Start.Insert(String.Format("{0}", text));
            tableRowCell.Blocks.Add(p);

            return tableRowCell;
        }
    }
}