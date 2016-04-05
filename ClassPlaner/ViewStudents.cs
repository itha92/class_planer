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
    public partial class ViewStudents : Form
    {
        public ViewStudents()
        {
            InitializeComponent();
        }

        private void ViewStudents_Load(object sender, EventArgs e)
        {
            List<string> estudiante_clases = new List<string>();
            string path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Estudiantes_Clases.txt";
            try
            {
                StreamReader sr = File.OpenText(path);
                listView1.View = View.Details;
                string s = "";
                string[] row;
                string[] comp = new string[2];

                while ((s = sr.ReadLine()) != null)
                {
                    row = s.Split(',');
                    comp[0]= row[0];
                    comp[1] = row[1];
                    
                    var listViewItem = new ListViewItem(comp);
                    listView1.Items.Add(listViewItem);

                    s = "";
                    comp = new string[2];
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
