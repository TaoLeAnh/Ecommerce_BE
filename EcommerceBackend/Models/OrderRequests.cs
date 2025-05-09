using System.Collections.Generic;

namespace Models
{
    public class OrderRequest
    {
        public List<Variant> Variants { get; set; } = new List<Variant>();

        public long UserId { get; set; }

        public long MerchantNumber { get; set; }

        public Dictionary<string, string> Detail { get; set; } = new Dictionary<string, string>();
    }
}