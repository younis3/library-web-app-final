<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintBookByGenreForm.aspx.cs" Inherits="LibraryApp4.PrintBookByGenreForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Book By Genre</title>
    <link rel="stylesheet" href="PrintBookByGenreFormStyle.css">
</head>
<body>
    <form id="form6" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Show Book By Genre
                </h1>
            </section>
            <section>
                <div class="content">                    
                    <asp:Label ID="labelBookGenre" class="lblStyle" runat="server" Text="Genre"></asp:Label>
                    <asp:TextBox ID="textBookGenre" runat="server"></asp:TextBox>


                    <asp:Button ID="buttonPrintByGenre" runat="server" OnClick="buttonPrintByGenre_Click" Text="Show Genre Books" />
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