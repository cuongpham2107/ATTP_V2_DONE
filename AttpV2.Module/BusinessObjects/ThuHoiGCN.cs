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
    [XafDisplayName("Thu hồi giấy chứng nhận")]
    [DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]
    public class ThuHoiGCN : BaseObject
    {
        public ThuHoiGCN(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        #region Properties
        DateTime ngayThuHoi;
        string lyDoThuHoi;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;

        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set
            {
                SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
            }
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

        [XafDisplayName("Mặt hàng sản xuất, kinh doanh")]
        [ModelDefault("AlowEdit", "False")]
        public string TenSanPham
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.TenSanPham;
                    }
                }
                return null;
            }
        }
        ThamDinh _thamDinh;
        [XafDisplayName("Căn cứ Thu hồi")]
        [RuleRequiredField]
        public ThamDinh ThamDinh
        {
            get => _thamDinh;
            set
            {
                if (_thamDinh == value) return;
                ThamDinh prevValue = _thamDinh;
                _thamDinh = value;
                if (IsLoading) return;
                if (prevValue != null && prevValue.ThuHoiGCN == this) prevValue.ThuHoiGCN = null;
                if (_thamDinh != null) _thamDinh.ThuHoiGCN = this;
                OnChanged(nameof(ThamDinh));
            }
        }

        [XafDisplayName("Xếp loại, Đánh giá cơ sở")]
        [ModelDefault("AlowEdit", "False")]
        public string XLoai
        {
            get
            {
                if(!IsLoading || !IsSaving)
                {
                    if(CoSoSanXuatKinhDoanh.XLoai != null)
                    {
                        return CoSoSanXuatKinhDoanh.XLoai;
                    }    
                    
                }
                return null;
            }
           
        }
        [XafDisplayName("Ngày Xếp loại, Đánh giá cơ sở")]
        [ModelDefault("AlowEdit", "False")]
        public DateTime NgayThamDinh
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.NgayThamDinhNgayNhat;
                    }
                    
                }
                return NgayThamDinh;
            }
        }

        [XafDisplayName("Lý do thu hồi")]
        public string LyDoThuHoi
        {
            get => lyDoThuHoi;
            set => SetPropertyValue(nameof(LyDoThuHoi), ref lyDoThuHoi, value);
        }

        [XafDisplayName("Ngày thu hồi")]
        public DateTime NgayThuHoi
        {
            get => ngayThuHoi;
            set => SetPropertyValue(nameof(NgayThuHoi), ref ngayThuHoi, value);
        }

        #endregion

        #region Association

        [Association("ThuHoiGCN-FileDLs")]
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