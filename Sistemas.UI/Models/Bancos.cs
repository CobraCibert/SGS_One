using Sistemas.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistemas.UI.Models
{
    public partial class Bancos : AbstractForm
    {
        public Bancos()
        {
            InitializeComponent();
        }
        #region Metodos

        //Este metodo es el que implementaras en tus formularios
        public override void OnSave()
        {
            //Aquí haces lo que necesites hacer.
            MessageBox.Show("Save....!");
        }
        //Este metodo es el que implementaras en tus formularios
        public override void OnClear()
        {
            //Aquí haces lo que necesites hacer.
            MessageBox.Show("Clear....!");
        }
        public override void OnUpdate()
        {
            //Aquí haces lo que necesites hacer.
            MessageBox.Show("Update....!");
        }
        public override void OnDelete()
        {
            //Aquí haces lo que necesites hacer.
            MessageBox.Show("Delete....!");
        }

        public override void OnPrinter()
        {
            //Aquí haces lo que necesites hacer.
            MessageBox.Show("Printer....!");
        }
        #endregion
    }
}
