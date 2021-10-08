using Sistemas.Core.QueryFilters;
using System;

namespace Sistemas.Core.Services.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}
