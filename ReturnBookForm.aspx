<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnBookForm.aspx.cs" Inherits="LibraryApp4.ReturnBookForm" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Book</title>
    <link rel="stylesheet" href="ReturnBookFormStyle.css">
</head>
<body>
    <form id="form4" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Return Book
                </h1>
            </section>
            <section>
                <div class="content">                    
                    <asp:Label ID="labelBookID" class="lblStyle" runat="server" Text="Book Key"></asp:Label>
                    <asp:TextBox ID="textBookID" runat="server"></asp:TextBox>


                    <asp:Label ID="labelSubID" class="lblStyle" runat="server" Text="Subscriber ID"></asp:Label>
                    <asp:TextBox ID="textSubID" runat="server"></asp:TextBox>


                    <asp:Button ID="buttonReturnBook" runat="server" OnClick="buttonReturnBook_Click" Text="Return Book" />
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
