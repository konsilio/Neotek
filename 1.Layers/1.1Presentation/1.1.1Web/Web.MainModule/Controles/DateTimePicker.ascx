<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimePicker.ascx.cs" Inherits="UserControls.DateTimePicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<header runat ="server">
    <title></title>
    <style type="text/css">.MyCalendar .ajax__calendar_header{background-color: Silver;}.MyCalendar .ajax__calendar_container{background-color: Gray;}</style>
</header>
<table cellpadding="0" cellspacing="0" ><tr><td style="vertical-align: middle"> 
    <div class="input-group">    
    <asp:Image ID="imgDatePicker" runat="server" ImageUrl="~/fonts/cal.png" Style="CURSOR: auto; width:30px; height:30px; image-rendering:inherit;" />
        <asp:TextBox ID="tbDatePicker" runat="server" Style="TEXT-ALIGN: center;" CssClass="form-control"></asp:TextBox></div><asp:RequiredFieldValidator
        ID="rfvDatePicker" runat="server" ErrorMessage="Fill the Date" ControlToValidate="tbDatePicker"
        Enabled="False" SetFocusOnError="True">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbDatePicker"
        ErrorMessage="Invalid Format Date" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator>
<cc1:CalendarExtender ID="ceDatePicker" runat="server" CssClass="MyCalendar" Format="dd/MM/yyyy"
    PopupButtonID="imgDatePicker" TargetControlID="tbDatePicker" >
</cc1:CalendarExtender>
</td></tr></table>

