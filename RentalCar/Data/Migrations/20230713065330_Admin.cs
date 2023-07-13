using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'6710341f-8a1d-48d6-b98d-d6809fc0212f', N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEEk5Hp9lNwZeGj1K1CoBQp2kkoFWMK94FvXN0qpYAv3kTCSZcx3QpHrPOtiKHrtF5g==', N'ULBWM44A3Y5CQUJWDWVAGRGQ3IAHANVD', N'a3d671c9-9568-40cc-a47b-872a469f2ffd', NULL, 0, 0, NULL, 1, 0, N'yazan', N'akel')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id='6710341f-8a1d-48d6-b98d-d6809fc0212f'");
        }
    }
}
