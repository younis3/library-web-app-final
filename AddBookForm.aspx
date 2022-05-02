<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBookForm.aspx.cs" Inherits="LibraryApp4.AddBookForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Book</title>
    <link rel="stylesheet" href="AddBookFormStyle.css">
</head>
<body>
    <form id="form2" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Add New Book
                </h1>
            </section>
            <section>
                <div class="content">
                    <asp:Label ID="labelBookID" class="lblStyle" runat="server" Text="Book ID"></asp:Label>
                    <asp:TextBox ID="txtBookID" runat="server"></asp:TextBox>

                    <asp:Label ID="labelTitle" class="lblStyle" runat="server" Text="Book Title"></asp:Label>
                    <asp:TextBox ID="textTitle" runat="server"></asp:TextBox>



                    <asp:Label ID="labelFirstName" class="lblStyle" runat="server" Text="Author First Name"></asp:Label>
                    <asp:TextBox ID="textFirstName" runat="server"></asp:TextBox>

                    <asp:Label ID="labelLastName" class="lblStyle" runat="server" Text="Author Last Name"></asp:Label>
                    <asp:TextBox ID="textLastName" runat="server"></asp:TextBox>

                    <asp:Label ID="labelGenre" class="lblStyle" runat="server" Text="Genre"></asp:Label>
                    <asp:TextBox ID="textGenre" runat="server"></asp:TextBox>

                    
                    <asp:Label ID="labelType" class="lblStyle" runat="server" Text="Book Type"></asp:Label>
                    <asp:RadioButton ID="radioTypePaper" Text="Paper" GroupName="bookType" Value="1" runat="server" />
                    <asp:RadioButton ID="radioTypeDigital" Text="Digital" GroupName="bookType" Value="2" runat="server" />

                    <asp:Button ID="buttonAddBook" runat="server" OnClick="buttonAddBook_Click" Text="Add Book" />
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
