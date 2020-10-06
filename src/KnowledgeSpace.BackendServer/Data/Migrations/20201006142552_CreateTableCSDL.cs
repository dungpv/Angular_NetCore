using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeSpace.BackendServer.Data.Migrations
{
    public partial class CreateTableCSDL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DmCapDonVi",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmCapDonVi", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmCapHoc",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmCapHoc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmCumThiDua",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmCumThiDua", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmDanToc",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenGoiKhac = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmDanToc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiCanBo",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNhomCanBo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGdtx = table.Column<int>(nullable: false),
                    IsPhong = table.Column<int>(nullable: false),
                    IsSo = table.Column<int>(nullable: false),
                    IsBo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiCanBo", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiHinh",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGdtx = table.Column<int>(nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiHinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiKhuyetTat",
                columns: table => new
                {
                    MA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TEN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    THU_TU = table.Column<int>(nullable: false),
                    NGUOI_TAO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NGAY_TAO = table.Column<DateTime>(nullable: false),
                    NGUOI_SUA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NGAY_SUA = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiKhuyetTat", x => x.MA);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiTruong",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGdtx = table.Column<int>(nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiTruong", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmLyDoNghiViec",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLyDoNghiViec", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmLyDoThoiHoc",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLyDoThoiHoc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmMonDayGV",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MaMonHoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGdtx = table.Column<int>(nullable: false),
                    IsPhong = table.Column<int>(nullable: false),
                    IsSo = table.Column<int>(nullable: false),
                    IsBo = table.Column<int>(nullable: false),
                    ThuTuMN = table.Column<int>(nullable: false),
                    ThuTuC1 = table.Column<int>(nullable: false),
                    ThuTuC2 = table.Column<int>(nullable: false),
                    ThuTuC3 = table.Column<int>(nullable: false),
                    ThuTuGDTX = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmMonDayGV", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmNgach",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNgach = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    LoaiNgach = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGDTX = table.Column<int>(nullable: false),
                    IsPhong = table.Column<int>(nullable: false),
                    IsSo = table.Column<int>(nullable: false),
                    IsBo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNgach", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmNhomCanBo",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMN = table.Column<int>(nullable: false),
                    IsC1 = table.Column<int>(nullable: false),
                    IsC2 = table.Column<int>(nullable: false),
                    IsC3 = table.Column<int>(nullable: false),
                    IsGDTX = table.Column<int>(nullable: false),
                    IsPhong = table.Column<int>(nullable: false),
                    IsSo = table.Column<int>(nullable: false),
                    IsBo = table.Column<int>(nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNhomCanBo", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmNhomCapHoc",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DSCap = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNhomCapHoc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmNhomTuoiMN",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaNhomTre = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNhomTuoiMN", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmNuoc",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNuoc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmSoBuoiHocTrenTuan",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmSoBuoiHocTrenTuan", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmTinh",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    Cap = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTinh", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DMTonGiao",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMTonGiao", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmTrangThaiCanBo",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrangThaiCanBo", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmTrangThaiHocSinh",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaChuyenDenChuyenDi = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrangThaiHocSinh", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmTrinhDoChuyenMonNghiepVu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    MaChuanMN = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaChuanC1 = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaChuanC2 = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaChuanC3 = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaChuanGDTX = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaNamHoc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmTrinhDoChuyenMonNghiepVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmVung",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmVung", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "NamHoc",
                columns: table => new
                {
                    Ma = table.Column<int>(maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamHoc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "SoGD",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    MaCumThiDua = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoGD", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DmKhoi",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaCapHoc = table.Column<string>(nullable: true),
                    DmCapHocMa = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    MaLoaiLop = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmKhoi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DmKhoi_DmCapHoc_DmCapHocMa",
                        column: x => x.DmCapHocMa,
                        principalTable: "DmCapHoc",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DmHuyen",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false),
                    NamHocMa = table.Column<int>(nullable: true),
                    MaTinh = table.Column<string>(nullable: true),
                    DmTinhMa = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    Cap = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmHuyen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DmHuyen_DmTinh_DmTinhMa",
                        column: x => x.DmTinhMa,
                        principalTable: "DmTinh",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DmHuyen_NamHoc_NamHocMa",
                        column: x => x.NamHocMa,
                        principalTable: "NamHoc",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DmMonHoc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DmCapHocMa = table.Column<string>(nullable: true),
                    MaNamHoc = table.Column<int>(nullable: false),
                    NamHocMa = table.Column<int>(nullable: true),
                    KieuMonHoc = table.Column<int>(nullable: false),
                    IsMonTC = table.Column<int>(nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    MaMon = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmMonHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DmMonHoc_DmCapHoc_DmCapHocMa",
                        column: x => x.DmCapHocMa,
                        principalTable: "DmCapHoc",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DmMonHoc_NamHoc_NamHocMa",
                        column: x => x.NamHocMa,
                        principalTable: "NamHoc",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhongGD",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaSoGD = table.Column<string>(nullable: false),
                    SoGDMa = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaTinh = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IdHuyen = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    MaHuyen = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ThuTu = table.Column<int>(nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongGD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhongGD_SoGD_SoGDMa",
                        column: x => x.SoGDMa,
                        principalTable: "SoGD",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DmXa",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaTinh = table.Column<string>(nullable: true),
                    DmTinhMa = table.Column<string>(nullable: true),
                    IdHuyen = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    DmHuyenId = table.Column<decimal>(nullable: true),
                    MaHuyen = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThuTu = table.Column<int>(nullable: false),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    Cap = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmXa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DmXa_DmHuyen_DmHuyenId",
                        column: x => x.DmHuyenId,
                        principalTable: "DmHuyen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DmXa_DmTinh_DmTinhMa",
                        column: x => x.DmTinhMa,
                        principalTable: "DmTinh",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Truong",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaSoGD = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SoGDMa = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPhongGD = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    PhongGDId = table.Column<decimal>(nullable: true),
                    MaPhongGD = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaTinh = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IdHuyen = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    MaHuyen = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaLoaiHinh = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaLoaiTruong = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MaNhomCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DSCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ThuTu = table.Column<int>(nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    MaVung = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IsCapMN = table.Column<int>(nullable: true),
                    IsCapTH = table.Column<int>(nullable: true),
                    IsCapTHCS = table.Column<int>(nullable: true),
                    IsCapTHPT = table.Column<int>(nullable: true),
                    IsCapGDTX = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Truong_PhongGD_PhongGDId",
                        column: x => x.PhongGDId,
                        principalTable: "PhongGD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Truong_SoGD_SoGDMa",
                        column: x => x.SoGDMa,
                        principalTable: "SoGD",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HocSinh",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaSoGD = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SoGDMa = table.Column<string>(nullable: true),
                    IdPhongGD = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    PhongGDId = table.Column<decimal>(nullable: true),
                    MaPhongGD = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IdTruong = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    TruongId = table.Column<decimal>(nullable: true),
                    MaTruong = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaGioiTinh = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "Date", nullable: false),
                    MaDanToc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaLyDoThoiHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaQuocTich = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaTonGiao = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaSoBuoiHocTrenTuan = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ThuTu = table.Column<int>(nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocSinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocSinh_PhongGD_PhongGDId",
                        column: x => x.PhongGDId,
                        principalTable: "PhongGD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HocSinh_SoGD_SoGDMa",
                        column: x => x.SoGDMa,
                        principalTable: "SoGD",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HocSinh_Truong_TruongId",
                        column: x => x.TruongId,
                        principalTable: "Truong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaSoGD = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SoGDMa = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPhongGD = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    PhongGDId = table.Column<decimal>(nullable: true),
                    MaPhongGD = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IdTruong = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    TruongId = table.Column<decimal>(nullable: true),
                    MaTruong = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaKhoi = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MaNhomTuoiMN = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaSoBuoiHocTrenTuan = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IsLopGhep = table.Column<int>(nullable: true),
                    TrangThai = table.Column<int>(nullable: false),
                    ThuTu = table.Column<int>(nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lop_PhongGD_PhongGDId",
                        column: x => x.PhongGDId,
                        principalTable: "PhongGD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lop_SoGD_SoGDMa",
                        column: x => x.SoGDMa,
                        principalTable: "SoGD",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lop_Truong_TruongId",
                        column: x => x.TruongId,
                        principalTable: "Truong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanSu",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNamHoc = table.Column<int>(nullable: false),
                    MaSoGD = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SoGDMa = table.Column<string>(nullable: true),
                    IdPhongGD = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    PhongGDId = table.Column<decimal>(nullable: true),
                    MaPhongGD = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IdTruong = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    TruongId = table.Column<decimal>(nullable: true),
                    MaTruong = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaCapHoc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaGioiTinh = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "Date", nullable: false),
                    MaDanToc = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaTrangThai = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaQuocTich = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaTonGiao = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaNhomCanBo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaLoaiCanBo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaNgach = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    MaTrinhDoChuyenMon = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ThuTu = table.Column<int>(nullable: true),
                    NguoiTao = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NgaySua = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSu_PhongGD_PhongGDId",
                        column: x => x.PhongGDId,
                        principalTable: "PhongGD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NhanSu_SoGD_SoGDMa",
                        column: x => x.SoGDMa,
                        principalTable: "SoGD",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NhanSu_Truong_TruongId",
                        column: x => x.TruongId,
                        principalTable: "Truong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DmHuyen_DmTinhMa",
                table: "DmHuyen",
                column: "DmTinhMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmHuyen_NamHocMa",
                table: "DmHuyen",
                column: "NamHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmKhoi_DmCapHocMa",
                table: "DmKhoi",
                column: "DmCapHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmMonHoc_DmCapHocMa",
                table: "DmMonHoc",
                column: "DmCapHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmMonHoc_NamHocMa",
                table: "DmMonHoc",
                column: "NamHocMa");

            migrationBuilder.CreateIndex(
                name: "IX_DmXa_DmHuyenId",
                table: "DmXa",
                column: "DmHuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_DmXa_DmTinhMa",
                table: "DmXa",
                column: "DmTinhMa");

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
                name: "IX_PhongGD_SoGDMa",
                table: "PhongGD",
                column: "SoGDMa");

            migrationBuilder.CreateIndex(
                name: "IX_Truong_PhongGDId",
                table: "Truong",
                column: "PhongGDId");

            migrationBuilder.CreateIndex(
                name: "IX_Truong_SoGDMa",
                table: "Truong",
                column: "SoGDMa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmCapDonVi");

            migrationBuilder.DropTable(
                name: "DmCumThiDua");

            migrationBuilder.DropTable(
                name: "DmDanToc");

            migrationBuilder.DropTable(
                name: "DmKhoi");

            migrationBuilder.DropTable(
                name: "DmLoaiCanBo");

            migrationBuilder.DropTable(
                name: "DmLoaiHinh");

            migrationBuilder.DropTable(
                name: "DmLoaiKhuyetTat");

            migrationBuilder.DropTable(
                name: "DmLoaiTruong");

            migrationBuilder.DropTable(
                name: "DmLyDoNghiViec");

            migrationBuilder.DropTable(
                name: "DmLyDoThoiHoc");

            migrationBuilder.DropTable(
                name: "DmMonDayGV");

            migrationBuilder.DropTable(
                name: "DmMonHoc");

            migrationBuilder.DropTable(
                name: "DmNgach");

            migrationBuilder.DropTable(
                name: "DmNhomCanBo");

            migrationBuilder.DropTable(
                name: "DmNhomCapHoc");

            migrationBuilder.DropTable(
                name: "DmNhomTuoiMN");

            migrationBuilder.DropTable(
                name: "DmNuoc");

            migrationBuilder.DropTable(
                name: "DmSoBuoiHocTrenTuan");

            migrationBuilder.DropTable(
                name: "DMTonGiao");

            migrationBuilder.DropTable(
                name: "DmTrangThaiCanBo");

            migrationBuilder.DropTable(
                name: "DmTrangThaiHocSinh");

            migrationBuilder.DropTable(
                name: "DmTrinhDoChuyenMonNghiepVu");

            migrationBuilder.DropTable(
                name: "DmVung");

            migrationBuilder.DropTable(
                name: "DmXa");

            migrationBuilder.DropTable(
                name: "HocSinh");

            migrationBuilder.DropTable(
                name: "Lop");

            migrationBuilder.DropTable(
                name: "NhanSu");

            migrationBuilder.DropTable(
                name: "DmCapHoc");

            migrationBuilder.DropTable(
                name: "DmHuyen");

            migrationBuilder.DropTable(
                name: "Truong");

            migrationBuilder.DropTable(
                name: "DmTinh");

            migrationBuilder.DropTable(
                name: "NamHoc");

            migrationBuilder.DropTable(
                name: "PhongGD");

            migrationBuilder.DropTable(
                name: "SoGD");
        }
    }
}
