<%@ Page Title="User List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="WebApplication6.UserList" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/userList.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="query centered" runat="server">
            <a class="queryText">Query: </a>
            <asp:TextBox runat="server" ID="input" CssClass="input"/>
            <asp:Button runat="server" CssClass="search" OnClick="Input" Text="Search"/>
        </div>
        <div class="query notcentered" runat="server">
            <a class="queryText">Order By Column: </a>
            <asp:TextBox runat="server" ID="column" CssClass="input"/>
            <asp:Button runat="server" CssClass="search" OnClick="InputColumn" Text="Search"/>

            <a class="queryText">Acsending: </a>
            <asp:RadioButton runat="server" AutoPostBack="true" ID="asc" CssClass="radio" GroupName="order" OnCheckedChanged="InputOrder"/>
            <a class="queryText">Descending: </a>
            <asp:RadioButton runat="server" AutoPostBack="true" ID="desc" CssClass="radio" GroupName="order" OnCheckedChanged="InputOrder"/>
        </div>
        <div id="table" runat="server">

        </div>
    </main>
</asp:Content>
