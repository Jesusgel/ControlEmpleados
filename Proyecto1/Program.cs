using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1 {

    class Program {

        public static void MostrarEncabezado() {
            Console.WriteLine("-------------BIENVENIDO-------------");
            Console.WriteLine("---------DATOS DE EMPLEADOS---------");
            Console.WriteLine("---------------MENU-----------------");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("- -1 Ingresar Empleado          ----");
            Console.WriteLine("-- 2 Buscar Empleado por codigo ----");
            Console.WriteLine("-- 3 Listar todos los empleados ----");
            Console.WriteLine("-- 0 Salir                      ----");
            Console.WriteLine("------------------------------------");
        }

        public static void GuardarTxt(List<Empleado> lista) {
            String file = "C:\\ejemplo\\Empleados.txt";
            try {
                if (!File.Exists(file)) {
                    //File.Delete(file);
                    using (StreamWriter sw = File.CreateText(file)) {
                        foreach (var e in lista)
                            sw.WriteLine(e.Id + ";" + e.Nombre + ";" + e.Apellido + ";" + e.Horas + ";" + e.Costohora + ";" + e.Sueldo());
                    }
                }
                else {
                    using (StreamWriter sw = File.AppendText(file)) {
                        foreach (var e in lista)
                            sw.WriteLine(e.Id + ";" + e.Nombre + ";" + e.Apellido + ";" + e.Horas + ";" + e.Costohora + ";" + e.Sueldo());
                    }
                }
            }
            catch (Exception Ex) {
                Console.WriteLine(Ex.ToString());
            }
        }
    }


    public class Empleado {
        private int id;
        private String nombre;
        private String apellido;
        private float sueldo;
        private float costohora;
        private int horas;

        public string Nombre {
            get {
                return nombre;
            }

            set {
                nombre = value;
            }
        }

        public string Apellido {
            get {
                return apellido;
            }

            set {
                apellido = value;
            }
        }

        public float Costohora {
            get {
                return costohora;
            }

            set {
                costohora = value;
            }
        }

        public int Horas {
            get {
                return horas;
            }

            set {
                horas = value;
            }
        }

        public int Id {
            get {
                return id;
            }

            set {
                id = value;
            }
        }

        public float Sueldo() {

            return horas * costohora;

        }

        public String NombreCompleto() {
            return this.nombre + " " + this.apellido;
        }

        public Empleado() { }

        public Empleado(int id,string nombre, string apellido, float costohora, int horas) {
            this.id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Costohora = costohora;
            this.Horas = horas;
        }


    }

    public class Principal {
        static void Main(string[] args) {
            int opc=0,id=0;
            bool e = true;
            Empleado emp;
            while (e) {
                Program.MostrarEncabezado();
                Console.Write("Seleccione una opcion: ");
                opc = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (opc) {
                    case 1:
                        // Ingresar empleados
                        IngresarEmpleado();
                        break;
                    case 2:
                        //Buscar empleado por ID
                        Console.Write("Ingrese el ID del empleado que desea buscar: ");
                        id = int.Parse(Console.ReadLine()); 
                        emp= BuscarEmpleado(id);
                        Console.WriteLine("    ");
                        if (emp != null) {
                            Console.WriteLine("Empleado encontrado: " +emp.Id+" "+emp.NombreCompleto());
                        }
                        else {
                            Console.WriteLine("Empleado NO ENCONTRADO");
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        //Mostrar todos los empleados
                        ImprimirTodoEmpleado(GetAllEmpleados());
                        Console.ReadKey();
                        break;
                    default:
                        e = false;
                        break;
                }
                Console.Clear();
            }
        }

        public static void IngresarEmpleado() {
            List<Empleado> listaEmp = new List<Empleado>();
            String nomb = "", ape = "";
            float cost;
            int hors = 0, resp = 0,id=0;

            id = BuscarUltimoIdEmpleado();
            do {
                Console.Write("Ingrese el Nombre: ");
                nomb = Console.ReadLine();
                Console.Write("Ingrese el Apellido: ");
                ape = Console.ReadLine();
                Console.Write("Ingrese las Horas: ");
                hors = int.Parse(Console.ReadLine());
                Console.Write("Ingrese el Costo: ");
                cost = float.Parse(Console.ReadLine());
                id++;
                Empleado emp = new Empleado(id, nomb, ape, cost, hors);
                listaEmp.Add(emp);

                Console.Write("Desea agregar otro Empleado(1- Si 0- No): ");
                resp = int.Parse(Console.ReadLine());
                Console.Clear();
            } while (resp == 1);

            Program.GuardarTxt(listaEmp); 
        }


        public static int BuscarUltimoIdEmpleado() {
            String file = "C:\\ejemplo\\Empleados.txt";
            int id,horas;
            String nombre, apellido;
            float costohora;
            List<Empleado> emp = new List<Empleado>();
            try {
                using (StreamReader sr = new StreamReader(file)) {
                    string line; 
                    while ((line = sr.ReadLine()) != null) {
                        String[] a= line.Split(';');
                        id = int.Parse(a[0]);
                        nombre = a[1];
                        apellido = a[2];
                        costohora = float.Parse(a[3]);
                        horas = int.Parse(a[4]);
                        emp.Add(new Empleado(id,nombre,apellido,costohora,horas));
                    }
                }
            }
            catch (Exception e) {
                
            }
            return emp.Count>0?emp.Count:0;
        }

        public static Empleado BuscarEmpleado(int index) {
            Empleado em = new Empleado();
            String file = "C:\\ejemplo\\Empleados.txt";
            int id, horas;
            String nombre, apellido;
            float costohora;
            List<Empleado> emp = new List<Empleado>();
            try {
                using (StreamReader sr = new StreamReader(file)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        String[] a = line.Split(';');
                        id = int.Parse(a[0]);
                        nombre = a[1];
                        apellido = a[2];
                        costohora = float.Parse(a[3]);
                        horas = int.Parse(a[4]);
                        emp.Add(new Empleado(id, nombre, apellido, costohora, horas));
                    }
                }
            }
            catch (Exception e) {

            }

            foreach (var e in emp) {
                if (e.Id==index) {
                    return e;
                }
            }

            return null;
        }

        public static List<Empleado> GetAllEmpleados() {
            List<Empleado> list = new List<Empleado>();
            String file = "C:\\ejemplo\\Empleados.txt";
            int id, horas;
            String nombre, apellido;
            float costohora;
            try {
                using (StreamReader sr = new StreamReader(file)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        String[] a = line.Split(';');
                        id = int.Parse(a[0]);
                        nombre = a[1];
                        apellido = a[2];
                        costohora = float.Parse(a[3]);
                        horas = int.Parse(a[4]);
                        list.Add(new Empleado(id, nombre, apellido, costohora, horas));
                    }
                }
            }
            catch (Exception e) {

            }
            return list;
        }

        public static void ImprimirTodoEmpleado(List<Empleado> lista) {

            foreach (var i in lista) {
                Console.WriteLine("ID: " + i.Id);
                Console.WriteLine("Nombre Completo: "+i.NombreCompleto());
                Console.WriteLine("Sueldo: " + i.Sueldo());
                Console.WriteLine("-------------------");
            }


            if (lista.Count < 1) {
                Console.WriteLine("NO HAY EMPLEADOS REGISTRADOS");
            }
        }

        }
}

