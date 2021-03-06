﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace SharpManager.Helpers
{
    class ImgurHandler
    {
        private const string ClientId = "0751ca59efd1d0d";

        public static string UploadImage(string image)
        {
            if (image == null) throw new ArgumentNullException("image");
            using (var w = new WebClient())
            {
                w.Headers.Add("Authorization", "Client-ID " + ClientId);
                var values = new NameValueCollection
                {
                    {"image", Convert.ToBase64String(File.ReadAllBytes(@image))}
                };

                byte[] response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);
                var document = XDocument.Load(new MemoryStream(response));
                var link = document.Element("data").Element("link").Value;
                
                return link;
            }
        }
    }
}
