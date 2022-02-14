<%@ Page Title="" Language="C#" MasterPageFile="~/PSIsystem.Master" AutoEventWireup="true" CodeBehind="PurchasePage.aspx.cs" Inherits="PSIsystem.PurchasePage" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .jumbotron {
            background-color: snow !important;
            height:500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1>進貨單管理</h1>
        
        <asp:Button ID="Print" runat="server" Text="Print" OnClick="Print_Click"/>
        <table class="table">
            <thead>
                <tr>
                    <th>單據編號</th>
                    <th>貨物種類</th>
                    <th>進貨數量</th>
                    <th>預計進貨時間</th>
                    <th>進貨金額</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="OrderRepeater" runat="server" >
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ItemCheckBox" runat="server" Text='<%# Eval("ID") %>' OnCheckedChanged="ItemCheckBox_CheckedChanged" AutoPostBack="true"/></td>
                            <td><%#Eval("ProductType") %></td>
                            <td><%#Eval("Count") %></td>
                            <td><%#Eval("Date") %></td>
                            <td><%#Eval("Total") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div>
        <asp:Button ID="Delete" runat="server" Text="刪除" OnClick="Delete_Click" OnClientClick="return confirm('確定要刪除嗎？');"/>
        <asp:Button ID="Add" runat="server" Text="新增" OnClick="Add_Click"/>

        <asp:PlaceHolder ID="PagePlaceHolder" runat="server">
            <asp:Repeater runat="server" ID="repPaging">
                <ItemTemplate>
                    <a href="<%# Eval("Link") %>" title="<%# Eval("Title") %>"><%# Eval("Name") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </asp:PlaceHolder>
    </div>
</asp:Content>
