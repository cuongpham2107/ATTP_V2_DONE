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
    [XafDisplayName("File dữ liệu")]
    [DefaultProperty(nameof(File))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.DataMenuItem)]

    public class FileDL : BaseObject
    { 
        public FileDL(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        XuPhatHanhChinh xuPhatHanhChinh;
        ThuHoiGCN thuHoiGCN;
        GiayChungNhan giayChungNhan;
        ThamDinh thamDinh;
        FileData file;
        DateTime ngayTao;
        string mota;

        [XafDisplayName("File dữ liệu")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }

        [XafDisplayName("Mô tả")]
        public string Mota
        {
            get => mota;
            set => SetPropertyValue(nameof(Mota), ref mota, value);
        }

        [XafDisplayName("Ngày tạo")]
        public DateTime NgayTao
        {
            get => ngayTao;
            set => SetPropertyValue(nameof(NgayTao), ref ngayTao, value);
        }


        #region Association

        [ModelDefault("AlowEdit","False")]
        [Association("ThamDinhCapGCN-FileDLs")]
        [XafDisplayName("Thẩm định")]
        public ThamDinh ThamDinh
        {
            get => thamDinh;
            set => SetPropertyValue(nameof(ThamDinh), ref thamDinh, value);
        }

        [ModelDefault("AlowEdit","False")]
        [Association("GiayChungNhan-FileDLs")]
        [XafDisplayName("Giấy chứng nhận")]
        public GiayChungNhan GiayChungNhan
        {
            get => giayChungNhan;
            set => SetPropertyValue(nameof(GiayChungNhan), ref giayChungNhan, value);
        }
        [ModelDefault("AlowEdit","False")]
        [Association("ThuHoiGCN-FileDLs")]
        [XafDisplayName("Thu hồi giấy chứng nhận")]
        public ThuHoiGCN ThuHoiGCN
        {
            get => thuHoiGCN;
            set => SetPropertyValue(nameof(GiayChungNhan), ref thuHoiGCN, value);
        }

        [Association("XuPhatHanhChinh-FileDLs")]
        [ModelDefault("AlowEdit","False")]
        [XafDisplayName("Xử phạt hành chính")]
        public XuPhatHanhChinh XuPhatHanhChinh
        {
            get => xuPhatHanhChinh;
            set => SetPropertyValue(nameof(XuPhatHanhChinh), ref xuPhatHanhChinh, value);
        }
        #endregion

    }
}