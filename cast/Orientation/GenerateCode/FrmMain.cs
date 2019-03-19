using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GenerateCode.CusInterface;
using GenerateCode.Domain.CusConst;
using GenerateCode.Domain.Menu;
using GenerateCode.Inherit;
using GenerateCode.Tools;
using Tools.CusTools;

namespace GenerateCode
{
  public partial class FrmMain : Form
  {
    #region fields

    protected Dictionary<string, int> DbTypes;

    protected DbTypes DbType;

    protected IDbOperation Operation;

    protected IDbConnection Conn;

    #endregion

    #region construct

    public FrmMain()
    {
      InitializeComponent();
    }

    #endregion

    #region Func

    private void USeDefaultDynamicCode()
    {
      this.rtb_dynamicCode.Text = ParamConst.DynamicCodeStr;
    }

    private void InitView()
    {
      this.cbo_connType.DataSource = DbTypes.Keys.ToList();

      this.cbo_connType.SelectedIndex = 1;

//      this.cbo_connType.DisplayMember = "";
//      this.cbo_connType.ValueMember = "";

    }

    private void InitData()
    {
      DbTypes = MenuTools.GetList(typeof(DbTypes));
    }

    protected void UseDefaultTemplate()
    {
      this.rtb_templateTable.Text = ParamConst.TemplateTableStr;
      this.rtb_templateField.Text = ParamConst.TemplateFieldStr;
    }

    #endregion

    #region event

    private void btn_work_Click(object sender, EventArgs e)
    {
      var savePlace = this.txt_savePlace.Text;

      if (string.IsNullOrWhiteSpace(savePlace))
      {
        MessageBox.Show("生成位置不能为空！", "Error Msg");
        return;
      }

      Func<string, string> generateTableNameFunc = null, generatePropNameFunc = null;

      try
      {
        var dynamicAssembly = DynamicClassTools.DynamicAssembly(this.rtb_dynamicCode.Text);

        var instance = DynamicClassTools.GetClass(dynamicAssembly, "Dynamicly.GenerateHelper");

        generateTableNameFunc = (str) => (string) DynamicClassTools.InvokeMethod(instance, "GetTableName", str);
        generatePropNameFunc = (str) => (string) DynamicClassTools.InvokeMethod(instance, "GetPropName", str);
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "Error Msg");
        return;
      }

      var connStr = this.txt_connStr.Text;

      if (string.IsNullOrWhiteSpace(connStr))
      {
        MessageBox.Show("连接字符串不能为空！", "Error Msg");
        return;
      }

      try
      {
        Conn = DbTools.GetConnection(connStr, DbType);

        //测试连接
        Conn.Open();

        Conn.Close();
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "Error Msg");
        return;
      }

      Operation = DbTools.GetOperation(DbType);

      if (Operation == null)
      {
        MessageBox.Show($"暂不支持{DbType}", "Error Msg");
        return;
      }

      CommandGenerateCode commandGenerate = new CommandGenerateCode(Conn, Operation);

      var dictionary = commandGenerate.Work();

      if (dictionary.Count == 0)
      {
        MessageBox.Show("查询数据为空", "Confirm Msg");
        return;
      }

      var templateField = this.rtb_templateField.Text;
      var templateTable = this.rtb_templateTable.Text;
      try
      {
        //并行写入文件
        dictionary.AsParallel().ForAll((item =>
            {
              var code = commandGenerate.GenerateCode(item.Key, item.Value, templateTable, templateField
                , generateTableNameFunc, generatePropNameFunc);

              FileTools.InFile(savePlace, $"{generateTableNameFunc.Invoke(item.Key.Name)}.class", code);
            }
          ));

        MessageBox.Show("Success!", "Confirm Msg");
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "Error Msg");
      }
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
      InitData();

      InitView();

      UseDefaultTemplate();

      USeDefaultDynamicCode();
    }

    private void btn_ChoicePlace_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.SelectedPath = this.txt_savePlace.Text;
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        this.txt_savePlace.Text = folderBrowserDialog.SelectedPath;
      }
    }

    private void btn_useTemplate_Click(object sender, EventArgs e)
    {
      UseDefaultTemplate();
    }

    private void btn_useDefaultMethod_Click(object sender, EventArgs e)
    {
      this.rtb_dynamicCode.Text = ParamConst.DynamicCodeStr;
    }

    private void btn_useCommandCode_Click(object sender, EventArgs e)
    {
      this.rtb_dynamicCode.Text = ParamConst.DynamicCommandCodeStr;
    }

    private void cbo_connType_SelectedIndexChanged(object sender, EventArgs e)
    {
      DbType = (DbTypes)DbTypes[this.cbo_connType.SelectedValue.ToString()];

      switch (DbType)
      {
        case Domain.Menu.DbTypes.Mysql:
          this.txt_connStr.Text = "Server=localhost;Database=数据库;User=用户名;Password=密码;charset=utf8mb4;";
          break;
        case Domain.Menu.DbTypes.Sqlserver:
          this.txt_connStr.Text = "Data Source=localhost;Initial Catalog=数据库;Persist Security Info=False;User ID=用户名;Password=密码;";
          break;
        case Domain.Menu.DbTypes.Oracle:
          this.txt_connStr.Text = "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=服务器地址)(PORT=端口号)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=数据库名称)));User Id=用户名;Password=密码;Persist Security Info=True;Enlist=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;";
          break;
      }

    }
    #endregion

  }
}