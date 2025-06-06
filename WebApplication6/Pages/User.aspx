<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebApplication6.UserPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/user.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="userbox">
            <div class="top">
                <h1 class="title"><%: user.Username %></h1>
                <div class="topbuttons">
                    <% if (canEdit) { %>
                        <a id="edituserbutton" class="button" onclick="document.getElementById('edituserbox').style['display'] = 'block'; document.getElementById('edituserbutton').style['display'] = 'none'">Edit</a>
                    <%} %>
                    <% if (isMe) { %>
                        <asp:Button runat="server" Text="Log Out" CssClass="logout" OnClick="LogOut" />
                    <%} %>
                </div>
            </div>
            <div class="subtext">
                <span class="bio"><%: user.Bio().Length > 0 ? user.Bio() : "No bio yet" %></span>
                <div class="info">
                    <a class="infttl">Email</a>: <%: user.Email %><br />
                    <a class="infttl">Score</a>: <%: user.Score().ToString("F2") %><br />
                    <a class="infttl">Joined: </a><%: user.CreatedAt.ToString("dd/MM/yyyy") %><br />
                    <%
                        string parsedDate = "";
                        try
                        {
                            parsedDate = DateTime.ParseExact(user.Birthday, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                        } catch
                        {
                            parsedDate = "idk";
                        }
                    %>
                    <a class="infttl">Birthday: </a><%:parsedDate %><br />
                    <% 
                        string gender = user.Gender;
                        bool imgG = false;
                        if (gender == "klein")
                        {
                            gender = "<img width=\"100\" src=\"../Assets/klein.png\"/>";
                            imgG = true;
                        }
                    %>
                    <% if (imgG) { %>
                    <div class="infimg">
                    <% } %>
                        <a class="infttl">Gender: </a><%=gender %>
                    <% if (imgG) { %>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>s
        <% if (canEdit) { %>
        <div class="userbox invisible" id="edituserbox">
            <h1 class="title">Edit User</h1>
            <p class="subtext">
                <a class="infttl">Email</a>:<asp:TextBox CssClass="editUserField" runat="server" ID="emailInput" TextMode="Email" /><br />
                <a class="infttl">Bio</a>:<br />
                <asp:TextBox CssClass="editUserLong" runat="server" ID="bioInput" TextMode="MultiLine" /><br />
            </p>
            <a runat="server" id="error" class="error">Butarga</a>
            <asp:Button CssClass="editUserSubmit" runat="server" Text="Submit" OnClick="SubmitUserEdit"/>
        </div>
        <%} %>
        <%
            if (tablatures.Length > 0)
            {
        %>
        <h1 class="boxtitle">Tablatures made by <%: user.Username %></h1>
        <%} %>
        <div class="boxcontainer">
            <%for(int i = 0; i < tablatures.Length; i++) {%>
                <% var tab = tablatures[i]; %>
                    <div class="box">
                        <a href="Tablature?t=<%:tab.UUID %>" class="title"><%: tab.ArtistName %> - <%:tab.SongName %></a>

                        <p class="description"><%= $"Released in {tab.ReleaseYear} on {tab.AlbumName},<br/> This song is in {tab.TuningType} tuning and it is rated {tab.Difficulty.ToLower()} difficulty" %></p>
                        <p class="inf"><a class="infttl">Created on </a><%:tab.CreatedAt.ToString("dd/MM/yyyy") %></p>
                    </div>
            <%} %>
        </div>
        <%
            if (posts.Length > 0)
            {
        %>
        <h1 class="boxtitle">Posts by <%: user.Username %></h1>
        <%} %>
        <div class="boxcontainer">
            <%for(int i = 0; i < posts.Length; i++) {%>
                <% var post = posts[i]; %>
                    <div class="box">
                        <a href="Post?p=<%:post.UUID %>" class="title">
                            <%: post.Title.Length > 35 ? post.Title.Substring(0, 35) + "..." : post.Title %>
                        </a>
                        <p class="inf"><a class="infttl">Posted on </a><%:post.CreatedAt.ToString("dd/MM/yyyy") %></p>
                    </div>
            <%} %>
        </div>
    </main>
</asp:Content>
