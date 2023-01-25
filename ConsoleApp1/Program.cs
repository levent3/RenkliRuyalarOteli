using RenkliRuyalarOteli.DAL.Contexts;
using RenkliRuyalarOteli.Entities.Entites.Concrete;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {



            OdaFiyat fiyat = new OdaFiyat
            {
                Baslangic = DateTime.Now,
                Bitis = new DateTime(2022, 12, 23),
                Fiyat = 10000,
                OdaId = Guid.Parse("9c0d7414-5d45-44a8-375b-08dae37c96a4"),
                KullaniciId = Guid.Parse("2a1f7d43-f5ee-4d79-b826-8bf920dbd436"),






            };






            SqlDbContext dbContext = new SqlDbContext();

            dbContext.OdaFiyatlari.Add(fiyat);

            dbContext.SaveChanges();


            Console.WriteLine("Hello, World!");
        }
    }
}