<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="BioGasSenseWeb.Homepage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> BioGas-Sense</title>
    <meta http-equiv="refresh" content="2" /> 
</head>
<body">
    <form id="form1" runat="server" >
    <div style="width: auto;height: auto; margin: 10% 10%;">
        <div style="margin-left:100px;margin-top:10px;">
            <asp:Label ID="Label13" runat="server" Font-Size="40pt" Text="BioGas-Sense" ForeColor="DarkBlue" Height="100px"></asp:Label>
        </div>
        <div style="margin-left:100px;margin-top:10px;">        
           <asp:Label  runat="server" Font-Size="30" Text="Sensor #1:  " Height="50px" ></asp:Label>
           <asp:Label ID="Label1" runat="server" Font-Size="30pt" Text="ON" Height="50px"  Font-Overline="False"/>
        </div>
       <div style="margin-left:100px;margin-top:10px;">         
           <asp:Label  runat="server" Text="Sensor #2:  " Font-Size="30" Height="50px"></asp:Label>
            <asp:Label ID="Label2" runat="server"  Text="ON" Font-Size="30" Height="50px" ></asp:Label>
        </div>
        <div style="margin-left:100px;margin-top:10px;">         
           <asp:Label  runat="server"  Text="Sensor #3:  " Font-Size="30" Height="50px" ></asp:Label>
            <asp:Label ID="Label3" runat="server"  Text="ON" Font-Size="30" Height="50px" ></asp:Label>
        </div>
        <div style="margin-left:100px;margin-top:10px;">       
           <asp:Label  runat="server"  Text="Sensor #4:  " Font-Size="30" Height="50px" ></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="ON" Font-Size="30" Height="50px" ></asp:Label>
        </div>
        <div style="margin-left:100px;margin-top:10px;">         
           <asp:Label runat="server" Text="Sensor #5:  " Font-Size="30" Height="50px" ></asp:Label>
            <asp:Label ID="Label5" runat="server"  Text="ON" Font-Size="30" Height="50px" ></asp:Label>
        </div>
        <div style="margin-left:100px;margin-top:50px;">
            <asp:Button ID="btnRefresh" Text="Refresh" Font-Size="30" Width="300" runat="server" OnClick="btnRefresh_Click"/>
        </div>
    <div style="margin-left:300px;margin-top:50px;">
        <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Developed by: Srujan Jha & Shubham Jain" Height="100px" ></asp:Label></div>
    </div>
    </form>
</body>
</html>