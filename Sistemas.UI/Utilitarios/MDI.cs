using System;
using System.Windows.Forms;

namespace Sistemas.UI.Utilitarios
{
    public partial class MDI : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public MDI(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        #region Eventos
        private void btnNew_Click(object sender, EventArgs e)
        {
            // Determinar la forma secundaria activa
            Form activeChild = ActiveMdiChild;

            //Verifica si hay un formulario secundario activo
            if (activeChild == null)
            {
                MessageBox.Show("Opción no disponible Nuevo...!");
                return;
            }

        //Si hay un formulario secundario activo, busca el metodo activo
        ((AbstractForm)ActiveMdiChild).OnClear();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Determinar la forma secundaria activa
            Form activeChild = ActiveMdiChild;

            //Verifica si hay un formulario secundario activo
            if (activeChild == null)
            {
                MessageBox.Show("Opción no disponible Imprimir...!");
                return;
            }

         //Si hay un formulario secundario activo, busque el metodo activo
         ((AbstractForm)ActiveMdiChild).OnPrinter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Determinar la forma secundaria activa
            Form activeChild = ActiveMdiChild;

            //Verifica si hay un formulario secundario activo
            if (activeChild == null)
            {
                MessageBox.Show("Opción no disponible Actualizar...!");
                return;
            }

          //Si hay un formulario secundario activo, busque el metodo activo
          ((AbstractForm)ActiveMdiChild).OnUpdate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Determinar la forma secundaria activa
            Form activeChild = ActiveMdiChild;

            //Verifica si hay un formulario secundario activo
            if (activeChild == null)
            {
                MessageBox.Show("Opción no disponible Eliminar...!");
                return;
            }

          //Si hay un formulario secundario activo, busque el metodo activo
          ((AbstractForm)ActiveMdiChild).OnDelete();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Determinar la forma secundaria activa
            Form activeChild = ActiveMdiChild;

            //Verifica si hay un formulario secundario activo
            if (activeChild == null)
            {
                MessageBox.Show("Opción no disponible Grabar...!");
                return;
            }

          //Si hay un formulario secundario activo, busque el metodo activo
          ((AbstractForm)ActiveMdiChild).OnSave();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // The user wants to exit the application. Close everything down.
            //El usuario quiere salir de la aplicación. Cierra todo.
            Application.Exit();
        }
        #endregion
    }
}
