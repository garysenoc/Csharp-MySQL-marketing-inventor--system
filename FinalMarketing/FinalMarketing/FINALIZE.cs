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
    public partial class FINALIZE : Form
    {
        public string sID;
        public string sql = "";
        public MySqlCommand sql_cmd = new MySqlCommand();
        public string date;
        public int number2;
        public FINALIZE()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close();
            main.ShowDialog();
        }

        private void FINALIZE_Load(object sender, EventArgs e)
        {
            timeAndDate();
            lbTotal.Text = clsMySQL.totalPrice;
        }

        private void timeAndDate()
        {
            date = System.DateTime.Today.ToString("dd/MM/yy");
            lbDate.Text = date;

            lbTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {

                return;
            }
            else if (!int.TryParse(textBox3.Text, out number2))
            {
                return;

            }
            else if (lbTotal.Text == "")
            {
                return;
            }
            else
            {
                double total;
                total = Convert.ToDouble(textBox3.Text) - Convert.ToDouble(lbTotal.Text);

                lbChange.Text = total.ToString();

            }
        }

        private void finalize()
        {
            sql = "INSERT INTO tbtransactions(product,brand,quantity,price,totalprice,customerid) SELECT productname,brand,quantity,price,totalPrice,customerid from tborder";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out number2))
            {
                MessageBox.Show("Please input a valid amount of Cash received");
            }
            else if (lbTotal.Text == "")
            {
                MessageBox.Show("You must order a product");
            }
            else if (Convert.ToDouble(lbTotal.Text) > Convert.ToDouble(textBox3.Text))
            {
                MessageBox.Show("Your money is insufficient");
            }

            else
            {
                finalize();
                updateTransaction();
                deleteOrder();
                updateCustomerID();
                MessageBox.Show("Transaction is complete");
                Form1 main = new Form1();
                this.Hide();
                main.ShowDialog();
            }
        }

        private void updateTransaction()
        {
            sql = "UPDATE tbtransactions SET customername = '" + textBox1.Text + "', date = '" + lbDate.Text + "', time = '" + lbTime.Text + "', transaction = 'Ordered Products', cashreceive = '" + textBox3.Text + "', changes = '" + lbChange.Text + "' where customerid = '" + clsMySQL.customerid + "'";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
        }

        private void deleteOrder()
        {

            sql = "DELETE FROM tborder ";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
        }

        private void updateCustomerID()
        {
            sql = "UPDATE dbcustomerid SET customerID = customerID + 1 WHERE id =1 ";
            sql_cmd = new MySqlCommand(sql, clsMySQL.sql_con);
            sql_cmd.ExecuteNonQuery();
        }
    }
}
