using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class Filter_Category
    {
        #region Fields
        /// <summary>
        /// Instance of Filter Category Product Class.
        /// </summary>
        private static Filter_Category instance;
        public static Filter_Category Instance
        {
            get
            {
                if (instance == null)
                    instance = new Filter_Category();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public Filter_Category()
        {
            client = new HttpClient();
        }
        #endregion
        #region Method
        public async void GetData(string wp_id, string session_key, string store_id, string category_id, Action<bool, string> callback)
        {
            string getRequest = "?";
            getRequest += "wpid=" + wp_id;
            getRequest += "&snky=" + session_key;
            getRequest += "&stid=" + store_id;
            getRequest += "&catid=" + category_id;

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/product/filter/category" + getRequest);
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
