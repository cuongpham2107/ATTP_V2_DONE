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
using System.Globalization;
using System.Linq;
using System.Text;

namespace AttpV2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Thẩm định cơ sở")]
    [DefaultProperty(nameof(TieuDe))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    [ListViewFilter("Tất cả ", "", Index = 0)]
    [ListViewFilter("Thẩm định cơ sở trong tháng này", "GetMonth([NgayThamDinh]) = GetMonth(Now())", Index = 1)]
    [ListViewFilter("Thẩm định cơ sở trong 6 tháng đầu năm", "GetMonth([NgayThamDinh]) <= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 2)]
    [ListViewFilter("Thẩm định cơ sở trong 6 tháng cuối năm", "GetMonth([NgayThamDinh]) >= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thẩm định cơ sở trong năm nay", "GetMonth([NgayThamDinh]) >= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thẩm định cơ sở trong quý 1", "GetMonth([NgayThamDinh]) >= 1 And GetMonth([NgayThamDinh]) <= 3", Index = 4)]
    [ListViewFilter("Thẩm định cơ sở trong quý 2", "GetMonth([NgayThamDinh]) >= 3 And GetMonth([NgayThamDinh]) <= 6", Index = 5)]
    [ListViewFilter("Thẩm định cơ sở trong quý 3", "GetMonth([NgayThamDinh]) >= 6 And GetMonth([NgayThamDinh]) <= 9", Index = 6)]
    [ListViewFilter("Thẩm định cơ sở trong quý 4", "GetMonth([NgayThamDinh]) >= 9 And GetMonth([NgayThamDinh]) <= 12", Index = 7)]

    
    public class ThamDinh : BaseObject
    {
        public ThamDinh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
       
        #region Properties

        LoaiThamDinh loaiThamDinh;
        string ghiChu;
        DateTime ngayThamDinh;
        XLoai xLoai;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;

        string tieuDe;
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Tiêu đề")]
        public string TieuDe
        {
            get
            {
                if(!IsSaving || !IsLoading)
                {
                    if(NgayThamDinh != null && CoSoSanXuatKinhDoanh != null && KetQuaThamDinh != null)
                    {
                        return $"{NgayThamDinh.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}, {KetQuaThamDinh}, {CoSoSanXuatKinhDoanh}";
                    }
                   
                }
                return tieuDe;
            }
            set => SetPropertyValue(nameof(TieuDe), ref tieuDe, value);
        }

        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("CoSoSanXuatKinhDoanh-ThamDinhs")]
        [RuleRequiredField("Bắt buộc phải có ThamDinh.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [ModelDefault("ImmediatePostData", "True")]
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
        [XafDisplayName("Loại thẩm định")]
        public LoaiThamDinh LoaiThamDinh
        {
            get => loaiThamDinh;
            set 
            {
                SetPropertyValue(nameof(LoaiThamDinh), ref loaiThamDinh, value);
               
            }
        }

        XLoai? ketQua;
        [XafDisplayName("Kết quả")]
        [VisibleInListView(false)]// không hiện ở ListView
        [RuleRequiredField("Bắt buộc phải có ThamDinh.KetQua", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public XLoai? KetQua
        {
            get => ketQua;
            set
            {
                SetPropertyValue(nameof(KetQua), ref ketQua, value);
                if (IsLoading || IsSaving) 
                    return;
                SetKetQua();
            }
        }

        string ketQuaThamDinh;
        [XafDisplayName("Kết quả thẩm định")]
        [VisibleInDetailView(false)] // không hiện ở DetailView
        [ModelDefault("AllowEdit", "false")]
        public string KetQuaThamDinh
        {
            get => ketQuaThamDinh;
            set => SetPropertyValue(nameof(KetQuaThamDinh), ref ketQuaThamDinh, value);
        }

        

        [XafDisplayName("Ngày thẩm định")]
        [RuleRequiredField("Bắt buộc phải có ThamDinh.NgayThamDinh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayThamDinh
        {
            get
            {
               
                return ngayThamDinh;
            }
            set 
            {
                SetPropertyValue(nameof(NgayThamDinh), ref ngayThamDinh, value);
                
            }
        }

        [XafDisplayName("Ghi chú")]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }


        private ThuHoiGCN _thuHoiGCN;
        [XafDisplayName("Thu hồi giấy chứng nhận")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("AlowEdit", "False")]
        public ThuHoiGCN ThuHoiGCN
        {
            get { return _thuHoiGCN; }
            set
            {
                if (_thuHoiGCN == value)
                    return;
                ThuHoiGCN prevValue = _thuHoiGCN;
                _thuHoiGCN = value;
                if (IsLoading) return;
                if (prevValue != null && prevValue.ThamDinh == this) prevValue.ThamDinh = null;
                if (_thuHoiGCN != null) _thuHoiGCN.ThamDinh = this;
                OnChanged(nameof(ThuHoiGCN));
            }
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

        [Association("ThamDinh-GiayChungNhans")]
        [ModelDefault("AllowLink", "False")]
        [ModelDefault("AllowUnlink", "False")]
        [XafDisplayName("Giấy chứng nhận")]
        public XPCollection<GiayChungNhan> GiayChungNhans
        {
            get
            {
                return GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
            }
        }

        [Association("ThamDinh-ThuHoiGCNs")]
        [XafDisplayName("Thu hồi giấy chứng nhận")]
        public XPCollection<ThuHoiGCN> ThuHoiGCNs
        {
            get
            {
                return GetCollection<ThuHoiGCN>(nameof(ThuHoiGCNs));
            }
        }

        [Association("ThamDinhCapGCN-FileDLs")]
        [XafDisplayName("File dữ liệu")]
        public XPCollection<FileDL> FileDLs
        {
            get
            {
                return GetCollection<FileDL>(nameof(FileDLs));
            }
        }
        #endregion

        
        #region Function
        private void SetKetQua()
        {
            KetQuaThamDinh = LoaiThamDinh switch
            {
                LoaiThamDinh.TDCapGiayChungNhan => $"{KetQua}1",
                LoaiThamDinh.TDDinhKy => $"{KetQua}2",
            };
        }
        #endregion
    }

    [DomainComponent]
    public class CapMoiGiayChungNhan
    {
        public string MaSo { get; set; }
        public string SoCap { get; set; }

        public DateTime NgayHetHan { get; set; }
    }
}