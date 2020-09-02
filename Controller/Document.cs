using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Model;
using System.IO;

namespace TindaPress
{
    public class Document
    {
        #region Fields
        /// <summary>
        /// Instance of Documents Class with approve, delete, insert, list and update metohd.
        /// </summary>
        private static Document instance;
        public static Document Instance
        {
            get
            {
                if (instance == null)
                    instance = new Document();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Document()
        {
            client = new HttpClient();
        }
        #endregion

        #region Approve Method
        public async void Approve(string wp_id, string session_key, string stid, string doc_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("doc_id", doc_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/documents/approve", content);
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
        public async void Delete(string wp_id, string session_key, string stid, string doc_id, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("doc_id", doc_id);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/documents/delete", content);
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
        public async void Insert(string wp_id, string session_key, string stid, string doc_type, string img, Action<bool, string> callback)
        {
            var multiForm = new MultipartFormDataContent();

            multiForm.Add(new StringContent(wp_id), "wpid");
            multiForm.Add(new StringContent(session_key), "snky");
            multiForm.Add(new StringContent(stid), "stid");
            multiForm.Add(new StringContent(doc_type), "doc_type");
            FileStream fs = File.OpenRead(img);
            multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/stores/documents/insert", multiForm);
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
        public async void List(string wp_id, string session_key, string stid, string doc_id, string doc_type, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wp_id);
                dict.Add("snky", session_key);
                dict.Add("stid", stid);
                dict.Add("doc_id", doc_id);
                dict.Add("doc_type", doc_type);
                dict.Add("status", status);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/documents/listing", content);
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
        public async void Update(string wp_id, string session_key, string stid, string doc_id, string doc_type, string img, Action<bool, string> callback)
        {
            var multiForm = new MultipartFormDataContent();

            multiForm.Add(new StringContent(wp_id), "wpid");
            multiForm.Add(new StringContent(session_key), "snky");
            multiForm.Add(new StringContent(stid), "stid");
            multiForm.Add(new StringContent(doc_id), "doc_id");
            multiForm.Add(new StringContent(doc_type), "doc_type");
            FileStream fs = File.OpenRead(img);
            multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

            var response = await client.PostAsync(TPHost.Instance.BaseDomain + "/tindapress/v1/documents/update", multiForm);
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
