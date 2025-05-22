<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication6.Register" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/login.css" rel="stylesheet" />
    <link href="/Styles/register.css" rel="stylesheet" />
    <script src="/Scripts/register.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title">Register</h1>

        <div class="form">
        <div runat="server">
            <div class="box">
                <label for="email">Email</label>
                <input type="email" id="email" name="email" placeholder="name@example.com" class="input" oninput="setError('')"/>

                <label for="username">Username</label>
                <input type="text" id="username" name="username" placeholder="amiri" class="input" oninput="setError('')"/>
            </div>

            <div class="box name">
                <label for="fname">Name</label>
                <input type="text" id="fname" name="fname" placeholder="First Name" class="input" oninput="setError('')"/>

                <input type="text" id="lname" name="lname" placeholder="Last Name" class="input" oninput="setError('')"/>
            </div>

            <div class="box">
                <label for="password">Password</label>
                <input type="password" id="password" name="password" class="input" placeholder="amirithekabanos" oninput="setError('')"/>
            </div>
            <p id="error" style="color: red;"><%:Session["error"] %></p>
            <% Session["error"] = null; %>
            <p>Already have an account? <a href="Login">Login</a></p>

            <input type="submit" id="submit" onclick="register(event)" name="submit" value="Register">
        </div>
        </div>
    </main>
</asp:Content>
