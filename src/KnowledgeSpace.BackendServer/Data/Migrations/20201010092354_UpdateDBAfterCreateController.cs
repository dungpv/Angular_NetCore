using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeSpace.BackendServer.Data.Migrations
{
    public partial class UpdateDBAfterCreateController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NGAY_SUA",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.DropColumn(
                name: "NGAY_TAO",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.DropColumn(
                name: "THU_TU",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.RenameColumn(
                name: "TrangThai",
                table: "HocSinh",
                newName: "MaTrangThai");

            migrationBuilder.RenameColumn(
                name: "TEN",
                table: "DmLoaiKhuyetTat",
                newName: "Ten");

            migrationBuilder.RenameColumn(
                name: "MA",
                table: "DmLoaiKhuyetTat",
                newName: "Ma");

            migrationBuilder.RenameColumn(
                name: "NGUOI_TAO",
                table: "DmLoaiKhuyetTat",
                newName: "NguoiTao");

            migrationBuilder.RenameColumn(
                name: "NGUOI_SUA",
                table: "DmLoaiKhuyetTat",
                newName: "NguoiSua");

            migrationBuilder.AddColumn<int>(
                name: "ThuTu",
                table: "DmNuoc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "DmLoaiKhuyetTat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "DmLoaiKhuyetTat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ThuTu",
                table: "DmLoaiKhuyetTat",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuTu",
                table: "DmNuoc");

            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.DropColumn(
                name: "ThuTu",
                table: "DmLoaiKhuyetTat");

            migrationBuilder.RenameColumn(
                name: "MaTrangThai",
                table: "HocSinh",
                newName: "TrangThai");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "DmLoaiKhuyetTat",
                newName: "TEN");

            migrationBuilder.RenameColumn(
                name: "Ma",
                table: "DmLoaiKhuyetTat",
                newName: "MA");

            migrationBuilder.RenameColumn(
                name: "NguoiTao",
                table: "DmLoaiKhuyetTat",
                newName: "NGUOI_TAO");

            migrationBuilder.RenameColumn(
                name: "NguoiSua",
                table: "DmLoaiKhuyetTat",
                newName: "NGUOI_SUA");

            migrationBuilder.AddColumn<DateTime>(
                name: "NGAY_SUA",
                table: "DmLoaiKhuyetTat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NGAY_TAO",
                table: "DmLoaiKhuyetTat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "THU_TU",
                table: "DmLoaiKhuyetTat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
