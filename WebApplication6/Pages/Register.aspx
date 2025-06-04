<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication6.Register" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/login.css" rel="stylesheet" />
    <link href="/Styles/register.css" rel="stylesheet" />
    <script>
        window.onload = function () {
            window.hiddenGender = document.getElementById('<%= gender.ClientID %>')
        };
    </script>
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

            <div class="box">
                <label for="phone">Phone</label>
                <input type="tel" id="phone" name="phone" class="input" placeholder="050-1234567" pattern="0[0-9]{2}-[0-9]{7}" title="Israeli phone number" oninput="setError('')"/>
            </div>
            
            <div class="box">
                <label for="birthday">Birthday</label>
                <input type="date" id="birthday" name="birthday" class="input" oninput="setError('')"/>
            </div>

            <div class="box name">
                <label class="glabel">Gender</label>
                <div class="gender-options">
                    <label><input type="radio" name="gender" id="male" onchange="setError('')"> Male</label>
                    <label><input type="radio" name="gender" id="female" onchange="setError('')"> Female</label>
                    <label><input type="radio" name="gender" id="agender" onchange="setError('')"> Agender</label>
                    <label><input type="radio" name="gender" id="bigender" onchange="setError('')"> Bigender</label>
                    <label><input type="radio" name="gender" id="cisgender" onchange="setError('')"> Cisgender</label>
                    <label><input type="radio" name="gender" id="transgender" onchange="setError('')"> Transgender</label>
                    <label><input type="radio" name="gender" id="genderfluid" onchange="setError('')"> Genderfluid</label>
                    <label><input type="radio" name="gender" id="genderqueer" onchange="setError('')"> Genderqueer</label>
                    <label><input type="radio" name="gender" id="nonbinary" onchange="setError('')"> Non-binary</label>
                    <label><input type="radio" name="gender" id="demiboy" onchange="setError('')"> Demiboy</label>
                    <label><input type="radio" name="gender" id="demigirl" onchange="setError('')"> Demigirl</label>
                    <label><input type="radio" name="gender" id="twospirit" onchange="setError('')"> Two-Spirit</label>
                    <label><input type="radio" name="gender" id="androgynous" onchange="setError('')"> Androgynous</label>
                    <label><input type="radio" name="gender" id="intergender" onchange="setError('')"> Intergender</label>
                    <label><input type="radio" name="gender" id="neutrois" onchange="setError('')"> Neutrois</label>
                    <label><input type="radio" name="gender" id="maverique" onchange="setError('')"> Maverique</label>
                    <label><input type="radio" name="gender" id="thirdgender" onchange="setError('')"> Third Gender</label>
                    <label><input type="radio" name="gender" id="aliagender" onchange="setError('')"> Aliagender</label>
                    <label><input type="radio" name="gender" id="polygender" onchange="setError('')"> Polygender</label>
                    <label><input type="radio" name="gender" id="greygender" onchange="setError('')"> Greygender</label>
                    <label><input type="radio" name="gender" id="pangender" onchange="setError('')"> Pangender</label>
                    <label><input type="radio" name="gender" id="autigender" onchange="setError('')"> Autigender</label>
                    <label><input type="radio" name="gender" id="femfluid" onchange="setError('')"> Femfluid</label>
                    <label><input type="radio" name="gender" id="mascfluid" onchange="setError('')"> Mascfluid</label>
                    <label><input type="radio" name="gender" id="androgyne" onchange="setError('')"> Androgyne</label>
                    <label><input type="radio" name="gender" id="iphone-se" onchange="setError('')"> iPhone SE</label>
                    <label><input type="radio" name="gender" id="novigender" onchange="setError('')"> Novigender</label>
                    <label><input type="radio" name="gender" id="aporagender" onchange="setError('')"> Aporagender</label>
                    <label><input type="radio" name="gender" id="lunagender" onchange="setError('')"> Lunagender</label>
                    <label><input type="radio" name="gender" id="verangender" onchange="setError('')"> Verangender</label>
                    <label><input type="radio" name="gender" id="cadensgender" onchange="setError('')"> Cadensgender</label>
                    <label><input type="radio" name="gender" id="genderflux" onchange="setError('')"> Genderflux</label>
                    <label><input type="radio" name="gender" id="quogender" onchange="setError('')"> Quoigender</label>
                    <label><input type="radio" name="gender" id="aesthetigender" onchange="setError('')"> Aesthetigender</label>
                    <label><input type="radio" name="gender" id="spiritgender" onchange="setError('')"> Spiritgender</label>
                    <label><input type="radio" name="gender" id="vaporeon" onchange="setError('')"> Vaporeon</label>
                    <label><input type="radio" name="gender" id="egogender" onchange="setError('')"> Egogender</label>
                    <label><input type="radio" name="gender" id="technogender" onchange="setError('')"> Technogender</label>
                    <label><input type="radio" name="gender" id="aerogender" onchange="setError('')"> Aerogender</label>
                    <label><input type="radio" name="gender" id="mutogender" onchange="setError('')"> Mutogender</label>
                    <label><input type="radio" name="gender" id="enigmagender" onchange="setError('')"> Enigmagender</label>
                    <label><input type="radio" name="gender" id="synergender" onchange="setError('')"> Synergender</label>
                    <label><input type="radio" name="gender" id="cryptogender" onchange="setError('')"> Cryptogender</label>
                    <label><input type="radio" name="gender" id="abimegender" onchange="setError('')"> Abimegender</label>
                    <label><input type="radio" name="gender" id="adamasgender" onchange="setError('')"> Adamasgender</label>
                    <label><input type="radio" name="gender" id="aethergender" onchange="setError('')"> Aethergender</label>
                    <label><input type="radio" name="gender" id="agenderflux" onchange="setError('')"> Agenderflux</label>
                    <label><input type="radio" name="gender" id="alexigender" onchange="setError('')"> Alexigender</label>
                    <label><input type="radio" name="gender" id="ambigender" onchange="setError('')"> Ambigender</label>
                    <label><input type="radio" name="gender" id="ambonec" onchange="setError('')"> Ambonec</label>
                    <label><input type="radio" name="gender" id="sald" onchange="setError('')"> Sald</label>
                    <label><input type="radio" name="gender" id="anongender" onchange="setError('')"> Anongender</label>
                    <label><input type="radio" name="gender" id="anxiegender" onchange="setError('')"> Anxiegender</label>
                    <label><input type="radio" name="gender" id="astrogender" onchange="setError('')"> Astrogender</label>
                    <label><input type="radio" name="gender" id="autonogender" onchange="setError('')"> Autonogender</label>
                    <label><input type="radio" name="gender" id="spoonother" onchange="setError('')"> Spoon Other</label>
                    <label><input type="radio" name="gender" id="axigender" onchange="setError('')"> Axigender</label>
                    <label><input type="radio" name="gender" id="cavusgender" onchange="setError('')"> Cavusgender</label>
                    <label><input type="radio" name="gender" id="cendgender" onchange="setError('')"> Cendgender</label>
                    <label><input type="radio" name="gender" id="centrigender" onchange="setError('')"> Centrigender</label>
                    <label><input type="radio" name="gender" id="ceresgender" onchange="setError('')"> Ceresgender</label>
                    <label><input type="radio" name="gender" id="cisn’t" onchange="setError('')"> Cisn’t</label>
                    <label><input type="radio" name="gender" id="cloudgender" onchange="setError('')"> Cloudgender</label>
                    <label><input type="radio" name="gender" id="collgender" onchange="setError('')"> Collgender</label>
                    <label><input type="radio" name="gender" id="colorgender" onchange="setError('')"> Colorgender</label>
                    <label><input type="radio" name="gender" id="commogender" onchange="setError('')"> Commogender</label>
                    <label><input type="radio" name="gender" id="triangletoaster" onchange="setError('')"> Triangle Toaster</label>
                    <label><input type="radio" name="gender" id="cosmogender" onchange="setError('')"> Cosmogender</label>
                    <label><input type="radio" name="gender" id="cyclogender" onchange="setError('')"> Cyclogender</label>
                    <label><input type="radio" name="gender" id="deliciagender" onchange="setError('')"> Deliciagender</label>
                    <label><input type="radio" name="gender" id="duragender" onchange="setError('')"> Duragender</label>
                    <label><input type="radio" name="gender" id="electrogender" onchange="setError('')"> Electrogender</label>
                    <label><input type="radio" name="gender" id="expecgender" onchange="setError('')"> Expecgender</label>
                    <label><input type="radio" name="gender" id="faesariangender" onchange="setError('')"> Faesariangender</label>
                    <label><input type="radio" name="gender" id="fictigender" onchange="setError('')"> Fictigender</label>
                    <label><input type="radio" name="gender" id="foggender" onchange="setError('')"> Foggender</label>
                    <label><input type="radio" name="gender" id="spoon" onchange="setError('')"> Spoon</label>
                    <label><input type="radio" name="gender" id="klein" onchange="setError('')"> <img width="100" src="../Assets/klein.png"/></label>
                    <label><input type="radio" name="gender" id="fraygender" onchange="setError('')"> Fraygender</label>
                    <label><input type="radio" name="gender" id="gemelgender" onchange="setError('')"> Gemelgender</label>
                    <label><input type="radio" name="gender" id="glimragender" onchange="setError('')"> Glimragender</label>
                    <label><input type="radio" name="gender" id="helicongender" onchange="setError('')"> Helicongender</label>
                    <label><input type="radio" name="gender" id="hologender" onchange="setError('')"> Hologender</label>
                    <label><input type="radio" name="gender" id="hurricanegender" onchange="setError('')"> Hurricanegender</label>
                    <label><input type="radio" name="gender" id="implogender" onchange="setError('')"> Implogender</label>
                    <label><input type="radio" name="gender" id="inexgender" onchange="setError('')"> Inexgender</label>
                    <label><input type="radio" name="gender" id="juxera" onchange="setError('')"> Juxera</label>
                    <label><input type="radio" name="gender" id="danshimmi" onchange="setError('')"> Dan Shimmi</label>
                    <label><input type="radio" name="gender" id="spoonman" onchange="setError('')"> Spoonman</label>
                    <label><input type="radio" name="gender" id="kaleidogender" onchange="setError('')"> Kaleidogender</label>
                    <label><input type="radio" name="gender" id="kitgender" onchange="setError('')"> Kitgender</label>
                    <label><input type="radio" name="gender" id="libragender" onchange="setError('')"> Libragender</label>
                    <label><input type="radio" name="gender" id="lucidgender" onchange="setError('')"> Lucidgender</label>
                    <label><input type="radio" name="gender" id="magigender" onchange="setError('')"> Magigender</label>
                    <label><input type="radio" name="gender" id="spoonwoman" onchange="setError('')"> Spoonwoman</label>
                    <label><input type="radio" name="gender" id="melogender" onchange="setError('')"> Melogender</label>
                    <label><input type="radio" name="gender" id="mirrorgender" onchange="setError('')"> Mirrorgender</label>
                    <label><input type="radio" name="gender" id="moongender" onchange="setError('')"> Moongender</label>
                    <label><input type="radio" name="gender" id="mosaigender" onchange="setError('')"> Mosaigender</label>
                    <label><input type="radio" name="gender" id="muskgender" onchange="setError('')"> Muskgender</label>
                    <label><input type="radio" name="gender" id="mythgender" onchange="setError('')"> Mythgender</label>
                    <asp:HiddenField ID="gender" runat="server" />
                </div>
            </div>
            <div class="box">
                <label for="securityQuestion">Security Question</label>

                <input list="secqs" id="securityQuestion" name="securityQuestion" class="select" />
                <datalist id="secqs">
                    <option value="What's your favorite color?">
                    <option value="What's your grandmother's name?">
                    <option value="What is the meaning of life for you">
                </datalist>
            </div>
            <div class="box">
                <label for="securityAnswer">Answer</label>
                <input type="text" id="securityAnswer" name="securityAnswer" class="input" oninput="setError('')"/>
            </div>
            <p id="error" style="color: red;"><%:Session["error"] %></p>
            <% Session["error"] = null;%>
            <p>Already have an account? <a href="Login">Login</a></p>

            <input type="submit" id="submit" onclick="register(event)" name="submit" value="Register" />
        </div>
        </div>
    </main>
</asp:Content>
