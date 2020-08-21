using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Address
{
    public class InsertAdd
    {
        #region Fields
        /// <summary>
        /// Instance of Insert Address Class.
        /// </summary>
        private static InsertAdd instance;
        public static InsertAdd Instance
        {
            get
            {
                if (instance == null)
                    instance = new InsertAdd();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public InsertAdd()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Insert(string wp_id, string session_key, string stid, string type, string st, string bg, string ct, string pv, string co, string lat, string lon, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("type", type);
            dict.Add("st", st);
            dict.Add("bg", bg);
            dict.Add("ct", ct);
            dict.Add("pv", pv);
            dict.Add("co", co);
            dict.Add("lat", lat);
            dict.Add("long", lon);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/address/insert", content);
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
