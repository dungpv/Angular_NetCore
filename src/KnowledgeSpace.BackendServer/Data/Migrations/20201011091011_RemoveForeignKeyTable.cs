using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeSpace.BackendServer.Data.Migrations
{
    public partial class RemoveForeignKeyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DmHuyen_DmTinh_DmTinhMa",
                table: "DmHuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_DmHuyen_NamHoc_NamHocMa",
                table: "DmHuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_DmKhoi_DmCapHoc_DmCapHocMa",
                table: "DmKhoi");

            migrationBuilder.DropForeignKey(
                name: "FK_DmMonHoc_DmCapHoc_DmCapHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_DmMonHoc_NamHoc_NamHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_DmXa_DmHuyen_DmHuyenId",
                table: "DmXa");

            migrationBuilder.DropForeignKey(
                name: "FK_DmXa_DmTinh_DmTinhMa",
                table: "DmXa");

            migrationBuilder.DropForeignKey(
                name: "FK_HocSinh_PhongGD_PhongGDId",
                table: "HocSinh");

            migrationBuilder.DropForeignKey(
                name: "FK_HocSinh_SoGD_SoGDMa",
                table: "HocSinh");

            migrationBuilder.DropForeignKey(
                name: "FK_HocSinh_Truong_TruongId",
                table: "HocSinh");

            migrationBuilder.DropForeignKey(
                name: "FK_Lop_PhongGD_PhongGDId",
                table: "Lop");

            migrationBuilder.DropForeignKey(
                name: "FK_Lop_SoGD_SoGDMa",
                table: "Lop");

            migrationBuilder.DropForeignKey(
                name: "FK_Lop_Truong_TruongId",
                table: "Lop");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanSu_PhongGD_PhongGDId",
                table: "NhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanSu_SoGD_SoGDMa",
                table: "NhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanSu_Truong_TruongId",
                table: "NhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_PhongGD_SoGD_SoGDMa",
                table: "PhongGD");

            migrationBuilder.DropForeignKey(
                name: "FK_Truong_PhongGD_PhongGDId",
                table: "Truong");

            migrationBuilder.DropForeignKey(
                name: "FK_Truong_SoGD_SoGDMa",
                table: "Truong");

            migrationBuilder.DropIndex(
                name: "IX_Truong_PhongGDId",
                table: "Truong");

            migrationBuilder.DropIndex(
                name: "IX_Truong_SoGDMa",
                table: "Truong");

            migrationBuilder.DropIndex(
                name: "IX_PhongGD_SoGDMa",
                table: "PhongGD");

            migrationBuilder.DropIndex(
                name: "IX_NhanSu_PhongGDId",
                table: "NhanSu");

            migrationBuilder.DropIndex(
                name: "IX_NhanSu_SoGDMa",
                table: "NhanSu");

            migrationBuilder.DropIndex(
                name: "IX_NhanSu_TruongId",
                table: "NhanSu");

            migrationBuilder.DropIndex(
                name: "IX_Lop_PhongGDId",
                table: "Lop");

            migrationBuilder.DropIndex(
                name: "IX_Lop_SoGDMa",
                table: "Lop");

            migrationBuilder.DropIndex(
                name: "IX_Lop_TruongId",
                table: "Lop");

            migrationBuilder.DropIndex(
                name: "IX_HocSinh_PhongGDId",
                table: "HocSinh");

            migrationBuilder.DropIndex(
                name: "IX_HocSinh_SoGDMa",
                table: "HocSinh");

            migrationBuilder.DropIndex(
                name: "IX_HocSinh_TruongId",
                table: "HocSinh");

            migrationBuilder.DropIndex(
                name: "IX_DmXa_DmHuyenId",
                table: "DmXa");

            migrationBuilder.DropIndex(
                name: "IX_DmXa_DmTinhMa",
                table: "DmXa");

            migrationBuilder.DropIndex(
                name: "IX_DmMonHoc_DmCapHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropIndex(
                name: "IX_DmMonHoc_NamHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropIndex(
                name: "IX_DmKhoi_DmCapHocMa",
                table: "DmKhoi");

            migrationBuilder.DropIndex(
                name: "IX_DmHuyen_DmTinhMa",
                table: "DmHuyen");

            migrationBuilder.DropIndex(
                name: "IX_DmHuyen_NamHocMa",
                table: "DmHuyen");

            migrationBuilder.DropColumn(
                name: "PhongGDId",
                table: "Truong");

            migrationBuilder.DropColumn(
                name: "SoGDMa",
                table: "Truong");

            migrationBuilder.DropColumn(
                name: "SoGDMa",
                table: "PhongGD");

            migrationBuilder.DropColumn(
                name: "PhongGDId",
                table: "NhanSu");

            migrationBuilder.DropColumn(
                name: "SoGDMa",
                table: "NhanSu");

            migrationBuilder.DropColumn(
                name: "TruongId",
                table: "NhanSu");

            migrationBuilder.DropColumn(
                name: "PhongGDId",
                table: "Lop");

            migrationBuilder.DropColumn(
                name: "SoGDMa",
                table: "Lop");

            migrationBuilder.DropColumn(
                name: "TruongId",
                table: "Lop");

            migrationBuilder.DropColumn(
                name: "PhongGDId",
                table: "HocSinh");

            migrationBuilder.DropColumn(
                name: "SoGDMa",
                table: "HocSinh");

            migrationBuilder.DropColumn(
                name: "TruongId",
                table: "HocSinh");

            migrationBuilder.DropColumn(
                name: "DmHuyenId",
                table: "DmXa");

            migrationBuilder.DropColumn(
                name: "DmTinhMa",
                table: "DmXa");

            migrationBuilder.DropColumn(
                name: "DmCapHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropColumn(
                name: "NamHocMa",
                table: "DmMonHoc");

            migrationBuilder.DropColumn(
                name: "DmCapHocMa",
                table: "DmKhoi");

            migrationBuilder.DropColumn(
                name: "DmTinhMa",
                table: "DmHuyen");

            migrationBuilder.DropColumn(
                name: "NamHocMa",
                table: "DmHuyen");

            migrationBuilder.AddColumn<int>(
                name: "MaNamHoc",
                table: "DmVung",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNamHoc",
                table: "DmVung");

            migrationBuilder.AddColumn<decimal>(
                name: "PhongGDId",
                table: "Truong",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGDMa",
                table: "Truong",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGDMa",
                table: "PhongGD",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PhongGDId",
                table: "NhanSu",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGDMa",
                table: "NhanSu",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TruongId",
                table: "NhanSu",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PhongGDId",
                table: "Lop",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGDMa",
                table: "Lop",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TruongId",
                table: "Lop",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PhongGDId",
                table: "HocSinh",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGDMa",
                table: "HocSinh",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TruongId",
                table: "HocSinh",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DmHuyenId",
                table: "DmXa",
                type: "numeric(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DmTinhMa",
                table: "DmXa",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DmCapHocMa",
                table: "DmMonHoc",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NamHocMa",
                table: "DmMonHoc",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DmCapHocMa",
                table: "DmKhoi",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DmTinhMa",
                table: "DmHuyen",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NamHocMa",
                table: "DmHuyen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Truong_PhongGDId",
                table: "Truong",
                column: "PhongGDId");

            migrationBuilder.CreateIndex(
                name: "IX_Truong_SoGDMa",
                table: "Truong",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_PhongGD_SoGDMa",
                table: "PhongGD",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSu_PhongGDId",
                table: "NhanSu",
                column: "PhongGDId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSu_SoGDMa",
                table: "NhanSu",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSu_TruongId",
                table: "NhanSu",
                column: "TruongId");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_PhongGDId",
                table: "Lop",
                column: "PhongGDId");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_SoGDMa",
                table: "Lop",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_Lop_TruongId",
                table: "Lop",
                column: "TruongId");

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_PhongGDId",
                table: "HocSinh",
                column: "PhongGDId");

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_SoGDMa",
                table: "HocSinh",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_TruongId",
                table: "HocSinh",
                column: "TruongId");

            migrationBuilder.CreateIndex(
                name: "IX_DmXa_DmHuyenId",
                table: "DmXa",
                column: "DmHuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_DmXa_DmTinhMa",
                table: "DmXa",
                column: "DmTinhMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmMonHoc_DmCapHocMa",
                table: "DmMonHoc",
                column: "DmCapHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmMonHoc_NamHocMa",
                table: "DmMonHoc",
                column: "NamHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmKhoi_DmCapHocMa",
                table: "DmKhoi",
                column: "DmCapHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmHuyen_DmTinhMa",
                table: "DmHuyen",
                column: "DmTinhMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmHuyen_NamHocMa",
                table: "DmHuyen",
                column: "NamHocMa");

            migrationBuilder.AddForeignKey(
                name: "FK_DmHuyen_DmTinh_DmTinhMa",
                table: "DmHuyen",
                column: "DmTinhMa",
                principalTable: "DmTinh",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmHuyen_NamHoc_NamHocMa",
                table: "DmHuyen",
                column: "NamHocMa",
                principalTable: "NamHoc",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmKhoi_DmCapHoc_DmCapHocMa",
                table: "DmKhoi",
                column: "DmCapHocMa",
                principalTable: "DmCapHoc",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmMonHoc_DmCapHoc_DmCapHocMa",
                table: "DmMonHoc",
                column: "DmCapHocMa",
                principalTable: "DmCapHoc",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmMonHoc_NamHoc_NamHocMa",
                table: "DmMonHoc",
                column: "NamHocMa",
                principalTable: "NamHoc",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmXa_DmHuyen_DmHuyenId",
                table: "DmXa",
                column: "DmHuyenId",
                principalTable: "DmHuyen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DmXa_DmTinh_DmTinhMa",
                table: "DmXa",
                column: "DmTinhMa",
                principalTable: "DmTinh",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HocSinh_PhongGD_PhongGDId",
                table: "HocSinh",
                column: "PhongGDId",
                principalTable: "PhongGD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HocSinh_SoGD_SoGDMa",
                table: "HocSinh",
                column: "SoGDMa",
                principalTable: "SoGD",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HocSinh_Truong_TruongId",
                table: "HocSinh",
                column: "TruongId",
                principalTable: "Truong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_PhongGD_PhongGDId",
                table: "Lop",
                column: "PhongGDId",
                principalTable: "PhongGD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_SoGD_SoGDMa",
                table: "Lop",
                column: "SoGDMa",
                principalTable: "SoGD",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_Truong_TruongId",
                table: "Lop",
                column: "TruongId",
                principalTable: "Truong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanSu_PhongGD_PhongGDId",
                table: "NhanSu",
                column: "PhongGDId",
                principalTable: "PhongGD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanSu_SoGD_SoGDMa",
                table: "NhanSu",
                column: "SoGDMa",
                principalTable: "SoGD",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanSu_Truong_TruongId",
                table: "NhanSu",
                column: "TruongId",
                principalTable: "Truong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhongGD_SoGD_SoGDMa",
                table: "PhongGD",
                column: "SoGDMa",
                principalTable: "SoGD",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Truong_PhongGD_PhongGDId",
                table: "Truong",
                column: "PhongGDId",
                principalTable: "PhongGD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Truong_SoGD_SoGDMa",
                table: "Truong",
                column: "SoGDMa",
                principalTable: "SoGD",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
