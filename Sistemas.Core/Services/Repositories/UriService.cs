using Sistemas.Core.QueryFilters;
using Sistemas.Core.Services.Interfaces;
using System;

namespace Sistemas.Core.Services.Repositories
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        //Constructor
        public UriService(string baseUri )
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
