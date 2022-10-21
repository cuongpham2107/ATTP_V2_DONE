using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    [XafDisplayName("Cơ sở sản xuất, kinh doanh")]
    [DefaultProperty(nameof(TenCoSo))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.CategoryMenuItem)]
   

    [Appearance("SaphethanGCN", AppearanceItemType = "ViewItem", TargetItems = "TenCoSo",
    Criteria = "DateDiffMonth(Today(), [ThoiHanGCN]) < 1 And [ThoiHanGCN] Is Not Null", Context = "ListView", FontColor = "Orange", Priority = 2)]
    [Appearance("HetHanGCN", AppearanceItemType = "ViewItem", TargetItems = "TenCoSo",
    Criteria = "[ThoiHanGCN] < Now() And [ThoiHanGCN] Is Null ", Context = "ListView", FontColor = "Red", Priority = 3)]
    public class CoSoSanXuatKinhDoanh : BaseObject
    { 
        public CoSoSanXuatKinhDoanh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        ThamDinh thamDinh;
        GiayChungNhan giayChungNhan;
        protected override void OnLoaded()
        {
            base.OnLoaded();

            thamDinh = ThamDinhs.OrderByDescending(i => i.NgayThamDinh).FirstOrDefault();
            giayChungNhan = GiayChungNhans.Where(t=>t.BiThuHoi == null).OrderByDescending(a=>a.NgayCapGiayChungNhan).FirstOrDefault();
        }

        string soDienThoai;
        LoaiHinhCoSo loaiHinhCoSo;
        CoQuanQuanLy coQuanQuanLy;
        string tenSanPham;
        string email;
        string diaChi;
        string tenChuDoanhNghiep;
        string maSoDoanhNghiep;
        string tenCoSo;

        [XafDisplayName("Mã số cơ sở")]
        [RuleRequiredField("Bắt buộc phải có CoSoSanXuatKinhDoanh.MaSoDoanhNghiep", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string MaSoDoanhNghiep
        {
            get { return maSoDoanhNghiep; }
            set { SetPropertyValue(nameof(MaSoDoanhNghiep), ref maSoDoanhNghiep, value); }
        }

        [XafDisplayName("Tên cơ sở ")]
        [RuleRequiredField("Bắt buộc phải có CoSoSanXuatKinhDoanh.TenCoSo", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string TenCoSo
        {
            get { return tenCoSo; }
            set { SetPropertyValue(nameof(TenCoSo), ref tenCoSo, value); }
        }
        [RuleRequiredField("Bắt buộc phải có CoSoSanXuatKinhDoanh.TenChuDoanhNghiep", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [XafDisplayName("Chủ cơ sở")]
        public string TenChuDoanhNghiep
        {
            get => tenChuDoanhNghiep;
            set => SetPropertyValue(nameof(TenChuDoanhNghiep), ref tenChuDoanhNghiep, value);
        }
        [XafDisplayName("Loại Hình cơ sở")]
        [RuleRequiredField("Bắt buộc phải có CoSoSanXuatKinhDoanh.LoaiHinhCoSo", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public LoaiHinhCoSo LoaiHinhCoSo
        {
            get => loaiHinhCoSo;
            set => SetPropertyValue(nameof(LoaiHinhCoSo), ref loaiHinhCoSo, value);
        }

        [XafDisplayName("Cơ quan quản lý")]
        [RuleRequiredField("Bắt buộc phải có CoSoSanXuatKinhDoanh.CoQuanQuanLy", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public CoQuanQuanLy CoQuanQuanLy
        {
            get => coQuanQuanLy;
            set => SetPropertyValue(nameof(CoQuanQuanLy), ref coQuanQuanLy, value);
        }

        [XafDisplayName("Địa chỉ")]
        public string DiaChi
        {
            get => diaChi;
            set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
        }

        [XafDisplayName("Số điện thoại")]
        public string SoDienThoai
        {
            get => soDienThoai;
            set => SetPropertyValue(nameof(SoDienThoai), ref soDienThoai, value);
        }
        [XafDisplayName("Địa chỉ Email")]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }
       

        [XafDisplayName("Tên sản phẩm")]
        public string TenSanPham
        {
            get => tenSanPham;
            set => SetPropertyValue(nameof(TenSanPham), ref tenSanPham, value);
        }
        private string xepLoaiCoSo;
        [XafDisplayName("Xếp loại của cơ sở")]
        [ModelDefault("AlowEdit", "False")]
        public string XepLoaiCoSo
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (thamDinh != null)
                    {
                        return $"Xếp loại {thamDinh.KetQuaThamDinh}, Ngày thẩm đinh {thamDinh.NgayThamDinh}";
                    }
                   
                }
                return xepLoaiCoSo;
            }
        }


        [XafDisplayName("Giấy chứng nhận của cơ sở sản xuất, kinh doanh")]
        [ModelDefault("AlowEdit","False")]
        public string GiayChungNhan
        {
            get
            {
                if(!IsLoading || !IsSaving)
                {
                    if(giayChungNhan != null)
                    {
                        return giayChungNhan.SoCap;
                    }
                }
                return null;
            }
            
        }
        DateTime thoiHanGCN;
        [XafDisplayName("Thời hạn còn lại của giấy chứng nhận")]
        [ModelDefault("AlowEdit", "False")]
        public DateTime ThoiHanGCN
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (giayChungNhan != null)
                    {
                        return giayChungNhan.NgayHetHanGCN;
                    }
                }
                return thoiHanGCN;
            }

        }


        #region Association
        [XafDisplayName("Kết quả thẩm đinh của cơ sở")]
        [ModelDefault("AllowLink","False")]
        [ModelDefault("AllowUnlink","False")]
        [Association("CoSoSanXuatKinhDoanh-ThamDinhs")]
        public XPCollection<ThamDinh> ThamDinhs
        {
            get
            {
                return GetCollection<ThamDinh>(nameof(ThamDinhs));
            }
        }

        [XafDisplayName("Giấy chứng nhận của cơ sở")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
        [Association("CoSoSanXuatKinhDoanh-GiayChungNhans")]
        public XPCollection<GiayChungNhan> GiayChungNhans
        {
            get
            {
                return GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
            }
        }

        [XafDisplayName("Xử phạt hành chính")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
        [Association("CoSoSanXuatKinhDoanh-XuPhatHanhChinhs")]
        public XPCollection<XuPhatHanhChinh> XuPhatHanhChinhs
        {
            get
            {
                return GetCollection<XuPhatHanhChinh>(nameof(XuPhatHanhChinhs));
            }
        }

        [XafDisplayName("Giấy chứng nhận thu hồi")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
        [Association("CoSoSanXuatKinhDoanh-ThuHoiGCNs")]
        public XPCollection<ThuHoiGCN> ThuHoiGCNs
        {
            get
            {
                return GetCollection<ThuHoiGCN>(nameof(ThuHoiGCNs));
            }
        }
        #endregion

    }
}