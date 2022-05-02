<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSubForm.aspx.cs" Inherits="LibraryApp4.AddSubForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Subscriber</title>
    <link rel="stylesheet" href="AddSubFormStyle.css">
    <%--<link rel="stylesheet" href="AddSubFormStyle.css?v=1">--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Add New Subscriber
                </h1>
            </section>
            <section>
                <div class="content">
                    <asp:Label ID="labelSubID" class="lblStyle" runat="server" Text="Subscriber ID"></asp:Label>
                    <asp:TextBox ID="txtSubID" runat="server"></asp:TextBox>

                    <asp:Label ID="labelSubFirstName" class="lblStyle" runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox ID="textSubFirstName" runat="server"></asp:TextBox>

                    <asp:Label ID="labelSubLastName" class="lblStyle" runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox ID="textSubLastName" runat="server"></asp:TextBox>

                    <asp:Button ID="buttonAddSub" runat="server" OnClick="buttonAddSub_Click" Text="Add Subscriber" />
                    <asp:Button ID="buttonBacktoMM" runat="server" OnClick="buttonBacktoMM_Click" Text="Back to Main Menu" />
                </div>
            </section>
            <section>
                <div class="msg">
                    <asp:Label ID="lblMsg" class="lblMsg" runat="server" Text=""></asp:Label>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
