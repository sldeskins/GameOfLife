<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameUI.aspx.cs" Inherits="WebApplication1.GameUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Game of Life</h1>

    <form id="formSetGameGrid" runat="server">
        <fieldset title="Game Controls">
            <div>
                <div>
                    <asp:Label ID="LabelRows" runat="server" Text="# of rows:"></asp:Label>
                    <asp:TextBox ID="TextBoxRows" runat="server" OnTextChanged="UpdateGameGridRowsColumns">20</asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="LabelColumns" runat="server" Text="# of columns"></asp:Label>
                    <asp:TextBox ID="TextBoxCoulumns" runat="server" OnTextChanged="UpdateGameGridRowsColumns">30</asp:TextBox>
                </div>
            </div>
            <asp:Button ID="ButtonMakeGameGrid" runat="server" OnClick="ResetLayoutGameGrid" Text="Make Game Grid" />
        </fieldset>
        <fieldset>
            <span>
                <asp:Button ID="ButtonGetNextGeneration" runat="server" Text="Get Next Generation" OnClick="GetNextGeneration" />
<%--                <asp:Button ID="ButtonStart" runat="server" Text="Start" />
                <asp:Button ID="ButtonPause" runat="server" Text="Pause" />
                <asp:Button ID="ButtonStop" runat="server" Text="Stop" />--%>
            </span>
        </fieldset>
        <div id="gameMessageArea">
        </div>

        <asp:Panel ID="GridAreaPanel" runat="server">
        </asp:Panel>
    </form>
</body>
</html>
