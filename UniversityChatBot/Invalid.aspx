<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Invalid.aspx.cs" Inherits="UniversityChatBot.Invalid" %>

<asp:Content ID="Body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="background-color: White">

        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Font-Underline="True"
            ForeColor="#41BBE4" Text="Invalid Question "></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Question  :-"></asp:Label>
        &nbsp;
    <asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Answer  :-"></asp:Label>
        &nbsp;
    <asp:Label ID="Label5" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        &nbsp;
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back"
        Width="56px" />
        <br />

        <br />
        <br />

    </div>
</asp:Content>
