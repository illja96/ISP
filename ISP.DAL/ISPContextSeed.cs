using ISP.DAL.DBModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL
{
    public static class ISPContextSeed
    {
        public static void SeedUsersAndRoles(ISPContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Administrator" };
            roleManager.Create(adminRole);

            var supportRole = new IdentityRole { Name = "Support" };
            roleManager.Create(supportRole);

            var userRole = new IdentityRole { Name = "Subscriber" };
            roleManager.Create(userRole);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var user = new User()
            {
                UserName = "root",
                Email = "root@localhost.localhost",
                PhoneNumber = "+380000000000",
                FirstName = "root",
                LastName = "root",
                Balance = 0,
                DoB = DateTime.UtcNow.Date,
                RegistrationDate = DateTime.UtcNow
            };
            userManager.Create(user, "rootroot");
            userManager.AddToRole(user.Id, "Administrator");

            context.SaveChanges();
        }

        public static void SeedTVChannelPackages(ISPContext context)
        {
            Random rand = new Random();

            Dictionary<string, double> tvChannelPackagesPrice = new Dictionary<string, double>
            {
                { "Развлекательный", 5},
                { "Познавательный", 5},
                { "Кино и сериалы", 4},
                { "Общественный", 2},
                { "Музыкальный", 3},
                { "Спортивный", 5},
                { "Новостной", 2},
                { "Детский", 2},
                { "Религиозный", 10},
                { "Взрослый", 10}
            };

            Dictionary<string, string[]> tvChannelsInPackages = new Dictionary<string, string[]>
            {
                { "Развлекательный", new string[] { "ТНТ", "СТС", "ТНТ4", "РЕН ТВ", "Пятница", "2х2", "Че", "ТЕТ", "Paramount Comedy", "НЛО ТВ", "СТС love", "Сарафан ТВ", "ТНТ", "RTVI", "Ю", "QTV", "Вопросы и ответы", "Fashion TV", "Парк Развлечений", "Zoom", "Бигуди", "World Fashion Channel", "ВТВ", "Юмор Box", "STV", "Живи!", "Тонус ТВ", "Телеканал Деда Мороза", "K1", "К2", "Pro Все", "Maxxi-TV", } },
                { "Познавательный", new string[] { "Discovery Channel", "Discovery Science", "National Geographic", "Nat Geo Wild", "Оружие", "Охотник и рыболов", "Наука 2.0", "Galaxy TV", "Viasat History", "Охота и Рыбалка", "Animal Planel", "Viasat Explorer", "Моя планета", "History Channel", "Первый автомобильный", "Техно 24", "ID Xtra", "История", "TLC", "Авто Плюс", "Viasat Nature", "Travel Channel", "Совершенно секретно", "365 дней", "Драйв ТВ", "Усадьба", "Кухня ТВ", "Телепутешествия", "English Club TV", "Россия К", "24 ДОК", "Russian Travel Guide", "Кто есть кто", "Бобёр ТВ", "CBS Reality", "Da Vinci Learning", "Нано ТВ", "Психология 21", "Zooпарк", "Еда ТВ", "Живая Планета", "Мега", "Загородная жизнь", "DTV", "Домашние животные", "Здоровое ТВ", "Outdoor Channel", "Зоо ТВ", "Мама", "Трофей", "Морской", "Food Network" } },
                { "Кино и сериалы", new string[] { "TV 1000 Action", "Кинохит", "Кинопремьера", "Русский Детектив", "Дом Кино", "ТВ 3", "TV 1000", "НТС", "Родное Кино", "Кинопоказ", "Наше Новое Кино", "Кинокомедия", "TV 1000 Русское Кино", "Наше любимое кино", "Киномикс", "Киносвидание", "FOX", "XXI", "SONY SCI-FI", "Иллюзион+", "Мужское Кино", "Еврокино", "Русский Иллюзион", "Enter-фильм", "FOX Life", "AMEDIA 2", "Киносерия", "HBO 2", "Paramount Channel", "Русский бестселлер", "CBS Drama", "Комедия", "SET", "Zee TV", "Индийское Кино", "Киносемья", "AMC" } },
                { "Общественный", new string[] { "Россия 1", "НТВ", "1+1", "СТБ", "2+2", "ICTV", "Интер", "Звезда", "Новый канал", "Украина", "5 канал", "Россия-РТР", "ТВЦ", "Первый канал (Европа)", "UA:Перший", "НТВ Мир", "Ретро ТВ", "Домашний", "Ностальгия", "TVCI", "Первый канал (СНГ)", "CBS 2 New York", "Интер+", "Мир", "Первый канал", "ОТР", "Время", "Беларусь 24", "Белсат ТВ", "Tonis", "Dobro TV", "Подмосковье", "Страна", "Первый канал (Евразия)", "1+1 Internation", "Красная линия", "Унiан", "Вместе РФ" } },
                { "Музыкальный", new string[] { "Europa Plus TV", "RU TV", "MTV Dance", "ТНТ-Music", "Муз ТВ", "VH1 Classic", "Шансон ТВ", "Scuzz", "Музыка Первого", "MTV Hits", "Bridge TV", "Dange TV", "M1", "EU Music", "Kerrang!", "MTV Russia", "Ля-минор", "VH1", "MTV Rocks", "Music Box UA", "Rusong TV", "MCM Top", "Mezzo", "Первый Музыкальный канал", "A-One", "4 Fun TV", "M2", "O-TV", "Music Box Ru", "Music Box Tv", "VOX Old's Cool TV", "Eska ROCK TV", "ESKA Party TV", "ESKA Best Music TV", "ESKA TV", "WAWA TV", "Retro Music TV", "Bridge TV Classic", "Bridge TV Dance", "Bridge HD" } },
                { "Спортивный", new string[] { "НТВ+ Наш футбол", "Футбол 1", "Матч! Арена", "Eurosport", "Футбол 2", "Eurosport 2", "Матч! Наш Спорт", "Матч! Игра", "ViasatSport", "КХЛ", "Сетанта Спорт", "Матч! Боец", "Sky Sports F1", "Viasat Sport Baltic", "Матч! Планета", "Беларусь 5", "Sky Sports 1", "Спорт 1", "BT Sport 1", "Viasat Hockey", "Сопрт 2", "SkySports 3", "NBA TV", "Premier Sports", "Sky Sports 2", "BT Sport 2", "ESPN America", "Motors TV", "Футбол", "Sky Sports 4", "Extreme Sports", "Sky Sports 5", "Viasat Motor", "Русский Экстрим", "TV3 Sport 2", "Viasat Golf", "Bein Sport 7", "Bein Sport 6", "Bein Sport 8", "Viasat Fotboll", "Eurosport British", "Eurosport 2 British", "Arena Sport 1", "Movistar F1", "Movistar MotoGP", "Arena Sport 2", "Sport TV 1", "BT Sport ESPN", "Nova Sport", "Arena Sport 3" } },
                { "Новостной", new string[] { "Россия 24", "Life News", "Дождь", "112 Украина", "5 канал", "РБК", "24", "EuroNews", "CNN International", "BBC World News", "Business", "Мир 24", "News One", "100% News" } },
                { "Детский", new string[] { "Мульт", "Disney Channel", "ПЛЮСПЛЮС", "Nickelodeon", "Карусель", "nick jr.", "Cartoon Network", "JimJam", "Детский", "Мультимания", "Детский Мир", "Пиксель", "Малятко TV", "Улыбка Ребенка", "Gulli", "Boomerang", "Радость моя", "TiJi", "Ani", "Рыжий", "Любимое ТВ" } },
                { "Религиозный", new string[] { "Союз", "Спас ТВ", "CNL" } },
                { "Взрослый", new string[] { "Platinum TV", "Brazzers TV Europe", "Satisfaction HD", "FrenchLoverTV", "XXL", "Exotica TV", "Hustler TV", "Русская ноь", "Redlight Premium", "O-la-la", "SCT", "CentoXCento TV", "Sext6Senso", "Dorcel TV", "Ночной клуб", "Искушения", "Private TV", "Nuart TV", "FAP TV amateur", "Husler Blue", "Daring TV", "Playboy TV", "FAP TV 2", "Эгоист ТВ", "Free-X TV", "FAP TV 4", "FAP TV 3", "FAP TV teens", "FAP TV Legal Porno", "FAP TV compilation", "FAP TV older", "FAP TV anal", "Brazzers TV", "FAP TV parody", "Phoenix Marie TV", "FAP TV pissing", "FAP TV lesbian", "FAP TV BBW", "FAP TV teaching", "Jasmin TV" } },
            };

            foreach (var tvChannelsInPackage in tvChannelsInPackages)
            {
                var tvChannelPackage = new TVChannelPackage()
                {
                    Name = tvChannelsInPackage.Key,
                    Price = tvChannelsInPackage.Value.Count() * (tvChannelPackagesPrice[tvChannelsInPackage.Key] / 2),
                    Channels = new List<TVChannel>()
                };

                foreach (var tvChannelInPackage in tvChannelsInPackage.Value)
                {
                    var tvChannel = new TVChannel()
                    {
                        Name = tvChannelInPackage,
                        Price = tvChannelPackagesPrice[tvChannelsInPackage.Key],
                        IsIPTV = rand.NextDouble() > 0.5,
                        IsTV = rand.NextDouble() > 0.25
                    };
                    tvChannel.IsCanceled = (!tvChannel.IsTV && !tvChannel.IsIPTV ? true : false);
                    tvChannelPackage.Channels.Add(tvChannel);
                }

                context.TVChannelPackages.Add(tvChannelPackage);
            }

            context.SaveChanges();
        }

        public static void SeedInternetPackages(ISPContext context)
        {
            Dictionary<string, double> internetPackagesPrice = new Dictionary<string, double>
            {
                { "Старт", 60 },
                { "Стандарт", 80 },
                { "Супер", 120 }
            };

            Dictionary<string, double[]> internetPackagesSpeed = new Dictionary<string, double[]>
            {
                {"Старт", new double[] { 10, 5 } },
                {"Стандарт", new double[] { 50, 35 } },
                {"Супер", new double[] { 100, 100 } }
            };

            foreach (var internetPackageInfo in internetPackagesPrice)
            {
                var internetPackage = new InternetPackage()
                {
                    Name = internetPackageInfo.Key,
                    Price = internetPackageInfo.Value,
                    DownloadSpeed = internetPackagesSpeed[internetPackageInfo.Key][0],
                    UploadSpeed = internetPackagesSpeed[internetPackageInfo.Key][1]
                };

                context.InternetPackages.Add(internetPackage);
            }

            context.SaveChanges();
        }
    }
}