<%@ Page Language="c#" CodeBehind="wfmEmpMenu.aspx.cs" AutoEventWireup="True" Inherits="AMSApp.wfmEmpMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>wfmEmpMenu</title>
    <meta name="vs_snapToGrid" content="False">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body ms_positioning="GridLayout" leftmargin="0" background="image/coolwp2.jpg">
    <table id="tblEmpMenu" cellspacing="1" cellpadding="1" width="146" border="0" align="left"
        runat="server">
        <tr id="trnoprom" runat="server">
            <td align="center" style="font-weight: bold; color: #330033; height: 38px" bgcolor="#ebf0ec">
                没有权限
            </td>
        </tr>
        <tr id="trEmpInfo" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmEmpInfo.aspx'"
                    href="javascript:">员工信息维护</a>
            </td>
        </tr>
        <tr id="trWorkDailyEvery" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmWorkDailyEvery.aspx'"
                    href="javascript:">排班操作</a>
            </td>
        </tr>
        <tr id="trShcQuery" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmShcQuery.aspx'"
                    href="javascript:">查询排班表</a>
            </td>
        </tr>
        <tr id="trSignCalc" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmSignCalc.aspx'"
                    href="javascript:">考勤计算</a>
            </td>
        </tr>
        <tr id="trEmpUnitSign" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmEmpUnitSign.aspx'"
                    href="javascript:">个人考勤查询</a>
            </td>
        </tr>
        <tr id="trSignQuery" runat="server">
            <td align="center" style="height: 38px" background="image/anniu.jpg">
                <a style="font-size: 12pt; color: #ffffff; text-decoration: none" onclick="parent.right.location='Employ/wfmSignQuery.aspx'"
                    href="javascript:">门店考勤统计</a>
            </td>
        </tr>
    </table>
</body>
</html>
