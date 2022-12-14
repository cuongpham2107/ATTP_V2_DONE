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
    [XafDisplayName("Thu hồi giấy chứng nhận")]
    [DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    [ListViewFilter("Tất cả ", "", Index = 0)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong tháng này", "GetMonth([NgayThuHoi]) = GetMonth(Now())", Index = 1)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong 6 tháng đầu năm", "GetMonth([NgayThuHoi]) <= 6 And GetYear([NgayThuHoi]) = GetYear(Now())", Index = 2)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong 6 tháng cuối năm", "GetMonth([NgayThuHoi]) >= 6 And GetYear([NgayThuHoi]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong năm nay", "GetMonth([NgayThuHoi]) >= 6 And GetYear([NgayThuHoi]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong quý 1", "GetMonth([NgayThuHoi]) >= 1 And GetMonth([NgayThuHoi]) <= 3", Index = 4)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong quý 2", "GetMonth([NgayThuHoi]) >= 3 And GetMonth([NgayThuHoi]) <= 6", Index = 5)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong quý 3", "GetMonth([NgayThuHoi]) >= 6 And GetMonth([NgayThuHoi]) <= 9", Index = 6)]
    [ListViewFilter("Thu hồi giấy chứng nhận cấp trong quý 4", "GetMonth([NgayThuHoi]) >= 9 And GetMonth([NgayThuHoi]) <= 12", Index = 7)]


    [Appearance("HideEdit", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "[Lock] = True Or [Close] = True", Context = "Any", Enabled = false)]
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
        [Association("CoSoSanXuatKinhDoanh-ThuHoiGCNs")]
        [RuleRequiredField("Bắt buộc phải có ThuHoiGCN.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set
            {
                SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
               
            }
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
    

        GiayChungNhan _giayChungNhan;
        [XafDisplayName("Thu hồi giấy chứng nhận")]
        [DataSourceProperty(nameof(GiayChungNhans))]
        [RuleRequiredField("Bắt buộc phải có ThuHoiGCN.GiayChungNhan", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public GiayChungNhan GiayChungNhan
        {
            get => _giayChungNhan;
            set
            {
                if (_giayChungNhan == value)
                    return;
                GiayChungNhan prevValue = _giayChungNhan;
                _giayChungNhan = value;
                if (IsLoading)
                    return;
                if (prevValue != null && prevValue.ThuHoiGCN == this)
                    prevValue.ThuHoiGCN = null;
                if (_giayChungNhan != null)
                    _giayChungNhan.ThuHoiGCN = this;
                OnChanged(nameof(GiayChungNhan));
            }
        }

        ThamDinh _thamDinh;
        [XafDisplayName("Căn cứ Thu hồi")]
        [Association("ThamDinh-ThuHoiGCNs")]
        [DataSourceProperty(nameof(ThamDinhs))]
        [RuleRequiredField("Bắt buộc phải có ThuHoiGCN.ThamDinh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public ThamDinh ThamDinh
        {
            get => _thamDinh;
            set
            {
                if (_thamDinh == value) return;
                ThamDinh prevValue = _thamDinh;
                _thamDinh = value;
                if (IsLoading) return;
                if (prevValue != null && prevValue.ThuHoiGCN == this)
                    prevValue.ThuHoiGCN = null;
                if (_thamDinh != null)
                    _thamDinh.ThuHoiGCN = this;
                OnChanged(nameof(ThamDinh));
            }
        }
        
        [XafDisplayName("Xếp loại, Đánh giá cơ sở")]
        [ModelDefault("AlowEdit", "False")]
        public string KetQuaXepLoai
        {
            get
            {
                if (!IsLoading || !IsSaving)
                {
                    if (CoSoSanXuatKinhDoanh != null)
                    {
                        return CoSoSanXuatKinhDoanh.XepLoaiCoSo;
                    }

                }
                return null;
            }

        }

    

        [XafDisplayName("Lý do thu hồi")]
        public string LyDoThuHoi
        {
            get => lyDoThuHoi;
            set => SetPropertyValue(nameof(LyDoThuHoi), ref lyDoThuHoi, value);
        }

        [XafDisplayName("Ngày thu hồi")]
        [RuleRequiredField("Bắt buộc phải có ThuHoiGCN.NgayThuHoi", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayThuHoi
        {
            get => ngayThuHoi;
            set => SetPropertyValue(nameof(NgayThuHoi), ref ngayThuHoi, value);
        }

        private bool _lock;
        [XafDisplayName("Lock"), ToolTip(""), ModelDefault("AllowEdit", "False")]
        public bool Lock
        {
            get { return _lock; }
            set { SetPropertyValue(nameof(Lock), ref _lock, value); }
        }
        private bool _close;
        [XafDisplayName("Close"), ToolTip(""), ModelDefault("AllowEdit", "False")]
        public bool Close
        {
            get { return _close; }
            set { SetPropertyValue(nameof(Close), ref _close, value); }
        }
        #endregion
        #region Action

        [Action(Caption = "Lock", ConfirmationMessage = "Lock dữ liệu này? Sau khi lock sẽ KHÔNG thể sửa chữa thông tin được nữa.", ImageName = "Security_Lock", AutoCommit = true, TargetObjectsCriteria = "[Lock]=False", TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueAtLeastForOne, SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
        public void LockAction()
        {
            Lock = true;
            Session.Save(this);
        }

        [Action(Caption = "UnLock", AutoCommit = true, TargetObjectsCriteria = "[Lock]=True", ImageName = "Security_Unlock")]
        public void LockActionUndo()
        {
            Lock = false;
            Session.Save(this);
        }



        [Action(Caption = "Close", ConfirmationMessage = "Đóng dữ liệu này? Sau khi đóng sẽ KHÔNG thể thay đổi dữ liệu được nữa.", AutoCommit = true, TargetObjectsCriteria = "[Close]=False", TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueAtLeastForOne, SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects, ImageName = "UnprotectDocument")]
        public void CloseAction()
        {
            Close = true;
            Lock = true;
            Session.Save(this);
        }

        [Action(Caption = "Open", AutoCommit = true, TargetObjectsCriteria = "[Close]=True", ImageName = "TrackingChanges_Accept")]
        public void CloseActionUndo()
        {
            Close = false;
            Session.Save(this);
        }
        #endregion

        #region Association

        [Browsable(false)]
        public XPCollection<ThamDinh> ThamDinhs
        {
            get
            {
                var result = Session.Query<ThamDinh>().Where(i => i.CoSoSanXuatKinhDoanh == this.CoSoSanXuatKinhDoanh);
                XPCollection<ThamDinh> thamdinhs = new XPCollection<ThamDinh>(Session, result);
                return thamdinhs;
            }
        }

        [Browsable(false)]
        public XPCollection<GiayChungNhan> GiayChungNhans
        {
            get
            {
                var result = Session.Query<GiayChungNhan>().Where(i => i.CoSoSanXuatKinhDoanh == this.CoSoSanXuatKinhDoanh);
                XPCollection<GiayChungNhan> gcns = new XPCollection<GiayChungNhan>(Session, result);
                return gcns;
            }
        }

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