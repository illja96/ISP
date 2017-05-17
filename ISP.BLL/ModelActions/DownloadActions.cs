using ISP.DAL.DBModels;
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
    }
}