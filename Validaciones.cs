using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InterfazCalculadora.Inicio;
namespace InterfazCalculadora
{
    internal class Validaciones
    {
        public static int operadores = 0;
        public static int operador = 0;
        public static int cont = 0;

        public Validaciones()
        {
        }
        public static bool validarCaracteres(string ecuacion)
        {
            if (ecuacion == null)
            {
                Console.Write("Esta vacio");
                return false;
            }

            for (int i = 0; i < ecuacion.Length; i++)
            {
                //Codigo ascci ',' 44
                bool caracteresValidos = (!((int)ecuacion[i] >= 47 && (int)ecuacion[i] <= 57) && !((int)ecuacion[i] >= 40 && (int)ecuacion[i] <= 45));
                bool operadoresValidos = (((int)ecuacion[i] >= 42 && (int)ecuacion[i] <= 45) || (int)ecuacion[i] == 47);

                if (caracteresValidos)
                {

                    errorProvider1.SetError(textEcuacion, "Solo estan permitidos numeros enteros y decimales con ',' y operadores / * + - ( )");
                    Console.WriteLine("Caracter no valido");
                    Console.WriteLine("Solo estan permitidos numeros enteros y decimales con ',' y operadores / * + - ( )");
                    return false;
                }

                if (operadoresValidos)
                {
                    //Validamos que no se comience ni termine con operadores
                    if (i == (ecuacion.Length - 1) || i == 0)
                    {
                        errorProvider1.SetError(textEcuacion, "No pueden haber un operador al inciar o al final de la ecuacion");
                        Console.WriteLine("No pueden haber un operador al inciar o al final de la ecuacion " + ecuacion[i]);
                        if (ecuacion[i] == 45)
                        {
                            Console.WriteLine("Los numeros negativos deben encerrarse entre ( )");
                        }
                        return false;
                    }

                    //HICE UNA MODIFICAION 41 por 42
                    bool operadoresValidosSiguiente = (((int)ecuacion[i + 1] >= 41 && (int)ecuacion[i + 1] <= 43) || ((int)ecuacion[i + 1] >= 45 && (int)ecuacion[i + 1] <= 47));

                    //Validamos que no hayan dos operadores juntos
                    if (operadoresValidosSiguiente)
                    {
                        errorProvider1.SetError(textEcuacion, "No pueden haber dos operadores juntos");
                        Console.WriteLine("No pueden haber dos operadores juntos" + ecuacion[i] + ecuacion[i + 1]);
                        return false;
                    }
                    if (!((int)ecuacion[i] == 44)) operadores++;


                }
                //Validamos que hayan operadores entre parentesis y numeros
                if (i < ecuacion.Length - 1)
                {
                    if (ecuacion[i] >= 48 && ecuacion[i] <= 57 && ecuacion[i + 1] == 40)
                    {
                        errorProvider1.SetError(textEcuacion, "Debe haber un operador entre un numero y un parentesis ");
                        Console.WriteLine("Debe haber un operador entre un numero y un parentesis " + ecuacion[i] + ecuacion[i + 1]);
                        return false;
                    }
                    if (ecuacion[i + 1] >= 48 && ecuacion[i + 1] <= 57 && ecuacion[i] == 41)
                    {
                        errorProvider1.SetError(textEcuacion, "Debe haber un operador entre un numero y un parentesis ");
                        Console.WriteLine("Debe haber un operador entre un numero y un parentesis " + ecuacion[i] + ecuacion[i + 1]);
                        return false;
                    }
                }
            }
            Console.WriteLine("Caracteres validos");
            return true;
        }

        public static bool validarParentesis(string ecuacion)
        {
            int parentesisAbierto = 0;
            int parentesisCerrado = 0;
            for (int i = 0; i < ecuacion.Length; i++)
            {

                if ((int)ecuacion[i] == 41) parentesisCerrado++;
                if ((int)ecuacion[i] == 40) parentesisAbierto++;
                if (parentesisCerrado > parentesisAbierto)
                {
                    errorProvider1.SetError(textEcuacion, "No pueden cerrarse paretensis que no han sido abiertos");
                    Console.WriteLine("No pueden cerrarse paretensis que no han sido abiertos");
                    return false;
                }
                if (!(i == 0 && ecuacion.Length - 1 == 0))
                {
                    if (((int)ecuacion[i] == 40) && ((int)ecuacion[i + 1] == 41))
                    {
                        errorProvider1.SetError(textEcuacion, "No pueden haber paretensis vacios");
                        Console.WriteLine("No pueden haber paretensis vacios");
                        return false;
                    }
                }
            }
            if (parentesisCerrado != parentesisAbierto)
            {
                errorProvider1.SetError(textEcuacion, "No se cerraron todos los parentesis");
                Console.WriteLine("No se cerraron todos los parentesis");
                return false;
            }
            Console.WriteLine("Parentesis correctos");
            return true;

        }
    }
}
