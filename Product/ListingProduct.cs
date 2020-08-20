using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class ListingProduct
    {
        #region Fields
        /// <summary>
        /// Instance of List of Product All/ By Status, Store ID or Category ID Class.
        /// </summary>
        private static ListingProduct instance;
        public static ListingProduct Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListingProduct();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ListingProduct()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Listing(string wp_id, string session_key, string stid, string catid, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("catid", catid);
            dict.Add("status", status);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/products/list", content);
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
