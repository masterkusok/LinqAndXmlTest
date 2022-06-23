using NOLinqToXmlExample;
using System.Net;
using System.Text;
using System.Xml;

namespace LinqToXmlExample
{
    public partial class Form1 : Form
    {
        private List<Product> _allProducts = new List<Product>();
        private List<Product> _selectedProducts = new List<Product>();
        public Form1()
        {
            InitializeComponent();
            LoadProductsFromXML();
            SetProductsToListView();
        }

        private void SetProductsToListView() 
        {
            ImageList imgList = new ImageList();
            foreach(Product product in _selectedProducts)
            {
                imgList.Images.Add(GetImageFromUrl(product.ImageUrl));
            }

            foreach(Product product in _selectedProducts)
            {
                ListViewItem item = new ListViewItem();
                item.Text = product.Name;
                item.SubItems.Add(product.Price.ToString()+"$");
                GoodsListView.Items.Add(item);
            }
            GoodsListView.SmallImageList = imgList;
        }

        private Image GetImageFromUrl(string url)
        {
            Image image = null;
            using(WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    image = Image.FromStream(ms);
                }
            }
            return image;
        }

        private void LoadProductsFromXML()
        {
            string xmlPath = $"{Directory.GetCurrentDirectory()}/Products.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement root = xmlDoc.DocumentElement;
            foreach(XmlNode node in root)
            {
                Product product = new Product();
                product.Name = node.Attributes["Name"].Value;
                product.ProductID = int.Parse(node.Attributes["Id"].Value);
                product.Type = node.Attributes["Type"].Value;
                product.ImageUrl = node.Attributes["Url"].Value;
                product.Price = int.Parse(node.Attributes["Price"].Value);
                _allProducts.Add(product);
            }
            _selectedProducts = _allProducts;
        }
    }
}