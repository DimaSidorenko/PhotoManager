using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TRRP1
{
    public class Photo
    {
        public string
            id,
            baseUrl,
            productUrl,
            mimeType,
            filename;

        public Photo(JToken temp)
        {
            id = temp[nameof(id)].ToString(); 
            baseUrl = temp[nameof(baseUrl)].ToString();
            productUrl = temp[nameof(productUrl)].ToString();
            mimeType = temp[nameof(mimeType)].ToString();
            filename = temp[nameof(filename)].ToString();
        }
    }
}
