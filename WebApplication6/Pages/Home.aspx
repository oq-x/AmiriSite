<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication6.Home" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <%
            WebApplication6.User user = ((WebApplication6.SiteMaster)Master).CurrentUser();
        %>


            <h1 id="title">Welcome  to AmiriGuitarSite!</h1>
            <h2 id="subtitle">Your favorite website</h2>

            <p class="subtitle">
                <%: user != null ? $"Welcome back, {user.Username}" : "Get started" %>
            </p>

            <div class="boxcontainer">
                <% if (user == null)
                    { %>

                    <div class="box">
                        <h1 class="title">Login</h1>

                        <p class="description">So you can upload your own content to the site</p>

                        <a class="button" href="Login">Go</a>
                    </div>

                <% } %>
                <div class="box">
                    <h1 class="title">Tablatures</h1>

                    <p class="description">Browse the tablatures made by the community</p>

                    <a class="button" href="Tablatures">Go</a>
                </div>
                <div class="box">
                    <h1 class="title">Chord trainer</h1>

                    <p class="description">Practice your chords by memorizing the notes</p>

                    <a class="button" href="ChordTrainer">Go</a>
                </div>
                <div class="box">
                    <h1 class="title">Forum</h1>

                    <p class="description">Discuss with other guitar players</p>

                    <a class="button" href="Forum">Go</a>
                </div>
            </div>
    </main>
</asp:Content>
