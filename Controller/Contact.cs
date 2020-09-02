using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Model;

namespace TindaPress
{
    public class Contact
    {
        #region Fields
        /// <summary>
        /// Instance of  Contact Class with activate, delete, insert, list and update method.
        /// </summary>
        private static Contact instance;
        public static Contact Instance
        {
            get
            {
                if (instance == null)
                    instance = new Contact();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Contact()
        {
            client = new HttpClient();
        }
        #endregion

        #region Activate Method
        public async void Activate(string wp_id, string session_key, string stid, string cid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("cid", cid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/contacts/activate", content);
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

        #region Delete Method
        public async void Delete(string wp_id, string session_key, string stid, string cid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("cid", cid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/contacts/delete", content);
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

        #region Insert Method
        public async void Insert(string wp_id, string session_key, string stid, string phone, string email, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("phone", phone);
                dict.Add("email", email);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/contacts/insert", content);
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

        #region List Method
        public async void List(string wp_id, string session_key, string stid, string type, string cid, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                if (type != "") { dict.Add("type", type); }
                if (cid != "") { dict.Add("cid", cid); }
                if (status != "") { dict.Add("status", status); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/contacts/list", content);
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

        #region Update Method
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

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/contacts/update", content);
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
