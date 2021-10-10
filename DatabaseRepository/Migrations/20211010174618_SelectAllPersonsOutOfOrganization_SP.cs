using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseRepository.Migrations
{
    public partial class SelectAllPersonsOutOfOrganization_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure SelectAllPersonsOutOfOrganization_SP
                                @organizationId uniqueidentifier
                                as
                                begin
                                set nocount on;
	                                select * from Persons

	                                except

	                                (select distinct P.* from Persons P 
	                                left join PersonOrganizations PO on PO.PersonId = P.Id
	                                where  PO.OrganizationId = @organizationId)
                                end";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure SelectAllPersonsOutOfOrganization_SP";
            migrationBuilder.Sql(procedure);
        }
    }
}
