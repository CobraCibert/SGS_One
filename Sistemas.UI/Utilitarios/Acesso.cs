using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Sistemas.UI.Utilitarios
{
    public partial class Acesso : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public Acesso(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        #region Atributos
        #endregion

        #region Metodos
        #endregion

        #region Eventos

        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //ActiveForm.Visible = false;
            Hide();
            var form = _serviceProvider.GetRequiredService<MDI>();
            form.Show(this);
        }
    }
}
