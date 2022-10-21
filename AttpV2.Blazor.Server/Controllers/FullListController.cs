
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;


namespace AttpV2.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FullListController : ViewController<ListView>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public FullListController()
        {
            InitializeComponent();
        }
       
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View.Editor is DxGridListEditor gridListEditor)
            {
                IDxGridAdapter dataGridAdapter = gridListEditor.GetGridAdapter();
                //dataGridAdapter.GridModel.ColumnResizeMode = DevExpress.Blazor.GridColumnResizeMode.Disabled;
                dataGridAdapter.GridModel.ShowGroupPanel = true;
                dataGridAdapter.GridModel.FooterDisplayMode = DevExpress.Blazor.GridFooterDisplayMode.Auto;
                dataGridAdapter.GridModel.AutoExpandAllGroupRows = true;
                dataGridAdapter.GridModel.ShowFilterRow = true;
            }
        }
       
    }
}
