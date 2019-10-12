/*
 Copyright 2017 Amazon.com

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/


PRINT('Installing AWS sampledb in database dms_sample...')
PRINT(CONCAT('Starting: ',CURRENT_TIMESTAMP));
GO

:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\create_dms_sample.sql
go

print('Enabling Sql Authentication...')
:r c:\windows\temp\controltower-cloudendure-simulated-environment\sqlserverdb\enable_sql_authentication.sql
go

print('Creating the dms_user login and user...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\user\create_dms_user.sql

use dms_sample;
SET NOCOUNT ON;
SET QUOTED_IDENTIFIER ON;

print('Installing mlb data...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\mlb_data.tab
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\mlb_data.sql
print('installing name data...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\name_data.tab
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\name_data.sql
print('Installing nfl data...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\nfl_data.tab
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\nfl_data.sql
print('Installing nfl stadium data...')
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\nfl_stadium_data.tab
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\nfl_stadium_data.sql
go
print('Creating objects and base data...')
print('Creating seat_type table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\seat_type.tab
print('Creating sport_type table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sport_type.tab
print('Creating sport_league table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sport_league.tab
print('Creating sport location table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sport_location.tab
print('Creating sport_division table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sport_division.tab
print('Creating sport_team table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sport_team.tab
print('Creating seat table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\seat.tab
print('Creating player table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\player.tab
print('Creating person table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\person.tab
print('Creating sporting_event table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sporting_event.tab
print('Loading baseball teams...')
go
exec loadMLBTeams
go
print('Setting mlb team home field...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\set_mlb_team_home_field.sql
print('Loading NFL teams...')
go
exec loadNFLTeams
go
print('Setting nfl team home field...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\set_nfl_team_home_field.sql
print('Creating stadium seats...')
go
exec generateSeats
go
print('Loading baseball players...')
go
exec loadMLBPlayers
print('Loading NFL players...')
go
exec loadNFLPlayers
print('Loading sporting events...')
print('Generating baseball season...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\generate_mlb_season.sql
print('Generating NFL season...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\generate_nfl_season.sql
print('Creating ticket table...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sporting_event_ticket.tab
print('Creating table ticket_purchase_hist...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\ticket_purchase_hist.tab
print('installing procedure generate_tickets...')
go

print('Loading utility objects...')
print('Creating view getNewId')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\getNewId.vw
print('Loading function rand_int...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\rand_int.sql

print('Loading ticket management procedures...')
go
print('Creating proceduer sellTickets...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sellTickets.sql
print('Creating procedure transferTicket...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\transferTicket.sql
print('Creating procedure generateTicketActivity...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\generateTicketActivity.sql
print('Creating procedure generateTransferActivity...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\generateTransferActivity.sql


print('Load procedure to generate tickets...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\generate_tickets.sql

print('Creating views...')
print('creating view sporting_event_info...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\sporting_event_info.vw
print('Creating view ticket_info...')
go
:r c:\windows\temp\aws-database-migration-samples\sqlserver\sampledb\v1\schema\ticket_info.vw

PRINT(CONCAT('Complete: ',CURRENT_TIMESTAMP));
GO
Print('.....   Done   .....')
GO






