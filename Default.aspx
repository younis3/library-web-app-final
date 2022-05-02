<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LibraryApp4.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" href="DefaultStyle.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="intro">
            <h2>Welcome To The Library App
            </h2>
            <h3>Student Name: Ahmad Younis
                <br />
                Student ID: 204443162 
                <br />
                Date Submitted: July 25, 2021 
                <br />
                C# ex4
            </h3>
        </div>
        <div>
            <asp:Button ID="GoToAddNewBook" runat="server" OnClick="GoToAddNewBook_Click" Text="Add New Book" />
            <asp:Button ID="GoToAddNewSub" runat="server" OnClick="GoToAddNewSub_Click" Text="Add New Subscriber" />
            <asp:Button ID="GoToLoanBook" runat="server" OnClick="GoToLoanBook_Click" Text="Loan Book" />
            <asp:Button ID="GoToReturnBook" runat="server" OnClick="GoToReturnBook_Click" Text="Return Book" />
            <asp:Button ID="GoToShowBookDtls" runat="server" OnClick="GoToShowBookDtls_Click" Text="Print Book Info" />
            <asp:Button ID="GoToPrintGenreBooks" runat="server" OnClick="GoToPrintGenreBooks_Click" Text="Print Books By Genre" />
            <asp:Button ID="GoToShowSubBookList" runat="server" OnClick="GoToShowSubBookList_Click" Text="Print Subscriber Books List" />
        </div>
    </form>
</body>
</html>
