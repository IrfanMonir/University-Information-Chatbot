<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityChatBot._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/Ideal-Image%20Slider/normalize.css" rel="stylesheet" />
    <link href="Content/Ideal-Image%20Slider/ideal-image-slider.css" rel="stylesheet" />
    <link href="Content/Ideal-Image%20Slider/default.css" rel="stylesheet" />

    <style media="screen">
        #slider {
            max-width: 700px;
            max-height: 500px;
            margin: 13px auto;
        }
    </style>



    <div id="slider">
        <img src="images/School%201.jpg" />
        <img src="images/School%202.jpg" />
        <img src="images/School%203.jpg" />
        <img src="images/School%204.jpg" />
        <img src="images/School%205.jpg" />
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Chatbot</h2>
            <p>
               A user can query any university related question through this system. The System analyses the question and then answers to the user with the help of artificial intelligence. The system replies using an effective Graphical user interface which gives an impression as if a real person is talking to the user. The system can respond to both voice and text command
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>IIUC</h2>
            <p style="text-align">
               The International Islamic University Chittagong (IIUC) is one of the most prestigious and elite private universities in Bangladesh. Islamic University Chittagong (IUC) was founded in 1995 by Islamic University Chittagong Trust (IUCT), a non-profit organization. In 2000, IUC was upgraded to International Islamic University Chittagong (IIUC). 
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>
    
    <script src="Scripts/Ideal-Image%20Slider/ideal-image-slider.js"></script>

    <script>
        var slider = new IdealImageSlider.Slider('#slider');
        slider.start();
    </script>


</asp:Content>
