namespace VUMTS
{
    public partial class View : Form, IView
    {
        private IModel model;
        private IController controller;

        IModel IView.Model
        {
            get { return model; }
            set { model = value; }
        }
        IController IView.Controller
        {
            get { return controller; }
            set { controller = value; }
        }
        //eine methode die den selben namen wie die Klasse hat
        public View()
        {
            InitializeComponent();
        }

        private void onClickStart(object sender, EventArgs e)
        {
            controller.cycleStart();
        }

        //entfernung ist ein paramete, weil der wert wieder gegeben wird
        public void anzeigen(int entfernung)
        {
            textBox1.Text = entfernung.ToString();
        }

        private void onClickStop(object sender, EventArgs e)
        {
            controller.cycleStop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
