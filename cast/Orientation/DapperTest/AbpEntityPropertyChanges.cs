//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DapperTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbpEntityPropertyChanges
    {
        public long Id { get; set; }
        public long EntityChangeId { get; set; }
        public string NewValue { get; set; }
        public string OriginalValue { get; set; }
        public string PropertyName { get; set; }
        public string PropertyTypeFullName { get; set; }
        public Nullable<int> TenantId { get; set; }
    
        public virtual AbpEntityChanges AbpEntityChanges { get; set; }
    }
}
