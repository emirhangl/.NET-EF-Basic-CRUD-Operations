using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProjectEmirhan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ListCategories();
            ListProducts();
        }

        /// <summary>
        /// Lists all of the products
        /// </summary>
        private void ListProducts()
        {
            using (var context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.ToList();
            }
        }

        /// <summary>
        /// Fills DataGridView according to given CategoryId parameter.
        /// </summary>
        /// <param name="categoryId"></param>
        private void ListProductsByCategoryId(int categoryId)
        {
            using (var context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.CategoryId == categoryId).ToList();
            }
        }

        /// <summary>
        /// Fills DataGridView according to given key parameter if contains any character.
        /// </summary>
        /// <param name="key"></param>
        private void ListProductsByProductName(string key)
        {
            using (var context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.ProductName.Contains(key)).ToList();
            }
        }

        /// <summary>
        /// Fills Categories dropdown menu
        /// </summary>
        private void ListCategories()
        {
            using (var context = new NorthwindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = "CategoryName";
                cbxCategory.ValueMember = "CategoryId";
            }
        }

        /// <summary>
        /// Lists products by CategoryId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListProductsByCategoryId(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch (Exception exception)
            {
                
            }
            
        }

        /// <summary>
        /// Searches for the key from DB, returns result-set of products if any character matches, when value is changed in Search Textbox.
        /// If Textbox is empty, lists all the products.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            string key = tbxSearch.Text;
            if (string.IsNullOrEmpty(key))
            {
                ListProducts();
            }
            else
            {
                ListProductsByProductName(tbxSearch.Text);
            }
            
        }
    }
}
