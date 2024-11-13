using Dapper;
using Project05_DapperNorthwind.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project05_DapperNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection connection = new SqlConnection ("Server='(localdb)\\MSSQLLocalDB';initial catalog='Db5Project20_northwind';integrated security=true");
        private async void btnCategoryList_Click(object sender, EventArgs e)
        {
            string query = "Select * from Categories";
            var values = await connection.QueryAsync<ResultCategoryDto>(query);

            dataGridView1.DataSource = values;

        }

        private async void btnCategoryCreate_Click(object sender, EventArgs e)
        {
            string query = "insert into Categories (CategoryName,Description) Values (@p1,@p2)";

            var parameters = new DynamicParameters();
            parameters.Add("@p1",textBoxCategoryName.Text);
            parameters.Add("@p2",textBoxCategoryDescription.Text);
            await connection.ExecuteAsync(query, parameters);
        }

        private async void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete from Categories Where CategoryId = @categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId",textBoxCategoryId.Text);
            await connection.ExecuteAsync(query,parameters);
        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update  Categories Set CategoryName=@categoryName,Description=@description Where CategoryId= @categoryId";
            var parameter = new DynamicParameters();
            parameter.Add("@categoryName",textBoxCategoryName);
            parameter.Add("@categoryId",textBoxCategoryId);
            parameter.Add("@description",textBoxCategoryDescription);
            await connection.ExecuteAsync(query, parameter);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Kategori Sayısı
            string query = "Select Count(*) from Categories ";
            var count = await connection.ExecuteScalarAsync<int>(query);
            lblCategoryCount.Text = "Toplam Kategori Sayısı: "+ count.ToString();

            // Ürün Sayısı

            string q = "Select Count(*) from Products";
            var count2= await connection.ExecuteScalarAsync<int>(q);
            lblProductCount.Text = "Toplam Ürün Sayısı: " + count2.ToString();

            // Ortalama ÜrünStok Sayısı

            string queryStock = "Select Avg(UnitsInStock) from Products";
            var countStock = await connection.ExecuteScalarAsync<int>(queryStock);
            lblAverageProductStock.Text="Toplam Ürün Stoğu Sayısı: " + countStock.ToString();

            //Deniz Ürünleri Toplam Birim Fiyatı

            string querySeafood = "Select Sum(UnitPrice) from Products p Join Categories c On c.CategoryId= p.CategoryId Where c.CategoryName='Seafood'";
            var countSeafood = await connection.ExecuteScalarAsync<double>(querySeafood);
            lblSeaFoodTotalPrice.Text = "Deniz Ürünleri Toplam Birim Fiyatı: " + countSeafood.ToString();
        }
    }
}
