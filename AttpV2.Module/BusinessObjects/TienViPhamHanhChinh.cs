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
  
    public class TienViPhamHanhChinh : BaseObject
    { 
        public TienViPhamHanhChinh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        int tienPhat;
        string hanhViViPham;
        [XafDisplayName("Hành vi vi phạm") ,ToolTip("")]
        public string HanhViViPham
        {
            get => hanhViViPham;
            set => SetPropertyValue(nameof(HanhViViPham), ref hanhViViPham, value);
        }

        [XafDisplayName("Tiền phạt"),ToolTip("")]
        public int TienPhat
        {
            get => tienPhat;
            set => SetPropertyValue(nameof(TienPhat), ref tienPhat, value);
        }

    }
}