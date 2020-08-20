using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class UpdateProduct
    {
        #region Fields
        /// <summary>
        /// Instance of Update Product Class.
        /// </summary>
        private static UpdateProduct instance;
        public static UpdateProduct Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateProduct();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateProduct()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Update(string wp_id, string session_key, string catid, string pdid, string stid, string title, string short_info, string long_info, string sku, string price, string weight, string preview, string dimension, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            if (catid != "")
            {
                dict.Add("catid", catid);
            }
            dict.Add("pdid", pdid);
            dict.Add("stid", stid);
            dict.Add("title", title);
            dict.Add("short_info", short_info);
            dict.Add("long_info", long_info);
            dict.Add("sku", sku);
            dict.Add("price", price);
            dict.Add("weight", weight);
            dict.Add("preview", preview);
            dict.Add("dimension", dimension);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/products/update", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }
        #endregion
    }
}
