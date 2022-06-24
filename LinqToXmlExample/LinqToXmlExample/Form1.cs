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
        private List<string> _productTypes = new List<string>();
        public Form1()
        {
            InitializeComponent();

            LoadProductsFromXML();
            LoadImagesToListView();
            SetProductsToListView();
            GetProductTypesFromList();
            SetProductTypesToComboBox();
        }

        private void LoadProductsFromXML()
        {
            try
            {
                string xmlPath = $"{Directory.GetCurrentDirectory()}/Products.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlElement root = xmlDoc.DocumentElement;
                foreach (XmlNode node in root)
                {
                    Product product = new Product();
                    product.Name = node.Attributes["Name"].Value;
                    product.ProductID = int.Parse(node.Attributes["Id"].Value);
                    product.Type = node.Attributes["Type"].Value;
                    product.ImageUrl = node.Attributes["Url"].Value;
                    product.Price = int.Parse(node.Attributes["Price"].Value);
                    _allProducts.Add(product);
                }
                CopyAllProductsToSelectedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during loading products from XML: \n {ex}");

            }
        }

        private void CopyAllProductsToSelectedProducts()
        {
            _selectedProducts.Clear();
            Product[] temp = new Product[_allProducts.Count];
            _allProducts.CopyTo(temp);
            _selectedProducts = temp.ToList();
        }

        private void LoadImagesToListView()
        {
            ImageList imgList = new ImageList();
            foreach (Product product in _allProducts)
            {
                imgList.Images.Add(Image.FromFile(product.ImageUrl));
            }
            GoodsListView.LargeImageList = imgList;
        }

        private void SetProductsToListView()
        {
            GoodsListView.Items.Clear();

            foreach (Product product in _selectedProducts)
            {
                ListViewItem item = new ListViewItem();
                item.Text = $"{product.Name} - {product.Price}$";
                item.ImageIndex = product.ProductID-1;
                GoodsListView.Items.Add(item);
            }

            GoodsListView.LargeImageList.ImageSize = new Size(120, 120);
        }

        private void GetProductTypesFromList()
        {
            foreach (Product product in _selectedProducts)
            {
                if (_productTypes.Contains(product.Type))
                    continue;
                _productTypes.Add(product.Type);
            }
        }

        private void SetProductTypesToComboBox()
        {
            foreach (string type in _productTypes)
            {
                GoodTypeComboBox.Items.Add(type);
            }
        }

        private void ApplyFiltersBtn_Click(object sender, EventArgs e)
        {
            ApplyFilters();
            SetProductsToListView();
        }

        private void ApplyFilters()
        {
            _selectedProducts.Clear();

            string choosenType = string.Empty;
            string choosenSort = string.Empty;

            if (GoodTypeComboBox.SelectedItem != null)
                choosenType = GoodTypeComboBox.SelectedItem.ToString() ?? string.Empty;

            if (SortComboBox.SelectedItem != null)
                choosenSort = SortComboBox.SelectedItem.ToString() ?? string.Empty;

            LeaveOnlyNeccessaryProductTypeInList(choosenType);
            SortProductList(choosenSort);
        }

        private void LeaveOnlyNeccessaryProductTypeInList(string choosenType)
        {
            if (string.IsNullOrEmpty(choosenType))
            {
                CopyAllProductsToSelectedProducts();
                return;
            }

            foreach (Product product in _allProducts)
            {
                if (product.Type == choosenType)
                    _selectedProducts.Add(product);
            }
        }
        private void SortProductList(string choosenSort)
        {
            switch (choosenSort)
            {
                case "Price ascending":
                    {
                        PriceAscendingSort();
                        break;
                    }
                case "Price descending":
                    {
                        PriceDescendingSort();
                        break;
                    }
                case "Alphabet order":
                    {
                        AlphabetOrderSort();
                        break;
                    }
                case "Reversed alphabet order":
                    {
                        ReversedAlphabetOrderSort();
                        break;
                    }
            }
        }

        private void PriceAscendingSort()
        {
            Product bubble;

            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (_selectedProducts[i].Price > _selectedProducts[j].Price)
                    {
                        bubble = _selectedProducts[j].Clone();
                        _selectedProducts[j] = _selectedProducts[i].Clone();
                        _selectedProducts[i] = bubble;
                    }
                }
            }
        }

        private void PriceDescendingSort()
        {
            Product bubble;

            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (_selectedProducts[i].Price < _selectedProducts[j].Price)
                    {
                        bubble = _selectedProducts[j].Clone();
                        _selectedProducts[j] = _selectedProducts[i].Clone();
                        _selectedProducts[i] = bubble;
                    }
                }
            }
        }

        private void AlphabetOrderSort()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Product bubble;

            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (alphabet.IndexOf(_selectedProducts[i].Name.ToLower()[0]) >
                        alphabet.IndexOf(_selectedProducts[j].Name.ToLower()[0]))
                    {
                        bubble = _selectedProducts[j].Clone();
                        _selectedProducts[j] = _selectedProducts[i].Clone();
                        _selectedProducts[i] = bubble;
                    }
                }
            }
        }
        private void ReversedAlphabetOrderSort()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Product bubble;

            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (alphabet.IndexOf(_selectedProducts[i].Name.ToLower()[0]) <
                        alphabet.IndexOf(_selectedProducts[j].Name.ToLower()[0]))
                    {
                        bubble = _selectedProducts[j].Clone();
                        _selectedProducts[j] = _selectedProducts[i].Clone();
                        _selectedProducts[i] = bubble;
                    }
                }
            }
        }
    }
}