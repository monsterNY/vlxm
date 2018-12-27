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
    
    public partial class PayTable
    {
        public int p_Id { get; set; }
        public string p_PayId { get; set; }
        public Nullable<int> p_UserId { get; set; }
        public string p_StudentId { get; set; }
        public Nullable<int> p_Type { get; set; }
        public string p_OrderName { get; set; }
        public Nullable<decimal> p_Money { get; set; }
        public Nullable<bool> p_UseBalance { get; set; }
        public Nullable<decimal> p_Balance { get; set; }
        public Nullable<decimal> p_PayMoney { get; set; }
        public string p_PayWay { get; set; }
        public Nullable<decimal> p_NotCash { get; set; }
        public Nullable<decimal> p_Cash { get; set; }
        public Nullable<decimal> p_Tuition { get; set; }
        public Nullable<decimal> p_Repayment { get; set; }
        public Nullable<decimal> p_Refund { get; set; }
        public string p_TransactionId { get; set; }
        public string p_ThirdTradeNo { get; set; }
        public string p_Certificate { get; set; }
        public Nullable<System.DateTime> p_PayTime { get; set; }
        public Nullable<System.DateTime> p_PayTime_invalid { get; set; }
        public Nullable<bool> p_PayTime_status { get; set; }
        public Nullable<bool> p_Enter { get; set; }
        public Nullable<decimal> p_EnterMoney { get; set; }
        public Nullable<int> p_Status { get; set; }
        public Nullable<int> p_OldStatus { get; set; }
        public Nullable<bool> p_Arrears { get; set; }
        public Nullable<int> p_SellerId { get; set; }
        public Nullable<System.DateTime> p_AddTime { get; set; }
        public string p_PayAccount { get; set; }
        public Nullable<int> p_MoneyTaker { get; set; }
        public Nullable<System.DateTime> p_AuditTime { get; set; }
        public Nullable<int> p_AuditStaffId { get; set; }
        public Nullable<int> p_ConsultId { get; set; }
        public string p_Pos_pay_type { get; set; }
        public Nullable<int> p_Pos_qr_type { get; set; }
        public string p_Pos_mer_id { get; set; }
        public string p_Pos_mer_name { get; set; }
        public string p_Pos_ter_id { get; set; }
        public string p_Pos_card_num { get; set; }
        public string p_Pos_card_name { get; set; }
        public string p_Pos_card_bank { get; set; }
        public Nullable<bool> p_Is_invoice { get; set; }
        public Nullable<int> p_Invoice_status { get; set; }
        public string p_Invoice_url { get; set; }
    }
}