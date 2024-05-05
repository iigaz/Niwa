using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoredFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"create or replace
function get_all_revisions(start_id uuid) returns setof ""NoteRevisions""  as $$
with recursive revisions as (
select
	*
from
	""NoteRevisions""
where
	""Id"" = start_id
union all
select
	nxt.*
from
	""NoteRevisions"" nxt
join revisions on
	revisions.""PreviousRevisionId"" = nxt.""Id""
)
select
	*
from
	revisions;
$$ language sql;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("drop function if exists get_all_revisions;");
        }
    }
}
