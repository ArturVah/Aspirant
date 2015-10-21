using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace AspirantSearch
{
    public partial class AspirantSearchingSystem : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public AspirantSearchingSystem()
        {
            InitializeComponent();
        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                (@"Data Source=Aspirant.sqlite;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }


        public DataTable GetDataTable(string sql)
        {

            DataTable dt = new DataTable();

            try
            {
                SQLiteConnection cnn = new SQLiteConnection("Data Source=Aspirant.sqlite;");

                cnn.Open();

                SQLiteCommand mycommand = new SQLiteCommand(cnn);

                mycommand.CommandText = sql;

                SQLiteDataReader reader = mycommand.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                cnn.Close();

            }

            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
            return dt;

        }


        private void Add()
        {
            try
            {
                string txtSQLQuery =
                    "insert into  Aspirant (NAME,SURNAME,AGE,ADDRESS,INFO)" +
                    " values ('" + txtName.Text + "','" + txtSurname.Text + "','" +
                    txtAge.Text + "','" + txtAdress.Text + "','" + txtInfo.Text + "');";

                txtName.Text = "";
                txtSurname.Text = "";
                txtAge.Text = "";
                txtAdress.Text = "";
                txtInfo.Text = "";

                ExecuteQuery(txtSQLQuery);
            }

            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Get()
        {

            string query = "SELECT NAME,SURNAME,AGE,ADDRESS,INFO  FROM Aspirant Where NAME =  '"
            + txtSelectName.Text + "';";

            

            DataTable dt;
            dt = GetDataTable(query);

            dataGridView1.DataSource = dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            Add();

        }

        private void btnGetPerson_Click(object sender, EventArgs e)
        {

            Get();
        }
    }
}