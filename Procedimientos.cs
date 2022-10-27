using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazCalculadora
{
    internal class Procedimientos
    {
        public static int opParentesisA = 40;
        public static int opParentesisC = 41;
        public static int opSuma = 43;
        public static int opResta = 45;
        public static int opDiv = 47;
        public static int opMult = 42;
        public static int opIgual = 61;
        public Procedimientos()
        {
        }

        public static string separadorTerminos(string ecuacion)
        {
            string termino = "";
            string termino2;
            for (int i = 0; i < ecuacion.Length; i++)
            {
                if (ecuacion[i] == opParentesisA)
                {
                    //Se llama a la funcion nuevamente enviando el resto del termino sin el parentesis abierto
                    termino2 = separadorTerminos(ecuacion.Substring(i + 1));
                    //Se modifica la ecuacion con el calculo del termino enviado
                    ecuacion = ecuacion.Substring(0, i) + termino2;
                }
                else if (ecuacion[i] == opParentesisC)
                {
                    //Se retorna el calculo de la expresion una vez q se encuentra el parentesis cerrado
                    ecuacion = ecuacion.Replace(termino + ")", calculadorTerminos(validadorMultDiv(termino)));
                    return ecuacion;
                }
                //Se llena el termino en hasta encontrar parentesis
                termino += "" + ecuacion[i];
            }
            return ecuacion;
        }

        public static string validadorMultDiv(string termino)
        {
            string termino1 = "";
            string termino2 = "";
            string termino3 = "";
            int operador = 0;
            bool inicio = true;
            int i = 0;
            int terminoleng = termino.Length;
            do
            {
                if (termino[i] == opMult || termino[i] == opDiv)
                {

                    termino2 = "";
                    operador = termino[i];
                    int o = i;
                    if (inicio)
                    {
                        do
                        {
                            o--;
                            termino1 = termino[o] + termino1;
                            if (o == 0) break;
                        } while ((termino[o] >= 48 && termino[o] <= 57) || termino[o] == 44);
                        inicio = false;
                    }
                    o = i;
                    o++;
                    while (termino[o] >= 48 && termino[o] <= 57 || (termino[o] == opResta && o == i + 1) || termino[o] == 44)
                    {
                        termino2 += termino[o];
                        o++;

                        if (termino[o - 1].ToString() == termino.Substring(o - 1))
                        {
                            break;
                        }
                    }
                    termino = termino.Replace(termino1 + termino[i] + termino2, calculo(termino1, termino2, operador));
                    i = 0;
                    termino1 = calculo(termino1, termino2, operador);
                }
                else if (termino[i] == opSuma || termino[i] == opResta)
                {
                    termino3 = validadorMultDiv(termino.Substring(i + 1));
                    termino = termino.Substring(0, i + 1) + termino3;
                    return termino;
                }
                i++;
                if (termino[i - 1].ToString() == termino.Substring(i - 1))
                {
                    break;
                }
            } while (i < terminoleng);
            return termino;
        }

        public static string calculadorTerminos(string termino)
        {
            string termino1 = "";
            string termino2 = "";
            int operador = 0;
            bool inicio = true; //Variable que permitira separar primer numero
            bool terminado = true; //Permite ver cuando termina un numero y esta el operador
            bool valorSolo = true;

            //Bucle para verificar si es solo un numero sin calculos dentro del termino
            for (int i = 0; i < termino.Length; i++)
            {
                if (!(termino[i] >= 48 && termino[i] <= 57 || termino[i] == 44 || (termino[i] == opResta && i == 0)))
                {
                    valorSolo = false;
                    break;
                }
            }
            if (valorSolo) return termino;
            //Bucle para calcular las sumas/restas del termino
            for (int i = 0; i < termino.Length; i++)
            {

                if (i < termino.Length - 1)
                {
                    if (termino[i] == opResta && termino[i + 1] == opResta)
                    {
                        termino.Replace(termino[i + 1], (char)opSuma);
                        continue;
                    }
                    if (termino[i] == opSuma && termino[i + 1] == opResta)
                    {
                        continue;
                    }

                }
                //Si son numeros se llenaran los termino 1 si es el inicio
                //y 2 hasta terminar
                if (termino[i] >= 48 && termino[i] <= 57 || termino[i] == 44 || (termino[i] == opResta && i == 0))
                {

                    if (inicio)
                    {
                        termino1 += termino[i];
                    }
                    else
                    {
                        termino2 += termino[i];
                    }
                }
                else
                {
                    //Si se encuentra un operador desactiva la variable inicio
                    //desactiva la variable terminado para seguir con numero 2
                    if (terminado)
                    {
                        operador = termino[i];
                        inicio = false;
                        terminado = false;
                    }
                    else
                    {
                        //Una vez dentro con las 3 variables se prosigue a calcularlo
                        //se habilita terminado para ver el nuevo termino y buscar num 2 nuevamente
                        termino1 = calculo(termino1, termino2, operador);
                        termino2 = "";
                        terminado = true;
                        i--;
                    }
                }
            }
            //Se calculan ultimos 2 elementos fuera del bucle
            termino1 = calculo(termino1, termino2, operador);

            return termino1;
        }
        //Recibe de a dos elementos y realiza el calculo en base al operador recibido
        public static string calculo(string termino1, string termino2, int operador)
        {
            try
            {
                float term1 = float.Parse(termino1);
                float term2 = float.Parse(termino2);
                float result = 0;

            
           
            switch (operador)
            {
                //Suma
                case 43:
                    result = term1 + term2;
                    break;
                //Resta
                case 45:
                    result = term1 - term2;
                    break;
                //Mult
                case 42:
                    result = term1 * term2;
                    break;
                //Div
                case 47:
                    if (term2 == 0) throw new DivideByZeroException("Se intento dividir por 0 ");

                    result = term1 / term2;
                    break;
            }
            return result.ToString();
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException("Se intento dividir por 0 ");
            }
            catch (Exception e)
            {

                throw new Exception("Se intento multiplicar/dividir por un numero muy grande o muy chico");
            }
        }
    }
}
