using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Acme.BookStore.Books;


namespace Acme.BookStore
{
    public class BookstoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {

        private readonly IRepository<Book, Guid> _bookrepository;

        public BookstoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookrepository = bookRepository;
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
        }

    }
}
