<%@ Page Title="Community Tablatures" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablatures.aspx.cs" Inherits="WebApplication6.TablaturesPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/tablatures.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title" runat="server">
        <h1>Community Tablatures</h1>
        <div class="query">
            <a id="query">Query: </a>
            <asp:TextBox runat="server" ID="input" OnTextChanged="Input" CssClass="input"/>
            <asp:Button runat="server" CssClass="search" OnClick="Input" Text="Search"/>
        </div>
        <div class="boxcontainer">
            <%for(int i = 0; i < tablatures.Length; i++) {%>
                <% var tab = tablatures[i]; %>
                    <div class="box">
                        <a href="Tablature?t=<%: tab.UUID %>" class="title"><%: tab.ArtistName %> - <%:tab.SongName %></a>
                        <div class="inner">
                            <div class="description">
                                <a class="infttl">Tuning: </a><%:tab.TuningType %><br />
                                <a class="infttl">Difficulty: </a><%:tab.Difficulty %><br />
                                <a class="infttl">Capo: </a><%:tab.Capo > 0 ? tab.Capo.ToString() : "None" %><br />
                            </div>
                            <div class="stars">
                                <div class="stars-outer">
                                    &#9733;&#9733;&#9733;&#9733;&#9733;
                                    <div class="stars-inner" style='width:<%: tab.Score / 5.0 * 100 %>%;'>
                                        &#9733;&#9733;&#9733;&#9733;&#9733;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="inf">
                            <p class="infl"><a class="infttl">Created on </a><%:tab.CreatedAt.ToString("dd/MM/yyyy") %></p>
                            <a class="infr" href="User?n=<%:creators[i].Username %>"><%:creators[i].Username %></a>
                        </div>
                    </div>
            <%} %>
        </div>
    </main>
</asp:Content>
