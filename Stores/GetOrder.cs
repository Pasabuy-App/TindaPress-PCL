using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Stores
{
    public class GetOrder
    {
        #region Fields
        /// <summary>
        /// Instance of Get Order By Date and Store ID Class.
        /// </summary>
        private static GetOrder instance;
        public static GetOrder Instance
        {
            get
            {
                if (instance == null)
                    instance = new GetOrder();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public GetOrder()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methodss
        public async void GetData(string wp_id, string session_key, string stid, string date, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("date", date);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/order/date", content);
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
