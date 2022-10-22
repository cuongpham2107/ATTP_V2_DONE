using AttpV2.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttpV2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ThamDinhController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public ThamDinhController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnAfterConstruction()
        {
            base.OnAfterConstruction();
            Btn_CapGiayChungNhan();

        }
       
        private void Btn_CapGiayChungNhan()
        {
            var action = new SimpleAction(this, $"{nameof(ThamDinh)}-{nameof(Btn_CapGiayChungNhan)}", "Edit")
            {
                Caption = "Cấp Giấy chứng nhận",
                ImageName = "SnapInsertHeader",
                TargetViewNesting = Nesting.Nested,
                TargetObjectType = typeof(GiayChungNhan), 
                TargetViewType = ViewType.ListView,
                SelectionDependencyType = SelectionDependencyType.Independent,
            };
            action.Execute += (sender, args) =>
            {
                if(((DetailView)ObjectSpace.Owner).CurrentObject is ThamDinh td)
                {
                  
                    NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
                    if (controller != null)
                    {
                        void Created(object sender, ObjectCreatedEventArgs e)
                        {
                            var gcn = e.CreatedObject as GiayChungNhan;
                            var thamdinh = e.ObjectSpace.GetObject(td);
                            gcn.ThamDinh = thamdinh;
                            gcn.CoSoSanXuatKinhDoanh = thamdinh.CoSoSanXuatKinhDoanh;
                            gcn.NgayCapGiayChungNhan = DateTime.Now;
                        }
                        controller.ObjectCreated += Created;
                        controller.NewObjectAction.DoExecute(controller.NewObjectAction.Items[0]);

                    }
                   
                }
            };
        }
    }
}
