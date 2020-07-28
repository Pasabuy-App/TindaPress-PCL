using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using TindaPress.Product.Struct;

namespace TindaPress.Product
{
    public class Filter_Newest
    {
        #region Fields
        /// <summary>
        /// Instance of Filter Newest Product Class.
        /// </summary>
        private static Filter_Newest instance;
        public static Filter_Newest Instance
        {
            get
            {
                if (instance == null)
                    instance = new Filter_Newest();
                return instance;
            }
        }
        #endregion
    }
}
