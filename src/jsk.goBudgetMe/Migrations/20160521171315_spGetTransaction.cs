using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace jsk.goBudgetMe.Migrations
{
    public partial class spGetTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dropProc = @"
IF EXISTS(select * from sys.objects where name = 'spGetTransaction' and [type] = 'P')
	DROP PROC spGetTransaction;
";
            string spGetTransaction = @"
CREATE PROCEDURE [dbo].[spGetTransaction]
	@userId VARCHAR(40),
	@startDate datetime,
	@endDate datetime
AS

WITH UserTran AS (
	SELECT * FROM dbo.[Transaction] WHERE UserId = @userId
)
, UserTranSum AS (
	SELECT TransactionId, UniqueId, TransactionDate, TransactionDesc, Amount, Posted,
		SUM(Amount) OVER (PARTITION BY UserId ORDER BY TransactionDate) AS Balance
	FROM UserTran
)
SELECT * FROM UserTranSum WHERE TransactionDate BETWEEN @startDate AND @endDate;

RETURN 0
";
            migrationBuilder.Sql(dropProc, true);
            migrationBuilder.Sql(spGetTransaction, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
