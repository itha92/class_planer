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
    public partial class ViewTheMagic : Form
    {
        public ViewTheMagic()
        {
            InitializeComponent();
        }


        private class Hora_Clase_Maestro
        {
            
            public string hora;
            public string codigo_clase;
            public string nombre_maestro;
            
            public Hora_Clase_Maestro() { }
            public Hora_Clase_Maestro( string hora, string codigo_clase, string nombre_maestro)
            {
                this.hora = hora;
                this.codigo_clase = codigo_clase;
                this.nombre_maestro = nombre_maestro;
            }
            
        }

        private void ViewTheMagic_Load(object sender, EventArgs e)
        {
            
            Dictionary<string, Dictionary<string, string>> maestros_horas_clases = GetTeachers();
            
            List<string> horas = GetHours();
            List< Hora_Clase_Maestro> l = new List<Hora_Clase_Maestro>();

            Dictionary<string, int> students_by_class = GetStudentsByClass();

            foreach (var item in horas)
            {
                foreach (var maestro in maestros_horas_clases)
                {
                    //Console.WriteLine("Maestro: " + maestro.Key + " ");

                    foreach (var clase_hora in maestro.Value)
                    {
                        if (String.Equals(item.Trim(), clase_hora.Key.Trim()))
                        {
                            l.Add(new Hora_Clase_Maestro(item, clase_hora.Value, maestro.Key));
                        }
                    }
                }
             }

            listView1.View = View.Details;
            Random r = new Random();
            l.OrderBy(o => o.hora);
            string[] row = new string[5];
            int cont = 0;
            string tmp = "07:00 AM"; 
            foreach (var item in l)
            {

                while (!String.Equals(item.hora.Trim(), tmp.Trim()))
                {
                    tmp = item.hora.Trim();
                    cont = 0;
                }
                row[0] = item.hora;
                row[2] = item.nombre_maestro;
                row[1] = item.codigo_clase;
                row[3] = students_by_class[item.codigo_clase.Trim()].ToString();
                if (cont < 10)
                {
                    row[4] = "R0" + cont;
                }
                else
                {
                    row[4] = "R" + cont;
                }
                cont++;
                
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);

                //Console.WriteLine("Hora: " + item.hora + " Maestro: "+ item.nombre_maestro + " Clase: "+ item.codigo_clase);
            }
            
            
            /*foreach (var item in students_by_class)
            {
                Console.WriteLine("clase: " + item.Key + " estudiantes: " + item.Value);
            }*/
            //load class by students



            // dictionary key hora value clase
            // dictionary key maestros value clase
            // dictionary key hora value maestros


        }

        public Dictionary<string, int> GetStudentsByClass()
        {
            string path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Estudiantes_Clases.txt";

            Dictionary<string, int> students_by_class = new Dictionary<string, int>();
            // <nombre < hora, clase>>
            
            try
            {
                StreamReader sr = File.OpenText(path);
                string s = "";

                string[] tmp2;
                string[] cl;
                Dictionary<int, string> clases = GetClasses();
               
                var n = clases.OrderBy(o=> o.Value).ToList();
                
                while ((s = sr.ReadLine()) != null)
                {
                    tmp2 = s.Split(',');
                    
                    foreach (var item in clases)
                    {
                        cl = tmp2[1].Split('#');
                        for (int i = 0; i < cl.Length; i++)
                        {
                            //Console.WriteLine(cl[i]);
                            if (String.Equals(cl[i].Trim(), item.Value.Trim() ) )
                            {
                                if (students_by_class.ContainsKey(item.Value))
                                {
                                    int cont = students_by_class[item.Value];
                                    students_by_class[item.Value] = cont + 1;
                                }
                                else
                                {
                                    students_by_class.Add(item.Value.Trim(), 1);
                                }
                            }
                        }
                    }
                    s = "";
                    tmp2 = new string[0];

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //Console.WriteLine("File not found");
            }
            //Console.WriteLine("Cool 3");
            return students_by_class;
        }


        public Dictionary<string, Dictionary<string, string>> GetTeachers()
        {
            string path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Maestro_Clases.txt";
            
            Dictionary<string, Dictionary<string, string>> maestro_clases_hora = new Dictionary<string, Dictionary<string, string>>();
            // <nombre < hora, clase>>
            try
            {
                StreamReader sr = File.OpenText(path);
                string s = "";

                string[] tmp2;
                string clases = "";

                while ((s = sr.ReadLine()) != null)
                {
                    Dictionary<string, string> horas_clases = new Dictionary<string, string>();
                    //horas_clases.Clear();

                    clases = s.Split(',')[1]; //clase#hora $ clase#hora $ clase#hora
                    
                    tmp2 = clases.Split('$');
                    for (int i = 0; i < tmp2.Length; i++)
                    {
                        // {[clase # hora],[clase # hora],[clase # hora]}
                        horas_clases.Add(tmp2[i].Split('#')[1], tmp2[i].Split('#')[0]);
                    }
                    maestro_clases_hora.Add(s.Split(',')[0], horas_clases);

                    s = "";
                    clases = "";
                    tmp2 = new string[0];
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //Console.WriteLine("File not found");
            }
            //Console.WriteLine("Cool 3");
            return maestro_clases_hora;
        }

        public List<string> GetHours()
        {
            string clase_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Horarios.txt";

            List<string> horas = new List<string>();
            try
            {
                StreamReader sr = File.OpenText(clase_path);

                string s = "";
                int cont = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    horas.Add(s);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("File not found");
            }
            return horas;
        }

        public Dictionary<int, string> GetClasses()
        {
            string clase_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Clases.txt";

            Dictionary<int, string> clases_dic = new Dictionary<int, string>();

            try
            {
                StreamReader sr = File.OpenText(clase_path);

                string s = "";
                int cont = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (!clases_dic.ContainsValue(s.Split(',')[0]))
                    {
                        clases_dic.Add(cont, s.Split(',')[0]);
                        cont++;
                    }
                }
                //foreach (var item in clases_dic)
                //{
                //    Console.WriteLine("Key: " + item.Key + " Value:" + item.Value);
                //}
            }
            catch (Exception)
            {
                Console.WriteLine("File not found");
            }
            return clases_dic;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
