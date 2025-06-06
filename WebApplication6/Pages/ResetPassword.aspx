<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WebApplication6.ResetPassword" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/login.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title">Reset Password</h1>

        <div class="form">
        <div runat="server">
            <label for="email">Email</label>
            <asp:TextBox TextMode="Email" ID="email" placeholder="name@example.com" CssClass="input" runat="server"/>

            <label for="securityQuestion" id="secql" style="display: none;" runat="server">Security Question</label>
            <p id="securityQuestion" style="text-align: left;" runat="server"></p>

            <asp:TextBox ID="securityAnswer" CssClass="input" runat="server" style="display: none;"/>

            <label for="newPassword" id="npassl" style="display: none;" runat="server">New Password</label>
            <asp:TextBox ID="newPassword" CssClass="input" runat="server" style="display: none;"/>

            <p id="error" style="color: red;" runat="server"></p>
            <asp:Button Text="Reset Password" ID="resetPassword" CssClass="submit" runat="server" OnClick="TryResetPassword" style="display: none;" />
            <asp:Button Text="Confirm" ID="submit" CssClass="submit" runat="server" OnClick="LookupUser" />
        </div>
        </div>
    </main>
</asp:Content>
