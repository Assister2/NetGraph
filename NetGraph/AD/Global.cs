namespace CyConex.AD
{
    public static class Global
    {
        private static string _idToken = "";
        public static string IdToken
        {
            get { return _idToken; }
            set { _idToken = value; }
        }
        private static string _enterpriseId = "";
        public static string enterpriseId
        {
            get { return _enterpriseId; }
            set { _enterpriseId = value; }
        }
    }
}
