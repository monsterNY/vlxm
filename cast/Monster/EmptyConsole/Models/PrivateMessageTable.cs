//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmptyConsole.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrivateMessageTable
    {
        public int p_Id { get; set; }
        public int p_Type { get; set; }
        public int p_User_id { get; set; }
        public int p_Face_id { get; set; }
        public string p_Content { get; set; }
        public bool p_Is_read { get; set; }
        public int p_Status { get; set; }
        public System.DateTime p_Created_at { get; set; }
    }
}