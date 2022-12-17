
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
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace AttpV2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Cơ sở sản xuất, kinh doanh")]
    [DefaultProperty(nameof(TenCoSo))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    [ListViewFilter("Tất cả", "", Index = 1)]
    [ListViewFilter("Xếp loại A", "[XLoai] = ##Enum#AttpV2.Module.BusinessObjects.XLoai,A#", Index = 2)]
    [ListViewFilter("Xếp loại B", "[XLoai] = ##Enum#AttpV2.Module.BusinessObjects.XLoai,B#", Index = 3)]
    [ListViewFilter("Xếp loại C", "[XLoai] = ##Enum#AttpV2.Module.BusinessObjects.XLoai,C#", Index = 4)]

    [Appearance("SaphethanGCN", AppearanceItemType = "ViewItem", TargetItems = "ThoiHanGCN",
    Criteria = "DateDiffMonth(Today(), [ThoiHanGCN]) < 6 And [ThoiHanGCN] Is Not Null And [ThoiHanGCN] > Now()", Context = "ListView", FontColor = "Black",BackColor = "Gold", FontStyle = System.Drawing.FontStyle.Bold, Priority = 2)]
    [Appearance("HetHanGCN", AppearanceItemType = "ViewItem", TargetItems = "GiayChungNhan",
    Criteria = "[ThoiHanGCN] < Now() Or [ThoiHanGCN] Is Null", Context = "ListView", FontColor = "Black", BackColor = "Red", FontStyle = System.Drawing.FontStyle.Bold, Priority = 3)]

    [Appearance("HoatDong", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "[HoatDong1] = ##Enum#AttpV2.Module.BusinessObjects.HoatDong,khd#", Context = "ListView", FontColor = "Black", BackColor ="OrangeRed",FontStyle =System.Drawing.FontStyle.Bold, Priority = 4)]
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
            giayChungNhan = GiayChungNhans.Where(t => t.BiThuHoi == null).OrderByDescending(a => a.NgayCapGiayChungNhan).FirstOrDefault();
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
            //get => coQuanQuanLy;
            get
            {
                var account = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);

                if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers"))
                    return coQuanQuanLy;
                return account.CoquanQuanly;
            }
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
        [XafDisplayName("Xếp loại")]
        [ModelDefault("AlowEdit", "False")]
        public string XepLoaiCoSo
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (thamDinh != null)
                    {
                        return $"{thamDinh.KetQuaThamDinh}, {thamDinh.NgayThamDinh.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}";
                    }

                }
                return null;
            }
        }
        XLoai? xLoai;
        [ModelDefault("AlowEdit", "False")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public XLoai? XLoai
        {
            get
            {
                if (thamDinh != null)
                {
                    return xLoai = thamDinh.KetQua;
                }
                return xLoai;
            }
        }


        [XafDisplayName("Giấy chứng nhận ")]
        [ModelDefault("AlowEdit", "False")]
        public string GiayChungNhan
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (giayChungNhan != null)
                    {
                        return $"{giayChungNhan.SoCap}, {giayChungNhan.NgayCapGiayChungNhan?.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}";
                    }
                }
                return null;
            }

        }
        DateTime? thoiHanGCN;
        [XafDisplayName("Thời hạn")]
        [ModelDefault("AlowEdit", "False")]
        public DateTime? ThoiHanGCN
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
            set { SetPropertyValue(nameof(ThoiHanGCN),ref thoiHanGCN,value); }  
        }
        HoatDong hoatDong;
        [XafDisplayName("Tình trạng cơ sở")]
        public HoatDong HoatDong1
        {
            get { return hoatDong; }
            set { SetPropertyValue(nameof(HoatDong1), ref hoatDong, value); }
        }


        #region Association
        [XafDisplayName("Kết quả thẩm đinh của cơ sở")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
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

        [XafDisplayName("Kết quả Thanh kiểm tra")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
        [Association("CoSoSanXuatKinhDoanh-KetQuaThanhKiemTras")]
        public XPCollection<KetQuaThanhKiemTra> KetQuaThanhKiemTras
        {
            get
            {
                return GetCollection<KetQuaThanhKiemTra>(nameof(KetQuaThanhKiemTras));
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
        #endregion

    }
    public enum HoatDong
    {
        [XafDisplayName("Hoạt động")] hd = 0,
        [XafDisplayName("Không hoạt động")] khd = 1,
    }
}