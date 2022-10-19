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
    [XafDisplayName("Thẩm định cơ sở kinh doanh")]
    [DefaultProperty(nameof(TieuDe))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]
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

        [XafDisplayName("Tiêu đề")]
        public string TieuDe
        {
            get => tieuDe;
            set => SetPropertyValue(nameof(TieuDe), ref tieuDe, value);
        }

        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("CoSoSanXuatKinhDoanh-ThamDinhs")]
        [RuleRequiredField("Bắt buộc phải có ThamDinhCapGCN.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
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
        [XafDisplayName("Loại thẩm định cơ sở")]
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
        public XLoai? KetQua
        {
            get => ketQua;
            set
            {
                SetPropertyValue(nameof(KetQua), ref ketQua, value);
                if (IsLoading || IsSaving) return;
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
        public DateTime NgayThamDinh
        {
            get => ngayThamDinh;
            set => SetPropertyValue(nameof(NgayThamDinh), ref ngayThamDinh, value);
        }

        [XafDisplayName("Ghi chú")]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }



        private GiayChungNhan _giayChungNhan;
        [XafDisplayName("Cấp giấy chứng nhận")]
        public GiayChungNhan GiayChungNhan
        {
            get { return _giayChungNhan; }
            set
            {
                if (_giayChungNhan == value) 
                    return;
                GiayChungNhan prevValue = _giayChungNhan;
                _giayChungNhan = value;
                if (IsLoading) 
                    return;
                if (prevValue != null && prevValue.ThamDinh == this) 
                    prevValue.ThamDinh = null;
                if (_giayChungNhan != null) 
                    _giayChungNhan.ThamDinh = this;
                OnChanged(nameof(GiayChungNhan));
            }
        }

        private ThuHoiGCN _thuHoiGCN;
        [XafDisplayName("Thu hồi giấy chứng nhận")]
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


        #endregion

        #region Association
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

        protected override void OnSaving()
        {
            base.OnSaving();
            if (this.LoaiThamDinh == LoaiThamDinh.TDCapGiayChungNhan)
            {
                
            }
        }
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
}