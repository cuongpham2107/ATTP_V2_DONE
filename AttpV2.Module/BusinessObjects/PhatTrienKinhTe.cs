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
    [XafDisplayName("Chỉ tiêu phát triển KT-XH")]
    [DefaultProperty(nameof(ChiTieu))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]
    public class PhatTrienKinhTe : BaseObject
    {
        public PhatTrienKinhTe(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        int nam;
        ChiTieu chiTieu;
        int thucHien;
        int keHoach;
        Vung vung;
        [XafDisplayName("Chỉ Tiêu")]
        [RuleRequiredField("Bắt buộc phải có PhatTrienKinhTe.ChiTieu", DefaultContexts.Save, "Dữ liệu không được để trống")]
        public ChiTieu ChiTieu
        {
            get => chiTieu;
            set => SetPropertyValue(nameof(ChiTieu), ref chiTieu, value);
        }


        [XafDisplayName("Vùng")]
        [RuleRequiredField("Bắt buộc phải có PhatTrienKinhTe.Vung", DefaultContexts.Save, "Dữ liệu không được để trống")]
        [Association("Vung-ChiTieuPhatTriemKinhTes")]
        public Vung Vung
        {
            get => vung;
            set => SetPropertyValue(nameof(Vung), ref vung, value);
        }
        [XafDisplayName("Kế hoạch")]

        public int KeHoach
        {
            get => keHoach;
            set => SetPropertyValue(nameof(KeHoach), ref keHoach, value);
        }

        [XafDisplayName("Thực hiện")]
        public int ThucHien
        {
            get => thucHien;
            set => SetPropertyValue(nameof(ThucHien), ref thucHien, value);
        }

        [XafDisplayName("Năm")]
        public int Nam
        {
            get => nam;
            set => SetPropertyValue(nameof(Nam), ref nam, value);
        }
    }
}