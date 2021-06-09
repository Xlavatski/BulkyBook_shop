using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.APIModels
{
    public class PostDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("userId")]
        public long userId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
