using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Variants
{
    public class InsertVar
    {
        #region Fields
        /// <summary>
        /// Instance of Update Variant Key, Value, and Child key by Variant ID and Product ID Class.
        /// </summary>
        private static InsertVar instance;
        public static InsertVar Instance
        {
            get
            {
                if (instance == null)
                    instance = new InsertVar();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public InsertVar()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Variant(string wp_id, string session_key, string pid, string product_id, string baseprice, string price, string name, string info, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("pid", pid);
            dict.Add("pdid", product_id);
            dict.Add("name", name);
            if (baseprice != "")
            {
                dict.Add("base", baseprice);
            }
            if (price != "")
            {
                dict.Add("price", price);
            }
            if (info != "")
            {
                dict.Add("info", info);
            }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/variants/insert", content);
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
