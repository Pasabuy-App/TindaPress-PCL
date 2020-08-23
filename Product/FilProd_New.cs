using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;
using System.Threading.Tasks;
using System.IO;

namespace TindaPress.Product
{
    public class Variants
    {
        public string name;
        public string baseprice;
        public Options[] options;

    }
    public class Options
    {
        public string name;
        public string price;
        public int order;

    }                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    public class FilProd_New
    {

        public string Bars()
        {
            Options var1 = new Options();
            Options var2 = new Options();
            Options var3 = new Options();
            Variants vars = new Variants();
            vars.name = "name";
            vars.baseprice = "baseprice";
            //vars.options[0].name = "small";
            //vars.options[1].name = "medium";
            //vars.options[2].name = "large";
            //vars.options[0].price = "50";
            //vars.options[1].price = "100";
            //vars.options[2].price = "150";
            var1.name = "small";
            var1.price = "50";
            var2.name = "medium";
            var2.price = "100";
            var3.name = "large";
            var3.price = "150";
            vars.options = new Options[3] { var1, var2, var3};

            string bbars = JsonConvert.SerializeObject(vars);
            return bbars;
        }
        #region Fields
        /// <summary>
        /// Instance of Product List Filter Newest Class.
        /// </summary>
        private static FilProd_New instance;
        public static FilProd_New Instance
        {
            get
            {
                if (instance == null)
                    instance = new FilProd_New();
                return instance;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public FilProd_New()
        {
            client = new HttpClient();

        }
        #endregion
        #region Methods
        public async void GetData(string wpid, string snky, string img, Action<bool, string> callback)
        {
            //var dict = new Dictionary<string, string>();
            //    dict.Add("val1", val1);
            //var content = new FormUrlEncodedContent(dict);

            // we need to send a request with multipart/form-data
            var multiForm = new MultipartFormDataContent();

            // add API method parameters
            multiForm.Add(new StringContent(wpid), "wpid");
            multiForm.Add(new StringContent(snky), "snky");

            // add file and directly upload it
            FileStream fs = File.OpenRead(img);
            multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));
            //Path.GetFullPath
            //callback(true, Path.GetFullPath(img));

            //var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/test/demoguy", content);
            var response = await client.PostAsync(BaseClass.BaseDomainUrl + "/datavice/v1/process/upload", multiForm);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                //callback(true, result);
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
