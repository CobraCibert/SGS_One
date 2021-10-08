using System.Windows.Forms;

namespace Sistemas.UI.Utilitarios
{
    public partial class AbstractForm : Form
    {
        public AbstractForm()
        {
            InitializeComponent();
        }
        //Este metodo es clase base
        public virtual void OnSave() { }
        public virtual void OnUpdate() { }
        public virtual void OnDelete() { }
        public virtual void OnClear() { }
        public virtual void OnPrinter() { }
    }
}
