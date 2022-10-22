using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AttpV2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Cơ quan quản lý")]
    [DefaultProperty(nameof(TenCoQuan))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.CategoryMenuItem)]
    public class CoQuanQuanLy : BaseObject
    { 
        public CoQuanQuanLy(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
        }


        CapQuanLy capQuanLy;
        string ghiChu;
        string tenCoQuan;

        [XafDisplayName("Tên cơ quan quản lý")]
        [RuleRequiredField("Bắt buộc phải có CoQuanQuanLy.TenCoQuan", DefaultContexts.Save, "Dữ liệu không được để trống")]
        public string TenCoQuan
        {
            get => tenCoQuan;
            set => SetPropertyValue(nameof(TenCoQuan), ref tenCoQuan, value);
        }

        [XafDisplayName("Ghi Chú")]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }

        [XafDisplayName("Cấp quản lý")]
        public CapQuanLy CapQuanLy
        {
            get => capQuanLy;
            set => SetPropertyValue(nameof(CapQuanLy), ref capQuanLy, value);
        }

        [XafDisplayName("Danh sách tài khoản"), ToolTip("")]
        [Association("CoQuanQuanLy-ApplicationUsers")]
        [VisibleInDetailView(false)]
        public XPCollection<ApplicationUser> Users => GetCollection<ApplicationUser>(nameof(Users));

    }
}