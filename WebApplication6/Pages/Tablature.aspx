<%@ Page Title="Tablature" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablature.aspx.cs" Inherits="WebApplication6.TablaturePage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/tablature.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="main">
            <h1 id="title"><%:tab.ArtistName %> - <%: tab.SongName %></h1>
            <h2 id="subtitle">Created by <%:creator.Username %> on <%:tab.CreatedAt.ToString("dd/MM/yyyy") %></h2>
            <a class="inf"><strong>Album: </strong><%: tab.AlbumName %> (<%: tab.ReleaseYear %>)</a><br />
            <a class="inf"><strong>Tuning: </strong><%: tab.TuningType %></a><br />
            <a class="inf"><strong>Difficulty: </strong><%: tab.Difficulty %></a><br />
            <a class="inf"><strong>Capo: </strong><%: tab.Capo > 0 ? tab.Capo.ToString() : "None" %></a><br />
            <div class="stars">
                <div class="stars-outer">
                    &#9733;&#9733;&#9733;&#9733;&#9733;
                    <div class="stars-inner" style="width:<%: (tab.Score / 5.0) * 100 %>%;">
                        &#9733;&#9733;&#9733;&#9733;&#9733;
                    </div>
                </div>
            </div>
            <div id="content"><%= tab.Content.Replace(Environment.NewLine, "<br />") %></div>
        </div>
        <div class="comments">
            <% foreach(var comment in comments.Reverse()) { %>
            <% var user = master.DataManager.GetUser(comment.SenderUUID); %>
                <div class="comment">
                    <div class="top"><a class="author"><%: user.Username %></a> <a class="date"><%: tab.CreatedAt %></a></div>
                    <br />
                    <a class="message"><%= comment.Content.Replace(Environment.NewLine, "<br/>") %></a>
                </div>
            <% } %>
        </div>
    </main>
</asp:Content>
