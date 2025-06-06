<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication6.Login" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/login.css" rel="stylesheet" />
    <script src="/Scripts/login.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title">Login</h1>

        <div class="form">
        <div runat="server">
            <label for="email">Email</label>
            <input type="email" id="email" name="email" placeholder="name@example.com" class="input" oninput="setError('')"/>

            <label for="password">Password</label>
            <input type="password" id="password" name="password" class="input" placeholder="amirithekabanos" oninput="setError('')"/>

            <p id="error" style="color: red;"><%:Session["error"] %></p>
            <% Session["error"] = null; %>
            <p>Forgot your password? <a href="ResetPassword">Reset</a></p>
            <p>Don't have an account? <a href="Register">Register</a></p>

            <input type="submit" class="submit" onclick="login(event)" name="submit" value="Login">
        </div>
        </div>
    </main>
</asp:Content>
