﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Variants
{
    public class ByProduct
    {
        #region Fields
        /// <summary>
        /// Instance of List of Variants By Product ID Class.
        /// </summary>
        private static ByProduct instance;
        public static ByProduct Instance
        {
            get
            {
                if (instance == null)
                    instance = new ByProduct();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ByProduct()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void GetData(string wp_id, string session_key, string product_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("pdid", product_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/variants/product/select", content);
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
