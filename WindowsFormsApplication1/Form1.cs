﻿using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }        
        public XmlReader xmlEmployees;
        public XmlReader xmlItems;
        public XmlReader xmlOrders;
        public XmlReader xmlParts;

        public DataSet dsEmployees;
        public DataSet dsItems;
        public DataSet dsOrders;
        public DataSet dsParts;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //xmlEmployees = XmlReader.Create("employee.xml", new XmlReaderSettings());
                xmlEmployees = XmlReader.Create(new StringReader(GET("http://localhost/docxml.php?file=employee")));

                dsEmployees = new DataSet();
                dsEmployees.ReadXml(xmlEmployees);
                dataGridView1.DataSource = dsEmployees.Tables[0];

                string[] namecol = new string[6] { "№ сотрудника", "Фамилия", "Имя", "PhoneExt", "Дата найма", "Зарплата" };
                                
                for (int i= 0; i < 6; i++)     dataGridView1.Columns[i].HeaderText = namecol[i];
                items();
                orders();
                parts();           
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.ShowDialog();
        }

        
        private void filt1(int numcol, string namecol)
        {
            string act = dataGridView1.CurrentRow.Cells[numcol].Value.ToString();
            DataTable firstTable = dsOrders.Tables[0];
            DataView view1 = new DataView(firstTable);
            BindingSource bs = new BindingSource();
            bs.DataSource = view1;
            bs.Filter = String.Format(namecol+" LIKE '{0}'", act);
            dataGridView3.DataSource = bs;

        }
        private void filt2(int numcol, string namecol)
        {
            string act = dataGridView3.CurrentRow.Cells[numcol].Value.ToString();
            DataTable firstTable = dsItems.Tables[0];
            DataView view1 = new DataView(firstTable);
            BindingSource bs = new BindingSource();
            bs.DataSource = view1;
            bs.Filter = String.Format(namecol + " LIKE '{0}'", act);
            dataGridView2.DataSource = bs;

        }
        private void filt3(int numcol, string namecol)
        {
            string act = dataGridView4.CurrentRow.Cells[numcol].Value.ToString();
            DataTable firstTable = dsItems.Tables[0];
            DataView view1 = new DataView(firstTable);
            BindingSource bs = new BindingSource();
            bs.DataSource = view1;
            bs.Filter = String.Format(namecol + " LIKE '{0}'", act);
            dataGridView2.DataSource = bs;

        }
        private void items()
            {
            try
            {
                //xmlEmployees = XmlReader.Create("items.xml", new XmlReaderSettings());
                xmlItems = XmlReader.Create(new StringReader(GET("http://localhost/docxml.php?file=items")));
                dsItems = new DataSet();
                dsItems.ReadXml(xmlItems);
                dataGridView2.DataSource = dsItems.Tables[0];
                string[] namecol = new string[5] { "№ заказа", "№ предмета", "№ детали", "Qty", "Скидка" };
                for (int i = 0; i < 5; i++) dataGridView2.Columns[i].HeaderText = namecol[i];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private string GET(string Url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            string output ="";
            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                output+=stream.ReadToEnd();
            }
            return output;
        }

        private void orders (){
            try
            {
                //xmlEmployees = XmlReader.Create("orders.xml", new XmlReaderSettings());
                xmlOrders = XmlReader.Create(new StringReader(GET("http://localhost/docxml.php?file=orders")));
                dsOrders = new DataSet();
                dsOrders.ReadXml(xmlOrders);
                dataGridView3.DataSource = dsOrders.Tables[0];
                string[] namecol = new string[5] { "№ заказа", "№ клиента", "Дата продажи", "Дата доставки", "№ сотрудника" };
                for (int i = 0; i < 5; i++) dataGridView3.Columns[i].HeaderText = namecol[i];
            }
            catch (Exception ex)
            { }
        }
        private void parts()
        {
            try
            {
                //xmlEmployees = XmlReader.Create("parts.xml", new XmlReaderSettings());
                xmlParts = XmlReader.Create(new StringReader(GET("http://localhost/docxml.php?file=parts")));
                dsParts = new DataSet();
                dsParts.ReadXml(xmlParts);
                dataGridView4.DataSource = dsParts.Tables[0];
                string[] namecol = new string[7] { "№ детали", "№ поставщика", "Описание", "В наличии", "На заказ", "Стоимость", "Список Цен" };
                for (int i = 0; i < 7; i++) dataGridView4.Columns[i].HeaderText = namecol[i];
            }
            catch (Exception ex)
            { }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void helpContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.TopicId;
            Help.ShowHelp (this, @"10.html", navigator);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filt1(0,"EmpNo");
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filt2(0, "OrderNo");
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filt3(0, "PartNo");
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = "Таблица для отображения сотрудников";
        }

        private void dataGridView3_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = "Таблица для отображения заказов";
        }

        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = "Таблица для отображения предметов";
        }

        private void dataGridView4_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = "Таблица для отображения деталей";
        }

        private void dataGridView1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            System.Diagnostics.Process.Start(@"20.html");
        }

        private void dataGridView3_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            System.Diagnostics.Process.Start(@"30.html");
        }

        private void dataGridView2_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            System.Diagnostics.Process.Start(@"40.html");
        }

        private void dataGridView4_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            System.Diagnostics.Process.Start(@"50.html");
        }
    }
}
