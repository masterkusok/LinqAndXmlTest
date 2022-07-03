using EFxLinqToXmlExample;

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

            GetAllProducts();
            LoadImagesToListView();
            SetProductsToListView();
            GetProductTypesFromList();
            SetProductTypesToComboBox();
            SetTrackBarMinAndMaxValues();
        }

        private void GetAllProducts()
        {
            try
            {
                LoadProductsFromContextToList();
                CopyAllProductsToSelectedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during loading products from Context: \n {ex}");
            }
        }

        private void LoadProductsFromContextToList()
        {
            using (ProductContext context = new ProductContext())
            {
                if(context.Products.Count() == 0)
                {
                    DefaultProductsFiller.FillDbWithDefaultProducts();
                }
                _allProducts = context.Products.ToList();
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
                item.ImageIndex = product.ProductID - 1;
                GoodsListView.Items.Add(item);
            }

            GoodsListView.LargeImageList.ImageSize = new Size(120, 120);
        }

        private void GetProductTypesFromList()
        {
            List<string> types = _allProducts.Select(product => product.Type).ToList();
            foreach (string type in types.Distinct())
            {
                GoodTypeComboBox.Items.Add(type);
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
            minPriceTrackBar.Minimum = _allProducts.Min(product => product.Price);
            minPriceTrackBar.Maximum = (int)_allProducts.Average(product => product.Price);

            maxPriceTrackBar.Minimum = (int)_allProducts.Average(product => product.Price);
            maxPriceTrackBar.Maximum = _allProducts.Max(product => product.Price);

            maxPriceTrackBar.Value = maxPriceTrackBar.Maximum;

            minPriceValueLabel.Text = minPriceTrackBar.Value.ToString() + "$";
            maxPriceValueLabel.Text = maxPriceTrackBar.Value.ToString() + "$";
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

            _selectedProducts = _allProducts.Where(product => product.Type.Equals(choosenType)).ToList();
        }

        private void ApplyPriceFilter()
        {
            int minPrice = minPriceTrackBar.Value;
            int maxPrice = maxPriceTrackBar.Value;

            _selectedProducts = _selectedProducts.
                Where(product => product.Price >= minPrice && product.Price <= maxPrice).ToList();
        }

        private void SortProductList(string choosenSort)
        {
            switch (choosenSort)
            {
                case "Price ascending":
                    {
                        _selectedProducts = _selectedProducts.OrderBy(product => product.Price).ToList();
                        break;
                    }
                case "Price descending":
                    {
                        _selectedProducts = _selectedProducts.OrderByDescending(product => product.Price).ToList();
                        break;
                    }
                case "Alphabet order":
                    {
                        _selectedProducts =
                            _selectedProducts.OrderBy(product => GetAlphabetNumberOfProductName(product)).ToList();
                        break;
                    }
                case "Reversed alphabet order":
                    {
                        _selectedProducts =
                            _selectedProducts.OrderByDescending(product => GetAlphabetNumberOfProductName(product)).ToList();
                        break;
                    }
            }
        }

        private int GetAlphabetNumberOfProductName(Product product)
        {
            return _alphabet.IndexOf(product.Name.ToLower()[0]);
        }

        private void ApplySearchByKeyWord(string keyword)
        {
            _selectedProducts = _selectedProducts.Where(product => ProductNameContainsKeyword(product, keyword)).ToList();
        }

        private bool ProductNameContainsKeyword(Product product, string keyword)
        {
            return product.Name.ToLower().Contains(keyword.ToLower());
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