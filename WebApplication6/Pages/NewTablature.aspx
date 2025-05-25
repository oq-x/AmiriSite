<%@ Page Title="Publish New Tablature" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewTablature.aspx.cs" Inherits="WebApplication6.NewTablaturePage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/newTablature.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <div class="top">
            <h1>Publish New Tablature</h1>
            <asp:Button runat="server" CssClass="publish" Text="Publish" OnClick="Publish" />
        </div>
        <a runat="server" id="errorMessage" class="errorMessage">Just put the gun to my head</a>
        <div class="form">
            <div class="info">
                <h2>Song Name</h2>
                <asp:TextBox runat="server" CssClass="short" ID="songName" placeholder="get those squiggly fuckers out of my brain" />
                <h2>Artist Name</h2>
                <asp:TextBox runat="server" CssClass="short" ID="artistName" placeholder="royziga" />
                <h2>Album Name</h2>
                <asp:TextBox runat="server" CssClass="short" ID="albumName" placeholder="royziga II" />
                <h2>Tuning Type</h2>
                <asp:TextBox runat="server" CssClass="short" ID="tuningType" placeholder="Standard" />
                <h2>Capo</h2>
                <asp:TextBox runat="server" CssClass="short" ID="capo" placeholder="0" TextMode="Number" />
                <h2>Release Year</h2>
                <asp:TextBox runat="server" CssClass="short" ID="releaseYear" placeholder="1972" TextMode="Number" />
                <h2>Difficulty</h2>
                <select class="short" name="difficulty" id="difficulty" runat="server">
                    <option value="Super Easy">Super Easy</option>
                    <option value="Easy">Easy</option>
                    <option value="Medium">Medium</option>
                    <option value="Hard">Hard</option>
                    <option value="Super Hard">Super Hard</option>
                </select>
            </div>
            <div class="content">
                <h2>Tablature</h2>
                <asp:TextBox runat="server" CssClass="long" ID="content" TextMode="MultiLine" Text="e|---------------------------------------------------------------|
B|---------------------------------------------------------------|
G|---------------------------------------------------------------|
D|---------------------------------------------------------------|
A|---------------------------------------------------------------|
E|---------------------------------------------------------------|"/> 
            </div>
        </div>
    </main>
</asp:Content>