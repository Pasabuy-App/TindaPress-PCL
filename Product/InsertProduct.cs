using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class InsertProduct
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Product Class.
        /// </summary>
        private static InsertProduct instance;
        public static InsertProduct Instance
        {
            get
            {
                if (instance == null)
                    instance = new InsertProduct();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public InsertProduct()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Insert(string wp_id, string session_key, string catid, string stid, string title, string short_info, string long_info, string sku, string price, string weight, string preview, string dimension, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("catid", catid);
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

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/products/insert", content);
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
