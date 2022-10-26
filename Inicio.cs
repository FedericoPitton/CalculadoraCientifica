using static InterfazCalculadora.Validaciones;
using static InterfazCalculadora.Procedimientos;
namespace InterfazCalculadora
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("Desea salir de la aplicación", "SALIR DEL PROGRAMA", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (opcion == DialogResult.OK)this.Close();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            string ecuacion = textEcuacion.Text;
            ecuacion = ecuacion.Replace(" ", String.Empty);
            try
            {
                ecuacion = separadorTerminos(ecuacion);
                ecuacion = validadorMultDiv(ecuacion);
                ecuacion = calculadorTerminos(ecuacion);
                textBox1.Text = ecuacion;
            }
            catch (Exception error)
            {
                opcion = MessageBox.Show(error.Message,"El resultado tiende a infinito",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(error.Message);
            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            btnCalcular.Enabled = false;
        }

        private void controlButtons(string ecuacion)
        {
            if (ecuacion.Trim() != string.Empty)
            {
                if (!validarCaracteres(ecuacion))
                {
                    btnCalcular.Enabled = false;
                }
                else if (!validarParentesis(ecuacion))
                {
                    btnCalcular.Enabled = false;
                }
                else
                {
                    btnCalcular.Enabled = true;
                    errorProvider1.SetError(textEcuacion, "");
                }
                
            } else
            {
                btnCalcular.Enabled=false;
                errorProvider1.SetError(textEcuacion, "Debe ingresar una ecuacion");
            }
            
        }

        private void textEcuacion_TextChanged(object sender, EventArgs e)
        {
            string ecuacion = textEcuacion.Text;
            ecuacion = ecuacion.Replace(" ", String.Empty);
            controlButtons(ecuacion);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}