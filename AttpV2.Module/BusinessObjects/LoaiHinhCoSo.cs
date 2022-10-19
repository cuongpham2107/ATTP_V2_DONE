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
    [XafDisplayName("Loại hình cơ sở")]
    [DefaultProperty(nameof(TenLoaiHinh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.CategoryMenuItem)]
    public class LoaiHinhCoSo : BaseObject
    { 
        public LoaiHinhCoSo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }


        string kyHieuMaHoa;
        string tenLoaiHinh;

        [XafDisplayName("Tên loại hình cơ sở")]
        public string TenLoaiHinh
        {
            get => tenLoaiHinh;
            set => SetPropertyValue(nameof(TenLoaiHinh), ref tenLoaiHinh, value);
        }

        [XafDisplayName("Ký hiệu mã hoá")]
        public string KyHieuMaHoa
        {
            get => kyHieuMaHoa;
            set => SetPropertyValue(nameof(KyHieuMaHoa), ref kyHieuMaHoa, value);
        }

    }
}