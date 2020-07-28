using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class PLF_Newest
    {
        #region Fields
        /// <summary>
        /// Instance of Product List Filter Newest Class.
        /// </summary>
        private static PLF_Newest instance;
        public static PLF_Newest Instance
        {
            get
            {
                if (instance == null)
                    instance = new PLF_Newest();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public PLF_Newest()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void GetData(string wp_id, string session_key, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid=" + wp_id;
            getRequest += "&snky=" + session_key;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/product/newest" + getRequest);
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
