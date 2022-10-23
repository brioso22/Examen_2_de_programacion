using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Empleados
    {
        private int id;
        private string nombre;
        private string codigo;
        private string dui;
        private double salario;
        private double afp;
        private double iss;
        private double descuento;
        private double sueldoliquido;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Codigo {
            get { return codigo; }
            set { codigo = value; }
        }


        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Dui
        {
            get { return dui; }
            set { dui = value; }
        }
        public double Salario
        {
            get { return salario; }
            set { salario = value; }
        }

        public double Afp
        {
            get { return afp; }
            set { afp = value; }
        }

        public double Iss
        {
            get { return iss; }
            set { iss = value; }
        }
        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }

        public double Sueldoliquido
        {
            get { return sueldoliquido; }
            set { sueldoliquido= value; }
        }

        public double AFP(double Salario)

        {
            double CalcAFP = Salario * 0.075;
            return CalcAFP;
        }
        public double ISSS(double Salario)
        {
            double CalcIss = Salario * 0.03;
            return CalcIss;
        }
        public double DESCUENTO(double AFP,double ISS)
        {
            double CalcDes= AFP + ISS;
            return CalcDes;
        }
        public double SUELDOLOQUIDO(double Salario, double DESCUENTO)
        {
            double CalcDes = Salario - DESCUENTO;
            return CalcDes;
        }
    }
}
  
