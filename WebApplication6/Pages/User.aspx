<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebApplication6.UserPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/user.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <%
            WebApplication6.User user = ((WebApplication6.SiteMaster)Master).DataManager.GetUserByUsername(Request.QueryString["n"]);
        %>

        <div class="userbox">
            <h1 class="title"><%: user.Username %></h1>
            <p class="subtext">
                <span class="bio">"<%: user.Bio() %>"</span>
                <span class="info">
                    <a class="infttl">Email</a>: <%: user.Email %><br />
                    <a class="infttl">Score</a>: <%: user.Score() %>
                </span>
            </p>
        </div>
        <h1 class="boxtitle">Tablatures made by <%: user.Username %></h1>
        <div class="boxcontainer">
            <%
                WebApplication6.Tablature[] tablatures = ((WebApplication6.SiteMaster)Master).DataManager.GetTablatures(user);
            %>
            <%for(int i = 0; i < tablatures.Length; i++) {%>
                <% var tab = tablatures[i]; %>
                    <div class="box">
                        <h1 class="title"><%: tab.ArtistName %> - <%:tab.SongName %></h1>

                        <p class="description"><%: $"Released in {tab.ReleaseYear} on {tab.AlbumName},"%><br /><%: $"This song is in {tab.TuningType} tuning and it is rated {tab.Difficulty.ToLower()} difficulty" %></p>

                        <a class="button" href="Login">Check it out</a>
                    </div>
            <%} %>
        </div>
    </main>
</asp:Content>
