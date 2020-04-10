using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Auth;
using TodoApi.DTO;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ReservationsDbContext _context;

        public AdminController(ReservationsDbContext context)
        {
            _context = context;
          
        }

        /// <summary>
        /// Nuke and restore all DB. Pass "drop" in command to confirm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Policy = "OnlySuperAdmin")]
        //  [Authorize(Policy = "OnlySiteAdmin")]

       
        public async Task<ActionResult<int>> NukeDb(string command)
        {
            if (command != "drop") return BadRequest("You don't want to nuke db, don't you?");
            
           // _context.Database.EnsureDeleted();

           /*var sql = @"DECLARE @tableName VARCHAR(200)  
SET @tableName=''  
WHILE EXISTS  
 (  
 --Find all child tables AND those which have no relations  
             SELECT T.table_name FROM INFORMATION_SCHEMA.TABLES T  
             LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC  
             ON T.table_name=TC.table_name  
             WHERE (TC.constraint_Type ='Foreign Key'or TC.constraint_Type IS NULL) AND  
             T.table_name NOT IN ('dtproperties','sysconstraints','syssegments')AND  
             Table_type='BASE TABLE' AND T.table_name > @TableName  
 )  
 BEGIN  
             SELECT @tableName=min(T.table_name) FROM INFORMATION_SCHEMA.TABLES T  
             LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC  
             ON T.table_name=TC.table_name  
             WHERE (TC.constraint_Type ='Foreign Key'or TC.constraint_Type IS NULL) AND  
             T.table_name NOT IN ('dtproperties','sysconstraints','syssegments') AND  
             Table_type='BASE TABLE' AND T.table_name > @TableName  
             --Truncate the table  
             EXEC('DELETE FROM '+@tablename)  
     PRINT 'DELETE FROM '+@tablename  
 END

SET @TableName=''  
WHILE EXISTS  
(  
            --Find all Parent tables  
            SELECT T.table_name FROM INFORMATION_SCHEMA.TABLES T  
            LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC  
            ON T.table_name=TC.table_name  
            WHERE TC.constraint_Type ='Primary Key'AND T.table_name <>'dtproperties' AND  
            Table_type='BASE TABLE' AND T.table_name > @TableName  
)  
BEGIN  
            SELECT @tableName=min(T.table_name) FROM INFORMATION_SCHEMA.TABLES T  
            LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON T.table_name=TC.table_name  
            WHERE TC.constraint_Type ='Primary Key' AND T.table_name <>'dtproperties' AND  
            Table_type='BASE TABLE' AND T.table_name > @TableName  
  
            --DELETE the table  
            EXEC('DELETE FROM '+ @tableName)  
            PRINT 'DELETE FROM '+ @tableName  
            --Reset identity column  
            If EXISTS  
            (  
                        SELECT * FROM information_schema.columns  
                        WHERE COLUMNPROPERTY(OBJECT_ID(QUOTENAME(table_schema)+'.'+  
                        QUOTENAME(@tableName)), column_name,'IsIdentity')=1  
            )  
            BEGIN  
                        DBCC CHECKIDENT (@tableName, RESEED, 1)  
                        PRINT @tableName  
            END  
END  
";*/
           /*using (var trans = _context.Database.BeginTransaction())
           {*/
            //   _context.Users.RemoveRange(_context.Users);
               _context.Fields.RemoveRange(_context.Fields);
               _context.Sites.RemoveRange(_context.Sites);
               _context.Floor.RemoveRange(_context.Floor);
               _context.Building.RemoveRange(_context.Building);
               _context.Image.RemoveRange(_context.Image);
               _context.Locations.RemoveRange(_context.Locations);
               _context.Regions.RemoveRange(_context.Regions);
               _context.Rooms.RemoveRange(_context.Rooms);
            _context.ReservationModels.RemoveRange(_context.ReservationModels);
            _context.SaveChanges();


                   /*var res = _context.Database.ExecuteSqlRaw(sql);*/

                // _context.Database.CommitTransaction();
             //   trans.Commit();


            /*}*/
          // _context.Database.Migrate();

            return Ok();
        }
    }
}