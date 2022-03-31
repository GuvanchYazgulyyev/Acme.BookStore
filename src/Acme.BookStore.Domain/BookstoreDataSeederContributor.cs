using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Acme.BookStore.Books;
using Acme.BookStore.Authors;

namespace Acme.BookStore
{
    public class BookstoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {

        private readonly IRepository<Book, Guid> _bookrepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public BookstoreDataSeederContributor(IRepository<Book, Guid> bookRepository, IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _bookrepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookrepository.GetCountAsync() <= 0)
            {
                await _bookrepository.InsertAsync(new Book
                {
                    Name = "1941",
                    Type = BookType.Hororr,
                    PublishDate = new DateTime(1954, 5, 9),
                    Price = 25.45f
                },
                autoSave: true

                );

                await _bookrepository.InsertAsync(
                    new Book
                    {
                        Name = "Bu Bir Denemedir!!!",
                        Type = BookType.Peotry,
                        PublishDate = new DateTime(2000, 10, 5),
                        Price = 42.0f
                    },
                    autoSave: true
                    );
            }

            //Adedd Authors
            if (await _authorRepository.GetCountAsync() <= 0)
            {
                await _authorRepository.InsertAsync(await _authorManager.CreateAsync(
                    "George Orwell",
                    new DateTime(1903, 05, 25),
                    "Eric Arthur Blair veya daha bilinen takma adıyla George Orwell, (25 Haziran 1903; Bihar - 21 Ocak 1950; Londra),[1] 20. yüzyıl İngiliz edebiyatının önde gelen kalemleri arasında yer alan İngiliz romancı, gazeteci ve eleştirmen. En çok, dünyaca ünlü Bin Dokuz Yüz Seksen Dört adlı romanı ve bu romanda yarattığı Big Brother (Büyük Birader) kavramı ile tanınır."
                    ));


                await _authorRepository.InsertAsync(await _authorManager.CreateAsync(
                    "Douglas Adams",
                    new DateTime(1952, 01, 25),
                    "Okula Essex'de gitti. Cambridge'de St. Johns College'e devam ederken Footlights tiyatro kulübünde görev aldı. Pek çok iş denedi. Hastanede hizmetlilik, inşaat işçiliği, kümes temizlikçiliği, bir Arap aile için korumalık yaptı. Daha sonra BBC'de Dr. Who dizisinde yapımcılık ve senaryo editörlüğü yaptı. Dr. Who'nun üç bölümünü yazdı. Monty Pyton grubundan Graham Chapman ile birlikte çalıştı."
                    ));
            }
        }

    }
}
