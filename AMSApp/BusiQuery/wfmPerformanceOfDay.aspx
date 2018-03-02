<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmPerformanceOfDay.aspx.cs"
    Inherits="AMSApp.BusiQuery.wfmPerformanceOfDay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门店业绩日报表</title>
    <script type="text/javascript" src="../js/calendar.js"> </script>
    <script type="text/javascript" src="../js/swfobject.js"></script>
    <script type="text/javascript" src="../js/downloadify.min.js"></script>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })();
        var tableToExcel2 = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table);
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML };
                return format(template, ctx);
            }
        })()
        function CreateExcelSheet(el) {

            var x = document.getElementById(el).rows;

            var xls = new ActiveXObject("Excel.Application");

            xls.visible = true;
            xls.Workbooks.Add
            for (i = 0; i < x.length; i++) {
                var y = x[i].cells;

                for (j = 0; j < y.length; j++) {
                    xls.Cells(i + 1, j + 1).Value = y[j].innerText;
                }
            }
            xls.Visible = true;
            xls.UserControl = true;

            return xls;
        }
        function load() {
            Downloadify.create('downloadify', {
                filename: function () {
                    return "门店业绩日报表.xls";
                },
                data: function () {
                    //return document.getElementById('testTable').value;
                    return tableToExcel2('testTable', '门店业绩日报表');
                    //return document.getElementById('testTable').innerHTML;
                },
                onComplete: function () { },
                onCancel: function () { alert('You have cancelled the saving of this file.'); },
                onError: function () { alert('You must put something in the File Contents or there will be nothing to save!'); },
                swf: '../js/media/downloadify.swf',
                downloadImage: '../js/images/download.png',
                width: 100,
                height: 30,
                transparent: true,
                append: false
            });
        }
    </script>
    <style>
        table
        {
            border-collapse: collapse;
        }
        table, th, td
        {
            border: 1px solid black;
        }
    </style>
</head>
<body onload="load();">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" Height="30"
                        Width="100" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="更新" OnClick="Button3_Click" Height="30"
                        Width="100" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="指标" OnClick="Button2_Click" Height="30"
                        Width="100" />
                </td>
                <td>
                    <div id="downloadify">
                        必须安装 Flash 10。
                    </div>
                </td>
            </tr>
        </table>
        <%--<input type="button" onclick="tableToExcel('testTable', '门店业绩日报表')" value="导出Excel">--%>
        <table id="testTable" style="text-align: center; width: 95%">
            <tr>
                <td colspan="7">
                    <asp:Label ID="Label3" runat="server" Text="门店业绩日报表" Font-Bold="True" Font-Size="16pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="background-color: #999999">
                    <asp:Label ID="Label4" runat="server" Text="业绩总体情况" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    日期
                </td>
                <td>
                    <asp:TextBox ID="txtDate" onfocus="HS_setDate(this)" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    星期
                </td>
                <td>
                    <asp:Label ID="lblWeek" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    天气
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtWeather" runat="server"></asp:TextBox>
                    <%--<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    本月业绩指标
                </td>
                <td>
                    <asp:Label ID="lblMonthAmount" runat="server" Text="......"></asp:Label>
                </td>
                <td colspan="2">
                    去年同月业绩
                </td>
                <td colspan="2">
                    <asp:Label ID="lblLastYearMonthAmount" runat="server" Text="......"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    门店总业绩
                </td>
                <td>
                    <asp:Label ID="lblAmount" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    对比前一天
                </td>
                <td>
                    <asp:Label ID="lblDifLastDateAmount" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    对比去年同一天
                </td>
                <td>
                    <asp:Label ID="lblLastYearDateAmount" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDifLastYearDateAmount" runat="server" Text="......"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    累计业绩
                </td>
                <td>
                    <asp:Label ID="lblSum" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    累计达成率
                </td>
                <td>
                    <asp:Label ID="lblRatio" runat="server" Text="......"></asp:Label>
                </td>
                <td>
                    预算全月达成率
                </td>
                <td colspan="2">
                    <asp:Label ID="lblMonthRatio" runat="server" Text="......"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    来客数
                </td>
                <td colspan="2">
                    <asp:Label ID="lblQuantity" runat="server" Text="......"></asp:Label>
                </td>
                <td colspan="2">
                    客单价
                </td>
                <td colspan="2">
                    <asp:Label ID="lblPrice" runat="server" Text="......"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="background-color: #999999">
                    新品及重点产品业绩
                </td>
            </tr>
            <%
                if (lKPIOfGoods != null && lKPIOfGoods.Count > 0)
                {
                    foreach (AMSApp.BusiQuery.KPIOfGoods goods in lKPIOfGoods)
                    {%>
            <tr>
                <td>
                    <%=goods.Desc %>
                </td>
                <td>
                    <%=goods.vcGoodsName %>
                </td>
                <td>
                    上市时间
                </td>
                <td>
                    <%=goods.NewDate %>
                </td>
                <td>
                    销售业绩
                </td>
                <td>
                    <%=goods.Quantity %><%=goods.Unit %>,<%=goods.Amount %>元
                </td>
            </tr>
            <%}
                }
            %>
            <tr>
                <td colspan="7" style="background-color: #999999">
                    各门店业绩完成情况
                </td>
            </tr>
            <tr>
                <td>
                    序号
                </td>
                <td>
                    门店
                </td>
                <td>
                    本月指标
                </td>
                <td>
                    当天业绩
                </td>
                <td>
                    累计业绩
                </td>
                <td>
                    达成
                </td>
                <td>
                    预计达成
                </td>
            </tr>
            <%
                if (lKPIOfDept != null && lKPIOfDept.Count > 0)
                {
                    foreach (AMSApp.BusiQuery.KPIOfDept dept in lKPIOfDept)
                    {%>
            <tr>
                <td>
                    <%=dept.Serial %>
                </td>
                <td>
                    <%=dept.vcDeptName%>
                </td>
                <td>
                    <%=dept.Kpi %>
                </td>
                <td>
                    <%=dept.Amount %>
                </td>
                <td>
                    <%=dept.Sum %>
                </td>
                <td>
                    <%=dept.Ratio %>
                </td>
                <td>
                    <%=dept.MonthRatio %>
                </td>
            </tr>
            <%}
                }
            %>
            <tr>
                <td colspan="7" style="background-color: #999999">
                    门店重大异常提报
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:TextBox ID="txtException" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
