using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Address
{
    public class UpdateAdd
    {
        #region Fields
        /// <summary>
        /// Instance of Update Address Class.
        /// </summary>
        private static UpdateAdd instance;
        public static UpdateAdd Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateAdd();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateAdd()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Update(string wp_id, string session_key, string stid, string addr, string st, string bg, string ct, string pv, string co, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("addr", addr);
            dict.Add("st", st);
            dict.Add("bg", bg);
            dict.Add("ct", ct);
            dict.Add("pv", pv);
            dict.Add("co", co);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/address/update", content);
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
