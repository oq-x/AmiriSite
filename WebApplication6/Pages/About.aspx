<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication6.About" %>

<asp:Content id="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/about.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 id="title">About AmiriGuitarSite</h1>
        <p>
            האתר AmiriGuitarSite הוא בית חם לגיטריסטים מכל הרמות! באתר תמצאו אוסף עשיר של תווים לשירים ששותפו על ידי קהילת המשתמשים, פורום פעיל שבו ניתן לדון ולשאול שאלות ולשתף חוויות בנושאים מוזיקליים מגוונים.

בין אם אתם בתחילת הדרך או נגנים מנוסים – AmiriGuitarSite נבנה עבורכם, כדי לחבר בין אוהבי גיטרה, להעשיר את הידע ולתמוך זה בזה במסע המוזיקלי.
            <br />
            את הקוד של האתר ניתן למצוא ב-<a class="link" href="https://github.com/oq-x/amirisite">GitHub</a>
        </p>
        <div class="gallery">
            <img id="gilmour" src="../Assets/GilmourPompeii.png" />
        </div>
    </main>
</asp:Content>
