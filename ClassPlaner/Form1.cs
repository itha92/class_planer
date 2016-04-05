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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewTeachers vt = new ViewTeachers();
            vt.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class Maestro
        {
            string nombre;
            string[] clases;
            string[] horas;

            public Maestro() { }

            public Maestro(string nombre, string[] clases, string[] horas)
            {
                this.nombre = nombre;
                this.clases = clases;
                this.horas = horas;
            }

            public void GenerateFile(List<Maestro> m)
            {

                string s = "";
                string clases = "";
                string path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Maestro_Clases.txt";

                StreamWriter sw = new StreamWriter(path);

                foreach (var item in m)
                {
                    for (int i = 0; i < item.clases.Length; i++)
                    {
                        if (i == item.clases.Length-1)
                        {
                            clases += item.clases[i] + " # " + item.horas[i] + "";
                        }
                        else
                        {
                            clases += item.clases[i] + " # " + item.horas[i] + " $ ";
                        }
                    }
                    s += item.nombre + " ,\t" + clases;
                    sw.WriteLine(s);
                    s = "";
                    clases = "";
                }
                sw.Flush();
                sw.Close();
            }
        }

        private class Estudiante
        {
            string nombre;
            string[] clases;

            public Estudiante()
            {

            }

            public Estudiante(string nombre, string[] clases)
            {
                this.nombre = nombre;
                this.clases = clases;
            }

            public void GenerateFile(List<Estudiante> e)
            {
                string s = "";
                string clases = "";
                string path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Estudiantes_Clases.txt";

                StreamWriter sw = new StreamWriter(path);

                foreach (var item in e)
                {
                    for (int i = 0; i < item.clases.Length; i++)
                    {
                        if (i == item.clases.Length - 1)
                        {
                            clases += item.clases[i] + "";
                        }
                        else
                        {
                            clases += item.clases[i] + " # ";
                        }
                    }
                    s += item.nombre + " ,\t" + clases;
                    sw.WriteLine(s);
                    s = "";
                    clases = "";
                }
                sw.Flush();
                sw.Close();
            
        }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            /* generar archivos nuevos de todo */
            //generar maestros y clases y horarios
            Generar_Maestros();
            Generar_Estudiantes();
            //generar estudiantes, clases

        }

        public static void Generar_Estudiantes()
        {
            List<string> estudiantes_list = new List<string>();
            List<string> clases_list = new List<string>();
            
            List<Estudiante> lista_estudiantes = new List<Estudiante>();

            string estudiantes_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Nombres.txt";
            string clases_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Clases.txt";

            try
            {
                StreamReader estudiante_reader = File.OpenText(estudiantes_path);
                StreamReader clases_reader = File.OpenText(clases_path);

                string s = "";
                
                //leer clases
                while ((s = clases_reader.ReadLine()) != null)
                {
                    clases_list.Add(s);
                }
                //leer maestros
                while ((s = estudiante_reader.ReadLine()) != null)
                {
                    estudiantes_list.Add(s);
                }

                // nombre, clase, clase , clase
                Random r = new Random();
                Estudiante m = new Estudiante();

                string[] clases;
                int cant_clases = 0;

                foreach (var name in estudiantes_list)
                {
                    cant_clases = r.Next(1, 6);
                    clases = new string[cant_clases];
                    string h = "";
                    bool b = true;
                    for (int i = 0; i < clases.Length; i++)
                    {
                        h = clases_list[r.Next(1, clases_list.Count)].Split(',')[0];
                        while (b)
                        {
                            if (clases.Contains(h))
                            {
                                h = clases_list[r.Next(1, clases_list.Count)].Split(',')[0];
                            }
                            else
                            {
                                clases[i] = h;
                                b = false;
                            }
                        }
                        b = true;

                    }
                    
                    lista_estudiantes.Add(new Estudiante(name, clases));

                    cant_clases = 0;
                }

                m.GenerateFile(lista_estudiantes);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

            }
        }

        public static void Generar_Maestros()
        {
            List<string> maestros_list = new List<string>();
            List<string> clases_list = new List<string>();
            List<string> horas_list = new List<string>();

            List<Maestro> lista_maestros = new List<Maestro>();

            string maestros_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Maestros.txt";
            string clases_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Clases.txt";
            string horas_path = "C:\\Users\\wmejia\\Desktop\\class_planer\\ClassPlaner\\Horarios.txt";

            try
            {
                StreamReader maestro_reader = File.OpenText(maestros_path);
                StreamReader clases_reader = File.OpenText(clases_path);
                StreamReader horas_reader = File.OpenText(horas_path);

                string s = "";
                //leer horas
                while ((s = horas_reader.ReadLine()) != null)
                {
                    horas_list.Add(s);
                }
                //leer clases
                while ((s = clases_reader.ReadLine()) != null)
                {
                    clases_list.Add(s);
                }
                //leer maestros
                while ((s = maestro_reader.ReadLine()) != null)
                {
                    maestros_list.Add(s);
                }

                //estructur de maestro - clases
                // nombre, clase, clase , clase, hora, hora, hora
                //numero de clases randoms entre 1 y 5 y las horas segun cantidad de clases;
                //si la hora ya esta en la lista, usar otra
                Random r = new Random();
                Maestro m = new Maestro();
                string[] clases;
                string[] horas;
                int cant_clases = 0;
                foreach (var name in maestros_list)
                {
                    cant_clases = r.Next(1, 9);
                    clases = new string[cant_clases];
                    for (int i = 0; i < clases.Length; i++)
                    {
                        clases[i] = clases_list[r.Next(1, clases_list.Count)].Split(',')[0];
                    }

                    horas = new string[cant_clases];
                    string h = "";
                    bool b = true;
                    for (int i = 0; i < horas.Length; i++)
                    {
                        h = horas_list[r.Next(0, horas_list.Count)];
                        while (b)
                        {
                            if (horas.Contains(h))
                            {
                                h = horas_list[r.Next(0, horas_list.Count)];
                            }
                            else
                            {
                                horas[i] = h;
                                b = false;
                            }
                        }
                        b = true;

                    }
                    lista_maestros.Add(new Maestro(name, clases, horas));

                    cant_clases = 0;


                }

                m.GenerateFile(lista_maestros);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewClasses vc = new ViewClasses();
            vc.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewTheMagic vtm = new ViewTheMagic();
            vtm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewStudents vs = new ViewStudents();
            vs.ShowDialog();
        }
    }
}
