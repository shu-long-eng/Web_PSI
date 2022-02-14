<%@ Page Title="" Language="C#" MasterPageFile="~/PSIsystem.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="PSIsystem.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .jumbotron {
            background-color: snow !important;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1>商品列表</h1>
    </div>
    <div id="context">
        <table class="table">
            <thead>
                <tr>
                    <td>商品編號</td>
                    <td>分類</td>
                    <td>商品名稱</td>
                    <td>單價</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Product" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ID") %></td>
                            <td><%# Eval("ProductType") %></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Price") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
