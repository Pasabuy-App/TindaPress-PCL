using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Documents
{
    public class ApproveDoc
    {
        #region Fields
        /// <summary>
        /// Instance of Approve Documents Class.
        /// </summary>
        private static ApproveDoc instance;
        public static ApproveDoc Instance
        {
            get
            {
                if (instance == null)
                    instance = new ApproveDoc();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public ApproveDoc()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methods
        public async void Approve(string wp_id, string session_key, string stid, string doc_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", wp_id);
            dict.Add("snky", session_key);
            dict.Add("stid", stid);
            dict.Add("doc_id", doc_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/documents/approve", content);
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
