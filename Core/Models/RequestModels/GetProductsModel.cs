using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.RequestModels
{
    public class GetProductsModel
    {
        public string? brand { get; set; }
        public string? type { get; set; }
        public string? sortType { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value?.Trim().ToLower();
        }
    }
}
