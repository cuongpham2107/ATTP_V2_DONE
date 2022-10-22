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

namespace AttpV2.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SetCoQuanQuanLyController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public SetCoQuanQuanLyController()
        {
            InitializeComponent();
            TargetObjectType = typeof(CoSoSanXuatKinhDoanh);
            SetCoQuanQuanLy();
        }
        private void SetCoQuanQuanLy()
        {
            PopupWindowShowAction action = new(this, "Đặt cơ quan quản lý", "Edit")
            {
                Caption = "Đặt cơ quan quản lý",
                ImageName = "GroupByResource",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            };
            action.CustomizePopupWindowParams += (sender, e) =>
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                string listViewID = Application.FindLookupListViewId(typeof(CoQuanQuanLy));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(CoQuanQuanLy), listViewID);
                e.View = Application.CreateListView(listViewID, collectionSource, true);
            };
            action.Execute += (s, e) => {
                var popupLoaihinh = e.PopupWindowViewCurrentObject as CoQuanQuanLy;
                var loaihinh = ObjectSpace.GetObject(popupLoaihinh);
                foreach (object item in View.SelectedObjects)
                {
                    (item as CoSoSanXuatKinhDoanh).CoQuanQuanLy = loaihinh;
                }
                ObjectSpace.CommitChanges();
            };
        }
    }
}
