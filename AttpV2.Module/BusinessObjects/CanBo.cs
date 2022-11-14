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
    
    public class CanBo : BaseObject
    { 
        public CanBo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }


        string chucVu;
        string email;
        string soDienThoai;
        string diaChi;
        string tenCanBo;

        public string TenCanBo
        {
            get => tenCanBo;
            set => SetPropertyValue(nameof(TenCanBo), ref tenCanBo, value);
        }

        public string DiaChi
        {
            get => diaChi;
            set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
        }

        public string SoDienThoai
        {
            get => soDienThoai;
            set => SetPropertyValue(nameof(SoDienThoai), ref soDienThoai, value);
        }

        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }
        
        public string ChucVu
        {
            get => chucVu;
            set => SetPropertyValue(nameof(ChucVu), ref chucVu, value);
        }

    }
}