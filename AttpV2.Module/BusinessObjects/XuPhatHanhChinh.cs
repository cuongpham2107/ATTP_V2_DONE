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
    [XafDisplayName("Xử phạt hành chính")]
    [DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]
    public class XuPhatHanhChinh : BaseObject
    { 
        public XuPhatHanhChinh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }


        DateTime ngayXuPhat;
        string lyDoXuPham;
        string soTienPhat;
        string chiTietViPham;
        string soMauViPham;
        string tongSoMauLay;
        string xuLyViPham;
        string hanhViViPham;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
        [Association("CoSoSanXuatKinhDoanh-XuPhatHanhChinhs")]
        [XafDisplayName("Cơ sở sản xuất, kinh doanh")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set => SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
        }
        [PersistentAlias("[CoSoSanXuatKinhDoanh.CoQuanQuanLy]")]
        [XafDisplayName("Cơ quan quản lý")]
        public CoQuanQuanLy CoQuanQuanLy
        {
            get
            {
                var tmp = EvaluateAlias(nameof(CoQuanQuanLy));
                if (tmp != null)
                {
                    return tmp as CoQuanQuanLy;
                }
                else
                {
                    return null;
                }
            }
        }
     
        [XafDisplayName("Lỗi vi phạm")]
        public string HanhViViPham
        {
            get => hanhViViPham;
            set => SetPropertyValue(nameof(HanhViViPham), ref hanhViViPham, value);
        }


        [XafDisplayName("Chi tiết vi phạm")]
        public string ChiTietViPham
        {
            get => chiTietViPham;
            set => SetPropertyValue(nameof(ChiTietViPham), ref chiTietViPham, value);
        }

        [XafDisplayName("Số tiền phạt")]
        public string SoTienPhat
        {
            get => soTienPhat;
            set => SetPropertyValue(nameof(SoTienPhat), ref soTienPhat, value);
        }

        [XafDisplayName("Ngày xử phạt")]
        public DateTime NgayXuPhat
        {
            get => ngayXuPhat;
            set => SetPropertyValue(nameof(NgayXuPhat), ref ngayXuPhat, value);
        }

        #region Association

        [Association("XuPhatHanhChinh-FileDLs")]
        [XafDisplayName("File Dữ liệu")]
        public XPCollection<FileDL> FileDLs
        {
            get
            {
                return GetCollection<FileDL>(nameof(FileDLs));
            }
        }
        #endregion
    }
}