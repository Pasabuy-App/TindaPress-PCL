using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Stores
{
    public class InsertStore
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Store Class.
        /// </summary>
        private static InsertStore instance;
        public static InsertStore Instance
        {
            get
            {
                if (instance == null)
                    instance = new InsertStore();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public InsertStore()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methodss
        public async void Insert(string wp_id, string session_key, string catid, string title, string short_info, string long_info, string logo, string banner, string st, string bg, string ct, string pv, string co,  Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("catid", catid);
            dict.Add("title", title);
            dict.Add("short_info", short_info);
            dict.Add("long_info", long_info);
            dict.Add("logo", logo);
            dict.Add("banner", banner);
            dict.Add("st", st);
            dict.Add("bg", bg);
            dict.Add("ct", ct);
            dict.Add("pv", pv);
            dict.Add("co", co);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/insert", content);
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
