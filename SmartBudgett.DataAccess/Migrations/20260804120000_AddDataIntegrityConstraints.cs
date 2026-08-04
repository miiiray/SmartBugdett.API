using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartBudgett.DataAccess.Context;

#nullable disable

namespace SmartBudgett.DataAccess.Migrations
{
    [DbContext(typeof(SmartBudgetContext))]
    [Migration("20260804120000_AddDataIntegrityConstraints")]
    public partial class AddDataIntegrityConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE [Users]
                SET [Email] = LOWER(LTRIM(RTRIM([Email])));

                IF EXISTS (
                    SELECT [Email]
                    FROM [Users]
                    GROUP BY [Email]
                    HAVING COUNT(*) > 1)
                BEGIN
                    THROW 50001, 'Migration durduruldu: yinelenen kullanici e-postalari var.', 1;
                END;

                IF EXISTS (
                    SELECT 1 FROM [Users]
                    WHERE LEN([FirstName]) > 50
                       OR LEN([LastName]) > 50
                       OR LEN([Email]) > 100
                       OR LEN([Password]) > 100)
                BEGIN
                    THROW 50002, 'Migration durduruldu: kullanici alanlarindan biri izin verilen uzunlugu asiyor.', 1;
                END;

                IF EXISTS (SELECT 1 FROM [Categories] WHERE LEN([Name]) > 100)
                BEGIN
                    THROW 50003, 'Migration durduruldu: 100 karakterden uzun kategori adi var.', 1;
                END;

                IF EXISTS (SELECT 1 FROM [Expenses] WHERE LEN([Description]) > 500)
                   OR EXISTS (SELECT 1 FROM [Incomes] WHERE LEN([Description]) > 500)
                BEGIN
                    THROW 50004, 'Migration durduruldu: 500 karakterden uzun aciklama var.', 1;
                END;

                IF EXISTS (
                    SELECT 1
                    FROM [Categories] AS [c]
                    LEFT JOIN [Users] AS [u] ON [u].[Id] = [c].[UserId]
                    WHERE [u].[Id] IS NULL)
                BEGIN
                    THROW 50005, 'Migration durduruldu: kullanicisi bulunmayan kategori var.', 1;
                END;

                IF EXISTS (
                    SELECT 1
                    FROM [Expenses] AS [e]
                    LEFT JOIN [Users] AS [u] ON [u].[Id] = [e].[UserId]
                    LEFT JOIN [Categories] AS [c] ON [c].[Id] = [e].[CategoryId]
                    WHERE [u].[Id] IS NULL
                       OR [c].[Id] IS NULL
                       OR [c].[UserId] <> [e].[UserId])
                BEGIN
                    THROW 50006, 'Migration durduruldu: gecersiz kullanici veya kategoriye bagli gider var.', 1;
                END;

                IF EXISTS (
                    SELECT 1
                    FROM [Incomes] AS [i]
                    LEFT JOIN [Users] AS [u] ON [u].[Id] = [i].[UserId]
                    LEFT JOIN [Categories] AS [c] ON [c].[Id] = [i].[CategoryId]
                    WHERE [u].[Id] IS NULL
                       OR [c].[Id] IS NULL
                       OR [c].[UserId] <> [i].[UserId])
                BEGIN
                    THROW 50007, 'Migration durduruldu: gecersiz kullanici veya kategoriye bagli gelir var.', 1;
                END;
                """);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Incomes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId_ExpenseDate",
                table: "Expenses",
                columns: new[] { "UserId", "ExpenseDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_CategoryId",
                table: "Incomes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId_IncomeDate",
                table: "Incomes",
                columns: new[] { "UserId", "IncomeDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Categories_CategoryId",
                table: "Incomes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Categories_CategoryId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes");

            migrationBuilder.DropIndex(name: "IX_Users_Email", table: "Users");
            migrationBuilder.DropIndex(name: "IX_Categories_UserId", table: "Categories");
            migrationBuilder.DropIndex(name: "IX_Expenses_CategoryId", table: "Expenses");
            migrationBuilder.DropIndex(name: "IX_Expenses_UserId_ExpenseDate", table: "Expenses");
            migrationBuilder.DropIndex(name: "IX_Incomes_CategoryId", table: "Incomes");
            migrationBuilder.DropIndex(name: "IX_Incomes_UserId_IncomeDate", table: "Incomes");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
