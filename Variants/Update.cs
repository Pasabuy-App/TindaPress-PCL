using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Variants
{
    public class Update
    {
        #region Fields
        /// <summary>
        /// Instance of Update Variant Key, Value, and Child key by Variant ID and Product ID Class.
        /// </summary>
        private static Update instance;
        public static Update Instance
        {
            get
            {
                if (instance == null)
                    instance = new Update();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Update()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Variant(string wp_id, string session_key, string variant_id, string product_id, string key, string value, string child_key, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("vid", variant_id);
            dict.Add("pdid", product_id);
            dict.Add("key", key);
            dict.Add("val", value);
            dict.Add("ckey", child_key);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/variants/update", content);
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
