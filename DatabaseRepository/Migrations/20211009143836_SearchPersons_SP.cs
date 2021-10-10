using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseRepository.Migrations
{
    public partial class SearchPersons_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure SP_SearchPerson 
                                @searchText nvarchar(100) = null
                                as
                                begin
	                                set nocount on;

	                                select * from Persons P
	                                where P.FirstName like '%'+@searchText+'%'
	                                or P.LastName like '%'+@searchText+'%'
	                                or p.PersonalNumber like '%'+@searchText+'%'
                                end";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure SP_SearchPerson";
            migrationBuilder.Sql(procedure);
        }
    }
}
