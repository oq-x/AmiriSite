<%@ Page Title="User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebApplication6.UserPage" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/user.css" rel="stylesheet" />
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
        <h1 class="channeltitle">Channels owned by <%: user.Username %></h1>
        <div class="channelbox">
            <%
                WebApplication6.Channel[] channels = ((WebApplication6.SiteMaster)Master).DataManager.GetChannels(user);
            %>
            <%for(int i = 0; i < channels.Length; i++) {%>
                <% var channel = channels[i]; %>
                <div class="channel">
                    <a class="channelname" href="Channel?c=<%:channel.UUID%>">
                        <%:channel.Name %> <i class="fa-solid fa-arrow-up-right-from-square"></i>
                    </a>
                    <br />
                    <span class="info">
                        <a class="infttl">Message Count</a>: <%: ((WebApplication6.SiteMaster)Master).DataManager.MessageCount(channel) %><br />
                    </span>
                </div>
               
            <%} %>
        </div>
    </main>
</asp:Content>
