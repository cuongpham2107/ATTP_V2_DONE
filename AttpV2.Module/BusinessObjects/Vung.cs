
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