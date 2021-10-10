using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseRepository.Migrations
{
    public partial class SearchOrganizations_Sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure [dbo].[SP_SearchOrganization] 
                                @searchText nvarchar(100) = null
                                as
                                begin
	                                set nocount on;

	                                select * from Organizations O
	                                where O.OrganizationName like '%'+@searchText+'%'
	                                or O.[Address] like '%'+@searchText+'%'
	                                or O.Activity like '%'+@searchText+'%'
                                end";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure SP_SearchOrganization";
            migrationBuilder.Sql(procedure);
        }
    }
}
