<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSubBookListForm.aspx.cs" Inherits="LibraryApp4.ShowSubBookListForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscriber Book List</title>
    <link rel="stylesheet" href="ShowSubBookListFormStyle.css">
</head>
<body>
    <form id="form7" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Show Subscriber Book List
                </h1>
            </section>
            <section>
                <div class="content">                    
                    <asp:Label ID="labelSubID" class="lblStyle" runat="server" Text="Subscriber ID"></asp:Label>
                    <asp:TextBox ID="textSubID" runat="server"></asp:TextBox>


                    <asp:Button ID="buttonSubBookList" runat="server" OnClick="buttonSubBookList_Click" Text="Show Subscriber Books" />
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