using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassPlaner
{
    public partial class ViewClasses : Form
    {
        public ViewClasses()
        {
            InitializeComponent();
        }

        private void ViewClasses_Load(object sender, EventArgs e)
        {

            string path = "C:\\Users\\Ithamar\\Documents\\Visual Studio 2015\\Projects\\ClassPlaner\\ClassPlaner\\Clases.txt";

            try
            {
                StreamReader sr = File.OpenText(path);
                listView1.View = View.Details;
                string s = "";
                string[] row;

                while ((s = sr.ReadLine()) != null)
                {
                    row = s.Split(',');
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);

                    s = "";
                    row = new string[0];                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("File not found");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
