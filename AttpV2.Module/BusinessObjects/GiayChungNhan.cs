using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    [XafDisplayName("Cấp giấy chứng nhận")]
    [DefaultProperty(nameof(SoCap))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]


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
            CapLai = false;
        }
        
        #region Properties

        object propertyName;
        bool coHieuLuc;
        DateTime ngayHetHanGCN;
        bool capLai;
        DateTime ngayCapGiayChungNhan;
        string soCap;
        string email;
        string dienThoai;
        string diaChi;
        CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
        string maSo;
        [XafDisplayName("Mã số ")]
        public string MaSo
        {
            get => maSo;
            set => SetPropertyValue(nameof(MaSo), ref maSo, value);
        }

        [XafDisplayName("Số cấp giấy chứng nhận")]
        public string SoCap
        {
            get => soCap;
            set => SetPropertyValue(nameof(SoCap), ref soCap, value);
        }

        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("CoSoSanXuatKinhDoanh-GiayChungNhans")]
        [RuleRequiredField("Bắt buộc phải có GiayChungNhan.CoSoSanXuatKinhDoanh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh
        {
            get => coSoSanXuatKinhDoanh;
            set
            {
                SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);

                if (!IsLoading || !IsSaving)
                {
                    if(CoSoSanXuatKinhDoanh != null)
                    {
                        if (Session.Query<CoSoSanXuatKinhDoanh>().Any(i => i.Oid == CoSoSanXuatKinhDoanh.Oid) == true)
                        {
                            CapLai = true;
                        }
                        else
                        {
                            CapLai = false;
                        }
                    }
                    else
                    {
                        CapLai = false;
                    }
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

        //XLoai xLoai;
        //public XLoai XLoai
        //{
        //    get => xLoai;
        //    set => SetPropertyValue(nameof(XLoai), ref xLoai, value);
        //}
        //Xếp loại thẩm định
        //Ngày xếp loại thẩm định

        ThamDinh _thamDinh;
        [XafDisplayName("Căn cứ cấp")]
        [RuleRequiredField]
        public ThamDinh ThamDinh
        {
            get => _thamDinh;
            set
            {
                if (_thamDinh == value) 
                    return;
                ThamDinh prevValue = _thamDinh;
                _thamDinh = value;
                if (IsLoading) 
                    return;
                if (prevValue != null && prevValue.GiayChungNhan == this) 
                    prevValue.GiayChungNhan = null;
                if (_thamDinh != null) 
                    _thamDinh.GiayChungNhan = this;
                OnChanged(nameof(ThamDinh));
            }
        }


        [XafDisplayName("Ngày cấp giấy chứng nhận")]
        public DateTime NgayCapGiayChungNhan
        {
            get => ngayCapGiayChungNhan;
            set => SetPropertyValue(nameof(NgayCapGiayChungNhan), ref ngayCapGiayChungNhan, value);
        }

        [XafDisplayName("Cấp lại")]
        public bool CapLai
        {
            get
            {
                return capLai;
            }
            set { SetPropertyValue(nameof(CapLai), ref capLai, value); }

        }

        [XafDisplayName("Ngày hết hạn Giấy chứng nhận")]
        public DateTime NgayHetHanGCN
        {
            get => ngayHetHanGCN;
            set => SetPropertyValue(nameof(NgayHetHanGCN), ref ngayHetHanGCN, value);
        }

        [XafDisplayName("Có hiệu lực")]
        public bool CoHieuLuc
        {
            get => coHieuLuc;
            set => SetPropertyValue(nameof(CoHieuLuc), ref coHieuLuc, value);
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