namespace GenerateCode
{
  partial class FrmMain
  {
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.txt_connStr = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbo_connType = new System.Windows.Forms.ComboBox();
      this.rtb_templateTable = new System.Windows.Forms.RichTextBox();
      this.btn_work = new System.Windows.Forms.Button();
      this.btn_ChoicePlace = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.rtb_templateField = new System.Windows.Forms.RichTextBox();
      this.btn_useTemplate = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.rtb_dynamicCode = new System.Windows.Forms.RichTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.btn_useDefaultMethod = new System.Windows.Forms.Button();
      this.btn_useCommandCode = new System.Windows.Forms.Button();
      this.txt_savePlace = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(74, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "连接字符串:";
      // 
      // txt_connStr
      // 
      this.txt_connStr.Location = new System.Drawing.Point(157, 25);
      this.txt_connStr.Name = "txt_connStr";
      this.txt_connStr.Size = new System.Drawing.Size(590, 21);
      this.txt_connStr.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(74, 67);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(71, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "数据库类型:";
      // 
      // cbo_connType
      // 
      this.cbo_connType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbo_connType.FormattingEnabled = true;
      this.cbo_connType.Location = new System.Drawing.Point(157, 67);
      this.cbo_connType.Name = "cbo_connType";
      this.cbo_connType.Size = new System.Drawing.Size(590, 20);
      this.cbo_connType.TabIndex = 3;
      this.cbo_connType.SelectedIndexChanged += new System.EventHandler(this.cbo_connType_SelectedIndexChanged);
      // 
      // rtb_templateTable
      // 
      this.rtb_templateTable.Location = new System.Drawing.Point(32, 205);
      this.rtb_templateTable.Name = "rtb_templateTable";
      this.rtb_templateTable.Size = new System.Drawing.Size(430, 318);
      this.rtb_templateTable.TabIndex = 4;
      this.rtb_templateTable.Text = "";
      // 
      // btn_work
      // 
      this.btn_work.Location = new System.Drawing.Point(471, 717);
      this.btn_work.Name = "btn_work";
      this.btn_work.Size = new System.Drawing.Size(108, 23);
      this.btn_work.TabIndex = 5;
      this.btn_work.Text = "生成";
      this.btn_work.UseVisualStyleBackColor = true;
      this.btn_work.Click += new System.EventHandler(this.btn_work_Click);
      // 
      // btn_ChoicePlace
      // 
      this.btn_ChoicePlace.Location = new System.Drawing.Point(159, 140);
      this.btn_ChoicePlace.Name = "btn_ChoicePlace";
      this.btn_ChoicePlace.Size = new System.Drawing.Size(99, 21);
      this.btn_ChoicePlace.TabIndex = 8;
      this.btn_ChoicePlace.Text = "选择生成位置";
      this.btn_ChoicePlace.UseVisualStyleBackColor = true;
      this.btn_ChoicePlace.Click += new System.EventHandler(this.btn_ChoicePlace_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(86, 110);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 12);
      this.label3.TabIndex = 9;
      this.label3.Text = "生成位置:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(30, 177);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(47, 12);
      this.label4.TabIndex = 11;
      this.label4.Text = "类模板:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(30, 547);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(59, 12);
      this.label5.TabIndex = 12;
      this.label5.Text = "字段模板:";
      // 
      // rtb_templateField
      // 
      this.rtb_templateField.Location = new System.Drawing.Point(32, 582);
      this.rtb_templateField.Name = "rtb_templateField";
      this.rtb_templateField.Size = new System.Drawing.Size(430, 106);
      this.rtb_templateField.TabIndex = 13;
      this.rtb_templateField.Text = "";
      // 
      // btn_useTemplate
      // 
      this.btn_useTemplate.Location = new System.Drawing.Point(32, 717);
      this.btn_useTemplate.Name = "btn_useTemplate";
      this.btn_useTemplate.Size = new System.Drawing.Size(106, 23);
      this.btn_useTemplate.TabIndex = 14;
      this.btn_useTemplate.Text = "使用默认模板";
      this.btn_useTemplate.UseVisualStyleBackColor = true;
      this.btn_useTemplate.Click += new System.EventHandler(this.btn_useTemplate_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.ForeColor = System.Drawing.Color.Tomato;
      this.label6.Location = new System.Drawing.Point(86, 177);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(233, 12);
      this.label6.TabIndex = 15;
      this.label6.Text = "使用\"_***_\"的为占位符,若不使用可以移除";
      // 
      // rtb_dynamicCode
      // 
      this.rtb_dynamicCode.Location = new System.Drawing.Point(511, 205);
      this.rtb_dynamicCode.Name = "rtb_dynamicCode";
      this.rtb_dynamicCode.Size = new System.Drawing.Size(430, 483);
      this.rtb_dynamicCode.TabIndex = 16;
      this.rtb_dynamicCode.Text = "";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(509, 177);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(59, 12);
      this.label7.TabIndex = 18;
      this.label7.Text = "动态方法:";
      // 
      // btn_useDefaultMethod
      // 
      this.btn_useDefaultMethod.Location = new System.Drawing.Point(157, 717);
      this.btn_useDefaultMethod.Name = "btn_useDefaultMethod";
      this.btn_useDefaultMethod.Size = new System.Drawing.Size(129, 23);
      this.btn_useDefaultMethod.TabIndex = 19;
      this.btn_useDefaultMethod.Text = "使用默认动态方法";
      this.btn_useDefaultMethod.UseVisualStyleBackColor = true;
      this.btn_useDefaultMethod.Click += new System.EventHandler(this.btn_useDefaultMethod_Click);
      // 
      // btn_useCommandCode
      // 
      this.btn_useCommandCode.Location = new System.Drawing.Point(301, 717);
      this.btn_useCommandCode.Name = "btn_useCommandCode";
      this.btn_useCommandCode.Size = new System.Drawing.Size(144, 23);
      this.btn_useCommandCode.TabIndex = 20;
      this.btn_useCommandCode.Text = "使用常用动态方法";
      this.btn_useCommandCode.UseVisualStyleBackColor = true;
      this.btn_useCommandCode.Click += new System.EventHandler(this.btn_useCommandCode_Click);
      // 
      // txt_savePlace
      // 
      this.txt_savePlace.Location = new System.Drawing.Point(157, 107);
      this.txt_savePlace.Name = "txt_savePlace";
      this.txt_savePlace.Size = new System.Drawing.Size(590, 21);
      this.txt_savePlace.TabIndex = 21;
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(986, 752);
      this.Controls.Add(this.txt_savePlace);
      this.Controls.Add(this.btn_useCommandCode);
      this.Controls.Add(this.btn_useDefaultMethod);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.rtb_dynamicCode);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.btn_useTemplate);
      this.Controls.Add(this.rtb_templateField);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btn_ChoicePlace);
      this.Controls.Add(this.btn_work);
      this.Controls.Add(this.rtb_templateTable);
      this.Controls.Add(this.cbo_connType);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txt_connStr);
      this.Controls.Add(this.label1);
      this.Name = "FrmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "代码生成器";
      this.Load += new System.EventHandler(this.FrmMain_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txt_connStr;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbo_connType;
    private System.Windows.Forms.RichTextBox rtb_templateTable;
    private System.Windows.Forms.Button btn_work;
    private System.Windows.Forms.Button btn_ChoicePlace;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.RichTextBox rtb_templateField;
    private System.Windows.Forms.Button btn_useTemplate;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RichTextBox rtb_dynamicCode;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btn_useDefaultMethod;
    private System.Windows.Forms.Button btn_useCommandCode;
    private System.Windows.Forms.TextBox txt_savePlace;
  }
}

