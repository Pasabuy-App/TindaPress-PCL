using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;


namespace TindaPress.Category
{
    public class Insert
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Category Class.
        /// </summary>
        private static Insert instance;
        public static Insert Instance
        {
            get
            {
                if (instance == null)
                    instance = new Insert();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Insert()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void Category(string wp_id, string session_key, string stid, string types, string title, string info, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            if (stid != null)
            {
                dict.Add("stid", stid);
            }
            dict.Add("types", types);
            dict.Add("title", title);
            dict.Add("info", info);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/category/insert", content);
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
