namespace TindaPress
{
    public class TPHost
    {
        private static TPHost instance;
        public static TPHost Instance
        {
            get
            {
                if (instance == null)
                    instance = new TPHost();
                return instance;
            }
        }

        private bool isInitialized = false;
        private string baseUrl = "http://localhost";
        public string BaseDomain
        {
            get
            {
                    return baseUrl + "/wp-json";
            }
        }

        public void Initialized(string url)
        {
            if (!isInitialized)
            {
                baseUrl = url;
                isInitialized = true;
            }
        }

    }
}
