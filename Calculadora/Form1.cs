using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using calc;

namespace InterfazCalculadora
{
    public partial class InterfazCalculadora : Form
    {
        public const string DIGITO_CERO = "0";
        public const string DIGITO_PUNTO = ".";
        public const string DIGITO_COMA = ",";
        public const string VACIO = "";
        public const int NUMERO_DIGITOS_PERMITIDOS = 15;

        public static int ESTADO;
        enum ESTADOS
        {
            INICIAL = 1,
            TRANSICION = 2,
            OPERANDO = 3,
            FIN = 4
        }

        private bool esDisplayDecimal = false;
        public Calculadora calculadora;

        public InterfazCalculadora()
        {
            InitializeComponent();
            calculadora = new Calculadora();
            ESTADO = (int)ESTADOS.INICIAL;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void displayTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Calculadora_Load(object sender, EventArgs e)
        {

        }

        private void rellenaDisplay(string digito)
        {
            string contenidoDisplay = displayLabel.Text;
            if (contenidoDisplay.Length < NUMERO_DIGITOS_PERMITIDOS)
            {
                if (ESTADO == (int)ESTADOS.INICIAL)
                {
                    if (contenidoDisplay.Length == 1 && contenidoDisplay.Equals(DIGITO_CERO))
                    {
                        contenidoDisplay = "";
                    }

                    contenidoDisplay += digito;
                    if (!this.esDisplayDecimal)
                    {
                        contenidoDisplay = estableceFormato(contenidoDisplay);
                    }

                }
                else if (ESTADO == (int)ESTADOS.OPERANDO)
                {
                    this.esDisplayDecimal = false;
                    contenidoDisplay = digito;
                    ESTADO = (int)ESTADOS.INICIAL;
                }

                displayLabel.Text = contenidoDisplay;
            }
        }

        private void reseteaDisplay(object sender, EventArgs e)
        {
            inicializaDisplay();
        }

        private void inicializaDisplay()
        {
            ESTADO = (int)ESTADOS.INICIAL;
            displayLabel.Text = DIGITO_CERO;
            displayOpPreview.Text = VACIO;
            this.esDisplayDecimal = false;
            calculadora = new Calculadora();
        }

        private void borrarUltimoDigito()
        {
            string contenidoDisplay = displayLabel.Text;
            contenidoDisplay = contenidoDisplay.Remove(contenidoDisplay.Length - 1);
            if (!this.esDisplayDecimal)
            {
                contenidoDisplay = estableceFormato(contenidoDisplay);
            }

            displayLabel.Text = contenidoDisplay;

            if (!displayLabel.Text.Contains(DIGITO_COMA))
            {
                this.esDisplayDecimal = false;
            }

            if (displayLabel.Text.Length == 0)
            {
                displayLabel.Text = DIGITO_CERO;
            }
        }

        private void convierteEnDecimal(object sender, EventArgs e)
        {
            addCommaToDisplay();
        }

        private void addCommaToDisplay()
        {
            if (!this.esDisplayDecimal)
            {
                displayLabel.Text += DIGITO_COMA;
                this.esDisplayDecimal = true;
            }
        }

        private string estableceFormato(string textoDisplay)
        {
            string numeroFormateado = "";
            char[] auxText = textoDisplay.Replace(".", "").ToCharArray();
            Array.Reverse(auxText);

            int count = 0;
            for (int i = 0; i < auxText.Length; i++)
            {
                if (count == 3)
                {
                    numeroFormateado += DIGITO_PUNTO;
                    count = 0;
                }
                numeroFormateado += auxText[i];
                count++;
            }
            char[] toretAuxString = numeroFormateado.ToCharArray();
            Array.Reverse(toretAuxString);
            return new string(toretAuxString);
        }

        private void preparaOperacion(string op)
        {
            almacenaOperando();

            char operador = ' ';
            switch (op)
            {
                case "+":
                    operador = '+';
                    break;
                case "-":
                    operador = '-';
                    break;
                case "×":
                    operador = '*';
                    break;
                case "÷":
                    operador = '/';
                    break;
            }

            
            calculadora.insert_op(operador);
            displayOpPreview.Text = calculadora.oper_actual();
            ESTADO = (int)ESTADOS.OPERANDO;
        }

        private void mostrarResultado(object sender, EventArgs e)
        {
            ejecutarOperacion();
        }

        private void ejecutarOperacion()
        {
            almacenaOperando();
            displayLabel.Text = calculadora.operar().ToString();
        }

        private void almacenaOperando()
        {
            string display = displayLabel.Text.Replace(".", "");

            if (!float.TryParse(display, out float n))
            {
                displayLabel.Text = "Error!";
            }

            calculadora.insert_num(n);
        }

        private void introduceDigitoPorTecla(object sender, KeyEventArgs e)
        {
            string digito = "";
            if (e.KeyCode.Equals(Keys.NumPad0))
            {
                digito = "0";
            }
            else if (e.KeyCode.Equals(Keys.NumPad1))
            {
                digito = "1";
            }
            else if (e.KeyCode.Equals(Keys.NumPad2))
            {
                digito = "2";
            }
            else if (e.KeyCode.Equals(Keys.NumPad3))
            {
                digito = "3";
            }
            else if (e.KeyCode.Equals(Keys.NumPad4))
            {
                digito = "4";
            }
            else if (e.KeyCode.Equals(Keys.NumPad5))
            {
                digito = "5";
            }
            else if (e.KeyCode.Equals(Keys.NumPad6))
            {
                digito = "6";
            }
            else if (e.KeyCode.Equals(Keys.NumPad7))
            {
                digito = "7";
            }
            else if (e.KeyCode.Equals(Keys.NumPad8))
            {
                digito = "8";
            }
            else if (e.KeyCode.Equals(Keys.NumPad9))
            {
                digito = "9";
            }

            if (int.TryParse(digito, out int number))
            {
                rellenaDisplay(digito);
            }
            else
            {
                if (e.KeyCode.Equals(Keys.Back))
                {
                    borrarUltimoDigito();
                }
                else if (e.KeyCode.Equals(Keys.Divide))
                {
                    preparaOperacion("÷");
                }
                else if (e.KeyCode.Equals(Keys.Multiply))
                {
                    preparaOperacion("×");
                }
                else if (e.KeyCode.Equals(Keys.Subtract))
                {
                    preparaOperacion("-");
                }
                else if (e.KeyCode.Equals(Keys.Add))
                {
                    preparaOperacion("+");
                }
                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    ejecutarOperacion();
                }
                else if (e.KeyCode.Equals(Keys.Escape))
                {
                    inicializaDisplay();
                } else if (e.KeyCode.Equals(Keys.Decimal))
                {
                    addCommaToDisplay();
                }
            }
        }

        private void rellenaDisplayPorBoton(object sender, EventArgs e)
        {
            string digito = (sender as Button).Text;
            rellenaDisplay(digito);
        }

        private void prepararOperacionPorBoton(object sender, EventArgs e)
        {
            string operador = (sender as Button).Text;
            preparaOperacion(operador);
        }

        private void borrarUltimoDigitoPorBtn(object sender, EventArgs e)
        {
            borrarUltimoDigito();
        }

        private void cambiarSignoDisplay(object sender, EventArgs e)
        {
            toggleNegativo();
        }

        private void toggleNegativo()
        {
            Console.WriteLine("ToggleNegativo-->" + displayLabel.Text);
            if (!esDigitoNegativo())
            {
                displayLabel.Text = "-" + displayLabel.Text;
                Console.WriteLine("cambio a negativo-->" + displayLabel.Text);
            } else
            {
                displayLabel.Text = displayLabel.Text.Substring(1);
                Console.WriteLine("cambio a positivo-->" + displayLabel.Text);
            }
        }

        private bool esDigitoNegativo()
        {
            bool negativo = false;
            if (int.TryParse(displayLabel.Text, out int numero))
            {
                negativo = numero < 0;
            }
            return negativo;
        }

        
    }
}
