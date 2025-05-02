<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebApplication6.UserPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/user.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <%
            WebApplication6.User user = ((WebApplication6.SiteMaster)Master).DataManager.GetUserByUsername(Request.QueryString["n"]);
        %>

        <div class="userbox">
            <h1 id="title"><%: user.Username %></h1>
            <p class="subtext">
                <span class="bio">"<%: user.Bio() %>"</span>
                <span class="info">
                    <a class="infttl">Email</a>: <%: user.Email %><br />
                    <a class="infttl">Score</a>: <%: user.Score() %>
                </span>
            </p>
        </div>
    </main>
</asp:Content>
