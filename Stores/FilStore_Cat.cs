using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Stores
{
    public class FilStore_Cat
    {
        #region Fields
        /// <summary>
        /// Instance of Store List Filter Category Class.
        /// </summary>
        private static FilStore_Cat instance;
        public static FilStore_Cat Instance
        {
            get
            {
                if (instance == null)
                    instance = new FilStore_Cat();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public FilStore_Cat()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void GetData(string wp_id, string session_key, string category_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("catid", category_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/store/category", content);
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
