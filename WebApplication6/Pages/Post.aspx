<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="WebApplication6.PostPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/tablature.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="main">
            <h1 id="title"><%:post.Title %></h1>
            <h2 id="subtitle">Posted by <%:creator.Username %> on <%:post.CreatedAt.ToString("dd/MM/yyyy") %></h2>
            <div class="content"><%= post.Content.Replace(Environment.NewLine, "<br />") %></div>
        </div>
        <div class="comments">
            <% foreach (var comment in comments.OrderBy(c => c.CreatedAt)) { %>
            <% var user = master.DataManager.GetUser(comment.SenderUUID); %>
                <div class="comment">
                    <div class="top"><a class="author" href="User?n=<%:user.Username %>"><%: user.Username %></a> <a class="date"><%: comment.CreatedAt %></a></div>
                    <br />
                    <a class="message"><%= comment.Content.Replace(Environment.NewLine, "<br/>") %></a>
                </div>
            <% } %>
            <% if (user == null)
                { %>

                <span class="logintext"><a id="loginhref" href="Login?p=<%:post.UUID %>">Login</a> to leave a comment</span>

            <%}
                else
                {
%>
                <div class="comment">
                    <a class="commenttext">Leave a comment: </a>
                    <asp:TextBox ID="commentInput" placeholder="This is pretty cool" runat="server" TextMode="MultiLine" CssClass="input"/>
                    <asp:Button ID="commentSubmit" runat="server" Text="Submit" CssClass="submit" OnClick="CommentSubmit"/>
                </div>
            <% } %>
        </div>
    </main>
</asp:Content>
