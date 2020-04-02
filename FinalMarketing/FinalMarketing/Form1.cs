using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalMarketing
{
    public partial class Form1 : Form
    {
        public string sID,sID2;
        public string sql = "";
        public string prodPrice;
        public string customerid;
        public int number2;
        public string quantityProd;
        public string toDeleteItem;
        public string tobeBack;
        public string backProd;
        public string temp;
        public MySqlCommand sql_cmd = new MySqlCommand();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsMySQL.sql_con.Close();
            clsMySQL.sql_con.Open();
            seeCustomerID();
            showAndRefreshProductList();
            showOrderedList();
            totalPrice();
            countItems();
            seeCustomerID();
            showProductinManage();
            disable();
            seeTransaction();
            seeDeliver();
            noOfProducts();
            sellWorth();
            noOfCustomer();
            noQuantity();
            button7.Enabled = false;
        }


        private void showAndRefreshProductList()
        {
            sql = "SELECT * FROM tbproduct";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            lvProd.Items.Clear();
            while (rd.Read())
            {
                lvProd.Items.Add(rd["id"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["price"].ToString());
            }
            rd.Close();
        }
        private void Show_ProductData(string srcID)
        {

            sql = "SELECT * FROM tbproduct WHERE id = " + srcID;
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                lbProduct.Text = rd["productname"].ToString();
                prodPrice = rd["price"].ToString();
                lbBrand.Text = rd["brand"].ToString();
                quantityProd= rd["quantity"].ToString();
                lbPrice.Text = prodPrice;


            }
            rd.Close();

        }

        private void lvProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            sID = lvProd.FocusedItem.Text;
            txQuantity.Text = "";
            if (sID == "" || sID == null) { return; }
            Show_ProductData(sID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txQuantity.Text == "")
            {
                lbPrice.Text = prodPrice;
                return;
            }
            else if (lbBrand.Text == "")
            {
                return;
            }
            else if (!int.TryParse(txQuantity.Text, out number2))
            {
                return;

            }
            else if ( sID != null)
            {
                double total;
                total = Convert.ToDouble(lbPrice.Text) * Convert.ToInt32(txQuantity.Text);

                lbPrice.Text = total.ToString();
            }
            else
            {
                return;
            }
         
         
        }
        private void addToCart()
        {
            sql = string.Format("INSERT INTO tborder VALUES (null, '{0}', '{1}', '{2}','{3}','{4}','{5}')",
        lbProduct.Text, lbBrand.Text, txQuantity.Text, prodPrice,lbPrice.Text,customerid);
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
            updateQuantity();
            showAndRefreshProductList();
            showOrderedList();
            totalPrice();
            countItems();
            txQuantity.Text = "";
            lbProduct.Text = "";
            lbBrand.Text = "";
            lbPrice.Text = "";
            
        }

       // to update the quantity
        private void updateQuantity()
        {
            int x = Convert.ToInt32(txQuantity.Text);
            sql = "UPDATE tbproduct SET quantity = quantity -" + x + " WHERE id = " + sID;
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(txQuantity.Text == "")
            {
                MessageBox.Show("Please input a valid amount of quantity"); 
            }
            else if (sID == null)
            {
                MessageBox.Show("Select a Product First");
            }
            else if(lbBrand.Text =="")
            {
                return;
            }
            else if (Convert.ToInt32(quantityProd) == 0)
            {
                MessageBox.Show("Out of stock");
            }
          else if (!int.TryParse(txQuantity.Text, out number2))
            {
                MessageBox.Show("Please input a valid amount of quantity");
            }
            else if (Convert.ToInt32(txQuantity.Text)>Convert.ToInt32(quantityProd))
            {
                MessageBox.Show("We have insufficient quantity of the selected product");
            }
            
            else
            {
                addToCart();
            }
     
        }

        private void showOrderedList()
        {
            sql = "SELECT * FROM tborder";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            lvOrder.Items.Clear();
            while (rd.Read())
            {
                lvOrder.Items.Add(rd["id"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["totalPrice"].ToString());
            }
            rd.Close();
            countItems();
        }
        private void totalPrice()
        {
            sql = "SELECT SUM(totalPrice) as total FROM tborder";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                lbTotal.Text = rd["total"].ToString();


            }
            rd.Close();
        }
        private void seeCustomerID()
        {
            sql = "SELECT * FROM dbcustomerid where id=1";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                customerid= rd["customerID"].ToString();
                clsMySQL.customerid = rd["customerID"].ToString();

            }
            rd.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void countItems()
        {
            label11.Text = lvOrder.Items.Count.ToString() ;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            removeItemOrder();
            button7.Enabled = false;
        }

        private void removeItemOrder()
        {
            int x = Convert.ToInt32(tobeBack);
            sql = "UPDATE tbproduct SET quantity = quantity +" + x + " WHERE productname = '" + backProd + "'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();


            sID = lvOrder.FocusedItem.Text;
            if (sID == "" || sID == null) { return; }

            else
            {
            

            }
            {
                sql = "DELETE FROM tborder WHERE id=" + sID;
                sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                sql_cmd.ExecuteNonQuery();
                showOrderedList();
                totalPrice();
                showAndRefreshProductList();
            }
        }

        private void lvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7.Enabled = true;
            sID = lvOrder.FocusedItem.Text;
            
            if (sID == "" || sID == null) { return; }
            show_Ordered(sID);
    
        }

        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            sql = "SELECT * FROM tbproduct WHERE productname LIKE '%" + txSearch.Text + "%' OR brand LIKE '%" + txSearch.Text + "%' OR quantity LIKE '%" + txSearch.Text + "%' OR price LIKE '%" + txSearch.Text + "%'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            lvProd.Items.Clear();
            while (rd.Read())
            {
                lvProd.Items.Add(rd["id"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                lvProd.Items[lvProd.Items.Count - 1].SubItems.Add(rd["price"].ToString());
            }
            rd.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sql = "SELECT * FROM tborder WHERE productname LIKE '%" + textBox2.Text + "%' OR brand LIKE '%" + textBox2.Text + "%' OR quantity LIKE '%" + textBox2.Text + "%' OR price LIKE '%" + textBox2.Text + "%' OR id LIKE '%" + textBox2.Text + "%'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            lvOrder.Items.Clear();
            while (rd.Read())
            {
                lvOrder.Items.Add(rd["id"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                lvOrder.Items[lvOrder.Items.Count - 1].SubItems.Add(rd["totalPrice"].ToString());
            }
            rd.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clsMySQL.totalPrice = lbTotal.Text;
                     this.Hide();
            FINALIZE final = new FINALIZE();
       
            final.ShowDialog();
        }

        private void lbTotal_Click(object sender, EventArgs e)
        {

        }
        private void show_Ordered(string srcID)
        {

            sql = "SELECT * FROM tborder WHERE id = " + srcID;
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                tobeBack = rd["quantity"].ToString();
                backProd = rd["productname"].ToString();

            }
            rd.Close();
        }

        // another panel
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(btnadd.Text =="Update Product")
            {
                Update_Product(sID2);
                disable();
                btnadd.Text = "Add Product";
            }
            else if (btnadd.Text == "Add Product")
            {

                if (!int.TryParse(txPrice.Text, out number2) || !int.TryParse(textBox1.Text, out number2))
                {
                    MessageBox.Show("Please put a valid amount");
                }
                else
                {
                    addToTbadd();
                    addToProduct();
                    noOfProducts();
                }
            
            }
         
        }

        private void addToProduct()
        {
                sql = string.Format("INSERT INTO tbproduct VALUES (null, '{0}', '{1}', '{2}','{3}')",
                txProdname.Text, txBrand.Text, textBox1.Text, txPrice.Text);
                sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                sql_cmd.ExecuteNonQuery();
                showProductinManage();
                clearAll();
                disable();
                MessageBox.Show("New product has been added successfully!", "Add Record");
        }
        private void showProductinManage()
        {
            sql = "SELECT * FROM tbproduct";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            listView1.Items.Clear();
            while (rd.Read())
            {
                listView1.Items.Add(rd["id"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["price"].ToString());
              
            }
            rd.Close();
        }
        private void disable()
        {
            txProdname.Enabled = false;
            textBox1.Enabled = false;
            txPrice.Enabled = false;
            txDeliver.Enabled = false;
            txBrand.Enabled = false;
            btnadd.Enabled = false;
        }

        private void enable()
        {
            btnadd.Enabled = true;
            txProdname.Enabled = true;
            textBox1.Enabled = true;
            txPrice.Enabled = true;
            txDeliver.Enabled = true;
            txBrand.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            sql = "SELECT * FROM tbproduct WHERE productname LIKE '%" + textBox3.Text + "%' OR quantity LIKE '%" + textBox3.Text + "%' OR price LIKE '%" + textBox3.Text + "%' OR brand LIKE '%" + textBox3.Text + "%'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            listView1.Items.Clear();
            while (rd.Read())
            {
                listView1.Items.Add(rd["id"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(rd["price"].ToString());
            }
            rd.Close();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Add Product";
            enable();
            clearAll();
        }

        private void clearAll()
        {
            txProdname.Text = "";
            textBox1.Text = "";
            txPrice.Text = "";
            txDeliver.Text = "";
            txBrand.Text = "";
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Add Product";
            disable();
            clearAll();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sID2 = listView1.FocusedItem.Text;

            if (sID2 == "" || sID2 == null) { return; }
            editProd(sID2);
        }

        private void editProd(string srcID)
        {
            sql = "SELECT * FROM tbproduct WHERE id = " + srcID;
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                txProdname.Text = rd["productname"].ToString();
                textBox1.Text = rd["quantity"].ToString();
                txBrand.Text = rd["brand"].ToString();
                txPrice.Text = rd["price"].ToString();
            }
            rd.Close();
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Update Product";
            enable();
            txPrice.Enabled = false;

        }


        private void Update_Product(string srcID)
        {

            sql = "UPDATE tbproduct SET productname = '" + txProdname.Text + "', quantity = '" + textBox1.Text + "', brand = '" + txBrand.Text + "', price = '" + txPrice.Text + "' WHERE id ='" + srcID+"'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
            showProductinManage();
            MessageBox.Show("Product has been update successfully!", "Update Product");
            clearAll();
            
        }

        private void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sID2 = listView1.FocusedItem.Text;
            if (sID2 == "" || sID2 == null) { return; }
            else
            {

            }
            {
                sql = "DELETE FROM tbproduct WHERE id=" + sID2;
                sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                sql_cmd.ExecuteNonQuery();
                clearAll();
                showProductinManage();

            }
        }
        private void addToTbadd()
        {

            
            sql = string.Format("INSERT INTO tbadded VALUES (null, '{0}', '{1}', '{2}','{3}', '{4}','{5}', '{6}')",
               txProdname.Text, txBrand.Text, textBox1.Text, txPrice.Text, txDeliver.Text, System.DateTime.Today.ToString("dd/MM/yy"),DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo));
               sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                sql_cmd.ExecuteNonQuery();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void seeTransaction()
        {
            sql = "SELECT * FROM tbtransactions";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            listView2.Items.Clear();
            while (rd.Read())
            {
                listView2.Items.Add(rd["id"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["customerid"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["customername"].ToString());
        
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["product"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["totalprice"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["date"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["time"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["transaction"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["cashreceive"].ToString());
                listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["changes"].ToString());
            }
            rd.Close();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void seeDeliver()
        {
            sql = "SELECT * FROM tbadded";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            listView3.Items.Clear();
            while (rd.Read())
            {
                listView3.Items.Add(rd["id"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["delivered"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["date"].ToString());
                listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["time"].ToString());
            }
            rd.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Products Delivered")
            {
                listView2.Visible = false;
                listView3.Visible = true;
            }
            else if(comboBox1.Text == "Products Ordered")
            {
                listView2.Visible = true;
                listView3.Visible = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            login lag = new login();
            this.Hide();
            lag.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login lag = new login();
            this.Hide();
            lag.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Products Delivered")
            {
                sql = "SELECT * FROM tbadded WHERE productname LIKE '%" + textBox4.Text + "%' OR brand LIKE '%" + textBox4.Text + "%' OR quantity LIKE '%" + textBox4.Text + "%' OR price LIKE '%" + textBox4.Text + "%' OR id LIKE '%" + textBox4.Text + "%' OR delivered LIKE '%" + textBox4.Text + "%'";
                sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                MySqlDataReader rd = sql_cmd.ExecuteReader();
                listView3.Items.Clear();
                while (rd.Read())
                {
                    listView3.Items.Add(rd["id"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["productname"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["delivered"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["date"].ToString());
                    listView3.Items[listView3.Items.Count - 1].SubItems.Add(rd["time"].ToString());
                }
                rd.Close();
            }
            else if (comboBox1.Text == "Products Ordered")
            {
                sql = "SELECT * FROM tbtransactions WHERE product LIKE '%" + textBox4.Text + "%' OR brand LIKE '%" + textBox4.Text + "%' OR quantity LIKE '%" + textBox4.Text + "%' OR price LIKE '%" + textBox4.Text + "%' OR id LIKE '%" + textBox4.Text + "%' OR totalprice LIKE '%" + textBox4.Text + "%' OR cashreceive LIKE '%" + textBox4.Text + "%'OR changes LIKE '%" + textBox4.Text + "%'OR customerid LIKE '%" + textBox4.Text + "%'OR customername LIKE '%" + textBox4.Text + "%'";
                sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
                MySqlDataReader rd = sql_cmd.ExecuteReader();
                listView2.Items.Clear();
                while (rd.Read())
                {

                    listView2.Items.Add(rd["id"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["customerid"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["customername"].ToString());

                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["product"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["brand"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["quantity"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["price"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["totalprice"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["date"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["time"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["transaction"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["cashreceive"].ToString());
                    listView2.Items[listView2.Items.Count - 1].SubItems.Add(rd["changes"].ToString());
                }
                rd.Close();
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        private void noOfProducts()
        {
            sql = "SELECT COUNT(*) AS TOTAL FROM tbproduct";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                label23.Text = rd["TOTAL"].ToString();
                
            }
            rd.Close();
        }

        private void sellWorth()
        {
            sql = "SELECT sum(totalprice) as total FROM tbtransactions";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                label24.Text = rd["TOTAL"].ToString();
                
            }
            rd.Close();
        }
        private void noOfCustomer()
        {
            sql = "SELECT customerID  FROM dbcustomerid where id = 1";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                temp = rd["customerID"].ToString();
                int x = Convert.ToInt32(temp) - 1;
                label25.Text = x.ToString();
            }
            rd.Close();
        }


        private void noQuantity()
        {

            sql = " SELECT sum(quantity) as total FROM tbtransactions";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            MySqlDataReader rd = sql_cmd.ExecuteReader();
            while (rd.Read())
            {


                label26.Text = rd["total"].ToString();
              
            }
            rd.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            panel5.Visible = true;
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            panel5.Visible = false;
            panel4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel5.Visible = true;
            panel4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            panel6.Visible = false;
            panel5.Visible = false;
            panel4.Visible = false;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
