using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Model;

namespace TindaPress
{
    public class Category
    {
        #region Fields
        /// <summary>
        /// Instance of  Category Class with activate, insert, delete, update and list method.
        /// </summary>
        private static Category instance;
        public static Category Instance
        {
            get
            {
                if (instance == null)
                    instance = new Category();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Category()
        {
            client = new HttpClient();
        }
        #endregion

        #region Activate Method
        public async void Activate(string wp_id, string session_key, string catid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("catid", catid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/category/activate", content);
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
        public async void Delete(string wp_id, string session_key, string catid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("catid", catid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/category/delete", content);
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
        public async void Insert(string wp_id, string session_key, string stid, string types, string title, string info, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                if (stid != null) { dict.Add("stid", stid); }
                dict.Add("types", types);
                dict.Add("title", title);
                dict.Add("info", info);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/category/insert", content);
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
        public async void List(string wp_id, string session_key, string catid, string stid, string type, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("catid", catid);
                dict.Add("stid", stid);
                dict.Add("status", status);
                if (type != "") { dict.Add("type", type); }
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/category/list", content);
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
        public async void Update(string wp_id, string session_key, string catid, string title, string info, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("catid", catid);
                dict.Add("title", title);
                dict.Add("info", info);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/category/update", content);
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
