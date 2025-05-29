<%@ Page Title="Statistics" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="WebApplication6.Statistics" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 id="title">Statistics</h1>

        <div class="boxcontainer">
            <div class="box">
                <h1 class="title">Visitors Online</h1>

                <p class="description"><%: numVisitorsOnline %></p>
            </div>

        <div class="box">
            <h1 class="title">Visitors Total</h1>

            <p class="description"><%: numVisitorsTotal %></p>
        </div>
        <div class="box">
            <h1 class="title">Users Total</h1>

            <p class="description"><%: numUsersTotal %></p>
        </div>
    </div>    
    </main>
</asp:Content>
