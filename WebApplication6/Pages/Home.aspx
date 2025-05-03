<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication6.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <%
            WebApplication6.User user = ((WebApplication6.SiteMaster)Master).CurrentUser();
        %>
        <h1>Welcome, <%: user?.Username ?? "Random" %></h1>
    </main>
</asp:Content>
