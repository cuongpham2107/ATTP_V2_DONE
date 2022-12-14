
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
    [NavigationItem(Menu.CategoryMenuItem)]
    [XafDisplayName("Vùng phát triển KT-XH")]
    [DefaultProperty(nameof(TenVung))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]

    public class Vung : BaseObject
    { 
        public Vung(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        
        string diaChi;
        string tenVung;
        [XafDisplayName("Tên vùng")]
        public string TenVung
        {
            get => tenVung;
            set => SetPropertyValue(nameof(TenVung), ref tenVung, value);
        }
        [XafDisplayName("Địa chỉ")]
        public string DiaChi
        {
            get => diaChi;
            set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
        }
        //[XafDisplayName("Cơ quan quản lý")]
        //[VisibleInListView(false)]
        //public CoQuanQuanLy CoQuanQuanLy
        //{
        //    get
        //    {
        //        if(!IsLoading && !IsSaving)
        //        {
        //            var account = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
        //            if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
        //            {
        //                return coQuanQuanLy;
        //            }
        //            else
        //            {
        //                return account.CoquanQuanly;
        //            }  
        //        }
        //        return coQuanQuanLy;
        //    }
        //    set => SetPropertyValue(nameof(CoQuanQuanLy), ref coQuanQuanLy, value);
        //}
        [XafDisplayName("Các chỉ tiêu")]
        [Association("Vung-ChiTieuPhatTriemKinhTes")]
        public XPCollection<PhatTrienKinhTe> ChiTieuPhatTriemKinhTes
        {
            get
            {
                return GetCollection<PhatTrienKinhTe>(nameof(ChiTieuPhatTriemKinhTes));
            }
        }
    }
}