using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassPlaner
{
    public partial class ViewTeachers : Form
    {
        public ViewTeachers()
        {
            InitializeComponent();

            List<string> maestros_clases = new List<string>();
            string path = "C:\\Users\\Ithamar\\Documents\\Visual Studio 2015\\Projects\\ClassPlaner\\ClassPlaner\\Maestro_Clases.txt";
            try
            {
                StreamReader sr = File.OpenText(path);
                listView1.View = View.Details;
                string s = "";
                string[] row;
                string[] clases;
                string nombre = "";
                string clase = "";
                string hora = "";
                string[] arr;
                string[] arr2;
                while ((s = sr.ReadLine()) != null)
                {
                    row = s.Split(',');
                    nombre = row[0];
                    clases = row[1].Split('$');
                    //Console.Write(nombre + "\t");
                    for (int i = 0; i < clases.Length; i++)
                    {
                        clase = clases[i].Split('#')[0];
                        hora = clases[i].Split('#')[1];
                        //Console.WriteLine(clase + ": " + hora);
                        if (maestros_clases.Contains(nombre))
                        {
                            maestros_clases.Add("\t" + "," + clase + "," + hora);
                        }
                        else
                        {
                            maestros_clases.Add(nombre + "," + clase + "," + hora);
                        }
                    }
                    arr = maestros_clases.ToArray();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr2 = arr[i].Split(',');
                        //Console.WriteLine(arr2[0] + " " + arr2[1] + " " + arr2[2]);
                        var listViewItem = new ListViewItem(arr2);
                        listView1.Items.Add(listViewItem);
                    }
                    s = "";
                    nombre = "";
                    hora = "";
                    clases = new string[0];
                    arr = new string[0];
                    arr2 = new string[0];
                    row = new string[0];
                    maestros_clases.Clear();
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
