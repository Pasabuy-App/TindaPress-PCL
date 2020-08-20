using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Category
{
    public class Activate
    {
        #region Fields
        /// <summary>
        /// Instance of Activate Category Class.
        /// </summary>
        private static Activate instance;
        public static Activate Instance
        {
            get
            {
                if (instance == null)
                    instance = new Activate();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Activate()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Category(string wp_id, string session_key, string catid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("catid", catid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/category/activate", content);
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
