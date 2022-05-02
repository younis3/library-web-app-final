<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowBookDtlsForm.aspx.cs" Inherits="LibraryApp4.ShowBookDtlsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Book Information</title>
    <link rel="stylesheet" href="ShowBookDtlsFormStyle.css">
</head>
<body>
    <form id="form5" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Show Book Information
                </h1>
            </section>
            <section>
                <div class="content">                    
                    <asp:Label ID="labelBookTitle" class="lblStyle" runat="server" Text="Book Title"></asp:Label>
                    <asp:TextBox ID="textBookTitle" runat="server"></asp:TextBox>

                    <asp:Label ID="labelBookAuthFirstName" class="lblStyle" runat="server" Text="Author First Name"></asp:Label>
                    <asp:TextBox ID="textBookAuthFirstName" runat="server"></asp:TextBox>

                    <asp:Label ID="labelBookAuthLastName" class="lblStyle" runat="server" Text="Author Last Name"></asp:Label>
                    <asp:TextBox ID="textBookAuthLastName" runat="server"></asp:TextBox>


                    <asp:Button ID="buttonShowBookDtls" runat="server" OnClick="buttonShowBookDtls_Click" Text="Print Book Info" />
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