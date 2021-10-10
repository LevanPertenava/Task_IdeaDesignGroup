using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseRepository.Migrations
{
    public partial class SelectPersonsInOrganization_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string proc = @"create proc [dbo].[SelectPersonsInOrganization_SP]
                            @OrganizationId uniqueidentifier
                            as
                            begin
	                            set nocount on;

	                            if not exists(select * from Organizations where Id = @OrganizationId and IsActive = 1)
	                            begin
		                            raiserror('Record not found', 16, 1);
		                            return 1;
	                            end

	                            select distinct P.*
	                            from Organizations O 
	                            join PersonOrganizations PO on PO.OrganizationId = @OrganizationId
	                            join Persons P on PO.PersonId = P.Id
                            end";
            migrationBuilder.Sql(proc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string proc = @"drop proc [dbo].[SelectPersonsInOrganization_SP]";

            migrationBuilder.Sql(proc);
        }
    }
}
