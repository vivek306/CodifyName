<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CodifyName.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
    
    </div>
        <p>
            <asp:TextBox ID="InputTextBox" runat="server" Height="131px" TextMode="MultiLine" Width="341px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="EncodeButton" runat="server" OnClick="EncodeButton_Click" Text="Encode" />
            <asp:Button ID="DecodeButton" runat="server" OnClick="DecodeButton_Click" Text="Decode" />
        </p>
        <p>
            <asp:Label ID="ResultLabel" runat="server" Font-Size="Large"></asp:Label>
        </p>
    </form>
</body>
</html>
