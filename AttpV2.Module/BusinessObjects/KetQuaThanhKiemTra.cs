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
    [XafDisplayName("Kết quả Thanh kiểm tra")]
    [DefaultProperty(nameof(TieuDe))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    [ListViewFilter("Tất cả ", "", Index = 0)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong tháng này", "GetMonth([NgayThamDinh]) = GetMonth(Now())", Index = 1)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong 6 tháng đầu năm", "GetMonth([NgayThamDinh]) <= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 2)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong 6 tháng cuối năm", "GetMonth([NgayThamDinh]) >= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thanh kiểm trah cơ sở trong năm nay", "GetMonth([NgayThamDinh]) >= 6 And GetYear([NgayThamDinh]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong quý 1", "GetMonth([NgayThamDinh]) >= 1 And GetMonth([NgayThamDinh]) <= 3", Index = 4)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong quý 2", "GetMonth([NgayThamDinh]) >= 3 And GetMonth([NgayThamDinh]) <= 6", Index = 5)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong quý 3", "GetMonth([NgayThamDinh]) >= 6 And GetMonth([NgayThamDinh]) <= 9", Index = 6)]
    [ListViewFilter("Thanh kiểm tra cơ sở trong quý 4", "GetMonth([NgayThamDinh]) >= 9 And GetMonth([NgayThamDinh]) <= 12", Index = 7)]

    [Appearance("HideEdit", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "[Lock] = True Or [Close] = True", Context = "Any", Enabled = false)]
    public class KetQuaThanhKiemTra : BaseObject
    { 
        public KetQuaThanhKiemTra(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        string tieuDe;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;


        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Tiêu đề")]
        public string TieuDe
        {
            get
            {
                if (!IsSaving || !IsLoading)
                {
                    if (NgayThamDinh != null && CoSoSanXuatKinhDoanh != null && KetQuaThamDinh != null)
                    {
                        return $"Thanh kiểm tra: {CoSoSanXuatKinhDoanh}, {NgayThamDinh?.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}, {KetQuaThamDinh}";
                    }

                }
                return tieuDe;
            }
            set => SetPropertyValue(nameof(TieuDe), ref tieuDe, value);
        }
        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("CoSoSanXuatKinhDoanh-KetQuaThanhKiemTras")]
        [RuleRequiredField("Bắt buộc phải có KetQuaThanhKiemTra.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
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
        XLoai? ketQua;
        [XafDisplayName("Kết quả")]
        [VisibleInListView(false)]// không hiện ở ListView
        [RuleRequiredField("Bắt buộc phải có KetQuaThanhKiemTra.KetQua", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public XLoai? KetQua
        {
            get => ketQua;
            set
            {
                SetPropertyValue(nameof(KetQua), ref ketQua, value);
            }
        }
         string ketQuaThamDinh;
        [XafDisplayName("Kết quả thanh kiểm tra")]
        [VisibleInDetailView(false)] // không hiện ở DetailView
        [ModelDefault("AllowEdit", "false")]
        public string KetQuaThamDinh
        {
            get => ketQuaThamDinh;
            set => SetPropertyValue(nameof(KetQuaThamDinh), ref ketQuaThamDinh, value);
        }
        DateTime? ngayThamDinh;
        [XafDisplayName("Ngày Thanh kiểm tra")]
        [RuleRequiredField("Bắt buộc phải có KetQuaThanhKiemTra.NgayThamDinh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime? NgayThamDinh
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
        string ghiChu;
        [XafDisplayName("Ghi chú")]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }

        private bool _lock;
        [VisibleInDetailView(false) ]
        [XafDisplayName("Lock"), ToolTip(""), ModelDefault("AllowEdit", "False")]
        public bool Lock
        {
            get { return _lock; }
            set { SetPropertyValue(nameof(Lock), ref _lock, value); }
        }

        private bool _close;
        [VisibleInDetailView(false)]
        [XafDisplayName("Close"), ToolTip(""), ModelDefault("AllowEdit", "False")]
        public bool Close
        {
            get { return _close; }
            set { SetPropertyValue(nameof(Close), ref _close, value); }
        }

       
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
    }
}