using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Category
{
    class Cat_List_Stores
    {
        #region Fields
        /// <summary>
        /// Instance of Categories List of Stores Class.
        /// </summary>
        private static Cat_List_Stores instance;
        public static Cat_List_Stores Instance
        {
            get
            {
                if (instance == null)
                    instance = new Cat_List_Stores();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication for our Backend.
        /// </summary>
        HttpClient client;
        public Cat_List_Stores()
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

            var response = await client.GetAsync(BaseClass.BaseDomainUrl + "/datavice/api/v1/ategory/stores" + getRequest);
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
