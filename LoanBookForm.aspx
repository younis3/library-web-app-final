<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanBookForm.aspx.cs" Inherits="LibraryApp4.LoanBookForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Loan Book</title>
    <link rel="stylesheet" href="LoanBookFormStyle.css">
</head>
<body>
    <form id="form3" runat="server">
        <div class="form-page">
            <section>
                <h1>
                    Loan Book
                </h1>
            </section>
            <section>
                <div class="content">                    
                    <asp:Label ID="labelOption" class="lblStyle" runat="server" Text="Find Book By:"></asp:Label>
                    <asp:RadioButton ID="radioOptionID" Text="Key" GroupName="bookOpt" Value="1" runat="server" />
                    <asp:RadioButton ID="radioOptionTitle" Text="Title" GroupName="bookOpt" Value="2" runat="server" />
                    <asp:TextBox ID="textinpt" PlaceHolder="Type Book Key/Name" runat="server"></asp:TextBox>

                    <asp:TextBox ID="textfindkey" PlaceHolder="Type Book Key" runat="server"></asp:TextBox>
                    <asp:Label ID="labelfindkeylist" class="lblMsg2" runat="server" Text="Search Results"></asp:Label>

                    <asp:Label ID="labelSubID" class="lblStyle" runat="server" Text="Subscriber ID"></asp:Label>
                    <asp:TextBox ID="txtSubID" runat="server"></asp:TextBox>


                    <asp:Button ID="buttonLoanBook" runat="server" OnClick="buttonLoanBook_Click" Text="Loan Book" />
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
