namespace NOLinqToXmlExample
{
    public class Product
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int ProductID { get; set; }

        private string _url;
        public string ImageUrl
        {
            get { return $"{Directory.GetCurrentDirectory()}/Product images/{_url}"; }
            set { _url = value; }
        }


        public Product Clone()
        {
            return this.MemberwiseClone() as Product;
        }
    }
}
