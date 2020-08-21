using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Address
{
    public class ListingAdd
    {
        #region Fields
        /// <summary>
        /// Instance of Listing of Address By Store ID, Type, Status, Address ID Class.
        /// </summary>
        private static ListingAdd instance;
        public static ListingAdd Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListingAdd();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ListingAdd()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void GetData(string wp_id, string session_key, string stid, string type, string addr, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                if (stid != "")
                {
                    dict.Add("stid", stid);
                }
                if (type != "")
                {
                    dict.Add("type", type);
                }
                if (addr != "")
                {
                    dict.Add("addr", addr);
                }
                if (status != "")
                {
                    dict.Add("status", status);
                }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/stores/address/list", content);
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
