using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
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

    [ListViewFilter("Tất cả ", "", Index = 0)]
    [ListViewFilter("Xử phạt cơ sở trong tháng này", "GetMonth([NgayXuPhat]) = GetMonth(Now())", Index = 1)]
    [ListViewFilter("Xử phạt cơ sở trong 6 tháng đầu năm", "GetMonth([NgayXuPhat]) <= 6 And GetYear([NgayXuPhat]) = GetYear(Now())", Index = 2)]
    [ListViewFilter("Xử phạt cơ sở trong 6 tháng cuối năm", "GetMonth([NgayXuPhat]) >= 6 And GetYear([NgayXuPhat]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Xử phạt cơ sở trong năm nay", "GetMonth([NgayXuPhat]) >= 6 And GetYear([NgayXuPhat]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Xử phạt cơ sở trong quý 1", "GetMonth([NgayXuPhat]) >= 1 And GetMonth([NgayXuPhat]) <= 3", Index = 4)]
    [ListViewFilter("Xử phạt cơ sở trong quý 2", "GetMonth([NgayXuPhat]) >= 3 And GetMonth([NgayXuPhat]) <= 6", Index = 5)]
    [ListViewFilter("Xử phạt cơ sở trong quý 3", "GetMonth([NgayXuPhat]) >= 6 And GetMonth([NgayXuPhat]) <= 9", Index = 6)]
    [ListViewFilter("Xử phạt cơ sở trong quý 4", "GetMonth([NgayXuPhat]) >= 9 And GetMonth([NgayXuPhat]) <= 12", Index = 7)]
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
        string soTienPhat;
        string chiTietViPham;
        string hanhViViPham;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
        [Association("CoSoSanXuatKinhDoanh-XuPhatHanhChinhs")]
        [XafDisplayName("Cơ sở sản xuất, kinh doanh")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set => SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
        }
        [XafDisplayName("Mã số doanh nghiệp")]
        [ModelDefault("AlowEdit", "False")]
        public string MaSoDoanhNghiep
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.MaSoDoanhNghiep;
                    }
                }
                return null;
            }
        }

        [PersistentAlias("[CoSoSanXuatKinhDoanh.LoaiHinhCoSo]")]
        [XafDisplayName("Loại hình cơ sở, sản xuất")]
        public LoaiHinhCoSo LoaiHinhCoSo
        {
            get
            {
                var tmp = EvaluateAlias(nameof(LoaiHinhCoSo));
                if (tmp != null)
                {
                    return tmp as LoaiHinhCoSo;
                }
                else
                {
                    return null;
                }
            }
        }

        [XafDisplayName("Địa chỉ")]
        [ModelDefault("AlowEdit", "False")]
        public string DiaChi
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.DiaChi;
                    }
                }
                return null;
            }
        }

        [XafDisplayName("Số điện thoại")]
        [ModelDefault("AlowEdit", "False")]
        public string DienThoai
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.SoDienThoai;
                    }
                }
                return null;
            }
        }

        [XafDisplayName("Địa chỉ email")]
        [ModelDefault("AlowEdit", "False")]
        public string Email
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.Email;
                    }
                }
                return null;
            }
        }

        [XafDisplayName("Hành vi vi phạm hành chính")]
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