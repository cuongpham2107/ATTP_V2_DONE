﻿using DevExpress.Data.Filtering;
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
    [XafDisplayName("Cấp giấy chứng nhận")]
    [DefaultProperty(nameof(SoCap))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    [ListViewFilter("Tất cả ", "", Index = 0)]
    [ListViewFilter("Giấy chứng nhận cấp trong tháng này", "GetMonth([NgayCapGiayChungNhan]) = GetMonth(Now())", Index = 1)]
    [ListViewFilter("Giấy chứng nhận cấp trong 6 tháng đầu năm", "GetMonth([NgayCapGiayChungNhan]) <= 6 And GetYear([NgayCapGiayChungNhan]) = GetYear(Now())", Index = 2)]
    [ListViewFilter("Giấy chứng nhận cấp trong 6 tháng cuối năm", "GetMonth([NgayCapGiayChungNhan]) >= 6 And GetYear([NgayCapGiayChungNhan]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Giấy chứng nhận cấp trong năm nay", "GetMonth([NgayCapGiayChungNhan]) >= 6 And GetYear([NgayCapGiayChungNhan]) = GetYear(Now())", Index = 3)]
    [ListViewFilter("Giấy chứng nhận cấp trong quý 1", "GetMonth([NgayCapGiayChungNhan]) >= 1 And GetMonth([NgayCapGiayChungNhan]) <= 3", Index = 4)]
    [ListViewFilter("Giấy chứng nhận cấp trong quý 2", "GetMonth([NgayCapGiayChungNhan]) >= 3 And GetMonth([NgayCapGiayChungNhan]) <= 6", Index = 5)]
    [ListViewFilter("Giấy chứng nhận cấp trong quý 3", "GetMonth([NgayCapGiayChungNhan]) >= 6 And GetMonth([NgayCapGiayChungNhan]) <= 9", Index = 6)]
    [ListViewFilter("Giấy chứng nhận cấp trong quý 4", "GetMonth([NgayCapGiayChungNhan]) >= 9 And GetMonth([NgayCapGiayChungNhan]) <= 12", Index = 7)]


    [Appearance("SaphethanGCN", AppearanceItemType = "ViewItem", TargetItems = "CoSoSanXuatKinhDoanh",
    Criteria = "DateDiffMonth(Today(), [NgayHetHanGCN]) < 1 And [NgayHetHanGCN] Is Not Null", Context = "ListView", FontColor = "Orange", Priority = 2)]
    [Appearance("HetHanGCN", AppearanceItemType = "ViewItem", TargetItems = "CoSoSanXuatKinhDoanh",
    Criteria = "[NgayHetHanGCN] < Now() Or [NgayHetHanGCN] Is Null Or [IsThuHoi] = False", Context = "ListView", FontColor = "Red", Priority = 3)]
    public class GiayChungNhan : BaseObject
    {
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public GiayChungNhan(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #region Properties

        DateTime ngayHetHanGCN;
        DateTime ngayCapGiayChungNhan;
        string soCap;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;

        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("CoSoSanXuatKinhDoanh-GiayChungNhans")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set
            {
                SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
            }
        }

        [XafDisplayName("Số cấp")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.SoCap", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string SoCap
        {
            get => soCap;
            set => SetPropertyValue(nameof(SoCap), ref soCap, value);
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

    

        ThamDinh _thamDinh;
        [XafDisplayName("Căn cứ cấp")]
        [Association("ThamDinh-GiayChungNhans")]
        [RuleRequiredField]
        public ThamDinh ThamDinh
        {
            get => _thamDinh;
            set
            {
                SetPropertyValue(nameof(ThamDinh), ref _thamDinh, value);
            }
        }

        private ThuHoiGCN _thuHoiGCN;
        [XafDisplayName("Thu hồi giấy chứng nhận")]
        //[VisibleInDetailView(false)]
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
                if (prevValue != null && prevValue.GiayChungNhan == this) prevValue.GiayChungNhan = null;
                if (_thuHoiGCN != null) _thuHoiGCN.GiayChungNhan = this;
                OnChanged(nameof(ThuHoiGCN));
            }
        }

        [XafDisplayName("Ngày cấp")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.NgayCapGiayChungNhan", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayCapGiayChungNhan
        {
            get => ngayCapGiayChungNhan;
            set => SetPropertyValue(nameof(NgayCapGiayChungNhan), ref ngayCapGiayChungNhan, value);
        }


        [XafDisplayName("Ngày hết hạn")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.NgayHetHanGCN", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayHetHanGCN
        {
            get => ngayHetHanGCN;
            set => SetPropertyValue(nameof(NgayHetHanGCN), ref ngayHetHanGCN, value);
        }

        [XafDisplayName("Bị thu hồi")]
        [Appearance("HideBiThuHoi", AppearanceItemType.ViewItem, "[Is Thu Hoi] = False", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [ModelDefault("AllowEdit", "False")]
        public string BiThuHoi
        {
            get 
            { 
                if(!IsLoading || !IsSaving)
                {
                    if(ThuHoiGCN != null)
                    {
                        return $"Thu hồi ngày {ThuHoiGCN.NgayThuHoi.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}";
                    }
                }
                return null;
            }
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public bool IsThuHoi
        {
            get
            {   if(ThuHoiGCN != null)
                {
                    return false;
                }
                return true;
               
            }
        }

        //kiểm tra xem đã tồn tại giấy chứng nhận từ kết quả thẩm định chưa
        [Browsable(false)]
        [RuleFromBoolProperty(nameof(IsDetail), DefaultContexts.Save, "Không thể tạo Giấy chứng nhận mới từ cùng một kết quả thẩm định này!", SkipNullOrEmptyValues = true, UsedProperties = "ThamDinh")]
        public bool IsDetail
        {
            get
            {
                return !Session.Query<GiayChungNhan>().Any(i => i.ThamDinh == ThamDinh);
            }
        }



        #endregion

        #region Association

        [Association("GiayChungNhan-FileDLs")]
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