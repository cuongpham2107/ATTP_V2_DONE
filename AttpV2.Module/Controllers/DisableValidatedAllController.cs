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
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttpV2.Module.Controllers
{

    
    public partial class DisableValidatedAllController : ViewController
    {
        public DisableValidatedAllController()
        {
            InitializeComponent();
            
            TargetViewNesting = Nesting.Any;
            TargetViewType = ViewType.Any;
            Activated += PhanquyenController_Activated;
        }
        private void PhanquyenController_Activated(object sender, EventArgs e)
        {
            var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);

            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers")) return;

            if (View is ListView view)
            {
                var criteria = view.CollectionSource.Criteria;
               

                if (View.ObjectTypeInfo.Type == typeof(CoSoSanXuatKinhDoanh))
                    criteria.Add("crit1", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

                if (View.ObjectTypeInfo.Type == typeof(GiayChungNhan))
                    criteria.Add("crit2", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

                if (View.ObjectTypeInfo.Type == typeof(ThamDinh))
                    criteria.Add("crit3", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

                if (View.ObjectTypeInfo.Type == typeof(ThuHoiGCN))
                    criteria.Add("crit4", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

                if (View.ObjectTypeInfo.Type == typeof(XuPhatHanhChinh))
                    criteria.Add("crit5", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

                if (View.ObjectTypeInfo.Type == typeof(KetQuaThanhKiemTra))
                    criteria.Add("crit6", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

            }
        }
    }
    public partial class DisableValidatedKetQuaThanhkiemTra : ObjectViewController<DetailView, KetQuaThanhKiemTra>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var os = Application.CreateObjectSpace(typeof(KetQuaThanhKiemTra));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
            {

                if (ViewCurrentObject?.Close == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
            else
            {
                if (ViewCurrentObject?.Lock == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
        }
    }
    public partial class DisableValidatedGiayChungNhan : ObjectViewController<DetailView, GiayChungNhan>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
            {
               
                if (ViewCurrentObject?.Close == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
            else
            {
                if (ViewCurrentObject?.Lock == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }  
        }
    }

    public partial class DisableValidatedThamDinh : ObjectViewController<DetailView, ThamDinh>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
            {
                if (ViewCurrentObject?.Close == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
            else
            {
                if (ViewCurrentObject?.Lock == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }

        }
    }


    public partial class DisableValidatedThuHoiGCN : ObjectViewController<DetailView, ThuHoiGCN>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
            {
                if (ViewCurrentObject?.Close == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
            else
            {
                if (ViewCurrentObject?.Lock == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }

        }
    }

    public partial class DisableValidatedXuPhatHanhChinh : ObjectViewController<DetailView, XuPhatHanhChinh>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
            var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
            {
                if (ViewCurrentObject?.Close == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }
            else
            {
                if (ViewCurrentObject?.Lock == true)
                {
                    var editors = View.GetItems<PropertyEditor>();
                    foreach (var item in editors)
                    {
                        item.AllowEdit["hihi"] = false;
                    }
                }
            }

        }
    }
}
