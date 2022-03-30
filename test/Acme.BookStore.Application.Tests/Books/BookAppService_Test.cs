using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Acme.BookStore.Books
{
    public class BookAppService_Test : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;

        public BookAppService_Test()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Gel_List_of_Books()
        {
            // Act
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1942");

        }

        [Fact]
        public async Task Should_Create_A_Valid_Books()
        {
            // Act
            var result = await _bookAppService.CreateAsync(new CreateUpdateBookDto
            {
                Name = "New Test Book 45",
                Price = 24,
                PublishDate = DateTime.Now,
                Type = BookType.Biographi
            }
              );

            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New Test Book 45");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exeption = await Assert.ThrowsAsync<AbpValidationException>(async () =>
              {
                  await _bookAppService.CreateAsync(new CreateUpdateBookDto
                  {
                      Name = "",
                      Price = 15,
                      PublishDate = DateTime.Now,
                      Type = BookType.Biographi
                  });
              });
            exeption.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }
    }
}
