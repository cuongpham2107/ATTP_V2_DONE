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
    [XafDisplayName("Loại chỉ tiêu")]
    [DefaultProperty(nameof(TenChiTieu))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    public class ChiTieu : BaseObject
    { 
        public ChiTieu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string moTa;
        string tenChiTieu;
        [XafDisplayName("Tên chỉ tiêu")]
        [RuleRequiredField("Bắt buộc phải có ChiTieu.TenChiTieu", DefaultContexts.Save, "Dữ liệu không được để trống")]
        public string TenChiTieu
        {
            get => tenChiTieu;
            set => SetPropertyValue(nameof(TenChiTieu), ref tenChiTieu, value);
        }
        [XafDisplayName("Mô tả")]
        public string MoTa
        {
            get => moTa;
            set => SetPropertyValue(nameof(MoTa), ref moTa, value);
        }


    }
}