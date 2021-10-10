using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseRepository.Migrations
{
    public partial class UnAssignCategoryToPerson_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string proc = @"create procedure UnAssignCategoryToPerson_SP
						@personId uniqueidentifier,
						@organizationId uniqueidentifier
						as
						begin
						set nocount on;
							  if not exists(select * from Organizations where Id = @OrganizationId and IsActive = 1)
							  begin
								   raiserror('Organization not found', 16, 1);
								   return 1;
							  end

							  if not exists(select * from Persons where Id = @personId and IsActive = 1)
							  begin
								   raiserror('Person not found', 16, 1);
								   return 1;
							  end

							  delete from PersonOrganizations
							  where PersonId = @personId and OrganizationId = @organizationId
						end";
			migrationBuilder.Sql(proc);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string proc = @"drop procedure UnAssignCategoryToPerson_SP";
			migrationBuilder.Sql(proc);
		}
    }
}
