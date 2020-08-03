using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class FilProd_Cat
    {
        #region Fields
        /// <summary>
        /// Instance of Product List Filter Category Class.
        /// </summary>
        private static FilProd_Cat instance;
        public static FilProd_Cat Instance
        {
            get
            {
                if (instance == null)
                    instance = new FilProd_Cat();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public FilProd_Cat()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void GetData(string wp_id, string session_key, string store_id, string category_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", store_id);
                dict.Add("catid", category_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/product/filter/category", content);
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
