<%@ Page Title="Publish New Post" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewPost.aspx.cs" Inherits="WebApplication6.NewPostPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/newTablature.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="top">
            <h1>Publish New Post</h1>
            <asp:Button runat="server" CssClass="publish" Text="Publish" OnClick="Publish" />
        </div>
        <a runat="server" id="errorMessage" class="errorMessage">Just put the gun to my head</a>
        <div class="info">
            <h2>Title</h2>
            <asp:TextBox runat="server" CssClass="short" ID="title" placeholder="Hi" />
        </div>
        <div class="content">
            <h2>Content</h2>
            <asp:TextBox runat="server" CssClass="long" ID="content" TextMode="MultiLine" placeholder="What oging on"/> 
        </div>
    </main>
</asp:Content>