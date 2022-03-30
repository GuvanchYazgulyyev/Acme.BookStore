using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService : ICrudAppService<BookDto,Guid, PagedAndSortedResultRequestDto,CreateUpdateBookDto>
    {
    }
}
