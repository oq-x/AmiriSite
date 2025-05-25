<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="WebApplication6.Forum" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/tablatures.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="top">
            <h1>Forum</h1>
            <% if (master.CurrentUser() != null) { %>
                <a class="newtab" href="NewPost">New</a>
            <%} %>
        </div>
        <div class="query" runat="server">
            <a id="query">Query: </a>
            <asp:TextBox runat="server" ID="input" OnTextChanged="Input" CssClass="input"/>
            <asp:Button runat="server" CssClass="search" OnClick="Input" Text="Search"/>
        </div>
        <div class="boxcontainer">
            <% 
                var sortedPosts = posts
                    .OrderByDescending(p => p.Pinned)
                    .ThenByDescending(p => p.CreatedAt)
                    .ToArray();
            %>
            <%for(int i = 0; i < sortedPosts.Length; i++) {%>
                <% var post = sortedPosts[i]; %>
                    <div class="box">
                        <a href="Post?p=<%: post.UUID %>" class="title"><%: post.Title %></a>
                        <% if (post.Pinned) { %>
                            <i class="fa-solid fa-thumbtack"></i>
                        <%} %>
                        <div class="inner">
                            <p class="description">
                                <%: post.Content.Length > 100 ? post.Content.Substring(0, 100) + "..." : post.Content %>
                            </p>
                        </div>
                        <div class="inf">
                            <p class="infl"><a class="infttl">Posted on </a><%:post.CreatedAt.ToString("dd/MM/yyyy") %></p>
                            <% var creator = master.DataManager.GetUser(post.PosterUUID); %>
                            <a class="infr" href="User?n=<%:creator.Username %>"><%:creator.Username %></a>
                        </div>
                    </div>
            <%} %>
        </div>
    </main>
</asp:Content>
