using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttpV2.Module.BusinessObjects
{
    public enum CapQuanLy
    {
        [XafDisplayName("Cấp Tỉnh")]  Tinh = 0,
        [XafDisplayName("Cấp Huyện")] Huyen = 1,
        [XafDisplayName("Cấp Xã")]  Xa = 2

    }

    public enum XLoai
    {
        [XafDisplayName("A")] A,
        [XafDisplayName("B")] B ,
        [XafDisplayName("C")] C ,
    }
    public enum LoaiThamDinh
    {
        [XafDisplayName("Thẩm định cấp Giấy chứng nhận")] TDCapGiayChungNhan ,
        [XafDisplayName("Thẩm định định kỳ")] TDDinhKy ,

    }
}
