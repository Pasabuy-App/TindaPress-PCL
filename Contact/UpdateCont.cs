using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Contact
{
    public  class UpdateCont
    {
        #region Fields
        /// <summary>
        /// Instance of Update Contact Class.
        /// </summary>
        private static UpdateCont instance;
        public static UpdateCont Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateCont();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateCont()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Update(string wp_id, string session_key, string stid, string cid, string type, string val, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("cid", cid);
            dict.Add("type", type);
            dict.Add("val", val);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/contacts/update", content);
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
