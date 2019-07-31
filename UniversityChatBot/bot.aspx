<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bot.aspx.cs" Inherits="UniversityChatBot.bot" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>

<%--        <script type="text/javascript">
        var jQuery_1_12_0 = $.noConflict(true);
    </script>--%>



    <%--<script src="Scripts/bootstrap.min.js"></script>--%>


    <style type="text/css">
        .style1 {
            height: 16px;
            text-align: left;
            margin-left: 40px;
        }

        .tab td {
            padding: 17px;
        }

        i.fa {
            display: inline-block;
            border-radius: 25px;
            box-shadow: 0px 0px 2px #888;
            padding: 0.5em 0.6em;
            font-size: 53px;
            color: indianred;
        }

            i.fa:hover, .active {
                color: white !important;
                background-color: indianred;
            }
    </style>

    <script type="text/javascript">

        /* $(document).ready(function () {
             $("#ctl00_ContentPlaceHolder1_Button1").click(function () {
                 var $img = $('#ctl00_ContentPlaceHolder1_ImageButton1');
                 var flag = false;
                 //                setInterval(function(){
                 //                $img.attr('src', 'images/th_Talking_Head.gif' );
                 //                }, 3000);
                 setTimeout($img.attr('src', 'images/th_Talking_Head.gif'), 3000);
             });
         }); */



        $(function () {
            try {
                var recognition = new webkitSpeechRecognition();
            } catch (e) {
                // var recognition = Object;
            }

            recognition.continuous = true;
            recognition.interimResults = true;
            recognition.lang = "en";
            /*reset();
            recognition.onend = reset(); */

            recognition.onresult = function (event) {
                var textarea = '';
                for (var i = event.resultIndex; i < event.results.length; ++i) {

                    textarea += event.results[i][0].transcript;

                }

                $("#<%=speak_text.ClientID%>").val("");
                $("#<%=speak_text.ClientID%>").val(textarea);

                setInterval(function () {

                    //alert(textarea);
                    if (textarea != "") {
                        var s = $('#<%=speak_text.ClientID%>').val();

                        $('#<%=speak_text.ClientID%>').val("");

                        window.location.href = "backend.aspx?type='" + s + "'";
                    }
                }, 4000);


            };

            /*function reset() {
                recognizing = false;
                //button.innerHTML = "Click to Speak";
            } */

            $(".text").click(function () {
                $("#edit").css("display", "initial");
                document.getElementById('<%=voice.ClientID%>').style.visibility = 'hidden';

                $("#icon2").addClass("active");
                $("#icon1").removeClass("active");
                $(".stop").css("display", "none");
                $(".start").css("display", "initial");
                recognition.stop();
                $("#<%=speak_text.ClientID%>").val("");
                $("#<%=Label4.ClientID%>").text("");
                $("#<%=HyperLink2.ClientID%>").text("");
                document.getElementById('<%=Button3.ClientID%>').style.visibility = 'hidden';
                return false;
            });
            $(".start").click(function () {
                $("#edit").css("display", "none");
                $(".stop").css("display", "initial");
                $(".start").css("display", "none");
                document.getElementById('<%=voice.ClientID%>').style.visibility = 'visible';

            $("#icon11").addClass("active");
            $("#icon2").removeClass("active");
            recognition.start();
            // recognizing = true;
            $("#<%=speak_text.ClientID%>").val("");
            $("#<%=Label4.ClientID%>").text("");
            $("#<%=HyperLink2.ClientID%>").text("");
            document.getElementById('<%=Button3.ClientID%>').style.visibility = 'hidden';
            return false;
        });
            $(".stop").click(function () {
                $(".stop").css("display", "none");
                $(".start").css("display", "initial");
                $("#icon1").addClass("active");
                $("#<%=speak_text.ClientID%>").val("");
            document.getElementById('<%=voice.ClientID%>').style.visibility = 'hidden';

            recognition.stop();
            $("#<%=speak_text.ClientID%>").val("");
            $("#<%=Label4.ClientID%>").text("");
            $("#<%=HyperLink2.ClientID%>").text("");
            document.getElementById('<%=Button3.ClientID%>').style.visibility = 'hidden';
            return false;
        });

            var action = $("#<%=Label5.ClientID %>").text();
            if (action != "") {
                recognition.start();
                $(".stop").css("display", "initial");
                $(".start").css("display", "none");
                return false;
            }
            else {

            }

        });

        /* function PageLoad()
         {
            // $("#voice").css("display", "initial");
             //alert("hello");
             // recognition.start();
            // $('#start').trigger('click');
            
         } */
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:ImageButton ID="ImageButton3" runat="server" Height="46px"
            ImageUrl="~/images/button.png" PostBackUrl="~/Login1.aspx" Width="199px"
            OnClick="ImageButton3_Click" Visible="False" /> --%>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    <div class="row intabular" style="border-radius: 0px; margin-right: 0px; margin-left: 0px; padding-top: 50px; padding-bottom: 50px;">
        <div class="col-md-6" style="text-align: right;">
            <a id="startRecognition" title="Start Recording" class="start" runat="server"><i class="fa fa-microphone" id="icon1"></i></a>
            <a id="A1" title="Stop Recording" class="stop" runat="server" style="display: none;"><i class="fa fa-stop" id="icon11"></i></a>
        </div>
        <div class="col-md-6" style="text-align: left;">
            <a id="LinkButton1" title="Text" class="text" runat="server"><i class="fa fa-edit" id="icon2"></i></a>
        </div>
    </div>

    <div id="edit" style="display: none;">
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:Table ID="Table1" runat="server" CssClass="intabular tab" Width="100%" Style="border-radius: 0px;">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" HorizontalAlign="Right" Width="50%">
                            <asp:Label ID="Label1" runat="server" Text="Chat Here"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell runat="server" HorizontalAlign="Left" Width="50%">
                            <asp:TextBox ID="txtuser" runat="server" OnTextChanged="txtuser_TextChanged" Width="348px"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" ColumnSpan="2" HorizontalAlign="Center">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Talk"
                                Width="79px" />&nbsp;&nbsp;
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"
                                Text="Invalid Ans" Width="100px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" HorizontalAlign="Right" Width="50%">
                            <asp:Label ID="Label2" runat="server" Text="Answer  :-   "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell runat="server" HorizontalAlign="Left" Width="50%">
                            <asp:Label ID="lblbot" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" ColumnSpan="2" HorizontalAlign="Center">
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                ForeColor="Blue" Visible="False">Link</asp:HyperLink>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:Panel ID="voice" runat="server">
        <asp:Table ID="Table2" runat="server" Width="100%" Style="border-radius: 0px; margin-right:20%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ColumnSpan="2" HorizontalAlign="Center">
                    <asp:TextBox ID="speak_text" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right" Width="50%">
                    <asp:Label ID="Label3" runat="server" Text="Answer  :-   " style="color: white"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label4" runat="server" style="color: white"></asp:Label>
                </asp:TableCell>

            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right" Width="50%">
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="True"
                        ForeColor="Blue" Visible="False">Link</asp:HyperLink>
                    <br />
                </asp:TableCell>

                <asp:TableCell runat="server" HorizontalAlign="Left" Width="70%">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"
                        Text="Invalid Ans" Width="100px" Visible="False" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <%-- <div id="" runat="server" oncientclick="voice()">
        
        </div> 
    --%>
    <%--
              
               <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
               <table width="100%" class="intabular">
                <tr>
                    <td id="myimage">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                        
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td bgcolor="White">
                        
                    </td>
                </tr>
                <tr>
                    <td id="myimage" class="style1" colspan="3" valign="top" align="center">
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
                        &nbsp;&nbsp;
           
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>    

                <tr>
                    <td id="myimage" align="center" class="style1" colspan="3" valign="top">&nbsp;</td>
                </tr>
            </table>
              
              <div style="background-color: #FFFFFF; color: #000000">
                <br />
               
               
                <br />
                
                <br />
                <br />
               </ContentTemplate>
    </asp:UpdatePanel> --%>

    <%--</form>--%>
</asp:Content>
