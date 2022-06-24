using NOLinqToXmlExample;
using System.Xml;

namespace LinqToXmlExample
{
    public partial class Form1 : Form
    {
        private const string _alphabet = "abcdefghijklmnopqrstuvwxyz";

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
            SetTrackBarMinAndMaxValues();
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

        private void SetTrackBarMinAndMaxValues()
        {
            minPriceTrackBar.Minimum = GetLowestProductPrice();
            minPriceTrackBar.Maximum = GetMidProductPrice();

            maxPriceTrackBar.Minimum = GetMidProductPrice();
            maxPriceTrackBar.Maximum = GetHighestProductPrice();

            maxPriceTrackBar.Value = maxPriceTrackBar.Maximum;

            minPriceValueLabel.Text = minPriceTrackBar.Value.ToString() + "$";
            maxPriceValueLabel.Text = maxPriceTrackBar.Value.ToString() + "$";
        }

        private int GetLowestProductPrice()
        {
            int lowest = int.MaxValue;
            foreach(Product product in _allProducts)
            {
                lowest = Math.Min(lowest, product.Price);
            }
            return lowest;
        }

        private int GetHighestProductPrice()
        {
            int highest = 0;
            foreach (Product product in _allProducts)
            {
                highest = Math.Max(highest, product.Price);
            }
            return highest;
        }

        private int GetMidProductPrice()
        {
            int average = 0;
            foreach(Product product in _allProducts)
            {
                average += product.Price;
            }
            return average/_allProducts.Count();
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
            string searchKeyword = string.Empty;

            if (GoodTypeComboBox.SelectedItem != null)
                choosenType = GoodTypeComboBox.SelectedItem.ToString() ?? string.Empty;

            if (SortComboBox.SelectedItem != null)
                choosenSort = SortComboBox.SelectedItem.ToString() ?? string.Empty;

            if (!SearchTextBox.Text.Equals("Search...") && !string.IsNullOrEmpty(SearchTextBox.Text)) 
                searchKeyword = SearchTextBox.Text;

            LeaveOnlyNeccessaryProductTypeInList(choosenType);
            ApplyPriceFilter();
            SortProductList(choosenSort);
            ApplySearchByKeyWord(searchKeyword);
        }

        private void LeaveOnlyNeccessaryProductTypeInList(string choosenType)
        {
            if (string.IsNullOrEmpty(choosenType) || choosenType == "all")
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

        private void ApplyPriceFilter()
        {
            int minPrice = minPriceTrackBar.Value;
            int maxPrice = maxPriceTrackBar.Value;

            for(int i = 0; i < _selectedProducts.Count; i++)
            {
                if(ProductPriceIsOutOfPriceRange(_selectedProducts[i], minPrice, maxPrice))
                {
                    _selectedProducts.RemoveAt(i);
                    i--;
                }
            }
        }

        private bool ProductPriceIsOutOfPriceRange(Product product, int minPrice, int maxPrice)
        {
            return product.Price > maxPrice || product.Price < minPrice;
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

        private void Swap(int i, int j)
        {
            Product bubble;
            bubble = _selectedProducts[j].Clone();
            _selectedProducts[j] = _selectedProducts[i].Clone();
            _selectedProducts[i] = bubble;
        }

        private void PriceAscendingSort()
        {
            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (_selectedProducts[i].Price > _selectedProducts[j].Price)
                    {
                        Swap(i, j);
                    }
                }
            }
        }

        private void PriceDescendingSort()
        {
            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (_selectedProducts[i].Price < _selectedProducts[j].Price)
                    {
                        Swap(i, j);
                    }
                }
            }
        }

        private int GetAlphabetNumberOfProductName(Product product)
        {
            return _alphabet.IndexOf(product.Name.ToLower()[0]);
        }

        private void AlphabetOrderSort()
        {

            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (GetAlphabetNumberOfProductName(_selectedProducts[i])>
                        GetAlphabetNumberOfProductName(_selectedProducts[j]))
                    {
                        Swap(i, j);
                    }
                }
            }
        }

        private void ReversedAlphabetOrderSort()
        {
            
            for (int i = 0; i < _selectedProducts.Count; i++)
            {
                for (int j = i; j < _selectedProducts.Count; j++)
                {
                    if (GetAlphabetNumberOfProductName(_selectedProducts[i]) <
                        GetAlphabetNumberOfProductName(_selectedProducts[j]))
                    {
                        Swap(i, j);
                    }
                }
            }
        }

        private void ApplySearchByKeyWord(string keyword)
        {
            for(int i = 0; i < _selectedProducts.Count; i++)
            {
                if (ProductNameDoesNotContainKeyword(_selectedProducts[i], keyword))
                {
                    _selectedProducts.RemoveAt(i);
                    i--;
                }
            }
        }

        private bool ProductNameDoesNotContainKeyword(Product product, string keyword)
        {
            return !product.Name.ToLower().Contains(keyword.ToLower());
        }

        private void minPriceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            minPriceValueLabel.Text = minPriceTrackBar.Value.ToString() + "$";
        }

        private void maxPriceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            maxPriceValueLabel.Text = maxPriceTrackBar.Value.ToString() + "$";
        }
    }
}