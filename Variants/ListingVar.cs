using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Variants
{
    public class ListingVar
    {
        #region Fields
        /// <summary>
        /// Instance of Listing of Variants by Parent ID, Product ID, Status, Variant ID and Type Class.
        /// </summary>
        private static ListingVar instance;
        public static ListingVar Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListingVar();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ListingVar()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Listing(string wp_id, string session_key, string pdid, string pid, string status, string vrid, string type, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("pdid", pdid);
            dict.Add("pid", pid);
            dict.Add("status", status);
            dict.Add("vrid", vrid);
            dict.Add("type", type);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/variants/list", content);
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
