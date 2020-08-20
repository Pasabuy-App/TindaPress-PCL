using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Stores
{
    public class UpdateStore
    {
        #region Fields
        /// <summary>
        /// Instance of Update Store Class.
        /// </summary>
        private static UpdateStore instance;
        public static UpdateStore Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateStore();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateStore()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methodss
        public async void Update(string wp_id, string session_key, string stid, string catid, string title, string short_info, string long_info, string logo, string banner, string address, string phone, string email, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("ctid", catid);
            dict.Add("title", title);
            dict.Add("short_info", short_info);
            dict.Add("long_info", long_info);
            dict.Add("logo", logo);
            dict.Add("banner", banner);
            dict.Add("address", address);
            dict.Add("phone", phone);
            dict.Add("email", email);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/update", content);
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
