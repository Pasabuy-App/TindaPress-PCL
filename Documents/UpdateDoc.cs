using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;
using System.IO;

namespace TindaPress.Documents
{
    public class UpdateDoc
    {
        #region Fields
        /// <summary>
        /// Instance of Update Document of Store Class.
        /// </summary>
        private static UpdateDoc instance;
        public static UpdateDoc Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateDoc();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public UpdateDoc()
        {
            client = new HttpClient();
        }
        #endregion
        #region Methodss
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

            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/tindapress/v1/documents/update", multiForm);
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
